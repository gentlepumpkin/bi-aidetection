using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

using System.IO;
using System.Net.Http;
using System.Net;

using Newtonsoft.Json; //deserialize DeepquestAI response

//for image cutting
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

//for telegram
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

using Microsoft.WindowsAPICodePack.Dialogs; //for file dialog

namespace WindowsFormsApp2
{

    public partial class Shell : Form
    {
        public string input_path = Properties.Settings.Default.input_path; //image input path
        public static string deepstack_url = Properties.Settings.Default.deepstack_url; //deepstack url
        public static bool log_everything = Properties.Settings.Default.log_everything; //save every action sent to Log() into the log file?
        public static bool send_errors = Properties.Settings.Default.send_errors; //send error messages to Telegram?
        public static string telegram_chatid = Properties.Settings.Default.telegram_chatid; //telegram chat id
        public static string[] telegram_chatids = telegram_chatid.Replace(" ", "").Split(','); //for multiple Telegram chats that receive alert images
        public static string telegram_token = Properties.Settings.Default.telegram_token; //telegram bot token
        public int errors = 0; //error counter
        public bool detection_running = false; //is detection running right now or not
        public int file_access_delay = 10; //delay before accessing new file in ms
        public int retry_delay = 10; //delay for first file acess retry - will increase on each retry
        List<Camera> CameraList = new List<Camera>(); //list containing all cameras

        static HttpClient client = new HttpClient();

        FileSystemWatcher watcher = new FileSystemWatcher(); //fswatcher checking the input folder for new images

        public Shell()
        {
            InitializeComponent();

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
            if (!System.IO.File.Exists(@"cameras/history.csv"))
            {
                Log("ATTENTION: Creating database cameras/history.csv .");
                try
                {
                    using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "cameras/history.csv"))
                    {
                        sw.WriteLine("filename|date and time|camera|detections|positions of detections|success");
                    }
                }
                catch
                {
                    lbl_errors.Text = "Can't create cameras/history.csv database!";
                }

            }


            splitContainer1.Panel2Collapsed = true; //collapse filter panel under left list
            comboBox_filter_camera.Items.Add("All Cameras"); //add "all cameras" entry in filter dropdown combobox
            comboBox_filter_camera.SelectedIndex = comboBox_filter_camera.FindStringExact("All Cameras"); //select all cameras entry


            //configure fswatcher to checks input_path for new images, images deleted and renamed images
            try
            {
                watcher.Path = input_path;
                watcher.Filter = "*.jpg";

                //fswatcher events
                watcher.Created += new FileSystemEventHandler(OnCreatedAsync);
                watcher.Renamed += new RenamedEventHandler(OnRenamed);
                watcher.Deleted += new FileSystemEventHandler(OnDeleted);

                //enable fswatcher
                watcher.EnableRaisingEvents = true;
            }
            catch
            {
                if (input_path == "")
                {
                    Log("ATTENTION: No input folder defined.");
                }
                else
                {
                    Log($"ERROR: Can't access input folder '{input_path}'.");
                }

            }


            //this method is slow if the database is large, so it's usually only called on startup. During runtime, DeleteListImage() is used to remove obsolete images from the history list
            CleanCSVList();

            //load entries from history.csv into history ListView
            //LoadFromCSV(); not neccessary because comboBox_filter_camera.SelectedIndex already calls LoadFromCSV()




            //---------------------------------------------------------------------------
            //SETTINGS TAB

            //fill settings tab with stored settings 
            tbInput.Text = input_path;
            tbDeepstackUrl.Text = deepstack_url;
            tb_telegram_chatid.Text = telegram_chatid;
            tb_telegram_token.Text = telegram_token;
            cb_log.Checked = log_everything;
            cb_send_errors.Checked = send_errors;

            //---------------------------------------------------------------------------
            //STATS TAB
            comboBox1.Items.Add("All Cameras"); //add all cameras stats entry
            comboBox1.SelectedIndex = comboBox1.FindStringExact("All Cameras"); //select all cameras entry


            this.Opacity = 1;

            Log("APP START complete.");
        }


