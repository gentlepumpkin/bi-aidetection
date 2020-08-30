using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json; //deserialize DeepquestAI response
//for image cutting
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.MetaData.Profiles.Exif;
//for telegram
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace AITool
{

    public partial class Shell:Form
    {

        //Use AppSettings.Settings.originalsetting from now on...
        //public string input_path = AppSettings.Settings.input_path; //image input path
        //public static string deepstack_url = AppSettings.Settings.deepstack_url; //deepstack url
        //public static bool log_everything = AppSettings.Settings.log_everything; //save every action sent to Log() into the log file?
        //public static bool send_errors = AppSettings.Settings.send_errors; //send error messages to Telegram?
        //public static string telegram_chatid = AppSettings.Settings.telegram_chatid; //telegram chat id
        //public static string[] telegram_chatids = telegram_chatid.Replace(" ", "").Split(','); //for multiple Telegram chats that receive alert images
        //public static string telegram_token = AppSettings.Settings.telegram_token; //telegram bot token

        ////deepstack, logging and blueiris settings added by Vorlon
        //public static string deepstack_adminkey = AppSettings.Settings.deepstack_adminkey;
        //public static string deepstack_apikey = AppSettings.Settings.deepstack_apikey;
        //public static string deepstack_mode = AppSettings.Settings.deepstack_mode;
        //public static string deepstack_port = AppSettings.Settings.deepstack_port;
        //public static string deepstack_installfolder = AppSettings.Settings.deepstack_installfolder;
        //public static bool deepstack_detectionapienabled = AppSettings.Settings.deepstack_detectionapienabled;
        //public static bool deepstack_faceapienabled = AppSettings.Settings.deepstack_faceapienabled;
        //public static bool deepstack_sceneapienabled = AppSettings.Settings.deepstack_sceneapienabled;
        //public static bool deepstack_autostart = AppSettings.Settings.deepstack_autostart;
        //public static IProgress<string> ProgressLogger = null;
        public static DeepStack DeepStackServerControl = null;
        public static RichTextBoxEx RTFLogger = null;
        public static LogFileWriter LogWriter = null;
        public static LogFileWriter HistoryWriter = null;
        public static BlueIris BlueIrisInfo = null;

        //keep track of timing
        //moving average will be faster for long running process with 1000's of samples
        public static MovingCalcs tcalc = new MovingCalcs(250);
        public static MovingCalcs dcalc = new MovingCalcs(250);
        public static MovingCalcs fcalc = new MovingCalcs(250);
        public static MovingCalcs qcalc = new MovingCalcs(250);
        public static MovingCalcs qsizecalc = new MovingCalcs(250);

        public int errors = 0; //error counter

        private ConcurrentQueue<ClsImageQueueItem> ImageProcessQueue = new ConcurrentQueue<ClsImageQueueItem>();

        //this is not thread safe, and I believe events run in diff threads - this is why we are still getting access deined errors for jpgs:
        //  See https://stackoverflow.com/questions/1764809/filesystemwatcher-changed-event-is-raised-twice
        //public bool detection_running = false; //is detection running right now or not

        //Instantiate a Singleton of the Semaphore with a value of 1. This means that only 1 thread can be granted access at a time.
        public static SemaphoreSlim semaphore_detection_running = new SemaphoreSlim(1, 1);
        //thread safe dictionary to prevent more than one file being processed at one time
        public static ConcurrentDictionary<string, string> detection_dictionary = new ConcurrentDictionary<string, string>();
        //public int file_access_delay = 50; //delay before accessing new file in ms - increased to 50, 10 was still giving frequent access denied errors -Vorlon
        //public int retry_delay = 10; //delay for first file acess retry - will increase on each retry
        //List<Camera> CameraList = new List<Camera>(); //list containing all cameras

        static HttpClient client = new HttpClient();

        //FileSystemWatcher watcher = new FileSystemWatcher(); //fswatcher checking the input folder for new images
        //handle multiple folders
        Dictionary<string, MyFileSystemWatcher> watchers = new Dictionary<string, MyFileSystemWatcher>();



        public Shell()
        {
            InitializeComponent();

            //---------------------------------------------------------------------------------------------------------
            // Section added by Vorlon
            //---------------------------------------------------------------------------------------------------------

            //this is to log messages from other classes to the RTF in Shell form, and to log file...
            Global.progress = new Progress<string>(EventMessage);

            //Initialize the rich text log window writer.   You can use any 'color' name in your log text
            //for example {red}Error!{white}.  Note if you use $ for the string, you have use two brackets like this: {{red}}
            RTFLogger = new RichTextBoxEx(RTF_Log, true);
            //initialize the log and history file writers - log entries will be queued for fast file logging performance AND if the file
            //is locked for any reason, it will wait in the queue until it can be written
            //The logwriter will also rotate out log files (each day, rename as log_date.txt) and delete files older than 60 days
            LogWriter = new LogFileWriter(AppSettings.Settings.LogFileName);
            HistoryWriter = new LogFileWriter(AppSettings.Settings.HistoryFileName);

            //if log file does not exist, create it - this used to be in LOG function but doesnt need to be checked everytime log written to
            if (!System.IO.File.Exists(AppSettings.Settings.LogFileName))
            {
                //the logwriter auto creates the file if needed
                LogWriter.WriteToLog("Log format: [dd.MM.yyyy, HH:mm:ss]: Log text.", true);

            }

            //load settings
            AppSettings.Load();

            LogWriter.MaxLogFileAgeDays = AppSettings.Settings.MaxLogFileAgeDays;
            HistoryWriter.MaxLogFileAgeDays = AppSettings.Settings.MaxLogFileAgeDays;

            string AssemVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Log("");
            Log("");
            lbl_version.Text = $"Version {AssemVer} built on {Global.RetrieveLinkerTimestamp()}";
            Log($"Starting {Application.ProductName} {lbl_version.Text}");
            if (AppSettings.AlreadyRunning)
            {
                Log("*** Another instance is already running *** ");
                Log(" --- Files will not be monitored from within this session ");
                Log(" --- Log tab will not display output from service instance. You will need to directly open log file for that ");
                Log(" --- Changes made here to settings will require that you stop/start the service ");
                Log(" --- You must close/reopen app to see NEW history items/detections");
            }
            if (Global.IsAdministrator())
            {
                Log("*** Running as administrator ***");
            }
            else
            {
                Log("Not running as administrator.");
            }
            //initialize blueiris info class to get camera names, clip paths, etc
            BlueIrisInfo = new BlueIris();
            if (BlueIrisInfo.IsValid)
            {
                Log($"BlueIris path is '{BlueIrisInfo.AppPath}', with {BlueIrisInfo.Cameras.Count()} cameras and {BlueIrisInfo.ClipPaths.Count()} clip folder paths configured.");
            }
            else
            {
                Log($"BlueIris not detected.");
            }
            //---------------------------------------------------------------------------------------------------------

            this.Resize += new System.EventHandler(this.Form1_Resize); //resize event to enable 'minimize to tray'

            //if camera settings folder does not exist, create it
            if (!Directory.Exists("./cameras/"))
            {
                //create folder
                DirectoryInfo di = Directory.CreateDirectory("./cameras");
                Log("./cameras/" + " dir created.");
            }

            //---------------------------------------------------------------------------
            //CAMERAS TAB

            //left list column setup
            list2.Columns.Add("Camera");

            //set left list column width segmentation (because of some bug -4 is necessary to achieve the correct width)
            list2.Columns[0].Width = list2.Width - 4;
            list2.FullRowSelect = true; //make all columns clickable

            LoadCameras(); //load camera list

            this.Opacity = 0;
            this.Show();

            //---------------------------------------------------------------------------
            //HISTORY TAB

            //left list column setup
            list1.Columns.Add("Name");
            list1.Columns.Add("Date and Time");
            list1.Columns.Add("Camera");
            list1.Columns.Add("Detections");
            list1.Columns.Add("Positions");
            list1.Columns.Add("✓");

            //set left list column width segmentation
            list1.Columns[0].Width = list1.Width * 0 / 100; //filename
            list1.Columns[1].Width = list1.Width * 47 / 100; //date
            list1.Columns[2].Width = list1.Width * 43 / 100; //cam name
            list1.Columns[3].Width = list1.Width * 0 / 100; //obj and confidences
            list1.Columns[4].Width = list1.Width * 0 / 100; // object positions of all detected objects separated by ";"
            list1.Columns[5].Width = list1.Width * 10 / 100; //checkmark if something relevant was detected or not
            list1.FullRowSelect = true; //make all columns clickable

            //check if history.csv exists, if not then create it
            if (!System.IO.File.Exists(AppSettings.Settings.HistoryFileName))
            {
                Log("ATTENTION: Creating database cameras/history.csv .");
                try
                {
                    HistoryWriter.WriteToLog("filename|date and time|camera|detections|positions of detections|success", true);

                    //using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "cameras/history.csv"))
                    //{
                    //    sw.WriteLine("filename|date and time|camera|detections|positions of detections|success");
                    //}
                }
                catch
                {
                    lbl_errors.Text = "Can't create cameras/history.csv database!";
                }

            }


            //this method is slow if the database is large, so it's usually only called on startup. During runtime, DeleteListImage() is used to remove obsolete images from the history list
            CleanCSVList();

            //load entries from history.csv into history ListView
            //LoadFromCSV(); not neccessary because below, comboBox_filter_camera.SelectedIndex will call LoadFromCSV()

            splitContainer1.Panel2Collapsed = true; //collapse filter panel under left list
            comboBox_filter_camera.Items.Add("All Cameras"); //add "all cameras" entry in filter dropdown combobox
            comboBox_filter_camera.SelectedIndex = comboBox_filter_camera.FindStringExact("All Cameras"); //select all cameras entry



            //---------------------------------------------------------------------------
            //SETTINGS TAB

            //fill settings tab with stored settings 

            cmbInput.Text = AppSettings.Settings.input_path;
            cb_inputpathsubfolders.Checked = AppSettings.Settings.input_path_includesubfolders;
            cmbInput.Items.Clear();
            foreach (string pth in BlueIrisInfo.ClipPaths)
            {
                cmbInput.Items.Add(pth);

            }

            tbDeepstackUrl.Text = AppSettings.Settings.deepstack_url;
            tb_telegram_chatid.Text = String.Join(",", AppSettings.Settings.telegram_chatids);
            tb_telegram_token.Text = AppSettings.Settings.telegram_token;
            cb_log.Checked = AppSettings.Settings.log_everything;
            cb_send_errors.Checked = AppSettings.Settings.send_errors;
            cbStartWithWindows.Checked = AppSettings.Settings.startwithwindows;

            //---------------------------------------------------------------------------
            //STATS TAB
            comboBox1.Items.Add("All Cameras"); //add all cameras stats entry
            comboBox1.SelectedIndex = comboBox1.FindStringExact("All Cameras"); //select all cameras entry


            //---------------------------------------------------------------------------
            //Deepstack server TAB


            //initialize the deepstack class - it collects info from running deepstack processes, detects install location, and
            //allows for stopping and starting of its service
            DeepStackServerControl = new DeepStack(AppSettings.Settings.deepstack_adminkey, AppSettings.Settings.deepstack_apikey, AppSettings.Settings.deepstack_mode, AppSettings.Settings.deepstack_sceneapienabled, AppSettings.Settings.deepstack_faceapienabled, AppSettings.Settings.deepstack_detectionapienabled, AppSettings.Settings.deepstack_port);


            if (DeepStackServerControl.NeedsSaving)
            {
                //this may happen if the already running instance has a different port, etc, so we update the config
                SaveDeepStackTab();
            }
            LoadDeepStackTab(true);

            //set httpclient timeout:
            client.Timeout = TimeSpan.FromMinutes(2);

            UpdateWatchers();

            //Start the thread that watches for the file queue
            Task.Run(ImageQueueLoop);


            this.Opacity = 1;

            Log("APP START complete.");
        }

        public class MyFileSystemWatcher
        {
            public string Name = "";
            public string Path = "";
            public bool IncludeSubdirectories = false;
            public FileSystemWatcher watcher = null;
            public MyFileSystemWatcher(string Name, string Path, FileSystemWatcher Watcher, bool IncludeSubFolders)
            {
                this.Name = Name;
                this.Path = Path;
                this.watcher = Watcher;
                this.IncludeSubdirectories = IncludeSubFolders;
            }
        }


        public void UpdateWatchers()
        {

            try
            {

                if (AppSettings.AlreadyRunning)
                {
                    Log("*** Another instance is already running, skip watching for changed files ***");
                    return;
                }
                //first add all the names and paths to check...
                List<string> names = new List<string>();
                Dictionary<string, string> paths = new Dictionary<string, string>();

                string pths = AppSettings.Settings.input_path.Trim().TrimEnd(@"\".ToCharArray());
                names.Add($"INPUT_PATH|{pths}|{AppSettings.Settings.input_path_includesubfolders}");
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    if (cam.enabled)
                    {
                        pths = cam.input_path.Trim().TrimEnd(@"\".ToCharArray());
                        names.Add($"{cam.name}|{pths}|{cam.input_path_includesubfolders}");
                    }
                }


                //check each one to see if needs to be added
                foreach (string item in names)
                {
                    List<string> splt = Global.Split(item, "|", false);
                    string name = splt[0];
                    string path = splt[1];
                    bool include = Convert.ToBoolean(splt[2]);
                    if (!paths.ContainsKey(path.ToLower()))
                    {
                        paths.Add(path.ToLower(), path);

                        if (!watchers.ContainsKey(name.ToLower()))
                        {
                            //this will return null if the path is invalid...
                            FileSystemWatcher curwatch = MyWatcherFatory(path, include);
                            MyFileSystemWatcher mywtc = new MyFileSystemWatcher(name, path, curwatch, include);
                            //add even if null to keep track of things
                            watchers.Add(name.ToLower(), mywtc);
                        }
                        else
                        {
                            //update path if needed, even to empty
                            watchers[name.ToLower()].Path = path;
                            if (watchers[name.ToLower()].watcher == null)
                            {
                                //could be null if path is bad
                                watchers[name.ToLower()].watcher = MyWatcherFatory(path, include);
                            }
                        }
                    }
                    else
                    {
                        Log($"Skipping duplicate path for '{name}': {path}");
                    }


                }

                //check to see if any need disabling - a camera was deleted
                foreach (MyFileSystemWatcher watcher1 in watchers.Values)
                {
                    bool fnd = false;
                    foreach (string item in names)
                    {
                        List<string> splt = Global.Split(item);
                        string name = splt[0];
                        if (name.ToLower() == watcher1.Name.ToLower())
                        {
                            fnd = true;
                            break;
                        }
                    }
                    if (!fnd)
                    {
                        watcher1.Path = "";
                    }

                }


                //enable or disable watchers
                int enabledcnt = 0;
                int disabledcnt = 0;

                Dictionary<string, string> dupes = new Dictionary<string, string>();

                foreach (MyFileSystemWatcher watcher in watchers.Values)
                {
                    if (watcher.watcher != null)
                    {
                        if (!String.IsNullOrWhiteSpace(watcher.Path))
                        {
                            if (!dupes.ContainsKey(watcher.Path.ToLower()))
                            {
                                if (watcher.Path != watcher.watcher.Path)
                                {
                                    watcher.watcher.Path = watcher.Path;
                                    Log($"Watcher '{watcher.Name}' changed from '{watcher.watcher.Path}' to '{watcher.Path}'.");
                                }

                                if (watcher.IncludeSubdirectories != watcher.watcher.IncludeSubdirectories)
                                {
                                    watcher.watcher.IncludeSubdirectories = watcher.IncludeSubdirectories;
                                    Log($"Watcher '{watcher.Name}' IncludeSubdirectories changed from '{watcher.watcher.IncludeSubdirectories}' to '{watcher.IncludeSubdirectories}'.");
                                }

                                if (watcher.watcher.EnableRaisingEvents != true)
                                {
                                    enabledcnt++;
                                    watcher.watcher.EnableRaisingEvents = true;
                                    dupes.Add(watcher.Path.ToLower(), watcher.Path);
                                    Log($"Watcher '{watcher.Name}' is now watching '{watcher.Path}'");
                                }

                            }
                            else
                            {
                                Log($"Watcher '{watcher.Name}' has a duplicate path, skipping '{watcher.Path}'");
                            }
                        }
                        else
                        {
                            //make sure it is disabled
                            disabledcnt++;

                            watcher.watcher.EnableRaisingEvents = false;
                            watcher.watcher.Dispose();
                            watcher.watcher = null;
                            Log($"Watcher '{watcher.Name}' has an empty path, just disabled.");
                        }

                    }
                    else if (!string.IsNullOrEmpty(watcher.Path))
                    {
                        Log($"Error: Watcher '{watcher.Name}' disabled. INVALID PATH='{watcher.Path}'");
                    }
                    else
                    {
                        Log($"Watcher '{watcher.Name}' already disabled.");
                    }


                }

                if (watchers.Count() == 0)
                {
                    Log("No FileSystemWatcher input folders defined yet.");
                }
                else
                {
                    if (enabledcnt == 0)
                    {
                        Log("No NEW FileSystemWatcher input folders found.");
                    }
                    else
                    {
                        Log($"Enabled {enabledcnt} FileSystemWatchers.");
                    }
                }

            }
            catch (Exception ex)
            {

                Log($"Error: {Global.ExMsg(ex)}");
            }

        }

        public FileSystemWatcher MyWatcherFatory(string path, bool IncludeSubdirectories = false, string filter = "*.jpg")
        {
            FileSystemWatcher watcher = null;

            try
            {
                // Be aware: https://stackoverflow.com/questions/1764809/filesystemwatcher-changed-event-is-raised-twice

                if (!String.IsNullOrWhiteSpace(path) && Directory.Exists(path))
                {
                    watcher = new FileSystemWatcher(path);
                    watcher.Path = path;
                    watcher.Filter = filter;
                    watcher.IncludeSubdirectories = IncludeSubdirectories;

                    //The 'default' is the bitwise OR combination of LastWrite, FileName, and DirectoryName'
                    watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

                    //fswatcher events
                    watcher.Created += new FileSystemEventHandler(OnCreatedAsync);
                    watcher.Renamed += new RenamedEventHandler(OnRenamed);
                    watcher.Deleted += new FileSystemEventHandler(OnDeleted);
                    watcher.Error += new ErrorEventHandler(OnError);

                }
            }
            catch (Exception ex)
            {
                Log($"Error: {Global.ExMsg(ex)}");
            }

            return watcher;
        }

        void EventMessage(string message)
        {
            //output messages from the deepstack, blueiris, etc class to the text log window and log file
            Log(message, "");
        }
        //----------------------------------------------------------------------------------------------------------
        //CORE
        //----------------------------------------------------------------------------------------------------------


        //analyze image with AI
        public async Task<bool> DetectObjects(ClsImageQueueItem CurImg, string DeepStackURL)
        {


            bool ret = false;

            //Only set error when there IS an error...
            string error = ""; //if code fails at some point, the last text of the error string will be posted in the log

            string filename = Path.GetFileName(CurImg.image_path);


            Stopwatch sw = Stopwatch.StartNew();
            Stopwatch swposttime = Stopwatch.StartNew();

            Uri url = new Uri(DeepStackURL);
            String CurSrv = url.Host + ":" + url.Port;

            Camera cam = Global.GetCamera(CurImg.image_path);
            cam.last_image_file = CurImg.image_path;

            // check if camera is still in the first half of the cooldown. If yes, don't analyze to minimize cpu load.
            //only analyze if 50% of the cameras cooldown time since last detection has passed
            double mins = (DateTime.Now - cam.last_trigger_time).TotalMinutes;
            double halfcool = cam.cooldown_time / 2;
            if (mins >= halfcool)
            {
                try
                {
                    Log($"{CurSrv} - Starting analysis of {CurImg.image_path}...");

                    using (FileStream image_data = System.IO.File.OpenRead(CurImg.image_path))
                    {
                        Log($"{CurSrv} - (1/6) Uploading image to DeepQuestAI Server at {DeepStackURL}");

                        //error = $"Can't reach DeepQuestAI Server at {fullDeepstackUrl}.";

                        var request = new MultipartFormDataContent();
                        request.Add(new StreamContent(image_data), "image", Path.GetFileName(CurImg.image_path));

                        swposttime = Stopwatch.StartNew();

                        using (HttpResponseMessage output = await client.PostAsync(url, request))
                        {
                            swposttime.Stop();

                            if (output.IsSuccessStatusCode)
                            {
                                string jsonString = await output.Content.ReadAsStringAsync();

                                string cleanjsonString = Global.CleanString(jsonString);

                                if (jsonString != null && !string.IsNullOrWhiteSpace(jsonString))
                                {
                                    Log($"{CurSrv} - (2/6) Posted in {{yellow}}{swposttime.ElapsedMilliseconds}ms{{white}}, Received a {jsonString.Length} byte response.");
                                    Log($"{CurSrv} - (3/6) Processing results...");

                                    Response response = null;
                                    try
                                    {
                                        //This can throw an exception
                                        response = JsonConvert.DeserializeObject<Response>(jsonString);
                                    }
                                    catch (Exception ex)
                                    {
                                        error = $"{CurSrv} - ERROR: Deserialization of 'Response' from DeepStack failed: {Global.ExMsg(ex)}, JSON: '{cleanjsonString}'";
                                        Log(error);
                                    }

                                    if (response != null)
                                    {

                                        //error = $"Failure in DeepStack processing the image.";

                                        //print every detected object with the according confidence-level
                                        string outputtext = $"{CurSrv} -    Detected objects:";

                                        if (response.predictions != null)
                                        {
                                            DeepStackServerControl.IsActivated = true;
                                            foreach (Object user in response.predictions)
                                            {
                                                if (user != null && !string.IsNullOrEmpty(user.label))
                                                {
                                                    DeepStackServerControl.VisionDetectionRunning = true;
                                                    outputtext += $"{user.label.ToString()} ({Math.Round((user.confidence * 100), 2).ToString() }%), ";
                                                }
                                                else
                                                {
                                                    outputtext += "(Error: null prediction? DeepStack may not be started with correct switches.), ";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            outputtext = $"{CurSrv} - (Error: No predictions?  JSON: '{cleanjsonString}')";
                                        }

                                        Log(outputtext);

                                        if (response.success == true)
                                        {

                                            //if camera is enabled
                                            if (cam.enabled == true)
                                            {

                                                //if something was detected
                                                if (response.predictions.Length > 0)
                                                {
                                                    List<string> objects = new List<string>(); //list that will be filled with all objects that were detected and are triggering_objects for the camera
                                                    List<float> objects_confidence = new List<float>(); //list containing ai confidence value of object at same position in List objects
                                                    List<string> objects_position = new List<string>(); //list containing object positions (xmin, ymin, xmax, ymax)

                                                    List<string> irrelevant_objects = new List<string>(); //list that will be filled with all irrelevant objects
                                                    List<float> irrelevant_objects_confidence = new List<float>(); //list containing ai confidence value of irrelevant object at same position in List objects
                                                    List<string> irrelevant_objects_position = new List<string>(); //list containing irrelevant object positions (xmin, ymin, xmax, ymax)


                                                    int masked_counter = 0; //this value is incremented if an object is in a masked area
                                                    int threshold_counter = 0; // this value is incremented if an object does not satisfy the confidence limit requirements
                                                    int irrelevant_counter = 0; // this value is incremented if an irrelevant (but not masked or out of range) object is detected

                                                    Log($"{CurSrv} - (4/6) Checking if detected object is relevant and within confidence limits:");
                                                    //add all triggering_objects of the specific camera into a list and the correlating confidence levels into a second list
                                                    foreach (Object user in response.predictions)
                                                    {
                                                        // just extra log lines - Log($"   {user.label.ToString()} ({Math.Round((user.confidence * 100), 2).ToString() }%):");

                                                        using (var img = new Bitmap(CurImg.image_path))
                                                        {
                                                            bool irrelevant_object = false;

                                                            //if object detected is one of the objects that is relevant
                                                            if (cam.triggering_objects_as_string.Contains(user.label))
                                                            {
                                                                // -> OBJECT IS RELEVANT

                                                                //if confidence limits are satisfied
                                                                if (user.confidence * 100 >= cam.threshold_lower && user.confidence * 100 <= cam.threshold_upper)
                                                                {
                                                                    // -> OBJECT IS WITHIN CONFIDENCE LIMITS

                                                                    ObjectPosition currentObject = new ObjectPosition(user.x_min, user.y_min, user.x_max, user.y_max, user.label,
                                                                                                                      img.Width, img.Height, cam);

                                                                    bool maskExists = false;
                                                                    if (cam.maskManager.masking_enabled)
                                                                    {
                                                                        //creates history and masked lists for objects returned
                                                                        maskExists = cam.maskManager.CreateDynamicMask(currentObject);
                                                                    }

                                                                    //only if the object is outside of the masked area
                                                                    if (Outsidemask(cam.name, user.x_min, user.x_max, user.y_min, user.y_max, img.Width, img.Height)
                                                                        && !maskExists)
                                                                    {
                                                                        // -> OBJECT IS OUTSIDE OF MASKED AREAS

                                                                        objects.Add(user.label);
                                                                        objects_confidence.Add(user.confidence);
                                                                        string position = $"{user.x_min},{user.y_min},{user.x_max},{user.y_max}";
                                                                        objects_position.Add(position);
                                                                        Log($"{CurSrv} -    {{orange}}{ user.label.ToString()} ({ Math.Round((user.confidence * 100), 2).ToString() }%) confirmed.");
                                                                    }
                                                                    else //if the object is in a masked area
                                                                    {
                                                                        masked_counter++;
                                                                        irrelevant_object = true;
                                                                    }
                                                                }
                                                                else //if confidence limits are not satisfied
                                                                {
                                                                    threshold_counter++;
                                                                    irrelevant_object = true;
                                                                }
                                                            }
                                                            else //if object is not relevant
                                                            {
                                                                irrelevant_counter++;
                                                                irrelevant_object = true;
                                                            }

                                                            if (irrelevant_object == true)
                                                            {
                                                                irrelevant_objects.Add(user.label);
                                                                irrelevant_objects_confidence.Add(user.confidence);
                                                                string position = $"{user.x_min},{user.y_min},{user.x_max},{user.y_max}";
                                                                irrelevant_objects_position.Add(position);
                                                                Log($"{CurSrv} -    { user.label.ToString()} ({ Math.Round((user.confidence * 100), 2).ToString() }%) is irrelevant.");
                                                            }
                                                        }

                                                    }  //end loop over current object list

                                                    if (cam.maskManager.masking_enabled)
                                                    {
                                                        //scan over all masked objects and decrement counter if not flagged as visible.
                                                        cam.maskManager.CleanUpExpiredMasks(cam.name);

                                                        //remove objects from history if they have not been detected in the history_save_mins and hit counter < history_threshold_count
                                                        cam.maskManager.CleanUpExpiredHistory(cam.name);

                                                        //log summary information for all masked objects
                                                        Log($"{CurSrv} - ### Masked objects summary for camera " + cam.name + " ###");
                                                        foreach (ObjectPosition maskedObject in cam.maskManager.masked_positions)
                                                        {
                                                            Log($"{CurSrv} - \t" + maskedObject.ToString());
                                                        }
                                                    }

                                                    //if one or more objects were detected, that are 1. relevant, 2. within confidence limits and 3. outside of masked areas
                                                    if (objects.Count() > 0)
                                                    {
                                                        //store these last detections for the specific camera
                                                        cam.last_detections = objects;
                                                        cam.last_confidences = objects_confidence;
                                                        cam.last_positions = objects_position;


                                                        //create summary string for this detection
                                                        StringBuilder detectionsTextSb = new StringBuilder();
                                                        for (int i = 0; i < objects.Count(); i++)
                                                        {
                                                            detectionsTextSb.Append(String.Format("{0} ({1}%) | ", objects[i], Math.Round((objects_confidence[i] * 100), 2)));
                                                        }
                                                        if (detectionsTextSb.Length >= 3)
                                                        {
                                                            detectionsTextSb.Remove(detectionsTextSb.Length - 3, 3);
                                                        }
                                                        cam.last_detections_summary = detectionsTextSb.ToString();
                                                        Log($"{CurSrv} - The summary:" + cam.last_detections_summary);


                                                        if (!cam.trigger_url_cancels)
                                                        {
                                                            Log($"{CurSrv} - (5/6) Performing alert actions:");
                                                            await Trigger(cam, CurImg); //make TRIGGER
                                                        }
                                                        cam.IncrementAlerts(); //stats update
                                                        Log($"{CurSrv} - (6/6) SUCCESS.");


                                                        //create text string objects and confidences
                                                        string objects_and_confidences = "";
                                                        string object_positions_as_string = "";
                                                        for (int i = 0; i < objects.Count; i++)
                                                        {
                                                            objects_and_confidences += $"{objects[i]} ({Math.Round((objects_confidence[i] * 100), 0)}%); ";
                                                            object_positions_as_string += $"{objects_position[i]};";
                                                        }

                                                        //add to history list
                                                        Log($"{CurSrv} - Adding detection to history list.");
                                                        CreateListItem(CurImg.image_path, DateTime.Now.ToString("dd.MM.yy, HH:mm:ss"), cam.name, objects_and_confidences, object_positions_as_string);

                                                    }
                                                    //if no object fulfills all 3 requirements but there are other objects: 
                                                    else if (irrelevant_objects.Count() > 0)
                                                    {
                                                        //IRRELEVANT ALERT

                                                        if (cam.trigger_url_cancels)
                                                        {
                                                            Log($"{CurSrv} - (5/6) Performing alert CANCEL actions:");
                                                            await Trigger(cam, CurImg); //make TRIGGER
                                                        }

                                                        cam.IncrementIrrelevantAlerts(); //stats update
                                                        Log($"{CurSrv} - (6/6) Camera {cam.name} caused an irrelevant alert.");
                                                        //Log("Adding irrelevant detection to history list.");

                                                        //retrieve confidences and positions
                                                        string objects_and_confidences = "";
                                                        string object_positions_as_string = "";
                                                        for (int i = 0; i < irrelevant_objects.Count; i++)
                                                        {
                                                            objects_and_confidences += $"{irrelevant_objects[i]} ({Math.Round((irrelevant_objects_confidence[i] * 100), 0)}%); ";
                                                            object_positions_as_string += $"{irrelevant_objects_position[i]};";
                                                        }

                                                        //string text contains what is written in the log and in the history list
                                                        string text = "";
                                                        if (masked_counter > 0)//if masked objects, add them
                                                        {
                                                            text += $"{masked_counter}x masked; ";
                                                        }
                                                        if (threshold_counter > 0)//if objects out of confidence range, add them
                                                        {
                                                            text += $"{threshold_counter}x not in confidence range; ";
                                                        }
                                                        if (irrelevant_counter > 0) //if other irrelevant objects, add them
                                                        {
                                                            text += $"{irrelevant_counter}x irrelevant; ";
                                                        }

                                                        if (text != "") //remove last ";"
                                                        {
                                                            text = text.Remove(text.Length - 2);
                                                        }

                                                        Log($"{CurSrv} - {text}, so it's an irrelevant alert.");
                                                        //add to history list
                                                        CreateListItem(CurImg.image_path, DateTime.Now.ToString("dd.MM.yy, HH:mm:ss"), cam.name, $"{text} : {objects_and_confidences}", object_positions_as_string);
                                                    }
                                                }
                                                //if no object was detected
                                                else if (response.predictions.Length == 0)
                                                {
                                                    // FALSE ALERT

                                                    if (cam.trigger_url_cancels)
                                                    {
                                                        Log($"{CurSrv} - (5/6) Performing alert CANCEL actions:");
                                                        await Trigger(cam, CurImg); //make TRIGGER
                                                    }

                                                    cam.IncrementFalseAlerts(); //stats update
                                                    Log($"{CurSrv} - (6/6) Camera {cam.name} caused a false alert, nothing detected.");

                                                    //add to history list
                                                    Log($"{CurSrv} - Adding false to history list.");
                                                    CreateListItem(CurImg.image_path, DateTime.Now.ToString("dd.MM.yy, HH:mm:ss"), cam.name, "false alert", "");
                                                }
                                            }

                                            //if camera is disabled.
                                            else if (cam.enabled == false)
                                            {
                                                Log($"{CurSrv} - (6/6) Selected camera is disabled.");
                                            }

                                            ret = true;  //even though camera may disabled
                                        }
                                        else if (response.success == false) //if nothing was detected
                                        {
                                            error = $"{CurSrv} - ERROR: Failure response from DeepStack. JSON: '{cleanjsonString}'";
                                            Log(error);
                                        }

                                    }
                                    else if (string.IsNullOrEmpty(error))
                                    {
                                        //deserialization did not cause exception, it just gave a null response in the object?
                                        //probably wont happen but just making sure
                                        error = $"{CurSrv} - ERROR: Deserialization of 'Response' from DeepStack failed. response is null. JSON: '{cleanjsonString}'";
                                        Log(error);
                                    }


                                }
                                else
                                {
                                    error = $"{CurSrv} - ERROR: Empty string returned from HTTP post.";
                                    Log(error);
                                }

                            }
                            else
                            {
                                error = $"{CurSrv} - ERROR: Got http status code '{Convert.ToInt32(output.StatusCode)}' in {{yellow}}{swposttime.ElapsedMilliseconds}ms{{red}}: {output.ReasonPhrase}";
                                Log(error);
                            }

                        }

                    }

                    //load updated camera stats info in camera tab if a camera is selected
                    MethodInvoker LabelUpdate = delegate
                    {
                        if (list2.SelectedItems.Count > 0)
                        {
                            //load only stats from Camera.cs object

                            //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                            int i = AppSettings.Settings.CameraList.FindIndex(x => x.name == list2.SelectedItems[0].Text);

                            //load cameras stats
                            string stats = $"Alerts: {AppSettings.Settings.CameraList[i].stats_alerts.ToString()} | Irrelevant Alerts: {AppSettings.Settings.CameraList[i].stats_irrelevant_alerts.ToString()} | False Alerts: {AppSettings.Settings.CameraList[i].stats_false_alerts.ToString()}";
                            if (AppSettings.Settings.CameraList[i].maskManager.masking_enabled)
                            {
                                stats += $" | Mask History Count: {AppSettings.Settings.CameraList[i].maskManager.last_positions_history.Count()} | Current Dynamic Masks: {AppSettings.Settings.CameraList[i].maskManager.masked_positions.Count()}";
                            }
                            lbl_camstats.Text = stats;
                        }


                    };
                    Invoke(LabelUpdate);
                    //break; //end retries if code was successful
                }
                catch (Exception ex)
                {

                    //We should almost never get here due to all the null checks and function to wait for file to become available...
                    //When the connection to deepstack fails we will get here
                    //exception.tostring should give the line number and ALL detail - but maybe only if PDB is in same folder as exe?
                    error = $"{CurSrv} - ERROR: {Global.ExMsg(ex)}";
                    Log(error);

                }

                if (!string.IsNullOrEmpty(error) && AppSettings.Settings.send_errors == true)
                {
                    //upload the alert image which could not be analyzed to Telegram
                    if (AppSettings.Settings.send_errors == true)
                    {
                        await TelegramUpload(CurImg);
                    }

                }


                //I notice deepstack takes a lot longer the very first run?

                CurImg.TotalTimeMS = sw.ElapsedMilliseconds + CurImg.QueueWaitMS + CurImg.FileLockMS;
                CurImg.DeepStackTimeMS = swposttime.ElapsedMilliseconds;

                tcalc.AddToCalc(CurImg.TotalTimeMS);
                dcalc.AddToCalc(CurImg.DeepStackTimeMS);
                qcalc.AddToCalc(CurImg.QueueWaitMS);
                fcalc.AddToCalc(CurImg.FileLockMS);


                Log($"{CurSrv} - ...Object detection finished: ");
                Log($"{CurSrv} -        Total Time:   {{yellow}}{CurImg.TotalTimeMS}ms{{white}} (Count={tcalc.Count}, Min={tcalc.Min}ms, Max={tcalc.Max}ms, Avg={tcalc.Average.ToString("#####")}ms)");
                Log($"{CurSrv} -    DeepStack Time:   {{yellow}}{CurImg.DeepStackTimeMS}ms{{white}} (Count={dcalc.Count}, Min={dcalc.Min}ms, Max={dcalc.Max}ms, Avg={dcalc.Average.ToString("#####")}ms)");
                Log($"{CurSrv} -    File lock Time:   {{yellow}}{CurImg.FileLockMS}ms{{red}} (Count={fcalc.Count}, Min={fcalc.Min}ms, Max={fcalc.Max}ms, Avg={fcalc.Average.ToString("#####")}ms)");

                //I want to highlight when we have to wait for the last detection (or for the file to become readable) too long
                if (CurImg.QueueWaitMS + CurImg.FileLockMS >= 500)
                {
                    Log($"{CurSrv} -  {{red}}Image Queue Time:   {{yellow}}{CurImg.QueueWaitMS}ms{{red}} (Count={qcalc.Count}, Min={qcalc.Min}ms, Max={qcalc.Max}ms, Avg={qcalc.Average.ToString("#####")}ms)");
                }
                else
                {
                    Log($"{CurSrv} -  {{white}}Image Queue Time:   {{yellow}}{CurImg.QueueWaitMS}ms{{white}} (Count={qcalc.Count}, Min={qcalc.Min}ms, Max={qcalc.Max}ms, Avg={qcalc.Average.ToString("#####")}ms)");
                }

                Log($"{CurSrv} - Image Queue Depth:   {{yellow}}{CurImg.CurQueueSize}{{white}} (Count={qsizecalc.Count}, Min={qsizecalc.Min}, Max={qsizecalc.Max}, Avg={qsizecalc.Average.ToString("#####")})");

                //}

            }
            else
            {
                Log($"{CurSrv} - Skipping detection. Found='{cam.name}', Mins since last submission='{mins}', halfcool={halfcool}");
                //result = good anyway so we dont retry the image
                ret = true;
            }

            return ret;


        }

        //call trigger urls
        public void CallTriggerURLs(string[] trigger_urls)
        {

            var client = new WebClient();
            foreach (string x in trigger_urls)
            {
                try
                {
                    string content = client.DownloadString(x);
                    Log($"   -> trigger URL called: {x}, response: '{content}'");
                }
                catch (Exception ex)
                {
                    Log($"ERROR: Could not trigger URL '{x}', please check if '{x}' is correct and reachable: {Global.ExMsg(ex)}");
                }

            }

            //if (trigger_urls.Length > 1)
            //{
            //    Log($"   -> {trigger_urls.Length} trigger URLs called.");
            //}
            //else
            //{
            //    Log("   -> Trigger URL called.");
            //}
        }

        //send image to Telegram
        public async Task TelegramUpload(ClsImageQueueItem CurImg)
        {
            if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
            {
                //telegram upload sometimes fails
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    using (var image_telegram = System.IO.File.OpenRead(CurImg.image_path))
                    {
                        var bot = new TelegramBotClient(AppSettings.Settings.telegram_token);

                        //upload image to Telegram servers and send to first chat
                        Log($"      uploading image to chat \"{AppSettings.Settings.telegram_chatids[0]}\"");
                        var message = await bot.SendPhotoAsync(AppSettings.Settings.telegram_chatids[0], new InputOnlineFile(image_telegram, "image.jpg"));
                        string file_id = message.Photo[0].FileId; //get file_id of uploaded image

                        //share uploaded image with all remaining telegram chats (if multiple chat_ids given) using file_id 
                        foreach (string chatid in AppSettings.Settings.telegram_chatids.Skip(1))
                        {
                            Log($"      uploading image to chat \"{chatid}\"");
                            await bot.SendPhotoAsync(chatid, file_id);
                        }
                    }
                }
                catch
                {
                    Log($"ERROR: Could not upload image {CurImg.image_path} to Telegram.");
                    //store image that caused an error in ./errors/
                    if (!Directory.Exists("./errors/")) //if folder does not exist, create the folder
                    {
                        //create folder
                        DirectoryInfo di = Directory.CreateDirectory("./errors");
                        Log("./errors/" + " dir created.");
                    }
                    //save error image
                    using (var image = SixLabors.ImageSharp.Image.Load(CurImg.image_path))
                    {
                        image.Save("./errors/" + "TELEGRAM-ERROR-" + Path.GetFileName(CurImg.image_path) + ".jpg");
                    }
                }


                Log($"...Finished in {{yellow}}{sw.ElapsedMilliseconds}ms{{white}}");

            }
        }

        //send text to Telegram
        public async Task TelegramText(string text)
        {
            if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
            {
                //telegram upload sometimes fails
                try
                {
                    var bot = new Telegram.Bot.TelegramBotClient(AppSettings.Settings.telegram_token);
                    foreach (string chatid in AppSettings.Settings.telegram_chatids)
                    {
                        await bot.SendTextMessageAsync(chatid, text);
                    }

                }
                catch
                {
                    if (AppSettings.Settings.send_errors == true && text.Contains("ERROR") || text.Contains("WARNING")) //if Error message originating from Log() methods can't be uploaded
                    {
                        AppSettings.Settings.send_errors = false; //shortly disable send_errors to ensure that the Log() does not try to send the 'Telegram upload failed' message via Telegram again (causing a loop)
                        Log($"ERROR: Could not send text \"{text}\" to Telegram.");
                        AppSettings.Settings.send_errors = true;

                        //inform on main tab that Telegram upload failed
                        MethodInvoker LabelUpdate = delegate { lbl_errors.Text = "Can't upload error message to Telegram!"; };
                        Invoke(LabelUpdate);
                    }
                    else
                    {
                        Log($"ERROR: Could not send text \"{text}\" to Telegram.");
                    }
                }

            }
        }

        //trigger actions
        public async Task Trigger(Camera cam, ClsImageQueueItem CurImg)
        {
            //only trigger if cameras cooldown time since last detection has passed
            if ((DateTime.Now - cam.last_trigger_time).TotalMinutes >= cam.cooldown_time)
            {
                //call trigger urls
                if (cam.trigger_urls.Length > 0)
                {
                    //replace url paramters with according values
                    string[] urls = new string[cam.trigger_urls.Count()];
                    int c = 0;
                    //call urls
                    foreach (string url in cam.trigger_urls)
                    {
                        try
                        {
                            urls[c] = url.Replace("[camera]", cam.name)
                                     .Replace("[detection]", cam.last_detections.ElementAt(0)) //only gives first detection (maybe not most relevant one)
                                     .Replace("[position]", cam.last_positions.ElementAt(0))
                                     .Replace("[confidence]", cam.last_confidences.ElementAt(0).ToString())
                                     .Replace("[detections]", string.Join(",", cam.last_detections))
                                     .Replace("[confidences]", string.Join(",", cam.last_confidences.ToString()))
                                     .Replace("[imagepath]", CurImg.image_path) //gives the full path of the image that caused the trigger
                                     .Replace("[imagefilename]", Path.GetFileName(CurImg.image_path)) //gives the image name of the image that caused the trigger
                                     .Replace("[summary]", Uri.EscapeUriString(cam.last_detections_summary)); //summary text including all detections and confidences, p.e."person (91,53%)"
                        }
                        catch (Exception ex)
                        {
                            Log($"{Global.ExMsg(ex)}");
                        }

                        c++;
                    }

                    CallTriggerURLs(urls);
                }


                //upload to telegram
                if (cam.telegram_enabled)
                {
                    Log("   Uploading image to Telegram...");
                    await TelegramUpload(CurImg);
                    Log("   -> Sent image to Telegram.");
                }
            }
            else
            {
                //log that nothing was done
                Log($"   Camera {cam.name} is still in cooldown. Trigger URL wasn't called and no image will be uploaded to Telegram.");
            }

            cam.last_trigger_time = DateTime.Now; //reset cooldown time every time an image contains something, even if no trigger was called (still in cooldown time)

            Task ignoredAwaitableResult = this.LastTriggerInfo(cam); //write info to label

        }



        //check if detected object is outside the mask for the specific camera
        public bool Outsidemask(string cameraname, double xmin, double xmax, double ymin, double ymax, int width, int height)
        {
            //Log($"      Checking if object is outside privacy mask of {cameraname}:");
            //Log("         Loading mask file...");
            try
            {
                if (System.IO.File.Exists("./cameras/" + cameraname + ".png")) //only check if mask image exists
                {
                    //load mask file (in the image all places that have color (transparency > 9 [0-255 scale]) are masked)
                    using (var mask_img = new Bitmap($"./cameras/{cameraname}.png"))
                    {
                        //if any coordinates of the object are outside of the mask image, th mask image must be too small.
                        if (mask_img.Width != width || mask_img.Height != height)
                        {
                            Log($"ERROR: The resolution of the mask './camera/{cameraname}.png' does not equal the resolution of the processed image. Skipping privacy mask feature. Image: {width}x{height}, Mask: {mask_img.Width}x{mask_img.Height}");
                            return true;
                        }

                        Log("         Checking if the object is in a masked area...");

                        //relative x and y locations of the 9 detection points
                        double[] x_factor = new double[] { 0.25, 0.5, 0.75, 0.25, 0.5, 0.75, 0.25, 0.5, 0.75 };
                        double[] y_factor = new double[] { 0.25, 0.25, 0.25, 0.5, 0.5, 0.5, 0.75, 0.75, 0.75 };

                        int result = 0; //counts how many of the 9 points are outside of masked area(s)

                        //check the transparency of the mask image in all 9 detection points
                        for (int i = 0; i < 9; i++)
                        {
                            //get image point coordinates (and converting double to int)
                            int x = (int)(xmin + (xmax - xmin) * x_factor[i]);
                            int y = (int)(ymin + (ymax - ymin) * y_factor[i]);

                            // Get the color of the pixel
                            Color pixelColor = mask_img.GetPixel(x, y);

                            //if the pixel is transparent (A refers to the alpha channel), the point is outside of masked area(s)
                            if (pixelColor.A < 10)
                            {
                                result++;
                            }
                        }

                        Log($"         { result.ToString() } of 9 detection points are outside of masked areas."); //print how many of the 9 detection points are outside of masked areas.

                        if (result > 4) //if 5 or more of the 9 detection points are outside of masked areas, the majority of the object is outside of masked area(s)
                        {
                            Log("      ->The object is OUTSIDE of masked area(s).");
                            return true;
                        }
                        else //if 4 or less of 9 detection points are outside, then 5 or more points are in masked areas and the majority of the object is so too
                        {
                            Log("      ->The object is INSIDE a masked area.");
                            return false;
                        }

                    }
                }
                else //if mask image does not exist, object is outside the non-existing masked area
                {
                    Log("     ->Camera has no mask, the object is OUTSIDE of the masked area.");
                    return true;
                }

            }
            catch
            {
                Log($"ERROR while loading the mask file ./cameras/{cameraname}.png.");
                return true;
            }

        }

        //save how many times an error happened
        public void IncrementErrorCounter()
        {
            errors++;
            try
            {
                if (this.Visible)
                {
                    MethodInvoker LabelUpdate = delegate
                    {
                        lbl_errors.Show();
                        lbl_errors.Text = $"{errors.ToString()} error(s) occurred. Click to open Log."; //update error counter label
                    };
                    //getting error here when called too early - had to check if Visible or not -Vorlon
                    Invoke(LabelUpdate);

                }

            }
            catch (Exception)
            {

            }
        }

        //add text to log
        public async void Log(string text, [CallerMemberName] string memberName = null)
        {

            try
            {

                //get current date and time

                string time = DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss");
                string rtftime = DateTime.Now.ToString("dHH:mm:ss");  //no need for date in log tab
                string ModName = "";
                if (memberName == ".ctor")
                    memberName = "Constructor";

                if (AppSettings.Settings.log_everything == true || AppSettings.Settings.deepstack_debug)
                {
                    time = DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss.fff");
                    rtftime = DateTime.Now.ToString("HH:mm:ss.fff");
                    if (memberName != null && !string.IsNullOrEmpty(memberName))
                        ModName = memberName.PadLeft(24) + "> ";

                    //when the global logger reports back to the progress logger we cant use CallerMemberName, so extract the member name from text

                    int gg = text.IndexOf(">> ");

                    if (gg > 0 && gg <= 24)
                    {
                        string modfromglobal = Global.GetWordBetween(text, "", ">> ");
                        if (!string.IsNullOrEmpty(modfromglobal))
                        {
                            ModName = modfromglobal.PadLeft(24) + "> ";
                            text = Global.GetWordBetween(text, ">> ", "");
                        }

                    }
                }

                //check for messages coming from deepstack processes and kill them if we didnt ask for debugging messages
                if (!AppSettings.Settings.deepstack_debug)
                {
                    if (text.ToLower().Contains("redis-server.exe>") || text.ToLower().Contains("python.exe>"))
                    {
                        return;
                    }
                }

                //make the error and warning detection case insensitive:
                bool HasError = (text.IndexOf("error", StringComparison.InvariantCultureIgnoreCase) > -1) || (text.IndexOf("exception", StringComparison.InvariantCultureIgnoreCase) > -1) || (text.IndexOf("fail", StringComparison.InvariantCultureIgnoreCase) > -1);
                bool HasWarning = (text.IndexOf("warning", StringComparison.InvariantCultureIgnoreCase) > -1);
                bool IsDeepStackMsg = (memberName.IndexOf("deepstack", StringComparison.InvariantCultureIgnoreCase) > -1) || (text.IndexOf("deepstack", StringComparison.InvariantCultureIgnoreCase) > -1) || (ModName.IndexOf("deepstack", StringComparison.InvariantCultureIgnoreCase) > -1);
                string RTFText = "";

                //set the color for RTF text window:
                if (HasError)
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{red}}{text}";
                }
                else if (HasWarning)
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{mediumorchid}}{text}";
                }
                else if (IsDeepStackMsg)
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{lime}}{text}";
                }
                else
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{white}}{text}";
                }

                //get rid of any common color coding before logging to file or console
                text = text.Replace("{yellow}", "").Replace("{red}", "").Replace("{white}", "").Replace("{orange}", "").Replace("{lime}", "").Replace("{orange}", "mediumorchid");

                //if log everything is disabled and the text is neither an ERROR, nor a WARNING: write only to console and ABORT
                if (AppSettings.Settings.log_everything == false && !HasError && !HasWarning)
                {
                    //Creates a lot of extra text in immediate window while debugging, disabling -Vorlon
                    //text += "Enabling \'Log everything\' might give more information.";
                    Console.WriteLine($"[{rtftime}]: {ModName}{text}");

                    return;
                }



                //add text to log
                try
                {
                    RTFLogger.LogToRTF(RTFText);
                    LogWriter.WriteToLog($"[{time}]:  {ModName}{text}", HasError);
                    //using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", append: true))
                    //{
                    //    sw.WriteLine($"[{time}]: {text}");
                    //}
                }
                catch
                {
                    MethodInvoker LabelUpdate = delegate { lbl_errors.Text = "Can't write to log.txt file!"; };
                    Invoke(LabelUpdate);
                }

                if (AppSettings.Settings.send_errors == true && HasError || HasWarning)
                {
                    await TelegramText($"[{time}]: {text}"); //upload text to Telegram
                }



                //add log text to console
                Console.WriteLine($"[{rtftime}]: {ModName}{text}");

                //increment error counter
                if (HasError || HasWarning)
                {
                    IncrementErrorCounter();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: In LOG, got: " + ex.Message);
            }

        }



        //----------------------------------------------------------------------------------------------------------
        //GUI
        //----------------------------------------------------------------------------------------------------------

        //minimize to tray
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }
            else
            {
                ResizeListViews();
            }
        }

        //open from tray
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        //open Log when clicking or error message
        private void lbl_errors_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppSettings.Settings.LogFileName))
            {
                System.Diagnostics.Process.Start(AppSettings.Settings.LogFileName);
                lbl_errors.Text = "";
            }
            else
            {
                MessageBox.Show("log missing");
            }

        }

        //adapt list views (history tab and cameras tab) to window size while considering scrollbar influence
        private void ResizeListViews()
        {
            //suspend layout of most complex tablelayout elements (gives a few milliseconds)
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();

            //variable storing list1 effective width
            int width = list1.Width;

            //subtract vertical scrollbar width if scrollbar is shown (scrollbar is shown when there are more items(including the header row) than fit in the visible space of the list) 
            try
            {
                if (list1.Items.Count > 0 && list1.Height <= (list1.GetItemRect(0).Height * (list1.Items.Count + 1)))
                {
                    width -= SystemInformation.VerticalScrollBarWidth;
                }
            }
            catch
            {
                Log("ERROR in ReziseListViews(), checking if scrollbar is shown and subtracting scrollbar width failed.");
            }

            //fix an exception where form_resize calls this function too early:
            if (list1.Columns.Count > 0)
            {
                if (width > 350) // if the list is wider than 350px, aditionally show the 'detections' column and mainly grow this column
                {
                    //set left list column width segmentation
                    list1.Columns[0].Width = width * 0 / 100; //filename
                    list1.Columns[1].Width = 120 + (width - 350) * 25 / 1000; //date
                    list1.Columns[2].Width = 120 + (width - 350) * 25 / 1000; //cam name
                    list1.Columns[3].Width = 80 + (width - 350) * 95 / 100; //obj and confidences
                    list1.Columns[4].Width = width * 0 / 100; // object positions of all detected objects separated by ";"
                    list1.Columns[5].Width = 30; //checkmark if something relevant detected or not

                }
                else //if the form is smaller than 350px in width, don't show the detections column
                {
                    //set left list column width segmentation
                    list1.Columns[0].Width = width * 0 / 100; //filename
                    list1.Columns[1].Width = width * 47 / 100; //date
                    list1.Columns[2].Width = width * 43 / 100; //cam name
                    list1.Columns[3].Width = width * 0 / 100; //obj and confidences
                    list1.Columns[4].Width = width * 0 / 100; // object positions of all detected objects separated by ";"
                    list1.Columns[5].Width = width * 10 / 100; //checkmark if something relevant detected or not
                }

            }

            if (list2.Columns.Count > 0)
                list2.Columns[0].Width = list2.Width - 4; //resize camera list column

            //resume layout again
            tableLayoutPanel7.ResumeLayout();
            tableLayoutPanel8.ResumeLayout();
            tableLayoutPanel9.ResumeLayout();
        }

        //add last trigger time to label on Overview page
        private async Task LastTriggerInfo(Camera cam)
        {
            string text1 = $"{cam.name} last triggered at {cam.last_trigger_time}. Sleeping for {cam.cooldown_time / 2} minutes."; //write last trigger time to label on Overview page
            lbl_info.Text = text1;

            int time = 30 * Convert.ToInt32(1000 * cam.cooldown_time);
            await Task.Delay(time); // wait while the analysis is sleeping for this camera
            if (lbl_info.Text == text1)
            {
                lbl_info.Text = $"{cam.name} last triggered at {cam.last_trigger_time}."; //Remove "sleeping for ..."
            }
        }


        //EVENTS:

        //event: mouse click on tab control
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            ResizeListViews();
        }

        //event: another tab selected (Only load certain things in tabs if they are actually open)
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                UpdatePieChart(); UpdateTimeline(); UpdateConfidenceChart();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                //CleanCSVList(); //removed to load the history list faster
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabDeepStack"])
            {
                LoadDeepStackTab(true);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabLog"])
            {
                //scroll to bottom, only when tab is active for better performance 
                Global.InvokeIFRequired(this.RTF_Log, () =>
                {
                    this.RTF_Log.SelectionStart = this.RTF_Log.Text.Length;
                    this.RTF_Log.ScrollToCaret();
                });
            }

        }


        //----------------------------------------------------------------------------------------------------------
        //STATS TAB
        //----------------------------------------------------------------------------------------------------------

        //other camera in combobox selected, display according PieChart
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {

                UpdatePieChart(); UpdateTimeline(); UpdateConfidenceChart();
            }
        }

        //update pie chart
        public void UpdatePieChart()
        {
            int alerts = 0;
            int irrelevantalerts = 0;
            int falsealerts = 0;

            if (comboBox1.Text == "All Cameras")
            {
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    alerts += cam.stats_alerts;
                    irrelevantalerts += cam.stats_irrelevant_alerts;
                    falsealerts += cam.stats_false_alerts;
                }
            }
            else
            {
                int i = AppSettings.Settings.CameraList.FindIndex(x => x.name.ToLower().Trim() == comboBox1.Text.ToLower().Trim());
                if (i > -1)
                {
                    alerts = AppSettings.Settings.CameraList[i].stats_alerts;
                    irrelevantalerts = AppSettings.Settings.CameraList[i].stats_irrelevant_alerts;
                    falsealerts = AppSettings.Settings.CameraList[i].stats_false_alerts;
                }
                else
                {
                    alerts = 0;
                    irrelevantalerts = 0;
                    falsealerts = 0;
                    Log($"Error: Could not match combobox dropdown '{comboBox1.Text}' to a known camera name?");
                }
            }

            chart1.Series[0].Points.Clear();

            chart1.Series[0].LegendText = "#VALY #VALX";
            chart1.Series[0]["PieLabelStyle"] = "Disabled";

            int index = -1;

            //show Alerts label
            index = chart1.Series[0].Points.AddXY("Alerts", alerts);
            chart1.Series[0].Points[index].Color = Color.Green;

            //show irrelevant Alerts label
            index = chart1.Series[0].Points.AddXY("irrelevant Alerts", irrelevantalerts);
            chart1.Series[0].Points[index].Color = Color.Orange;

            //show false Alerts label
            index = chart1.Series[0].Points.AddXY("false Alerts", falsealerts);
            chart1.Series[0].Points[index].Color = Color.OrangeRed;
        }

        //update timeline
        public async void UpdateTimeline()
        {
            Log("Loading time line from cameras/history.csv ...");

            //clear previous values
            timeline.Series[0].Points.Clear();
            timeline.Series[1].Points.Clear();
            timeline.Series[2].Points.Clear();
            timeline.Series[3].Points.Clear();

            List<string> result = new List<string>(); //List that later on will be containing all lines of the csv file

            Stopwatch SW = Stopwatch.StartNew();

            bool Success = await Global.WaitForFileAccessAsync(AppSettings.Settings.HistoryFileName, FileSystemRights.Read, FileShare.ReadWrite);

            if (Success)
            {
                if (comboBox1.Text.Trim() == "All Cameras") //all cameras selected
                {
                    //load all lines except the first line
                    foreach (string line in System.IO.File.ReadAllLines(AppSettings.Settings.HistoryFileName).Skip(1))
                    {
                        result.Add(line);
                    }
                }
                else //camera selection
                {
                    string cameraname = comboBox1.Text.Trim();

                    //load all lines from the history.csv except the first line into List (the first line is the table heading and not an alert entry)
                    foreach (string line in System.IO.File.ReadAllLines(AppSettings.Settings.HistoryFileName).Skip(1))
                    {
                        if (line.Split('|')[2] == cameraname)
                        {
                            result.Add(line);
                        }
                    }
                }

            }
            else
            {
                Log($"Error: Could not gain access to history file for {SW.ElapsedMilliseconds}ms - {AppSettings.Settings.HistoryFileName}");

            }

            //every int represents the number of ai calls in successive half hours (p.e. relevant[0] is 0:00-0:30 o'clock, relevant[1] is 0:30-1:00 o'clock) 
            int[] all = new int[48];
            int[] falses = new int[48];
            int[] irrelevant = new int[48];
            int[] relevant = new int[48];

            //fill arrays with amount of calls/half hour
            foreach (var val in result)
            {               //example of time column entry: 23.08.19, 18:31:09
                //get hour
                string hourstring = val.Split('|')[1].Split(',')[1].Split(':')[0];
                int hour;
                Int32.TryParse(hourstring, out hour);

                //get minute
                string minutestring = val.Split('|')[1].Split(',')[1].Split(':')[1];
                int minute;
                Int32.TryParse(minutestring, out minute);

                int halfhour; //stores the half hour in which the alert occured

                //add +1 to counter for corresponding half-hour
                if (minute > 30) //if alert occured after half o clock
                {
                    halfhour = hour * 2 + 1;
                }
                else //if alert occured before half o clock
                {
                    halfhour = hour * 2;
                }

                //if detection was successful
                if (val.Split('|')[5] == "true")
                {
                    relevant[halfhour]++;
                }
                //if it was a false alert
                else if (val.Split('|')[3] == "false alert")
                {
                    falses[halfhour]++;
                }
                //if something irrelevant was detected
                else
                {
                    irrelevant[halfhour]++;
                }

                all[halfhour]++;
            }

            //add to graph "all":

            /*the graph will have a gap at the end and at the beginning if we don'f specify a value
            * with an x value outside the visible area at the end and before the first visible point. 
            * So the first point is at -0.25 and has the value of the last visible point and the 
            * last point is at 24.25 and has the value of the first visible point. */

            timeline.Series[0].Points.AddXY(-0.25, all[47]); // beginning point with value of last visible point

            //and now add all visible points 
            double x = 0.25;
            foreach (int halfhour in all)
            {
                int index = timeline.Series[0].Points.AddXY(x, halfhour);
                x = x + 0.5;
            }

            timeline.Series[0].Points.AddXY(24.25, all[0]); // finally add last point with value of first visible point

            //add to graph "falses":

            timeline.Series[1].Points.AddXY(-0.25, falses[47]); // beginning point with value of last visible point
            //and now add all visible points 
            x = 0.25;
            foreach (int halfhour in falses)
            {
                int index = timeline.Series[1].Points.AddXY(x, halfhour);
                x = x + 0.5;
            }
            timeline.Series[1].Points.AddXY(24.25, falses[0]); // finally add last point with value of first visible point

            //add to graph "irrelevant":

            timeline.Series[2].Points.AddXY(-0.25, irrelevant[47]); // beginning point with value of last visible point
            //and now add all visible points 
            x = 0.25;
            foreach (int halfhour in irrelevant)
            {
                int index = timeline.Series[2].Points.AddXY(x, halfhour);
                x = x + 0.5;
            }
            timeline.Series[2].Points.AddXY(24.25, irrelevant[0]); // finally add last point with value of first visible point

            //add to graph "relevant":

            timeline.Series[3].Points.AddXY(-0.25, relevant[47]); // beginning point with value of last visible point
            //and now add all visible points 
            x = 0.25;
            foreach (int halfhour in relevant)
            {
                int index = timeline.Series[3].Points.AddXY(x, halfhour);
                x = x + 0.5;
            }
            timeline.Series[3].Points.AddXY(24.25, relevant[0]); // finally add last point with value of first visible point


        }

        //update confidence_frequency chart
        public async void UpdateConfidenceChart()
        {
            Log("Loading confidence-frequency chart from cameras/history.csv ...");

            //clear previous values
            chart_confidence.Series[0].Points.Clear();
            chart_confidence.Series[1].Points.Clear();

            List<string> result = new List<string>(); //List that later on will be containing all lines of the csv file

            Stopwatch SW = Stopwatch.StartNew();

            bool Success = await Global.WaitForFileAccessAsync(AppSettings.Settings.HistoryFileName, FileSystemRights.Read, FileShare.ReadWrite);

            if (Success)
            {
                if (comboBox1.Text == "All Cameras") //all cameras selected
                {
                    //load all lines except the first line
                    foreach (string line in System.IO.File.ReadAllLines(AppSettings.Settings.HistoryFileName).Skip(1))
                    {
                        result.Add(line);
                    }
                }
                else //camera selection
                {
                    string cameraname = comboBox1.Text.Trim();

                    //load all lines from the history.csv except the first line into List (the first line is the table heading and not an alert entry)
                    foreach (string line in System.IO.File.ReadAllLines(AppSettings.Settings.HistoryFileName).Skip(1))
                    {
                        if (line.Split('|')[2] == cameraname)
                        {
                            result.Add(line);
                        }
                    }
                }

            }
            else
            {
                Log($"Error: Could not gain access to history file for {SW.ElapsedMilliseconds}ms - {AppSettings.Settings.HistoryFileName}");

            }

            //this array stores the Absolute frequencies of all possible confidence values (0%-100%)
            int[] green_values = new int[101];
            int[] orange_values = new int[101];

            //fill array with frequencies
            foreach (var line in result)
            {
                //example of detections column entry: "person (41%); person (97%);" or "masked: person (41%); person (97%);"
                string detections_column = line.Split('|')[3];
                if (detections_column.Contains(':'))
                {
                    detections_column = detections_column.Split(':')[1];

                    string[] detections = detections_column.Split(';');

                    //write the confidence of every detection into the green_values string
                    foreach (string detection in detections)
                    {
                        if (detection.Contains('%'))
                        {
                            //example: -> "person (41%)"
                            Int32.TryParse(detection.Split('(')[1].Split('%')[0], out int x_value); //example: -> "41"
                            orange_values[x_value]++;
                        }
                    }
                }
                else
                {
                    string[] detections = detections_column.Split(';');

                    //write the confidence of every detection into the green_values string
                    foreach (string detection in detections)
                    {
                        if (detection.Contains('%'))
                        {
                            //example: -> "person (41%)"
                            Int32.TryParse(detection.Split('(')[1].Split('%')[0], out int x_value); //example: -> "41"
                            green_values[x_value]++;
                        }
                    }
                }
            }


            //write green series in chart
            int i = 0;
            foreach (int y_value in green_values)
            {
                chart_confidence.Series[1].Points.AddXY(i, y_value);
                i++;
            }

            //write orange series in chart
            i = 0;
            foreach (int y_value in orange_values)
            {
                chart_confidence.Series[0].Points.AddXY(i, y_value);
                i++;
            }

        }


        //----------------------------------------------------------------------------------------------------------
        //HISTORY TAB
        //----------------------------------------------------------------------------------------------------------

        // load images from input_path to left list
        /*public void LoadList()
        {
            list1.Items.Clear();
            try
            {
                string[] files = Directory.GetFiles(input_path, $"*.jpg");

                foreach (string file in files)
                {

                    string fileName = Path.GetFileName(file);
                    ListViewItem item = new ListViewItem(new string[] { fileName, "content" });
                    item.Tag = file;

                    list1.Items.Add(item);

                }
            }
            catch
            {
                MessageBox.Show("Can't find the input directory, please check it.");
            }
            if (list1.Items.Count > 0)
            {
                list1.Items[0].Selected = true; //select first image
            }
        }*/

        //show or hide the privacy mask overlay
        private void showHideMask()
        {
            if (cb_showMask.Checked == true) //show overlay
            {
                Log("Show mask toggled.");
                if (list1.SelectedItems.Count > 0)
                {
                    if (System.IO.File.Exists("./cameras/" + list1.SelectedItems[0].SubItems[2].Text + ".png")) //check if privacy mask file exists
                    {
                        using (var img = new Bitmap("./cameras/" + list1.SelectedItems[0].SubItems[2].Text + ".png"))
                        {
                            pictureBox1.Image = new Bitmap(img); //load mask as overlay
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null; //if file does not exist, empty mask overlay (from possible overlays of previous images)
                    }

                }
            }
            else //if showmask toggle-button is not checked, hide the mask overlay
            {
                pictureBox1.Image = null;
            }

        }

        //show rectangle overlay
        private void showObject(PaintEventArgs e, Color color, int _xmin, int _ymin, int _xmax, int _ymax, string text)
        {
            try
            {
                if ((list1.SelectedItems.Count > 0) && (pictureBox1 != null) && (pictureBox1.BackgroundImage != null))
                {
                    //1. get the padding between the image and the picturebox border

                    //get dimensions of the image and the picturebox
                    float imgWidth = pictureBox1.BackgroundImage.Width;
                    float imgHeight = pictureBox1.BackgroundImage.Height;
                    float boxWidth = pictureBox1.Width;
                    float boxHeight = pictureBox1.Height;

                    //these variables store the padding between image border and picturebox border
                    int absX = 0;
                    int absY = 0;

                    //because the sizemode of the picturebox is set to 'zoom', the image is scaled down
                    float scale = 1;


                    //Comparing the aspect ratio of both the control and the image itself.
                    if (imgWidth / imgHeight > boxWidth / boxHeight) //if the image is p.e. 16:9 and the picturebox is 4:3
                    {
                        scale = boxWidth / imgWidth; //get scale factor
                        absY = (int)(boxHeight - scale * imgHeight) / 2; //padding on top and below the image
                    }
                    else //if the image is p.e. 4:3 and the picturebox is widescreen 16:9
                    {
                        scale = boxHeight / imgHeight; //get scale factor
                        absX = (int)(boxWidth - scale * imgWidth) / 2; //padding left and right of the image
                    }

                    //2. inputted position values are for the original image size. As the image is probably smaller in the picturebox, the positions must be adapted. 
                    int xmin = (int)(scale * _xmin) + absX;
                    int xmax = (int)(scale * _xmax) + absX;
                    int ymin = (int)(scale * _ymin) + absY;
                    int ymax = (int)(scale * _ymax) + absY;

                    //set alpha/transparency so you can see under the label
                    Color newColor = Color.FromArgb(100, color);  //The alpha component specifies how the shape and background colors are mixed; alpha values near 0 place more weight on the background colors, and alpha values near 255 place more weight on the shape color.

                    //3. paint rectangle
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
                    using (Pen pen = new Pen(newColor, 2))
                    {
                        e.Graphics.DrawRectangle(pen, rect); //draw rectangle
                    }

                    //object name text below rectangle
                    rect = new System.Drawing.Rectangle(xmin - 1, ymax, (int)boxWidth, (int)boxHeight); //sets bounding box for drawn text


                    Brush brush = new SolidBrush(newColor); //sets background rectangle color

                    System.Drawing.SizeF size = e.Graphics.MeasureString(text, new Font("Segoe UI Semibold", 10)); //finds size of text to draw the background rectangle
                    e.Graphics.FillRectangle(brush, xmin - 1, ymax, size.Width, size.Height); //draw grey background rectangle for detection text
                    e.Graphics.DrawString(text, new Font("Segoe UI Semibold", 10), Brushes.Black, rect); //draw detection text



                }

            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }
        }

        //load object rectangle overlays
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (cb_showObjects.Checked && list1.SelectedItems.Count > 0) //if checkbox button is enabled
            {
                //Log("Loading object rectangles...");
                int countr = list1.SelectedItems[0].SubItems[4].Text.Split(';').Count();

                Color color = new Color();
                string detections = list1.SelectedItems[0].SubItems[3].Text;
                if (detections.Contains("irrelevant") || detections.Contains("masked") || detections.Contains("confidence"))
                {
                    color = Color.Silver;
                    detections = detections.Split(':')[1]; //removes the "1x masked, 3x irrelevant:" before the actual detection, otherwise this would be displayed in the detection tags
                }
                else
                {
                    color = Color.Red;
                }

                //display a rectangle around each relevant object
                for (int i = 0; i < countr - 1; i++)
                {
                    string[] detectionsArray = detections.Split(';');//creates array of detected objects, used for adding text overlay
                    //load 'xmin,ymin,xmax,ymax' from third column into a string
                    string position = list1.SelectedItems[0].SubItems[4].Text.Split(';')[i];

                    //store xmin, ymin, xmax, ymax in separate variables
                    Int32.TryParse(position.Split(',')[0], out int xmin);
                    Int32.TryParse(position.Split(',')[1], out int ymin);
                    Int32.TryParse(position.Split(',')[2], out int xmax);
                    Int32.TryParse(position.Split(',')[3], out int ymax);

                    //Log($"{i} - {xmin}, {ymin}, {xmax},  {ymax}");

                    showObject(e, color, xmin, ymin, xmax, ymax, detectionsArray[i]); //call rectangle drawing method, calls appropriate detection text

                    //Log("Done.");
                }
            }
        }

        // add new entry in left list
        public void CreateListItem(string filename, string date, string camera, string objects_and_confidence, string object_positions)
        {
            string success;
            if (objects_and_confidence.Contains("%") && !objects_and_confidence.Contains(':'))
            {
                success = "true";
            }
            else
            {
                success = "false";
            }
            MethodInvoker LabelUpdate = delegate
            {
                if (checkListFilters(camera, success, objects_and_confidence)) //only show the entry in the history list if no filter applies
                {
                    ListViewItem item;
                    if (success == "true")
                    {
                        item = new ListViewItem(new string[] { filename, date, camera, objects_and_confidence, object_positions, "✓" });
                        item.ForeColor = Color.Green;
                    }
                    else
                    {
                        item = new ListViewItem(new string[] { filename, date, camera, objects_and_confidence, object_positions, "X" });
                    }

                    //add the FULL path to the item tag so we dont need to add a column
                    //** no need, saving full filename in list anyway
                    //item.Tag = filename;

                    list1.Items.Insert(0, item);

                    ResizeListViews();
                }



                //update history CSV
                string line = $"{filename}|{date}|{camera}|{objects_and_confidence}|{object_positions}|{success}";
                try
                {
                    HistoryWriter.WriteToLog(line);
                    //using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "cameras/history.csv", append: true))
                    //{
                    //    sw.WriteLine(line);
                    //}
                }
                catch { }
            };
            Invoke(LabelUpdate);


        }

        //remove entry from left list
        public void DeleteListItem(string filename)
        {

            using (Global_GUI.CursorWait cw = new Global_GUI.CursorWait(false, false))
            {
                Stopwatch SW = Stopwatch.StartNew();

                MethodInvoker LabelUpdate = async delegate
                {
                    //bool isfullpath = filename.Contains("\\");
                    //string justfile = filename;
                    //if (isfullpath)
                    //{
                    //    justfile = Path.GetFileName(filename);
                    //}

                    ListViewItem listviewitem = new ListViewItem();
                    for (int i = 0; i < list1.Items.Count; i++)
                    {
                        listviewitem = list1.Items[i];
                        if (listviewitem.Text.ToLower().Contains(filename.ToLower()))
                        {
                            list1.Items.Remove(listviewitem);
                            break;
                        }
                    }

                    ResizeListViews();

                    //remove entry from history csv
                    try
                    {
                        bool Success = await Global.WaitForFileAccessAsync(AppSettings.Settings.HistoryFileName, FileSystemRights.Read, FileShare.ReadWrite);
                        if (Success)
                        {
                            string[] oldLines = System.IO.File.ReadAllLines(AppSettings.Settings.HistoryFileName);
                            string[] newLines = oldLines.Where(line => !line.Split('|')[0].ToLower().Contains(filename.ToLower())).ToArray();
                            if (oldLines.Count() != newLines.Count())
                            {
                                System.IO.File.WriteAllLines(AppSettings.Settings.HistoryFileName, newLines);
                            }
                        }
                        else
                        {
                            Log($"Error: Could not gain access to history file for {SW.ElapsedMilliseconds}ms - {AppSettings.Settings.HistoryFileName}");

                        }

                    }
                    catch (Exception ex)
                    {
                        Log("ERROR: Can't write to cameras/history.csv: " + Global.ExMsg(ex));
                    }

                };

                Invoke(LabelUpdate);

                string val = "";
                detection_dictionary.TryRemove(filename.ToLower(), out val);

                //try to get a better feel how much time this function consumes - Vorlon
                //Log($"Removed alert image '{filename}' from history list and from cameras/history.csv in {{yellow}}{SW.ElapsedMilliseconds}ms{{white}} ({list1.Items.Count} list items)");

            }

        }

        //remove all obsolete entries (associated image does not exist anymore) from the history.csv 
        public void CleanCSVList()
        {

            if (AppSettings.AlreadyRunning)
            {
                Log($"Skipping clean of history.csv, instance already running.");
                return;
            }

            using (Global_GUI.CursorWait cw = new Global_GUI.CursorWait(false, false))
            {

                Log($"Cleaning cameras/history.csv if necessary...");

                Stopwatch SW = Stopwatch.StartNew();
                Int32 oldcsvlines = 0;
                Int32 newcsvlines = 0;

                MethodInvoker LabelUpdate = async delegate
                {
                    try
                    {
                        if (System.IO.File.Exists(AppSettings.Settings.HistoryFileName))
                        {

                            bool Success = await Global.WaitForFileAccessAsync(AppSettings.Settings.HistoryFileName, FileSystemRights.Read, FileShare.ReadWrite);

                            if (Success)
                            {
                                string[] oldLines = System.IO.File.ReadAllLines(AppSettings.Settings.HistoryFileName); //old history.csv
                                oldcsvlines = oldLines.Count();

                                List<string> newLines = new List<string>(); //new history.csv
                                newLines.Add(oldLines[0]); // add title line from old to new history.csv

                                foreach (string line in oldLines.Skip(1)) //check for every line except title line if associated image still exists in input folder 
                                {
                                    if (System.IO.File.Exists(line.Split('|')[0]))
                                    {
                                        newLines.Add(line);
                                    }
                                }
                                newcsvlines = newLines.Count;

                                System.IO.File.WriteAllLines(AppSettings.Settings.HistoryFileName, newLines); //write new history.csv
                            }
                            else
                            {
                                Log($"Error: Could not gain access to history file for {SW.ElapsedMilliseconds}ms - {AppSettings.Settings.HistoryFileName}");
                            }

                        }
                        else
                        {
                            Log("File does not exist yet: cameras/history.csv");
                        }
                    }
                    catch
                    {
                        Log("ERROR: Can't clean the cameras/history.csv!");
                    }

                };
                Invoke(LabelUpdate);

                //try to get a better feel how much time this function consumes - Vorlon
                Log($"...Cleaned list in {{yellow}}{SW.ElapsedMilliseconds}ms{{white}}, {newcsvlines} CVS lines, removed {oldcsvlines - newcsvlines}");

            }

        }

        //load stored entries in history CSV into history ListView
        private async Task LoadFromCSVAsync()
        {
            try
            {
                using (Global_GUI.CursorWait cw = new Global_GUI.CursorWait(false, false))
                {

                    if (System.IO.File.Exists(AppSettings.Settings.HistoryFileName))
                    {
                        Log("Loading history list from cameras/history.csv ...");

                        Stopwatch SW = Stopwatch.StartNew();

                        //delete obsolete entries from history.csv
                        //CleanCSVList(); //removed to load the history list faster

                        List<string> result = new List<string>(); //List that later on will be containing all lines of the csv file

                        bool Success = await Global.WaitForFileAccessAsync(AppSettings.Settings.HistoryFileName);

                        if (Success)
                        {
                            //load all lines except the first line into List (the first line is the table heading and not an alert entry)
                            foreach (string line in System.IO.File.ReadAllLines(AppSettings.Settings.HistoryFileName).Skip(1))
                            {
                                result.Add(line);
                            }

                            List<string> itemsToDelete = new List<string>(); //stores all filenames of history.csv entries that need to be removed

                            MethodInvoker LabelUpdate = delegate
                            {
                                list1.Items.Clear();

                                //load all List elements into the ListView for each row
                                foreach (var val in result)
                                {
                                    string camera = val.Split('|')[2];
                                    string success = val.Split('|')[5];
                                    string objects_and_confidence = val.Split('|')[3];
                                    if (!checkListFilters(camera, success, objects_and_confidence)) { continue; } //do not load the entry if a filter applies (checking as early as possible)
                                    string filename = val.Split('|')[0];
                                    string date = val.Split('|')[1];
                                    string object_positions = val.Split('|')[4];

                                    ListViewItem item;
                                    if (success == "true")
                                    {
                                        item = new ListViewItem(new string[] { filename, date, camera, objects_and_confidence, object_positions, "✓" });
                                        item.ForeColor = Color.Green;
                                    }
                                    else
                                    {
                                        item = new ListViewItem(new string[] { filename, date, camera, objects_and_confidence, object_positions, "X" });
                                    }

                                    list1.Items.Insert(0, item);
                                }

                                ResizeListViews();

                            };
                            Invoke(LabelUpdate);

                            //try to get a better feel how much time this function consumes - Vorlon
                            Log($"...Loaded list in {{yellow}}{SW.ElapsedMilliseconds}ms{{white}}, {list1.Items.Count} lines.");

                        }
                        else
                        {
                            Log($"Error: Could not gain access to history file for {SW.ElapsedMilliseconds}ms - {AppSettings.Settings.HistoryFileName}");

                        }

                    }
                    else
                    {
                        Log("File does not exist yet - cameras/history.csv");
                    }

                }

            }
            catch { }
        }

        //check if a filter applies on given string of history list entry 
        private bool checkListFilters(string cameraname, string success, string objects_and_confidence)
        {
            if (!objects_and_confidence.Contains("person") && cb_filter_person.Checked) { return false; }
            if (!(objects_and_confidence.Contains("car") ||
                  objects_and_confidence.Contains("boat") ||
                  objects_and_confidence.Contains("bicycle") ||
                  objects_and_confidence.Contains("truck") ||
                  objects_and_confidence.Contains("airplane") ||
                  objects_and_confidence.Contains("motorcycle") ||
                  objects_and_confidence.Contains("horse")) && cb_filter_vehicle.Checked) { return false; }
            if (!(objects_and_confidence.Contains("dog") ||
                  objects_and_confidence.Contains("sheep") ||
                  objects_and_confidence.Contains("bird") ||
                  objects_and_confidence.Contains("cow") ||
                  objects_and_confidence.Contains("cat") ||
                  objects_and_confidence.Contains("horse") ||
                  objects_and_confidence.Contains("bear")) && cb_filter_animal.Checked) { return false; }
            if (success != "true" && cb_filter_success.Checked) { return false; } //if filter "only successful detections" is enabled, don't load false alerts
            if (success == "true" && cb_filter_nosuccess.Checked) { return false; } //if filter "only unsuccessful detections" is enabled, don't load true alerts
            if (comboBox_filter_camera.Text != "All Cameras" && cameraname.Trim().ToLower() != comboBox_filter_camera.Text.Trim().ToLower()) { return false; }
            return true;
        }



        //EVENTS

        //EVENT: new image added to input_path -> START AI DETECTION
        async void OnCreatedAsync(object source, FileSystemEventArgs e)
        {
            //string filename = Path.Combine(AppSettings.Settings.input_path, e.FullPath);

            //make sure we are not processing a duplicate file...
            if (detection_dictionary.ContainsKey(e.FullPath.ToLower()))
            {
                Log("Skipping duplicate Created File Event: " + e.FullPath);
            }
            else
            {
                //Note:  Interwebz says ConCurrentQueue.Count may be slow for large number of items but I dont think we have to worry here in most cases
                int qsize = ImageProcessQueue.Count + 1;
                if (qsize > AppSettings.Settings.MaxImageQueueSize)
                {
                    Log("");
                    Log($"Error: Skipping image because queue is greater than '{AppSettings.Settings.MaxImageQueueSize}'. (Adjust 'MaxImageQueueSize' in .JSON file if needed): " + e.FullPath);
                }
                else
                {
                    Log("");
                    Log("Adding new image to queue: " + e.FullPath);
                    detection_dictionary.TryAdd(e.FullPath.ToLower(), e.FullPath);
                    ImageProcessQueue.Enqueue(new ClsImageQueueItem(e.FullPath, qsize));
                    qsizecalc.AddToCalc(qsize);
                }
                UpdateQueueLabel();
            }



        }


        public class ClsImageQueueItem
        {
            public string image_path { get; set; }
            public DateTime TimeAdded { get; set; }
            public long QueueWaitMS { get; set; }
            public long TotalTimeMS { get; set; }
            public long DeepStackTimeMS { get; set; }
            public long FileLockMS { get; set; }
            public long CurQueueSize { get; set; }
            public int ErrCount { get; set; }
            public string ResultMessage { get; set; }
            public ClsImageQueueItem(String FileName, long CurQueueSize)
            {
                this.image_path = FileName;
                this.TimeAdded = DateTime.Now;
                this.CurQueueSize = CurQueueSize;
            }

        }

        private async Task<bool> ProcessImage(ClsImageQueueItem CurImg, string DeepStackURL)
        {

            bool ret = false;
            string filename = Path.GetFileName(CurImg.image_path);

            MethodInvoker LabelUpdate = delegate { label2.Text = $"Accessing {filename}..."; };
            Invoke(LabelUpdate);

            CurImg.QueueWaitMS = (long)(DateTime.Now - CurImg.TimeAdded).TotalMilliseconds;

            try
            {
                using (Global_GUI.CursorWait cw = new Global_GUI.CursorWait(false, false))
                {

                    //Wait up to 30 seconds to gain access to the file that was just created.  This should
                    //prevent the need to retry in the detection routine
                    Stopwatch sw = Stopwatch.StartNew();

                    bool success = await Global.WaitForFileAccessAsync(CurImg.image_path, FileSystemRights.Read, FileShare.Read, 30000, 20);

                    sw.Stop();

                    CurImg.FileLockMS = sw.ElapsedMilliseconds;

                    if (success)
                    {

                        //output "Processing Image" to Overview Tab
                        LabelUpdate = delegate { label2.Text = $"Processing {filename}..."; };
                        Invoke(LabelUpdate);

                        //------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------
                        ret = await DetectObjects(CurImg, DeepStackURL); //ai process image
                        //------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------

                        //output Running on Overview Tab
                        LabelUpdate = delegate { label2.Text = "Running"; };
                        Invoke(LabelUpdate);

                        //only update charts if stats tab is open

                        LabelUpdate = delegate
                        {
                            //Console.WriteLine(tabControl1.SelectedIndex);

                            if (tabControl1.SelectedIndex == 1)
                            {

                                UpdatePieChart(); UpdateTimeline(); UpdateConfidenceChart();
                                //Console.WriteLine("updated");
                            }
                        };
                        Invoke(LabelUpdate);

                    }
                    else
                    {
                        //could not access the file for 30 seconds??   Or unexpected error
                        Log($"Error: Could not gain access to {CurImg.image_path} for {{yellow}}{sw.Elapsed.TotalSeconds}{{red}} seconds, giving up.");
                    }

                }

            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }
            finally
            {
                UpdateQueueLabel();
            }

            return ret;

        }

        public async Task ImageQueueLoop()
        {
            //This runs in another thread, waiting for items to appear in the queue and process them one at a time
            try
            {
                Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;

                ClsImageQueueItem CurImg;
                List<String> DeepStackURLList = new List<string>();

                string LastURLS = AppSettings.Settings.deepstack_url;

                //Start infinite loop waiting for images to come into queue
                while (true)
                {
                    while (!this.ImageProcessQueue.IsEmpty)
                    {
                        
                        //Check to see if we need to get updated URL list
                        if (DeepStackURLList.Count == 0 || LastURLS != AppSettings.Settings.deepstack_url)
                        {
                            DeepStackURLList = Global.Split(AppSettings.Settings.deepstack_url, "|;");
                            
                            //check to see if any need updating with http or path
                            for (int i = 0; i < DeepStackURLList.Count; i++)
                            {
                                if (!DeepStackURLList[i].Contains("://"))
                                    DeepStackURLList[i] = "http://" + DeepStackURLList[i];
                                if (!DeepStackURLList[i].ToLower().Contains("/v1/vision/detection"))
                                    DeepStackURLList[i] = DeepStackURLList[i] + "/v1/vision/detection";
                            }

                            LastURLS = AppSettings.Settings.deepstack_url;

                        }

                        var allRunningTasks = new List<Task>();

                        //build the url thread safe queue since we will be modifying the queue in another thread
                        ConcurrentQueue<string> DSURLQueue = new ConcurrentQueue<string>();
                        foreach (string url in DeepStackURLList)
                        {
                            DSURLQueue.Enqueue(url);
                        }

                        while (!this.ImageProcessQueue.IsEmpty)
                        {
                            while (!DSURLQueue.IsEmpty && !this.ImageProcessQueue.IsEmpty)
                            {
                                //get the next url
                                string url;
                                DSURLQueue.TryDequeue(out url);
                                //get the next image
                                this.ImageProcessQueue.TryDequeue(out CurImg);
                                //add A task to process the image
                                Log($"Adding task #{allRunningTasks.Count + 1} for file '{Path.GetFileName(CurImg.image_path)}' on URL '{url}'");
                                allRunningTasks.Add(Task.Run(async () =>
                                                            {
                                                                bool success = await ProcessImage(CurImg, url);
                                                                //put url back in queue when done
                                                                //asdfasdf
                                                                DSURLQueue.Enqueue(url);
                                                                if (!success)
                                                                {
                                                                    //put back in queue to be processed by another deepstack server
                                                                    CurImg.ErrCount++;
                                                                    if (CurImg.ErrCount <= 4)
                                                                    {
                                                                        Log($"...Putting image back in queue due to URL '{url}' failure: '{CurImg.image_path}'");
                                                                        this.ImageProcessQueue.Enqueue(CurImg);
                                                                    }
                                                                    else
                                                                    {
                                                                        Log($"...Error: Removing image from queue. Tried 4 times on URL '{url}', Image: '{CurImg.image_path}'");
                                                                    }
                                                                }
                                                            })
                                                    );
                            }
                            //wait for ANY task in the list to complete if there are any
                            if (allRunningTasks.Count > 0)
                            {
                                Log($"Waiting for any of {allRunningTasks.Count} tasks to get done...");
                                int taskidx = Task.WaitAny(allRunningTasks.ToArray());
                                Log($"...Task at index {taskidx} done.");
                                //remove task from list
                                allRunningTasks.RemoveAt(taskidx);
                            }
                        }

                        Log("Done processing current image queue.");
                        
                    }

                    //Only loop 10 times a second conserve cpu
                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message);
            }
        }


        //event: image in input_path renamed
        void OnRenamed(object source, RenamedEventArgs e)
        {
            DeleteListItem(e.OldName);
            //CreateListItem(e.Name);
        }

        //event: image in input path deleted
        void OnDeleted(object source, FileSystemEventArgs e)
        {
            DeleteListItem(e.Name);
        }

        void OnError(object sender, ErrorEventArgs e)
        {
            Log("Error: File watcher error: " + e.GetException().Message);
        }
        //event: load selected image to picturebox
        private async void list1_SelectedIndexChanged(object sender, EventArgs e) //Bild ändern
        {

            string filename = "";
            try
            {
                if (list1.SelectedItems.Count > 0)
                {

                    //this now stores the full filename
                    filename = list1.SelectedItems[0].Text;

                    if (!String.IsNullOrEmpty(filename) && filename.Contains("\\") && File.Exists(filename))
                    {
                        using (var img = new Bitmap(filename))
                        {
                            pictureBox1.BackgroundImage = new Bitmap(img); //load actual image as background, so that an overlay can be added as the image
                        }
                        showHideMask();
                        lbl_objects.Text = list1.SelectedItems[0].SubItems[3].Text;
                    }
                    else
                    {
                        lbl_objects.Text = "Image not found";
                        pictureBox1.BackgroundImage = null;
                        //delete entry that caused the issue
                        try
                        {
                            DeleteListItem(filename);
                        }
                        //if deleting fails because the filename could not be retrieved, do a complete clean up
                        catch
                        {
                            CleanCSVList();
                            await LoadFromCSVAsync();
                        }
                    }

                }
                else
                {
                    //lbl_objects.Text = "Nothing selected";
                    //pictureBox1.BackgroundImage = null;
                }
            }
            catch (Exception ex)
            {
                Log($"ERROR: Loading entry from History list failed. This might have happened because obsolete entries weren't correctly deleted. {Global.ExMsg(ex)} )");

            }



        }

        //event: show mask button clicked
        private void cb_showMask_CheckedChanged(object sender, EventArgs e)
        {
            if (list1.SelectedItems.Count > 0)
            {
                showHideMask();
            }
        }

        //event: show objects button clicked
        private void cb_showObjects_MouseUp(object sender, MouseEventArgs e)
        {
            if (list1.SelectedItems.Count > 0)
            {
                pictureBox1.Refresh();
            }
        }

        //event: show history list filters button clicked
        private void cb_showFilters_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_showFilters.Checked)
            {
                cb_showFilters.Text = "˅ Filter";
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                cb_showFilters.Text = "˄ Filter";
                splitContainer1.Panel2Collapsed = true;
            }

            ResizeListViews();

        }

        //event: filter "only revelant alerts" checked or unchecked
        private async void cb_filter_success_CheckedChanged(object sender, EventArgs e)
        {
            await LoadFromCSVAsync();
        }

        //event: filter "only alerts with people" checked or unchecked
        private async void cb_filter_person_CheckedChanged(object sender, EventArgs e)
        {
            await LoadFromCSVAsync();
        }

        //event: filter "only alerts with people" checked or unchecked
        private async void cb_filter_vehicle_CheckedChanged(object sender, EventArgs e)
        {
            await LoadFromCSVAsync();
        }

        //event: filter "only alerts with animals" checked or unchecked
        private async void cb_filter_animal_CheckedChanged(object sender, EventArgs e)
        {
            await LoadFromCSVAsync();
        }

        //event: filter "only false / irrevelant alerts" checked or unchecked
        private async void cb_filter_nosuccess_CheckedChanged(object sender, EventArgs e)
        {
            await LoadFromCSVAsync();
        }

        //event: filter camera dropdown changed
        private async void comboBox_filter_camera_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadFromCSVAsync();
        }

        //----------------------------------------------------------------------------------------------------------
        //CAMERAS TAB
        //----------------------------------------------------------------------------------------------------------

        //BASIC METHODS

        // load cameras to camera list
        public void LoadCameras()
        {

            try
            {

                //start by getting last selected camera if any
                string oldname = "";
                if (list2.SelectedItems != null && list2.SelectedItems.Count > 0)
                    oldname = list2.SelectedItems[0].Text;

                list2.Items.Clear();
                comboBox1.Items.Clear();
                comboBox_filter_camera.Items.Clear();
                comboBox_filter_camera.Items.Add("All Cameras"); //add "all cameras" entry in filter dropdown combobox
                int i = 0;
                int oldidx = 0;
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    //Add loaded camera to list2
                    ListViewItem item = new ListViewItem(new string[] { cam.name });
                    if (!cam.enabled)
                    {
                        item.ForeColor = Color.Gray;
                    }
                    //item.Tag = file; //tag is not used anywhere I can see
                    list2.Items.Add(item);
                    //add camera to combobox on overview tab and to camera filter combobox in the History tab 
                    comboBox1.Items.Add($"   {cam.name}");
                    comboBox_filter_camera.Items.Add($"   {cam.name}");
                    if (oldname.Trim().ToLower() == cam.name.Trim().ToLower())
                    {
                        oldidx = i;
                    }
                    i++;

                }

                //select first camera, or last selected camera
                if (list2.Items.Count > 0 && list2.Items.Count >= oldidx)
                {
                    list2.Items[oldidx].Selected = true;
                }

                ////3. UPDATE LIST2
                ////update list2 entry
                //var item = list2.FindItemWithText(oldname);
                //list2.Items[list2.Items.IndexOf(item)].Text = name;


                //update camera  combobox on overview tab and to camera filter combobox in the History tab 
                //comboBox1.Items[comboBox1.Items.IndexOf($"   {oldname}")] = $"   {name}";
                //comboBox_filter_camera.Items[comboBox_filter_camera.Items.IndexOf($"   {oldname}")] = $"   {name}";
                //string[] files = Directory.GetFiles("./cameras", $"*.txt"); //load all settings files in a string array

                ////create a camera object for every camera settings file
                //int i = 0;
                //foreach (string file in files)
                //{
                //    string result = LoadCamera(file); //do LoadCamera() and save returned result in string
                //    Log(result);

                //    //if LoadCamera() returned an error
                //    if (result.Contains("ERROR"))
                //    {
                //        MessageBox.Show($"Could not load config file {file}: {result}");
                //    }

                //    //Add loaded camera to list2
                //    ListViewItem item = new ListViewItem(new string[] { AppSettings.Settings.CameraList[i].name });
                //    item.Tag = file;
                //    list2.Items.Add(item);
                //    i++;

                //}
            }
            catch
            {
                Log("ERROR LoadCameras() failed.");
                MessageBox.Show("ERROR LoadCameras() failed.");
            }

        }

        //load existing camera (settings file exists) into CameraList, into Stats dropdown and into History filter dropdown 
        //private string LoadCamera(string config_path)
        //{
        //    //check if camera with specified name or its prefix already exists. If yes, then abort.
        //    foreach (Camera c in AppSettings.Settings.CameraList)
        //    {
        //        if (c.name == Path.GetFileNameWithoutExtension(config_path))
        //        {
        //            return ($"ERROR: Camera name must be unique,{Path.GetFileNameWithoutExtension(config_path)} already exists.");
        //        }
        //        if (c.prefix == System.IO.File.ReadAllLines(config_path)[2].Split('"')[1])
        //        {
        //            return ($"ERROR: Every camera must have a unique prefix ('Input file begins with'), but the prefix of {Path.GetFileNameWithoutExtension(config_path)} equals the prefix of the existing camera {c.name} .");
        //        }
        //    }
        //    Camera cam = new Camera(); //create new camera object
        //    Log("read config");
        //    cam.ReadConfig(config_path); //read camera's config from file
        //    Log("add");
        //    AppSettings.Settings.CameraList.Add(cam); //add created camera object to CameraList

        //    //add camera to combobox on overview tab and to camera filter combobox in the History tab 
        //    comboBox1.Items.Add($"   {cam.name}");
        //    comboBox_filter_camera.Items.Add($"   {cam.name}");

        //    return ($"SUCCESS: {Path.GetFileNameWithoutExtension(config_path)} loaded.");
        //}

        //add camera
        private string AddCamera(string name, string prefix, string trigger_urls_as_string, string triggering_objects_as_string, bool telegram_enabled, bool enabled, double cooldown_time, int threshold_lower, int threshold_upper,
                                 string _input_path, bool _input_path_includesubfolders,
                                 bool masking_enabled,
                                 bool trigger_cancels) //, int history_mins, int mask_create_counter, int mask_remove_counter, double percent_variance)
        {
            //check if camera with specified name already exists. If yes, then abort.
            foreach (Camera c in AppSettings.Settings.CameraList)
            {
                if (c.name == name)
                {
                    MessageBox.Show($"ERROR: Camera name must be unique,{name} already exists.");
                    return ($"ERROR: Camera name must be unique,{name} already exists.");
                }
            }

            //check if name is empty
            if (name == "")
            {
                MessageBox.Show($"ERROR: Camera name may not be empty.");
                return ($"ERROR: Camera name may not be empty.");
            }

            Camera cam = new Camera(); //create new camera object

            if (BlueIrisInfo.IsValid && !String.IsNullOrWhiteSpace(BlueIrisInfo.URL))
            {
                //http://10.0.1.99:81/admin?trigger&camera=BACKFOSCAM&user=AITools&pw=haha&memo=[summary]
                trigger_urls_as_string = $"{BlueIrisInfo.URL}/admin?trigger&camera=[camera]&user=ENTERUSERNAMEHERE&pw=ENTERPASSWORDHERE&flagalert=1&memo=[summary]";
            }

            foreach (string pth in BlueIrisInfo.ClipPaths)
            {
                //try to automatically pick the path that starts with AI if not already set
                if ((pth.ToLower().Contains(name.ToLower()) || pth.ToLower().Contains(prefix.ToLower())) && string.IsNullOrWhiteSpace(_input_path))
                {
                    _input_path = pth;
                }
            }

            cam.WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper,
                           _input_path, _input_path_includesubfolders,
                           masking_enabled,
                           trigger_cancels); //, history_mins, mask_create_counter, mask_remove_counter, percent_variance); //set parameters

            AppSettings.Settings.CameraList.Add(cam); //add created camera object to CameraList

            ////add camera to list2
            //ListViewItem item = new ListViewItem(new string[] { name });
            //item.Tag = name;
            //list2.Items.Add(item);

            ////add camera to combobox on overview tab and to camera filter combobox in the History tab 
            //comboBox1.Items.Add($"   {cam.name}");
            //comboBox_filter_camera.Items.Add($"   {cam.name}");

            LoadCameras();

            return ($"SUCCESS: {name} created.");
        }

        //change settings of camera
        private string UpdateCamera(string oldname, string name, string prefix, string trigger_urls_as_string, string triggering_objects_as_string, bool telegram_enabled, bool enabled, double cooldown_time, int threshold_lower, int threshold_upper,
                                    string _input_path, bool _input_path_includesubfolders,
                                    bool masking_enabled,
                                    bool trigger_cancels) //, int history_mins, int mask_create_counter, int mask_remove_counter, Double percent_variance)
        {
            //1. CHECK NEW VALUES 
            //check if name is empty
            if (name == "")
            {
                DisplayCameraSettings(); //reset displayed settings
                return ($"WARNING: Camera name may not be empty.");
            }

            //check if camera with specified name exists. If no, then abort.
            if (!AppSettings.Settings.CameraList.Exists(x => x.name.ToLower() == oldname.ToLower()))
            {
                return ($"WARNING: Camera can't be modified because old name {oldname} wasn't found.");
            }

            // check if the new name isn't taken by another camera already (in case the name was changed)
            if (name.ToLower() != oldname.ToLower() && AppSettings.Settings.CameraList.Exists(x => String.Equals(name, x.name, StringComparison.OrdinalIgnoreCase)))
            {
                DisplayCameraSettings(); //reset displayed settings
                return ($"WARNING: Camera name must be unique, but new camera name {name} already exists.");
            }

            int index = -1;
            index = AppSettings.Settings.CameraList.FindIndex(x => x.name.Trim().ToLower() == oldname.Trim().ToLower()); //index of specified camera in list

            if (index == -1) { Log("ERROR updating camera, could not find original camera profile."); }

            //check if new prefix isn't already taken by another camera
            if (prefix.ToLower() != AppSettings.Settings.CameraList[index].prefix.ToLower() && AppSettings.Settings.CameraList.Exists(x => x.prefix.ToLower() == prefix.ToLower()))
            {
                DisplayCameraSettings(); //reset displayed settings
                return ($"WARNING: Every camera must have a unique prefix ('Input file begins with'), but the prefix of {name} already exists.");
            }

            //2. WRITE CONFIG
            AppSettings.Settings.CameraList[index].WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper,
                                                               _input_path, _input_path_includesubfolders,
                                                               masking_enabled,
                                                               trigger_cancels); //, history_mins, mask_create_counter, mask_remove_counter, percent_variance); //set parameters

            LoadCameras();


            return ($"SUCCESS: Camera {oldname} was updated to {name}.");
        }

        //remove camera
        private void RemoveCamera(string name)
        {
            Log($"Removing camera {name}...");
            if (list2.Items.Count > 0) //if list is empty, nothing can be deleted
            {
                if (AppSettings.Settings.CameraList.Exists(x => x.name.ToLower() == name.ToLower())) //check if camera with specified name exists in list
                {

                    //find index of specified camera in list
                    int index = -1;

                    //check for each camera in the cameralist if its name equals the name of the camera that is selected to be deleted
                    for (int i = 0; i < AppSettings.Settings.CameraList.Count; i++)
                    {
                        if (AppSettings.Settings.CameraList[i].name.ToLower().Equals(name.ToLower()))
                        {
                            index = i;

                        }
                    }

                    if (index != -1) //only delete camera if index is known (!= its default value -1)
                    {
                        //AppSettings.Settings.CameraList[index].Delete(); //delete settings file of specified camera

                        //move all cameras following the specified camera one position forward in the list
                        //the position of the specified camera is overridden with the following camera, the position of the following camera is overridden with its follower, and so on
                        for (int i = index; i < AppSettings.Settings.CameraList.Count - 1; i++)
                        {
                            AppSettings.Settings.CameraList[i] = AppSettings.Settings.CameraList[i + 1];
                        }

                        AppSettings.Settings.CameraList.Remove(AppSettings.Settings.CameraList[AppSettings.Settings.CameraList.Count - 1]); //lastly, remove camera from list

                        //remove list2 entry
                        var item = list2.FindItemWithText(name);
                        list2.Items[list2.Items.IndexOf(item)].Remove();

                        //remove camera from combobox on overview tab and from camera filter combobox in the History tab 
                        comboBox1.Items.Remove($"   {name}");
                        comboBox_filter_camera.Items.Remove($"   {name}");

                        //select first camera
                        if (list2.Items.Count > 0)
                        {
                            list2.Items[0].Selected = true;
                        }

                        //if list2 is empty, clear settings fields (to prevent that values of a deleted camera are shown)
                        if (list2.Items.Count == 0)
                        {
                            tbName.Text = "";
                            tbPrefix.Text = "";
                            cb_enabled.Checked = false;
                            CheckBox[] cbarray = new CheckBox[] { cb_airplane, cb_bear, cb_bicycle, cb_bird, cb_boat, cb_bus, cb_car, cb_cat, cb_cow, cb_dog, cb_horse, cb_motorcycle, cb_person, cb_sheep, cb_truck };
                            foreach (CheckBox c in cbarray)
                            {
                                c.Checked = false;
                            }
                            tbTriggerUrl.Text = "";
                            cb_telegram.Checked = false;
                            //disable camera settings if there are no cameras setup yet
                            tableLayoutPanel6.Enabled = false;
                        }
                    }
                    else
                    {
                        Log("ERROR: Can't find the selected camera, camera wasn't deleted.");
                    }


                }
            }
        }

        //display camera settings for selected camera
        private void DisplayCameraSettings()
        {
            if (list2.SelectedItems.Count > 0)
            {

                tableLayoutPanel6.Enabled = true;

                tbName.Text = list2.SelectedItems[0].Text; //load name textbox from name in list2

                //load remaining settings from Camera.cs object

                //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                Camera cam = Global.GetCamera(tbName.Text);   //int i = AppSettings.Settings.CameraList.FindIndex(x => x.name.Trim().ToLower() == list2.SelectedItems[0].Text.Trim().ToLower());

                //load cameras stats

                string stats = $"Alerts: {cam.stats_alerts.ToString()} | Irrelevant Alerts: {cam.stats_irrelevant_alerts.ToString()} | False Alerts: {cam.stats_false_alerts.ToString()}";

                if (cam.maskManager.masking_enabled)
                {
                    stats += $" | Mask History Count: {cam.maskManager.last_positions_history.Count()} | Current Dynamic Masks: {cam.maskManager.masked_positions.Count()}";
                }
                lbl_camstats.Text = stats;

                //load if ai detection is active for the camera
                if (cam.enabled == true)
                {
                    cb_enabled.Checked = true;
                }
                else
                {
                    cb_enabled.Checked = false;
                }
                tbPrefix.Text = cam.prefix; //load 'input file begins with'
                lbl_prefix.Text = tbPrefix.Text + ".××××××.jpg"; //prefix live preview
                tbTriggerUrl.Text = cam.trigger_urls_as_string; //load trigger url

                cmbcaminput.Text = cam.input_path;
                cmbcaminput.Items.Clear();
                foreach (string pth in BlueIrisInfo.ClipPaths)
                {
                    cmbcaminput.Items.Add(pth);
                    //try to automatically pick the path that starts with AI if not already set
                    if ((pth.ToLower().Contains(tbName.Text.ToLower()) || pth.ToLower().Contains(tbPrefix.Text.ToLower())) && string.IsNullOrWhiteSpace(cmbcaminput.Text))
                    {
                        cmbcaminput.Text = pth;
                    }
                }
                cb_monitorCamInputfolder.Checked = cam.input_path_includesubfolders;
                tb_cooldown.Text = cam.cooldown_time.ToString(); //load cooldown time
                tb_threshold_lower.Text = cam.threshold_lower.ToString(); //load lower threshold value
                tb_threshold_upper.Text = cam.threshold_upper.ToString(); // load upper threshold value

                //load is masking enabled 
                cb_masking_enabled.Checked = cam.maskManager.masking_enabled;

                //load telegram image sending on/off option
                cb_telegram.Checked = cam.telegram_enabled;


                cb_TriggerCancels.Checked = cam.trigger_url_cancels;

                //load triggering objects
                //first create arrays with all checkboxes stored in
                CheckBox[] cbarray = new CheckBox[] { cb_airplane, cb_bear, cb_bicycle, cb_bird, cb_boat, cb_bus, cb_car, cb_cat, cb_cow, cb_dog, cb_horse, cb_motorcycle, cb_person, cb_sheep, cb_truck };
                //create array with strings of the triggering_objects related to the checkboxes in the same order
                string[] cbstringarray = new string[] { "airplane", "bear", "bicycle", "bird", "boat", "bus", "car", "cat", "cow", "dog", "horse", "motorcycle", "person", "sheep", "truck" };

                //clear all checkmarks
                foreach (CheckBox cb in cbarray)
                {
                    cb.Checked = false;
                }

                //check for every triggering_object string if it is active in the settings file. If yes, check according checkbox
                for (int j = 0; j < cbarray.Length; j++)
                {
                    if (cam.triggering_objects_as_string.Contains(cbstringarray[j]))
                    {
                        cbarray[j].Checked = true;
                    }
                }
            }
        }



        // SPECIAL METHODS

        //input file begins with live preview
        private void tbPrefix_TextChanged(object sender, EventArgs e)
        {
            lbl_prefix.Text = tbPrefix.Text + ".××××××.jpg";
        }

        //event: if SPACE is pressed in trigger url field, automatically add a comma
        private void tbTriggerUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                tbTriggerUrl.Text += ","; //add comma
                tbTriggerUrl.Select(tbTriggerUrl.Text.Length, 0); //move cursor to end
            }
        }

        //event: if COMMA is pressed in trigger url field, automatically add a space behind it
        private void tbTriggerUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemcomma)
            {
                tbTriggerUrl.Text += " "; //add space
                tbTriggerUrl.Select(tbTriggerUrl.Text.Length, 0); //move cursor to end
            }
        }

        //event: camera list another item selected
        private void list2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayCameraSettings(); //display new item's settings
        }

        //event: camera add button
        private void btnCameraAdd_Click(object sender, EventArgs e)
        {

            using (var form = new InputForm("Camera Name:", "New Camera", cbitems: BlueIrisInfo.Cameras))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string name = form.text;
                    string camresult = AddCamera(name, name, "", "person", false, true, 0, 0, 100,
                                                 "", false,
                                                 false,
                                                 false);
                    MessageBox.Show(camresult);
                }
            }
        }

        //event: save camera settings button
        private void btnCameraSave_Click_1(object sender, EventArgs e)
        {
            if (list2.Items.Count > 0)
            {
                //1. GET SETTINGS INPUTTED
                //all checkboxes in one array

                //person,   bicycle,   car,   motorcycle,   airplane,
                //bus,   train,   truck,   boat,   traffic light,   fire hydrant,   stop_sign,
                //parking meter,   bench,   bird,   cat,   dog,   horse,   sheep,   cow,   elephant,
                //bear,   zebra, giraffe,   backpack,   umbrella,   handbag,   tie,   suitcase,
                //frisbee,   skis,   snowboard, sports ball,   kite,   baseball bat,   baseball glove,
                //skateboard,   surfboard,   tennis racket, bottle,   wine glass,   cup,   fork,
                //knife,   spoon,   bowl,   banana,   apple,   sandwich,   orange, broccoli,   carrot,
                //hot dog,   pizza,   donot,   cake,   chair,   couch,   potted plant,   bed, dining table,
                //toilet,   tv,   laptop,   mouse,   remote,   keyboard,   cell phone,   microwave,
                //oven,   toaster,   sink,   refrigerator,   book,   clock,   vase,   scissors,   teddy bear,
                //hair dryer, toothbrush.

                CheckBox[] cbarray = new CheckBox[] { cb_airplane, cb_bear, cb_bicycle, cb_bird, cb_boat, cb_bus, cb_car, cb_cat, cb_cow, cb_dog, cb_horse, cb_motorcycle, cb_person, cb_sheep, cb_truck };
                //create array with strings of the triggering_objects related to the checkboxes in the same order
                string[] cbstringarray = new string[] { "airplane", "bear", "bicycle", "bird", "boat", "bus", "car", "cat", "cow", "dog", "horse", "motorcycle", "person", "sheep", "truck" };

                //go through all checkboxes and write all triggering_objects in one string
                string triggering_objects_as_string = "";
                for (int i = 0; i < cbarray.Length; i++)
                {
                    if (cbarray[i].Checked == true)
                    {
                        triggering_objects_as_string += $"{cbstringarray[i].Trim()}, ";
                    }
                }

                //get cooldown time from textbox
                Double.TryParse(tb_cooldown.Text, out double cooldown_time);

                //get lower and upper threshold values from textboxes
                Int32.TryParse(tb_threshold_lower.Text, out int threshold_lower);
                Int32.TryParse(tb_threshold_upper.Text, out int threshold_upper);



                ////2. UPDATE SETTINGS
                //// save new camera settings, display result in MessageBox
                string result = UpdateCamera(list2.SelectedItems[0].Text, tbName.Text, tbPrefix.Text, tbTriggerUrl.Text, triggering_objects_as_string, cb_telegram.Checked, cb_enabled.Checked, cooldown_time, threshold_lower, threshold_upper,
                                             cmbcaminput.Text, cb_monitorCamInputfolder.Checked,
                                             cb_masking_enabled.Checked,
                                             cb_TriggerCancels.Checked); //, history_mins, mask_create_counter, mask_remove_counter, percent_variance);

                AppSettings.Save();

                UpdateWatchers();

                Log(result);

                MessageBox.Show(result, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            DisplayCameraSettings();
        }

        //event: delete camera button
        private void btnCameraDel_Click(object sender, EventArgs e)
        {
            if (list2.Items.Count > 0)
            {
                using (var form = new InputForm($"Delete camera {list2.SelectedItems[0].Text} ?", "Delete Camera?", false))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        //Log("about to del cam");
                        RemoveCamera(list2.SelectedItems[0].Text);
                    }
                }
            }
        }

        //event: DELETE key pressed
        private void list2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (list2.Items.Count > 0)
                {
                    using (var form = new InputForm($"Delete camera {list2.SelectedItems[0].Text} ?", "Delete Camera?", false))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            RemoveCamera(list2.SelectedItems[0].Text);
                        }
                    }
                }
            }
        }

        //event: leaving empty lower confidence limit textbox
        private void tb_threshold_lower_Leave(object sender, EventArgs e)
        {
            if (tb_threshold_lower.Text == "")
            {
                tb_threshold_lower.Text = "0";
            }
        }

        //event: leaving empty upper confidence limit textbox
        private void tb_threshold_upper_Leave(object sender, EventArgs e)
        {
            if (tb_threshold_upper.Text == "")
            {
                tb_threshold_upper.Text = "100";
            }
        }



        //----------------------------------------------------------------------------------------------------------
        //SETTING TAB
        //----------------------------------------------------------------------------------------------------------


        //settings save button
        private void BtnSettingsSave_Click_1(object sender, EventArgs e)
        {
            Log($"Saving settings to {AppSettings.Settings.SettingsFileName}");
            //save inputted settings into App.settings
            AppSettings.Settings.input_path = cmbInput.Text;
            AppSettings.Settings.input_path_includesubfolders = cb_inputpathsubfolders.Checked;
            AppSettings.Settings.deepstack_url = tbDeepstackUrl.Text;
            AppSettings.Settings.telegram_chatids = Global.Split(tb_telegram_chatid.Text, ",", true, true);
            AppSettings.Settings.telegram_token = tb_telegram_token.Text;
            AppSettings.Settings.log_everything = cb_log.Checked;
            AppSettings.Settings.send_errors = cb_send_errors.Checked;
            AppSettings.Settings.startwithwindows = cbStartWithWindows.Checked;

            Global.Startup(AppSettings.Settings.startwithwindows);

            if (AppSettings.Save())
            {
                Log("...Saved.");
            }
            else
            {
                Log("...Not saved.  No changes?");
            }
            //update variables
            //input_path = AppSettings.Settings.input_path;
            //deepstack_url = AppSettings.Settings.deepstack_url;
            //telegram_chatid = AppSettings.Settings.telegram_chatid;
            //telegram_chatids = telegram_chatid.Replace(" ", "").Split(','); //for multiple Telegram chats that receive alert images
            //telegram_token = AppSettings.Settings.telegram_token;
            //log_everything = AppSettings.Settings.log_everything;
            //send_errors = AppSettings.Settings.send_errors;

            //update fswatcher to watch new input folder
            UpdateWatchers();

            //clean history.csv database
            CleanCSVList();

            //LoadList();
        }

        //input path select dialog button
        private void btn_input_path_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                if (!string.IsNullOrEmpty(cmbInput.Text))
                {
                    dialog.InitialDirectory = cmbInput.Text;

                }
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    cmbInput.Text = dialog.FileName;
                }
            }
        }

        //open log button
        private void btn_open_log_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppSettings.Settings.LogFileName))
            {
                System.Diagnostics.Process.Start(AppSettings.Settings.LogFileName);
                lbl_errors.Text = "";
            }
            else
            {
                MessageBox.Show("log missing");
            }

        }

        //ask before closing AI Tool to prevent accidentally closing
        private void Shell_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppSettings.Settings.close_instantly <= 0) //if it's eigther enabled or not set  -1 = not set | 0 = ask for confirmation | 1 = don't ask
            {
                using (var form = new InputForm($"Stop and close AI Tool?", "AI Tool", false))
                {
                    var result = form.ShowDialog();
                    if (AppSettings.Settings.close_instantly == -1)
                    {
                        //if it's the first time, ask if the confirmation dialog should ever appear again
                        using (var form1 = new InputForm($"Confirm closing AI Tool every time?", "AI Tool", false, "NO, Never!", "YES"))
                        {
                            var result1 = form1.ShowDialog();
                            if (result1 == DialogResult.Cancel)
                            {
                                AppSettings.Settings.close_instantly = 0;
                            }
                            else
                            {
                                AppSettings.Settings.close_instantly = 1;
                            }
                        }
                    }

                    e.Cancel = (result == DialogResult.Cancel);
                }
            }

            Global_GUI.SaveWindowState(this);

            AppSettings.Save();  //save settings in any case


        }

        private void Shell_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
        }
        private void SaveDeepStackTab()
        {

            DeepStackServerControl.GetDeepStackRun();

            if (RB_Medium.Checked)
                AppSettings.Settings.deepstack_mode = "Medium";
            if (RB_Low.Checked)
                AppSettings.Settings.deepstack_mode = "Low";
            if (RB_High.Checked)
                AppSettings.Settings.deepstack_mode = "High";

            AppSettings.Settings.deepstack_detectionapienabled = Chk_DetectionAPI.Checked;
            AppSettings.Settings.deepstack_faceapienabled = Chk_FaceAPI.Checked;
            AppSettings.Settings.deepstack_sceneapienabled = Chk_SceneAPI.Checked;
            AppSettings.Settings.deepstack_autostart = Chk_AutoStart.Checked;
            AppSettings.Settings.deepstack_debug = Chk_DSDebug.Checked;
            AppSettings.Settings.deepstack_highpriority = chk_HighPriority.Checked;
            AppSettings.Settings.deepstack_adminkey = Txt_AdminKey.Text.Trim();
            AppSettings.Settings.deepstack_apikey = Txt_APIKey.Text.Trim();
            AppSettings.Settings.deepstack_installfolder = Txt_DeepStackInstallFolder.Text.Trim();
            AppSettings.Settings.deepstack_port = Txt_Port.Text.Trim();


            AppSettings.Save();


            if (DeepStackServerControl.IsInstalled)
            {
                if (DeepStackServerControl.IsStarted)
                {


                    if (DeepStackServerControl.IsActivated)
                    {
                        Lbl_BlueStackRunning.Text = "*RUNNING*";
                        Btn_Start.Enabled = false;
                        Btn_Stop.Enabled = true;
                    }
                    else
                    {
                        Lbl_BlueStackRunning.Text = "*NOT ACTIVATED, RUNNING*";
                        Btn_Start.Enabled = false;
                        Btn_Stop.Enabled = true;
                    }
                }
                else
                {
                    Lbl_BlueStackRunning.Text = "*NOT RUNNING*";
                    Btn_Start.Enabled = true;
                    Btn_Stop.Enabled = false;
                }
            }
            else
            {
                Btn_Start.Enabled = false;
                Btn_Stop.Enabled = false;
                Lbl_BlueStackRunning.Text = "*NOT INSTALLED*";

            }

            DeepStackServerControl.Update(AppSettings.Settings.deepstack_adminkey, AppSettings.Settings.deepstack_apikey, AppSettings.Settings.deepstack_mode, AppSettings.Settings.deepstack_sceneapienabled, AppSettings.Settings.deepstack_faceapienabled, AppSettings.Settings.deepstack_detectionapienabled, AppSettings.Settings.deepstack_port);

        }

        private async void LoadDeepStackTab(bool StartIfNeeded)
        {

            try
            {
                //first update the port in the deepstack_url if found
                //string prt = Global.GetWordBetween(AppSettings.Settings.deepstack_url, ":", " |/");
                //if (!string.IsNullOrEmpty(prt) && (Convert.ToInt32(prt) > 0))
                //{
                //    DeepStackServerControl.Port = prt;
                //}

                //This will OVERRIDE the port if the deepstack processes found running already have a different port, mode, etc:
                DeepStackServerControl.GetDeepStackRun();

                if (DeepStackServerControl.Mode.ToLower() == "medium")
                    RB_Medium.Checked = true;
                if (DeepStackServerControl.Mode.ToLower() == "low")
                    RB_Low.Checked = true;
                if (DeepStackServerControl.Mode.ToLower() == "high")
                    RB_High.Checked = true;

                Chk_DetectionAPI.Checked = DeepStackServerControl.DetectionAPIEnabled;
                Chk_FaceAPI.Checked = DeepStackServerControl.FaceAPIEnabled;
                Chk_SceneAPI.Checked = DeepStackServerControl.SceneAPIEnabled;

                //have seen a few cases nothing is checked but it is required
                if (!Chk_DetectionAPI.Checked && !Chk_FaceAPI.Checked && !Chk_SceneAPI.Checked)
                {
                    Chk_DetectionAPI.Checked = true;
                    DeepStackServerControl.DetectionAPIEnabled = true;
                }

                Chk_AutoStart.Checked = AppSettings.Settings.deepstack_autostart;
                Chk_DSDebug.Checked = AppSettings.Settings.deepstack_debug;
                chk_HighPriority.Checked = AppSettings.Settings.deepstack_highpriority;
                Txt_AdminKey.Text = DeepStackServerControl.AdminKey;
                Txt_APIKey.Text = DeepStackServerControl.APIKey;
                Txt_DeepStackInstallFolder.Text = DeepStackServerControl.DeepStackFolder;
                Txt_Port.Text = DeepStackServerControl.Port;

                //if (prt != Txt_Port.Text)
                //{
                //    //server:port/maybe/more/path
                //    string serv = Global.GetWordBetween(AppSettings.Settings.deepstack_url, "", ":");
                //    if (!string.IsNullOrEmpty(serv))
                //    {
                //        tbDeepstackUrl.Text = serv + ":" + Txt_Port.Text;
                //        //AppSettings.Settings.deepstack_url = serv + ":" + Txt_Port.Text;
                //        //AppSettings.Settings.deepstack_url = tbDeepstackUrl.Text;
                //        //AppSettings.Save();
                //    }
                //}

                if (DeepStackServerControl.IsInstalled)
                {
                    if (DeepStackServerControl.IsStarted && !DeepStackServerControl.HasError)
                    {
                        if (DeepStackServerControl.IsActivated && (DeepStackServerControl.VisionDetectionRunning || DeepStackServerControl.DetectionAPIEnabled))
                        {
                            Lbl_BlueStackRunning.Text = "*RUNNING*";
                            Btn_Start.Enabled = false;
                            Btn_Stop.Enabled = true;
                        }
                        else if (!DeepStackServerControl.IsActivated)
                        {
                            Lbl_BlueStackRunning.Text = "*NOT ACTIVATED, RUNNING*";
                            Btn_Start.Enabled = false;
                            Btn_Stop.Enabled = true;
                        }
                        else if (!DeepStackServerControl.VisionDetectionRunning || DeepStackServerControl.DetectionAPIEnabled)
                        {
                            Lbl_BlueStackRunning.Text = "*DETECTION API NOT RUNNING*";
                            Btn_Start.Enabled = false;
                            Btn_Stop.Enabled = true;
                        }

                    }
                    else if (DeepStackServerControl.HasError)
                    {
                        Lbl_BlueStackRunning.Text = "*ERROR*";
                        Btn_Start.Enabled = false;
                        Btn_Stop.Enabled = true;
                    }
                    else
                    {
                        Lbl_BlueStackRunning.Text = "*NOT RUNNING*";
                        Btn_Start.Enabled = true;
                        Btn_Stop.Enabled = false;
                        if (Chk_AutoStart.Checked && StartIfNeeded)
                        {
                            if (await DeepStackServerControl.StartAsync())
                            {
                                if (DeepStackServerControl.IsStarted && !DeepStackServerControl.HasError)
                                {
                                    Lbl_BlueStackRunning.Text = "*RUNNING*";
                                    Btn_Start.Enabled = false;
                                    Btn_Stop.Enabled = true;
                                }
                                else if (DeepStackServerControl.HasError)
                                {
                                    Lbl_BlueStackRunning.Text = "*ERROR*";
                                    Btn_Start.Enabled = false;
                                    Btn_Stop.Enabled = true;
                                }

                            }
                            else
                            {
                                Lbl_BlueStackRunning.Text = "*ERROR*";
                                Btn_Start.Enabled = false;
                                Btn_Stop.Enabled = true;
                            }
                        }
                    }
                }
                else
                {
                    Btn_Start.Enabled = false;
                    Btn_Stop.Enabled = false;
                    Lbl_BlueStackRunning.Text = "*NOT INSTALLED*";

                }

            }
            catch (Exception ex)
            {

                Log(Global.ExMsg(ex));
            }
        }

        private async void Btn_Start_Click(object sender, EventArgs e)
        {
            Lbl_BlueStackRunning.Text = "STARTING...";
            Btn_Start.Enabled = false;
            Btn_Stop.Enabled = false;
            SaveDeepStackTab();
            await DeepStackServerControl.StartAsync();
            LoadDeepStackTab(true);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            SaveDeepStackTab();
        }

        private async void Btn_Stop_Click(object sender, EventArgs e)
        {
            Lbl_BlueStackRunning.Text = "STOPPING...";
            Btn_Start.Enabled = false;
            Btn_Stop.Enabled = false;
            await DeepStackServerControl.StopAsync();
            LoadDeepStackTab(false);
        }

        private void btnStopscroll_Click(object sender, EventArgs e)
        {
            RTFLogger.AutoScroll = false;
        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppSettings.Settings.LogFileName))
            {
                System.Diagnostics.Process.Start(AppSettings.Settings.LogFileName);
                lbl_errors.Text = "";
            }
            else
            {
                MessageBox.Show("log missing");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppSettings.Settings.LogFileName))
            {
                System.Diagnostics.Process.Start(AppSettings.Settings.LogFileName);
                lbl_errors.Text = "";
            }
            else
            {
                MessageBox.Show("log missing");
            }
        }

        private void Chk_AutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_AutoScroll.Checked)
            {
                RTFLogger.AutoScroll = true;
            }
            else
            {
                RTFLogger.AutoScroll = false;
            }
        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                if (!string.IsNullOrEmpty(cmbcaminput.Text))
                {
                    dialog.InitialDirectory = cmbcaminput.Text;

                }
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    cmbcaminput.Text = dialog.FileName;
                }
            }
        }





        private void BtnDynamicMaskingSettings_Click(object sender, EventArgs e)
        {
            using (Frm_DynamicMasking frm = new Frm_DynamicMasking())
            {

                //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                int i = AppSettings.Settings.CameraList.FindIndex(x => x.name.Trim().ToLower() == list2.SelectedItems[0].Text.Trim().ToLower());

                Camera cam = AppSettings.Settings.CameraList[i];

                //Merge ClassObject's code
                frm.num_history_mins.Value = cam.maskManager.history_save_mins;//load minutes to retain history objects that have yet to become masks
                frm.num_mask_create.Value = cam.maskManager.history_threshold_count; // load mask create counter
                frm.num_mask_remove.Value = cam.maskManager.mask_counter_default; //load mask remove counter
                frm.num_percent_var.Value = (decimal)cam.maskManager.thresholdPercent * 100;



                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ////get masking values from textboxes


                    Int32.TryParse(frm.num_history_mins.Text, out int history_mins);
                    Int32.TryParse(frm.num_mask_create.Text, out int mask_create_counter);
                    Int32.TryParse(frm.num_mask_remove.Text, out int mask_remove_counter);
                    Int32.TryParse(frm.num_percent_var.Text, out int variance);

                    ////convert to percent
                    Double percent_variance = (double)variance / 100;

                    cam.maskManager.history_save_mins = history_mins;
                    cam.maskManager.history_threshold_count = mask_create_counter;
                    cam.maskManager.mask_counter_default = mask_remove_counter;
                    cam.maskManager.thresholdPercent = percent_variance;

                    cam.maskManager.masking_enabled = cb_masking_enabled.Checked;

                    AppSettings.Save();

                }
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {

            using (Frm_DynamicMaskDetails frm = new Frm_DynamicMaskDetails())
            {

                //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                int i = AppSettings.Settings.CameraList.FindIndex(x => x.name.Trim().ToLower() == list2.SelectedItems[0].Text.Trim().ToLower());

                frm.cam = AppSettings.Settings.CameraList[i];

                frm.ShowDialog();
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void QueueLblTmr_Tick(object sender, EventArgs e)
        {

            UpdateQueueLabel();
        }

        private void UpdateQueueLabel()
        {
            MethodInvoker LabelUpdate = delegate { lblQueue.Text = $"Images in queue: {ImageProcessQueue.Count}, Max: {qsizecalc.Max} ({qcalc.Max}ms), Average: {qsizecalc.Average.ToString("#####")} ({qcalc.Average.ToString("#####")}ms queue wait time)"; };
            Invoke(LabelUpdate);


        }

        private void btnCustomMask_Click(object sender, EventArgs e)
        {
            using (Frm_CustomMasking frm = new Frm_CustomMasking())
            {

                //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                int i = AppSettings.Settings.CameraList.FindIndex(x => x.name.Trim().ToLower() == list2.SelectedItems[0].Text.Trim().ToLower());

                frm.cam = AppSettings.Settings.CameraList[i];

                frm.ShowDialog();
            }

        }
    }


    //classes for AI analysis

    class Response
    {

        public bool success { get; set; }
        public Object[] predictions { get; set; }

    }

    class Object
    {

        public string label { get; set; }
        public float confidence { get; set; }
        public int y_min { get; set; }
        public int x_min { get; set; }
        public int y_max { get; set; }
        public int x_max { get; set; }

    }


    //enhanced TableLayoutPanel loads faster
    public partial class DBLayoutPanel:TableLayoutPanel
    {
        public DBLayoutPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.UserPaint, true);
        }

        public DBLayoutPanel(IContainer container)
        {
            container.Add(this);
            SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.UserPaint, true);
        }
    }
}