//----------------------------------------------------------------------------------------------------------
//CORE
//----------------------------------------------------------------------------------------------------------

        //analyze image with AI
        public async Task DetectObjects(string image_path)
        {
            
            string error = ""; //if code fails at some point, the last text of the error string will be posted in the log
            Log("");
            Log($"Starting analysis of {image_path}");
            
            var fullDeepstackUrl = "";
            //allows both "http://ip:port" and "ip:port"
            if(!deepstack_url.Contains("http://")) //"ip:port"
            {
                fullDeepstackUrl = "http://" + deepstack_url + "/v1/vision/detection";
            }
            else //"http://ip:port"
            {
                fullDeepstackUrl = deepstack_url + "/v1/vision/detection";
            }
            
            var request = new MultipartFormDataContent();
            for (int attempts = 1; attempts < 10; attempts++)  //retry if file is in use by another process.
            {
                try
                {
                    error = "loading image failed";
                    using (var image_data = System.IO.File.OpenRead(image_path))
                    {
                        Log("(1/6) Uploading image to DeepQuestAI Server");
                        error = $"Can't reach DeepQuestAI Server at {fullDeepstackUrl}.";
                        request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                        var output = await client.PostAsync(fullDeepstackUrl, request);
                        Log("(2/6) Waiting for results");
                        var jsonString = await output.Content.ReadAsStringAsync();
                        Response response = JsonConvert.DeserializeObject<Response>(jsonString);

                        Log("(3/6) Processing results:");
                        error = $"Failure in AI Tool processing the image.";

                        //print every detected object with the according confidence-level
                        string outputtext = "   Detected objects:";

                        foreach (var user in response.predictions)
                        {
                            outputtext += $"{user.label.ToString()} ({Math.Round((user.confidence * 100), 2).ToString() }%), ";
                        }
                        Log(outputtext);

                        if (response.success == true)
                        {

                            string fileprefix = Path.GetFileNameWithoutExtension(image_path).Split('.')[0]; //get prefix of inputted file

                            int index = CameraList.FindIndex(x => x.prefix == fileprefix); //get index of camera with same prefix, is =-1 if no camera has the same prefix 

                            //if there is no camera with the same prefix
                            if (index == -1)
                            {
                                Log("   No camera with the same prefix found...");
                                //check if there is a default camera which accepts any prefix, select it
                                if (CameraList.Exists(x => x.prefix == ""))
                                {
                                    index = CameraList.FindIndex(x => x.prefix == "");
                                    Log("(   Found a default camera.");
                                }
                                else
                                {
                                    Log("WARNING: No default camera found. Aborting.");
                                }
                            }

                            //index == -1 now means that no camera has the same prefix and no default camera exists. The alert therefore won't be used. 


                            //if a camera finally is associated with the inputted alert image
                            if (index != -1)
                            {

                                //if camera is enabled
                                if (CameraList[index].enabled == true)
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

                                        Log("(4/6) Checking if detected object is relevant and within confidence limits:");
                                        //add all triggering_objects of the specific camera into a list and the correlating confidence levels into a second list
                                        foreach (var user in response.predictions)
                                        {
                                            Log($"   {user.label.ToString()} ({Math.Round((user.confidence * 100), 2).ToString() }%):");

                                            using (var img = new Bitmap(image_path))
                                            {
                                                bool irrelevant_object = false;

                                                //if object detected is one of the objects that is relevant
                                                if (CameraList[index].triggering_objects_as_string.Contains(user.label))
                                                {
                                                    // -> OBJECT IS RELEVANT

                                                    //if confidence limits are satisfied
                                                    if (user.confidence * 100 >= CameraList[index].threshold_lower && user.confidence * 100 <= CameraList[index].threshold_upper)
                                                    {
                                                        // -> OBJECT IS WITHIN CONFIDENCE LIMITS

                                                        //only if the object is outside of the masked area
                                                        if (Outsidemask(CameraList[index].name, user.x_min, user.x_max, user.y_min, user.y_max, img.Width, img.Height))
                                                        {
                                                            // -> OBJECT IS OUTSIDE OF MASKED AREAS

                                                            objects.Add(user.label);
                                                            objects_confidence.Add(user.confidence);
                                                            string position = $"{user.x_min},{user.y_min},{user.x_max},{user.y_max}";
                                                            objects_position.Add(position);
                                                            Log($"   { user.label.ToString()} ({ Math.Round((user.confidence * 100), 2).ToString() }%) confirmed.");
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
                                                    Log($"   { user.label.ToString()} ({ Math.Round((user.confidence * 100), 2).ToString() }%) is irrelevant.");
                                                }
                                            }

                                        }

                                        //if one or more objects were detected, that are 1. relevant, 2. within confidence limits and 3. outside of masked areas
                                        if (objects.Count() > 0)
                                        {
                                            //store these last detections for the specific camera
                                            CameraList[index].last_detections = objects;
                                            CameraList[index].last_confidences = objects_confidence;
                                            CameraList[index].last_positions = objects_position;

                                            //RELEVANT ALERT
                                            Log("(5/6) Performing alert actions:");
                                            await Trigger(index, image_path); //make TRIGGER
                                            CameraList[index].IncrementAlerts(); //stats update
                                            Log($"(6/6) SUCCESS.");


                                            


                                            //create text string objects and confidences
                                            string objects_and_confidences = "";
                                            string object_positions_as_string = "";
                                            for (int i = 0; i < objects.Count; i++)
                                            {
                                                objects_and_confidences += $"{objects[i]} ({Math.Round((objects_confidence[i] * 100), 0)}%); ";
                                                object_positions_as_string += $"{objects_position[i]};";
                                            }

                                            //add to history list
                                            Log("Adding detection to history list.");
                                            CreateListItem(Path.GetFileName(image_path), DateTime.Now.ToString("dd.MM.yy, HH:mm:ss"), CameraList[index].name, objects_and_confidences, object_positions_as_string);

                                        }
                                        //if no object fulfills all 3 requirements but there are other objects: 
                                        else if (irrelevant_objects.Count() > 0)
                                        {
                                            //IRRELEVANT ALERT


                                            CameraList[index].IncrementIrrelevantAlerts(); //stats update
                                            Log($"(6/6) Camera {CameraList[index].name} caused an irrelevant alert.");
                                            Log("Adding irrelevant detection to history list.");

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

                                            Log($"{text}, so it's an irrelevant alert.");
                                            //add to history list
                                            CreateListItem(Path.GetFileName(image_path), DateTime.Now.ToString("dd.MM.yy, HH:mm:ss"), CameraList[index].name, $"{text} : {objects_and_confidences}", object_positions_as_string);
                                        }
                                    }
                                    //if no object was detected
                                    else if (response.predictions.Length == 0)
                                    {
                                        // FALSE ALERT

                                        CameraList[index].IncrementFalseAlerts(); //stats update
                                        Log($"(6/6) Camera {CameraList[index].name} caused a false alert, nothing detected.");

                                        //add to history list
                                        Log("Adding false to history list.");
                                        CreateListItem(Path.GetFileName(image_path), DateTime.Now.ToString("dd.MM.yy, HH:mm:ss"), CameraList[index].name, "false alert", "");
                                    }
                                }

                                //if camera is disabled.
                                else if (CameraList[index].enabled == false)
                                {
                                    Log("(6/6) Selected camera is disabled.");
                                }

                            }

                        }
                        else if (response.success == false) //if nothing was detected
                        {
                            Log("ERROR: no response from AI Server");
                        }
                    }

                    //load updated camera stats info in camera tab if a camera is selected
                    MethodInvoker LabelUpdate = delegate
                    {
                        if (list2.SelectedItems.Count > 0)
                        {
                            //load only stats from Camera.cs object

                            //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                            int i = CameraList.FindIndex(x => x.name == list2.SelectedItems[0].Text);

                            //load cameras stats
                            string stats = $"Alerts: {CameraList[i].stats_alerts.ToString()} | Irrelevant Alerts: {CameraList[i].stats_irrelevant_alerts.ToString()} | False Alerts: {CameraList[i].stats_false_alerts.ToString()}";
                            lbl_camstats.Text = stats;
                        }


                    };
                    Invoke(LabelUpdate);
                    break; //end retries if code was successful
                }
                catch (Exception ex) 
                {
                    Log($"{ex.GetType().ToString()} | {ex.Message.ToString()} (code: {ex.HResult} )");

                    if (error == "loading image failed") //this was a file exception error - retry file access
                    {
                        if (attempts != 9) //failure at attempt 1-8
                        {
                            Log($"Could not access file - will retry after {attempts * retry_delay} ms delay");
                        }
                        else //last attempt failed
                        {
                            Log($"ERROR: Could not access image '{image_path}'.");
                        }
                    }
                    else //all other exceptions
                    {
                        Log($"ERROR: Processing the following image '{image_path}' failed. {error}");
                        //upload the alert image which could not be analyzed to Telegram
                        if (send_errors == true)
                        {
                            await TelegramUpload(image_path);
                        }
                        break; //end retries - this was not a file access error
                    }
                    
                }
                System.Threading.Thread.Sleep(retry_delay * attempts);
                Log($"Retrying image processing - retry  {attempts}");
            }
            /*
            try
            {
                System.IO.File.Delete(image_path);
            }
            catch
            {
                Console.WriteLine($"ERROR: Could not delete {image_path} .");
            }*/


        }

        //call trigger urls
        public void CallTriggerURLs(string[] trigger_urls)
        {

            var client = new WebClient();
            foreach (string x in trigger_urls)
            {
                try
                {
                    Log($"   trigger url: {x}");
                    var content = client.DownloadString(x);
                }
                catch(Exception ex)
                {
                    Log(ex.Message);
                    Log($"ERROR: Could not trigger URL '{x}', please check if '{x}' is correct and reachable.");
                }
                
            }

            if (trigger_urls.Length > 1)
            {
                Log($"   -> {trigger_urls.Length} trigger URLs called.");
            }
            else
            {
                Log("   -> Trigger URL called.");
            }
        }

        //send image to Telegram
        public async Task TelegramUpload(string image_path)
        {
            if (telegram_chatid != "" && telegram_token != "")
            {
                //telegram upload sometimes fails
                try
                {
                    using (var image_telegram = System.IO.File.OpenRead(image_path))
                    {
                        var bot = new TelegramBotClient(telegram_token);

                        //upload image to Telegram servers and send to first chat
                        Log($"      uploading image to chat \"{telegram_chatids[0]}\"");
                        var message = await bot.SendPhotoAsync(telegram_chatids[0], new InputOnlineFile(image_telegram, "image.jpg"));
                        string file_id = message.Photo[0].FileId; //get file_id of uploaded image

                        //share uploaded image with all remaining telegram chats (if multiple chat_ids given) using file_id 
                        foreach (string chatid in telegram_chatids.Skip(1))
                        {
                            Log($"      uploading image to chat \"{chatid}\"");
                            await bot.SendPhotoAsync(chatid, file_id);
                        }     
                    }
                }
                catch
                {
                    Log($"ERROR: Could not upload image {image_path} to Telegram.");
                    //store image that caused an error in ./errors/
                    if (!Directory.Exists("./errors/")) //if folder does not exist, create the folder
                    {
                        //create folder
                        DirectoryInfo di = Directory.CreateDirectory("./errors");
                        Log("./errors/" + " dir created.");
                    }
                    //save error image
                    using (var image = SixLabors.ImageSharp.Image.Load(image_path))
                    {
                        image.Save("./errors/" + "TELEGRAM-ERROR-" + Path.GetFileName(image_path) + ".jpg");
                    }
                }

            }
        }

        //send text to Telegram
        public async Task TelegramText(string text)
        {
            if (telegram_chatid != "" && telegram_token != "")
            {
                //telegram upload sometimes fails
                try
                {
                    var bot = new Telegram.Bot.TelegramBotClient(telegram_token);
                    foreach (string chatid in telegram_chatids)
                    {
                        await bot.SendTextMessageAsync(chatid, text);
                    }

                }
                catch
                {
                    if(send_errors == true && text.Contains("ERROR") || text.Contains("WARNING")) //if Error message originating from Log() methods can't be uploaded
                    {
                        send_errors = false; //shortly disable send_errors to ensure that the Log() does not try to send the 'Telegram upload failed' message via Telegram again (causing a loop)
                        Log($"ERROR: Could not send text \"{text}\" to Telegram.");
                        send_errors = true;

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
        public async Task Trigger(int index, string image_path)
        {
            //only trigger if cameras cooldown time since last detection has passed
            if ((DateTime.Now - CameraList[index].last_trigger_time).TotalMinutes >= CameraList[index].cooldown_time)
            {
                //call trigger urls
                if (CameraList[index].trigger_urls.Length > 0)
                {
                    //replace url paramters with according values
                    string[] urls = new string[CameraList[index].trigger_urls.Count()];
                    int c = 0;
                    //call urls
                    foreach (string url in CameraList[index].trigger_urls)
                    {
                        urls[c] = url.Replace("[camera]", CameraList[index].name)
                                     .Replace("[detection]", CameraList[index].last_detections.ElementAt(1))
                                     .Replace("[position]", CameraList[index].last_positions.ElementAt(1))
                                     .Replace("[confidence]", CameraList[index].last_confidences.ElementAt(1).ToString())
                                     .Replace("[detections]", string.Join(",", CameraList[index].last_detections))
                                     .Replace("[confidences]", string.Join(",", CameraList[index].last_confidences.ToString()));
                        c++;
                    }

                    CallTriggerURLs(urls);
                }


                //upload to telegram
                if (CameraList[index].telegram_enabled)
                {
                    Log("   Uploading image to Telegram...");
                    await TelegramUpload(image_path);
                    Log("   -> Sent image to Telegram.");
                }
            }
            else
            {
                //log that nothing was done
                Log($"   Camera {CameraList[index].name} is still in cooldown. Trigger URL wasn't called and no image will be uploaded to Telegram.");
            }

            CameraList[index].last_trigger_time = DateTime.Now; //reset cooldown time every time an image contains something, even if no trigger was called (still in cooldown time)

        }



        //check if detected object is outside the mask for the specific camera
        public bool Outsidemask(string cameraname, double xmin, double xmax, double ymin, double ymax, int width, int height)
        {
            Log($"      Checking if object is outside privacy mask of {cameraname}:");
            Log("         Loading mask file...");
            try
            {
                if (System.IO.File.Exists("./cameras/" + cameraname + ".png")) //only check if mask image exists
                {
                    //load mask file (in the image all places that have color (transparency > 9 [0-255 scale]) are masked)
                    using (var mask_img = new Bitmap($"./cameras/{cameraname}.png"))
                    {
                        //if any coordinates of the object are outside of the mask image, th mask image must be too small.
                        if(mask_img.Width != width || mask_img.Height != height)
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
            MethodInvoker LabelUpdate = delegate
            {
                lbl_errors.Show();
                lbl_errors.Text = $"{errors.ToString()} error(s) occured. Click to open Log."; //update error counter label
            };
            Invoke(LabelUpdate);
        }

        //add text to log
        public async void Log(string text)
        {

            //if log everything is disabled and the text is neighter an ERROR, nor a WARNING: write only to console and ABORT
            if (log_everything == false && !text.Contains("ERROR") && !text.Contains("WARNING" ) )
            {
                text += "Enabling \'Log everything\' might give more information.";
                Console.WriteLine($"{text}");
                return;
            }


            //get current date and time

            string time = DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss");

            if (log_everything == true)
            {
                time = DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss.fff");
            }
            

            //if log file does not exist, create it
            if (!System.IO.File.Exists("./log.txt"))
            {
                Console.WriteLine("ATTENTION: Creating log file.");
                try
                {
                    using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt"))
                    {
                        sw.WriteLine("Log format: [dd.MM.yyyy, HH:mm:ss]: Log text.");
                    }
                }
                catch
                {
                    MethodInvoker LabelUpdate = delegate { lbl_errors.Text = "Can't create log.txt file!"; };
                    Invoke(LabelUpdate);
                }
                
            }

            //add text to log
            try
            {
                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", append: true))
                {
                    sw.WriteLine($"[{time}]: {text}");
                }
            }
            catch
            {
                MethodInvoker LabelUpdate = delegate { lbl_errors.Text = "Can't write to log.txt file!"; };
                Invoke(LabelUpdate);
            }

            if(send_errors == true && text.Contains("ERROR") || text.Contains("WARNING"))
            {
                await TelegramText($"[{time}]: {text}"); //upload text to Telegram
            }
            
          

            //add log text to console
            Console.WriteLine($"[{time}]: {text}");

            //increment error counter
            if (text.Contains("ERROR") || text.Contains("WARNING"))
            {
                IncrementErrorCounter();
            }

        }

        //update input path for fswatcher
        public void UpdateFSWatcher()
        {
            try
            {
                watcher.Path = input_path;
            }
            catch
            {
                if (input_path == "")
                {
                    Log("ATTENTION: No input folder defined.");
                }
                else
                {
                    Log($"ERROR: Can't access input folder '{input_path}'.");
                }

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
            if (System.IO.File.Exists("log.txt"))
            {
                System.Diagnostics.Process.Start("log.txt");
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

            list2.Columns[0].Width = list2.Width - 4; //resize camera list column
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
            else if(tabControl1.SelectedIndex == 2)
            {
                //CleanCSVList(); //removed to load the history list faster
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
                foreach (Camera cam in CameraList)
                {
                    alerts += cam.stats_alerts;
                    irrelevantalerts += cam.stats_irrelevant_alerts;
                    falsealerts += cam.stats_false_alerts;
                }
            }
            else
            {
                int i = CameraList.FindIndex(x => x.name == comboBox1.Text.Substring(3));
                alerts = CameraList[i].stats_alerts;
                irrelevantalerts = CameraList[i].stats_irrelevant_alerts;
                falsealerts = CameraList[i].stats_false_alerts;
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
        public void UpdateTimeline()
        {
            Log("Loading time line from cameras/history.csv ...");

            //clear previous values
            timeline.Series[0].Points.Clear();
            timeline.Series[1].Points.Clear();
            timeline.Series[2].Points.Clear();
            timeline.Series[3].Points.Clear();

            List<string> result = new List<string>(); //List that later on will be containing all lines of the csv file

            if (comboBox1.Text == "All Cameras") //all cameras selected
            {
                //load all lines except the first line
                foreach (string line in System.IO.File.ReadAllLines(@"cameras/history.csv").Skip(1))
                {
                    result.Add(line);
                }
            }
            else //camera selection
            {
                string cameraname = comboBox1.Text.Substring(3);

                //load all lines from the history.csv except the first line into List (the first line is the table heading and not an alert entry)
                foreach (string line in System.IO.File.ReadAllLines(@"cameras/history.csv").Skip(1))
                {
                    if(line.Split('|')[2] == cameraname )
                    {
                        result.Add(line);
                    } 
                }
            }

            //every int represents the number of ai calls in successive half hours (p.e. relevant[0] is 0:00-0:30 o'clock, relevant[1] is 0:30-1:00 o'clock) 
            int[] relevant = new int[48];
            int[] irrelevant = new int[48];
            int[] falses = new int[48];
            int[] all = new int[48];

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
                else if (val.Split('|')[3] =="false alert")
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
            foreach(int halfhour in all)
            {
                int index = timeline.Series[0].Points.AddXY(x, halfhour);
                x = x + 0.5;  
            }

            timeline.Series[0].Points.AddXY(24.25, all[0]); // finally add last point with value of first visible point



            //add to graph "relevant":

            timeline.Series[1].Points.AddXY(-0.25, relevant[47]); // beginning point with value of last visible point
            //and now add all visible points 
            x = 0.25;
            foreach (int halfhour in relevant)
            {
                int index = timeline.Series[1].Points.AddXY(x, halfhour);
                x = x + 0.5;
            }
            timeline.Series[1].Points.AddXY(24.25, relevant[0]); // finally add last point with value of first visible point


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


            //add to graph "falses":

            timeline.Series[3].Points.AddXY(-0.25, falses[47]); // beginning point with value of last visible point
            //and now add all visible points 
            x = 0.25;
            foreach (int halfhour in falses)
            {
                int index = timeline.Series[3].Points.AddXY(x, halfhour);
                x = x + 0.5;
            }
            timeline.Series[3].Points.AddXY(24.25, falses[0]); // finally add last point with value of first visible point

        }

        //update confidence_frequency chart
        public void UpdateConfidenceChart()
        {
            Log("Loading confidence-frequency chart from cameras/history.csv ...");

            //clear previous values
            chart_confidence.Series[0].Points.Clear();
            chart_confidence.Series[1].Points.Clear();

            List<string> result = new List<string>(); //List that later on will be containing all lines of the csv file

            if (comboBox1.Text == "All Cameras") //all cameras selected
            {
                //load all lines except the first line
                foreach (string line in System.IO.File.ReadAllLines(@"cameras/history.csv").Skip(1))
                {
                    result.Add(line);
                }
            }
            else //camera selection
            {
                string cameraname = comboBox1.Text.Substring(3);

                //load all lines from the history.csv except the first line into List (the first line is the table heading and not an alert entry)
                foreach (string line in System.IO.File.ReadAllLines(@"cameras/history.csv").Skip(1))
                {
                    if (line.Split('|')[2] == cameraname)
                    {
                        result.Add(line);
                        
                    }
                }
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
            if(cb_showMask.Checked == true) //show overlay
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
        private void showObject(PaintEventArgs e, Color color, int _xmin, int _ymin, int _xmax, int _ymax)
        {
            if (list1.SelectedItems.Count > 0)
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

                //3. paint rectangle
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin, ymin, xmax-xmin, ymax-ymin);
                using (Pen pen = new Pen(color, 2))
                {
                    e.Graphics.DrawRectangle(pen, rect); //draw rectangle
                }
            }
        }

        //load object rectangle overlays
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (cb_showObjects.Checked && list1.SelectedItems.Count > 0) //if checkbox button is enabled
            {
                Log("Loading object rectangles...");
                int countr = list1.SelectedItems[0].SubItems[4].Text.Split(';').Count();

                Color color = new Color();
                string detections = list1.SelectedItems[0].SubItems[3].Text;
                if (detections.Contains("irrelevant") || detections.Contains("masked") || detections.Contains("confidence"))
                {
                    color = Color.Silver;
                }
                else
                {
                    color = Color.Red;
                }

                //display a rectangle around each relevant object
                for(int i = 0; i< countr-1; i++)
                {
                    //load 'xmin,ymin,xmax,ymax' from third column into a string
                    string position = list1.SelectedItems[0].SubItems[4].Text.Split(';')[i];

                    //store xmin, ymin, xmax, ymax in separate variables
                    Int32.TryParse(position.Split(',')[0], out int xmin);
                    Int32.TryParse(position.Split(',')[1], out int ymin);
                    Int32.TryParse(position.Split(',')[2], out int xmax);
                    Int32.TryParse(position.Split(',')[3], out int ymax);

                    Log($"{i} - {xmin}, {ymin}, {xmax},  {ymax}");

                    showObject(e, color, xmin, ymin, xmax, ymax); //call rectangle drawing method

                    Log("Done.");
                }
            }
        }

        // add new entry in left list
        public void CreateListItem( string filename, string date, string camera, string objects_and_confidence, string object_positions )
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
                    }
                    else
                    {
                        item = new ListViewItem(new string[] { filename, date, camera, objects_and_confidence, object_positions, "X" });
                    }

                    list1.Items.Insert(0, item);

                    ResizeListViews();
                }
                


                //update history CSV
                string line = $"{filename}|{date}|{camera}|{objects_and_confidence}|{object_positions}|{success}";
                try
                {
                    using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "cameras/history.csv", append: true))
                    {
                        sw.WriteLine(line);
                    }
                }
                catch { }
            };
            Invoke(LabelUpdate);

            
        }

        //remove entry from left list
        public void DeleteListItem( string filename )
        {
            Log($"Removing alert image {filename} from history list and from cameras/history.csv ...");
            MethodInvoker LabelUpdate = delegate
            {
                ListViewItem listviewitem = new ListViewItem();
                for (int i = 0; i < list1.Items.Count; i++)
                {
                    listviewitem = list1.Items[i];
                    if (filename == listviewitem.Text)
                    {
                        list1.Items.Remove(listviewitem);
                        break;
                    }
                }
                ResizeListViews();

                //remove entry from history csv
                try
                {
                    var oldLines = System.IO.File.ReadAllLines(@"cameras/history.csv");
                    var newLines = oldLines.Where(line => !line.Split('|')[0].Contains(filename));
                    System.IO.File.WriteAllLines(@"cameras/history.csv", newLines);
                }
                catch
                {
                    Log("ERROR: Can't write to cameras/history.csv!");
                }

            };

            Invoke(LabelUpdate);
        }

        //remove all obsolete entries (associated image does not exist anymore) from the history.csv 
        public void CleanCSVList()
        {
            Log($"Cleaning cameras/history.csv if neccessary...");
            MethodInvoker LabelUpdate = delegate
            {
                try
                {
                    string[] oldLines = System.IO.File.ReadAllLines(@"cameras/history.csv"); //old history.csv
                    List<string> newLines = new List<string>(); //new history.csv
                    newLines.Add(oldLines[0]); // add title line from old to new history.csv

                    foreach (string line in oldLines.Skip(1)) //check for every line except title line if associated image still exists in input folder 
                    {
                        if(System.IO.File.Exists(input_path + "/" + line.Split('|')[0]) && input_path != "")
                        {
                            newLines.Add(line);
                        }
                    }
                    System.IO.File.WriteAllLines(@"cameras/history.csv", newLines); //write new history.csv
                }
                catch
                {
                    Log("ERROR: Can't clean the cameras/history.csv!");
                }

            };
            Invoke(LabelUpdate);
        }

        //load stored entries in history CSV into history ListView
        private void LoadFromCSV()
        {
            try
            {
                Log("Loading history list from cameras/history.csv ...");

                //delete obsolete entries from history.csv
                //CleanCSVList(); //removed to load the history list faster

                List<string> result = new List<string>(); //List that later on will be containing all lines of the csv file

                //load all lines except the first line into List (the first line is the table heading and not an alert entry)
                foreach (string line in System.IO.File.ReadAllLines(@"cameras/history.csv").Skip(1))
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
            if (comboBox_filter_camera.Text != "All Cameras" && cameraname != comboBox_filter_camera.Text.Substring(3)) { return false; }
            return true;
        }



        //EVENTS

        //EVENT: new image added to input_path -> START AI DETECTION
        async void OnCreatedAsync(object source, FileSystemEventArgs e)
        {
            System.Threading.Thread.Sleep(file_access_delay); //shorty wait to ensure that the whole image is saved correctly

            while (detection_running == true) { } //wait until other detection process is finished
            detection_running = true; //set marker variable to show that a new detection process is running

            //output "Processing Image" to Overview Tab
            MethodInvoker LabelUpdate = delegate{ label2.Text = $"Processing Image..."; };
            Invoke(LabelUpdate);

            
            await DetectObjects(input_path + "/" + e.Name); //ai process image
            
            //output Running on Overview Tab
            LabelUpdate = delegate { label2.Text = "Running"; };
            Invoke(LabelUpdate);

            //only update charts if stats tab is open

            LabelUpdate = delegate {
                Console.WriteLine(tabControl1.SelectedIndex);

                if (tabControl1.SelectedIndex == 1)
                {
                    
                    UpdatePieChart(); UpdateTimeline(); UpdateConfidenceChart();
                    Console.WriteLine("updated");
                }
            };
            Invoke(LabelUpdate);



            detection_running = false; //reset variable

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

        //event: load selected image to picturebox
        private void list1_SelectedIndexChanged(object sender, EventArgs e) //Bild ändern
        {
            if (list1.SelectedItems.Count > 0)
            {
                using (var img = new Bitmap(input_path + "/" + list1.SelectedItems[0].Text))
                {
                    pictureBox1.BackgroundImage = new Bitmap(img); //load actual image as background, so that an overlay can be added as the image
                }
                showHideMask();
                lbl_objects.Text = list1.SelectedItems[0].SubItems[3].Text;
            }

        }

        //event: show mask button clicked
        private void cb_showMask_CheckedChanged_1(object sender, EventArgs e)
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
        private void cb_filter_success_CheckedChanged(object sender, EventArgs e)
        {
            LoadFromCSV();
        }
        
        //event: filter "only alerts with people" checked or unchecked
        private void cb_filter_person_CheckedChanged(object sender, EventArgs e)
        {
            LoadFromCSV();
        }
        
        //event: filter "only alerts with people" checked or unchecked
        private void cb_filter_vehicle_CheckedChanged(object sender, EventArgs e)
        {
            LoadFromCSV();
        }
        
        //event: filter "only alerts with animals" checked or unchecked
        private void cb_filter_animal_CheckedChanged(object sender, EventArgs e)
        {
            LoadFromCSV();
        }

        //event: filter "only false / irrevelant alerts" checked or unchecked
        private void cb_filter_nosuccess_CheckedChanged(object sender, EventArgs e)
        {
            LoadFromCSV();
        }

        //event: filter camera dropdown changed
        private void comboBox_filter_camera_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFromCSV();
        }

//----------------------------------------------------------------------------------------------------------
//CAMERAS TAB
//----------------------------------------------------------------------------------------------------------

        //BASIC METHODS

        // load cameras to camera list
        public void LoadCameras()
        {
            list2.Items.Clear();

            try
            {
                string[] files = Directory.GetFiles("./cameras", $"*.txt"); //load all settings files in a string array

                //create a camera object for every camera settings file
                int i = 0;
                foreach (string file in files)
                {
                    string result = LoadCamera(file); //do LoadCamera() and save returned result in string
                    Log(result);

                    //if LoadCamera() returned an error
                    if (result.Contains("ERROR"))
                    {
                        MessageBox.Show($"Could not load config file {file}: {result}");
                    }

                    //Add loaded camera to list2
                    ListViewItem item = new ListViewItem(new string[] { CameraList[i].name });
                    item.Tag = file;
                    list2.Items.Add(item);
                    i++;
                    
                }
            }
            catch
            {
                Log("ERROR LoadCameras() failed.");
                MessageBox.Show("ERROR LoadCameras() failed.");
            }

            //select first camera
            if (list2.Items.Count > 0)
            {
                list2.Items[0].Selected = true; 
            }
        }

        //load existing camera (settings file exists) into CameraList, into Stats dropdown and into History filter dropdown 
        private string LoadCamera(string config_path)
        {
            //check if camera with specified name or its prefix already exists. If yes, then abort.
            foreach (Camera c in CameraList)
            {
                if (c.name == Path.GetFileNameWithoutExtension(config_path))
                {
                    return ($"ERROR: Camera name must be unique,{Path.GetFileNameWithoutExtension(config_path)} already exists.");
                }
                if (c.prefix == System.IO.File.ReadAllLines(config_path)[2].Split('"')[1])
                {
                    return ($"ERROR: Every camera must have a unique prefix ('Input file begins with'), but the prefix of {Path.GetFileNameWithoutExtension(config_path)} equals the prefix of the existing camera {c.name} .");
                }
            }
            Camera cam = new Camera(); //create new camera object
            Log("read config");
            cam.ReadConfig(config_path); //read camera's config from file
            Log("add");
            CameraList.Add(cam); //add created camera object to CameraList

            //add camera to combobox on overview tab and to camera filter combobox in the History tab 
            comboBox1.Items.Add($"   {cam.name}"); 
            comboBox_filter_camera.Items.Add($"   {cam.name}"); 

            return ($"SUCCESS: {Path.GetFileNameWithoutExtension(config_path)} loaded.");
        }

        //add camera
        private string AddCamera(string name, string prefix, string trigger_urls_as_string, string triggering_objects_as_string, bool telegram_enabled, bool enabled, double cooldown_time, int threshold_lower, int threshold_upper)
        {
            //check if camera with specified name already exists. If yes, then abort.
            foreach (Camera c in CameraList)
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
            cam.WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper); //set parameters
            CameraList.Add(cam); //add created camera object to CameraList

            //add camera to list2
            ListViewItem item = new ListViewItem(new string[] { name });
            item.Tag = name;
            list2.Items.Add(item);

            //add camera to combobox on overview tab and to camera filter combobox in the History tab 
            comboBox1.Items.Add($"   {cam.name}");
            comboBox_filter_camera.Items.Add($"   {cam.name}");

            //select first camera
            if (list2.Items.Count == 1)
            {
                list2.Items[0].Selected = true;
            }

            return ($"SUCCESS: {name} created.");
        }

        //change settings of camera
        private string UpdateCamera(string oldname, string name, string prefix, string trigger_urls_as_string, string triggering_objects_as_string, bool telegram_enabled, bool enabled, double cooldown_time, int threshold_lower, int threshold_upper)
        {
        //1. CHECK NEW VALUES 
            //check if name is empty
            if (name == "")
            {
                DisplayCameraSettings(); //reset displayed settings
                return ($"WARNING: Camera name may not be empty.");
            }

            //check if camera with specified name exists. If no, then abort.
            if (!CameraList.Exists(x => x.name == oldname))
            {
                return ($"WARNING: Camera can't be modified because old name {oldname} wasn't found.");
            }

            // check if the new name isn't taken by another camera already (in case the name was changed)
            if (name != oldname && CameraList.Exists(x => String.Equals(name, x.name, StringComparison.OrdinalIgnoreCase)))
            {
                DisplayCameraSettings(); //reset displayed settings
                return ($"WARNING: Camera name must be unique, but new camera name {name} already exists.");
            }

            int index = -1; 
            index = CameraList.FindIndex(x => x.name == oldname); //index of specified camera in list

            if(index == -1) { Log("ERROR updating camera, could not find original camera profile."); }

            //check if new prefix isn't already taken by another camera
            if (prefix != CameraList[index].prefix  &&  CameraList.Exists(x => x.prefix == prefix ))
            {
                DisplayCameraSettings(); //reset displayed settings
                return ($"WARNING: Every camera must have a unique prefix ('Input file begins with'), but the prefix of {name} already exists.");
            }

        //2. WRITE CONFIG
            CameraList[index].WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper); //set parameters
        
        //3. UPDATE LIST2
            //update list2 entry
            var item = list2.FindItemWithText(oldname);
            list2.Items[list2.Items.IndexOf(item)].Text = name;


            //update camera  combobox on overview tab and to camera filter combobox in the History tab 
            comboBox1.Items[comboBox1.Items.IndexOf($"   {oldname}")] = $"   {name}";
            comboBox_filter_camera.Items[comboBox_filter_camera.Items.IndexOf($"   {oldname}")] = $"   {name}";


            return ($"SUCCESS: Camera {oldname} was updated to {name}.");
        }

        //remove camera
        private void RemoveCamera(string name)
        {
            Log($"Removing camera {name}...");
            if (list2.Items.Count > 0) //if list is empty, nothing can be deleted
            {
                if (CameraList.Exists(x => x.name == name)) //check if camera with specified name exists in list
                {

                    //find index of specified camera in list
                    int index = -1;

                    //check for each camera in the cameralist if its name equals the name of the camera that is selected to be deleted
                    for(int i = 0; i < CameraList.Count; i++)
                    {
                        if (CameraList[i].name.Equals(name))
                        {
                            index = i;

                        }
                    }

                    if(index != -1) //only delete camera if index is known (!= its default value -1)
                    {
                        CameraList[index].Delete(); //delete settings file of specified camera

                        //move all cameras following the specified camera one position forward in the list
                        //the position of the specified camera is overridden with the following camera, the position of the following camera is overridden with its follower, and so on
                        for (int i = index; i < CameraList.Count - 1; i++)
                        {
                            CameraList[i] = CameraList[i + 1];
                        }

                        CameraList.Remove(CameraList[CameraList.Count - 1]); //lastly, remove camera from list

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

                tbName.Text = list2.SelectedItems[0].Text; //load name textbox from name in list2

                //load remaining settings from Camera.cs object

                //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                int i = CameraList.FindIndex(x => x.name == list2.SelectedItems[0].Text);

                //load cameras stats

                string stats = $"Alerts: {CameraList[i].stats_alerts.ToString()} | Irrelevant Alerts: {CameraList[i].stats_irrelevant_alerts.ToString()} | False Alerts: {CameraList[i].stats_false_alerts.ToString()}";
                lbl_camstats.Text = stats;

                //load if ai detection is active for the camera
                if (CameraList[i].enabled == true)
                {
                    cb_enabled.Checked = true;
                }
                else
                {
                    cb_enabled.Checked = false;
                }
                tbPrefix.Text = CameraList[i].prefix; //load 'input file begins with'
                lbl_prefix.Text = tbPrefix.Text + ".××××××.jpg"; //prefix live preview
                tbTriggerUrl.Text = CameraList[i].trigger_urls_as_string; //load trigger url
                tb_cooldown.Text = CameraList[i].cooldown_time.ToString(); //load cooldown time
                tb_threshold_lower.Text = CameraList[i].threshold_lower.ToString(); //load lower threshold value
                tb_threshold_upper.Text = CameraList[i].threshold_upper.ToString(); // load upper threshold value

                //load telegram image sending on/off option
                if (CameraList[i].telegram_enabled)
                {
                    cb_telegram.Checked = true;
                }
                else
                {
                    cb_telegram.Checked = false;
                }


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
                    if (CameraList[i].triggering_objects_as_string.Contains(cbstringarray[j]))
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
            if(e.KeyCode == Keys.Space)
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

            using (var form = new InputForm("Camera Name:", "New Camera", true))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string name = form.text;
                    AddCamera(name, name, "", "person", false, true, 0, 0, 100);
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
                CheckBox[] cbarray = new CheckBox[] { cb_airplane, cb_bear, cb_bicycle, cb_bird, cb_boat, cb_bus, cb_car, cb_cat, cb_cow, cb_dog, cb_horse, cb_motorcycle, cb_person, cb_sheep, cb_truck };
                //create array with strings of the triggering_objects related to the checkboxes in the same order
                string[] cbstringarray = new string[] { "airplane", "bear", "bicycle", "bird", "boat", "bus", "car", "cat", "cow", "dog", "horse", "motorcycle", "person", "sheep", "truck" };

                //go through all checkboxes and write all triggering_objects in one string
                string triggering_objects_as_string = "";
                for (int i = 0; i < cbarray.Length; i++)
                {
                    if (cbarray[i].Checked == true)
                    {
                        triggering_objects_as_string += $"{cbstringarray[i]}, ";
                    }
                }

                //get cooldown time from textbox
                Double.TryParse(tb_cooldown.Text, out double cooldown_time);

                //get lower and upper threshold values from textboxes
                Int32.TryParse(tb_threshold_lower.Text, out int threshold_lower);
                Int32.TryParse(tb_threshold_upper.Text, out int threshold_upper);


                //2. UPDATE SETTINGS
                // save new camera settings, display result in MessageBox
                string result = UpdateCamera(list2.SelectedItems[0].Text, tbName.Text, tbPrefix.Text, tbTriggerUrl.Text, triggering_objects_as_string, cb_telegram.Checked, cb_enabled.Checked, cooldown_time, threshold_lower, threshold_upper);

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
                        Log("about to del cam");
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
            //save inputted settings into App.settings
            Properties.Settings.Default.input_path = tbInput.Text;
            Properties.Settings.Default.deepstack_url = tbDeepstackUrl.Text;
            Properties.Settings.Default.telegram_chatid = tb_telegram_chatid.Text;
            Properties.Settings.Default.telegram_token = tb_telegram_token.Text;
            Properties.Settings.Default.log_everything = cb_log.Checked;
            Properties.Settings.Default.send_errors = cb_send_errors.Checked;
            Properties.Settings.Default.Save();

            //update variables
            input_path = Properties.Settings.Default.input_path;
            deepstack_url = Properties.Settings.Default.deepstack_url;
            telegram_chatid = Properties.Settings.Default.telegram_chatid;
            telegram_chatids = telegram_chatid.Replace(" ", "").Split(','); //for multiple Telegram chats that receive alert images
            telegram_token = Properties.Settings.Default.telegram_token;
            log_everything = Properties.Settings.Default.log_everything;
            send_errors = Properties.Settings.Default.send_errors;

            //update fswatcher to watch new input folder
            UpdateFSWatcher();

            //clean history.csv database
            CleanCSVList();

        //LoadList();
        }

        //input path select dialog button
        private void btn_input_path_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.InitialDirectory = "C:\\";
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    tbInput.Text = dialog.FileName;
                }
            }
        }

        private void btn_open_log_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("log.txt"))
            {
                System.Diagnostics.Process.Start("log.txt");
                lbl_errors.Text = "";
            }
            else
            {
                MessageBox.Show("log missing");
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

}
