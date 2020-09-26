using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.XPath;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Threading;
using SixLabors.ImageSharp;
using System.Collections.ObjectModel;

namespace AITool
{

    public static class AppSettings
    {
        public static ClsSettings Settings = new ClsSettings();
        private static string LastSettingsJSON = "";
        public static bool AlreadyRunning = false;
        private static Object ThreadLock = new Object();
        public class ClsSettings
        {
            [JsonIgnore]
            public string SettingsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location) + ".Settings.JSON");
            [JsonIgnore]
            public string LogFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location) + ".LOG");
            [JsonIgnore]
            public string HistoryFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cameras\\history.csv");
            public string HistoryDBFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location) + ".Database.SQLITE3");

            public string telegram_token = "";
            public double telegram_cooldown_minutes = 0.0833333;  //Default to no more often than 5 seconds.   In minutes (How many minutes must have passed since the last detection. Used to separate event to ensure that every event only causes one telegram message.)
            public int Telegram_RetryAfterFailSeconds = 300;  //default to 5 minutes if telegram exception
            public string input_path = "";
            public bool input_path_includesubfolders = false;
            public List<string> telegram_chatids = new List<string>();
            public bool log_everything = false;
            public bool send_errors = true;
            public bool startwithwindows = false;
            public int close_instantly = -1;
            public List<Camera> CameraList = new List<Camera>();
            public string deepstack_url = "127.0.0.1:81";
            public string deepstack_adminkey = "";
            public string deepstack_apikey = "";
            public string deepstack_installfolder = "C:\\DeepStack";
            public string deepstack_port = "81";
            public string deepstack_mode = "Medium";
            public bool deepstack_urls_are_queued = true;
            public bool deepstack_autostart = false;
            public bool deepstack_debug = false;
            public bool deepstack_highpriority = true;
            public bool deepstack_sceneapienabled = false;
            public bool deepstack_faceapienabled = false;
            public bool deepstack_detectionapienabled = false;
            public int file_access_delay = 50;
            public int retry_delay = 10;
            public bool SettingsValid = false;
            public int MaxLogFileAgeDays = 14;
            public long MaxLogFileSize = ((1024 * 2024) * 5);  //5mb in bytes
            public int MaxImageQueueSize = 100;
            public double MaxImageQueueTimeMinutes = 30;  //Take an image out of the queue if it sits in there over this time
            public int MaxQueueItemRetries = 5;  //will be disabled if fails this many times - Also applies to individual image failures
            public int URLResetAfterDisabledMinutes = 60;  //If any AI/Deepstack URL's have been disabled for over this time, all URLs will be reset to try again
            public int MinSecondsBetweenFailedURLRetry = 30;   //if a URL has failed, dont retry try more often than xx seconds
            public int HTTPClientTimeoutSeconds = 55;    //httpclient.timeout - https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout?view=netcore-3.1
            public int AIDetectionTimeoutSeconds = 60;  //cancelationsource task timeout timeout
            //public int MaxDeepStackProcessTimeSeconds = 120;
            public int RectRelevantColorAlpha = 150;  //255=solid, 127 half transparent
            public int RectIrrelevantColorAlpha = 150;
            public int RectDetectionTextSize = 12;
            public string RectDetectionTextFont = "Segoe UI Semibold";
            public System.Drawing.Color RectRelevantColor = System.Drawing.Color.Red;
            public System.Drawing.Color RectIrrelevantColor = System.Drawing.Color.Silver;
            public string image_copy_folder = "";

            public string mqtt_serverandport = "mqtt:1883";
            public string mqtt_username = "user";
            public bool mqtt_UseTLS = false;
            public string mqtt_password = "password";
            public string mqtt_clientid = "AITool";

            public bool Autoscroll_log = false;

            public string DisplayPercentageFormat = "({0:0.00}%)";
            public string DateFormat = "dd.MM.yy, HH:mm:ss";
            public int TimeBetweenListRefreshsMS = 3000;

        }

        public static bool Save()
        {
            bool Ret = false;
            try
            {
                //multiple threads may be trying to save at the same time
                lock (ThreadLock)
                {
                    if (!Global.IsClassEqual(AppSettings.LastSettingsJSON, AppSettings.Settings))
                    {
                        //keep a backup file in case of corruption
                        if (IsFileValid(AppSettings.Settings.SettingsFileName))
                        {
                            if (File.Exists(AppSettings.Settings.SettingsFileName + ".bak"))
                                File.Delete(AppSettings.Settings.SettingsFileName + ".bak");
                            if (File.Exists(AppSettings.Settings.SettingsFileName))
                                File.Move(AppSettings.Settings.SettingsFileName, AppSettings.Settings.SettingsFileName + ".bak");
                        }
                        else
                        {
                            //file corrupt or doesnt exist
                            if (File.Exists(AppSettings.Settings.SettingsFileName))
                                File.Delete(AppSettings.Settings.SettingsFileName);
                        }

                        //update threshold in all masks if changed during session
                        foreach (Camera cam in AppSettings.Settings.CameraList)
                        {
                            cam.maskManager.Update(cam);
                        }

                        Settings.SettingsValid = true;
                        String CurSettingsJSON = Global.WriteToJsonFile<ClsSettings>(AppSettings.Settings.SettingsFileName, Settings);

                        if (!string.IsNullOrEmpty(CurSettingsJSON) && IsFileValid(AppSettings.Settings.SettingsFileName))
                        {
                            Settings.SettingsValid = true;
                            Ret = true;
                            AppSettings.LastSettingsJSON = CurSettingsJSON;
                            //Global.Log($"Settings saved to {AppSettings.Settings.SettingsFileName}");
                        }
                        else
                        {
                            Settings.SettingsValid = false;
                            Global.Log($"Error: Failed to save Settings to {AppSettings.Settings.SettingsFileName}");
                        }


                    }
                    else
                    {
                        //does not need saving
                        //Global.Log("Settings have not changed, skipping save.");
                        Ret = true;
                        Settings.SettingsValid = true;
                    }

                }

            }
            catch (Exception ex)
            {

                Global.Log("Error: Could not save settings: " + Global.ExMsg(ex));
            }

            if (!Ret)
            {
                try
                {
                    //Revert to backup copy if exists AND is not corrupt
                    if (IsFileValid(AppSettings.Settings.SettingsFileName + ".bak"))
                    {
                        Global.Log("Error: Settings save failed, reverting to backup copy: " + AppSettings.Settings.SettingsFileName + ".bak");

                        if (File.Exists(AppSettings.Settings.SettingsFileName))
                            File.Delete(AppSettings.Settings.SettingsFileName);

                        File.Move(AppSettings.Settings.SettingsFileName + ".bak", AppSettings.Settings.SettingsFileName);
                    }
                    else
                    {
                        Global.Log("Error: Settings save failed, Backup copy is not found or is corrupt: " + AppSettings.Settings.SettingsFileName + ".bak");
                    }

                }
                catch (Exception ex)
                {

                    Global.Log("Error: Could not save settings: " + Global.ExMsg(ex));
                }
            }

            return Ret;
        }
        public static bool IsFileValid(string Filename)
        {
            bool Ret = false;
            try
            {
                if (File.Exists(Filename))
                {
                    Stopwatch SW = Stopwatch.StartNew();
                    FileInfo fi = new FileInfo(Filename);
                    if (fi.Length > 800)
                    {
                        //try to prevent multiple threads from erroring out writing the json file...
                        Task<bool> Success = Global.WaitForFileAccess(Filename, FileSystemRights.Read, FileShare.ReadWrite, 5000);
                        if (Success.Result)
                        {
                            //check its contents, 0 bytes indicate corruption
                            string contents = File.ReadAllText(Filename);
                            if (!contents.Contains("\0"))
                            {
                                if (contents.TrimStart().StartsWith("{") && contents.TrimEnd().EndsWith("}"))
                                {
                                    Ret = true;
                                }
                                else
                                {
                                    Global.Log($"Error: Settings file does not look like JSON: {Filename}");
                                }
                            }
                            else
                            {
                                Global.Log("Error: Settings file contains null bytes, corrupt: " + Filename);
                            }
                        }
                        else
                        {
                            Global.Log($"Error: Could not gain access to file for {SW.ElapsedMilliseconds}ms - {Filename}");
                        }

                    }
                    else
                    {
                        Global.Log($"Error: Settings file is too small at {fi.Length} bytes: {Filename}");
                    }
                }

                else
                {
                    Global.Log("Settings file does not exist yet: " + Filename);
                }
            }
            catch (Exception ex)
            {

                Global.Log($"Error: While validating settings file '{Filename}' got error '{ex.Message}'.");
            }
            return Ret;
        }

        public static bool Load()
        {
            bool Ret = false;
            try
            {

                //multiple threads may be trying to save at the same time
                lock (ThreadLock)
                {
                    Settings.SettingsValid = false;  //assume failure
                    bool Resave = false;

                    //read the old configuration file
                    if (!IsFileValid(AppSettings.Settings.SettingsFileName) && !IsFileValid(AppSettings.Settings.SettingsFileName + ".bak"))
                    {
                        //--------------------------------------------------------------------------------------------------------------------
                        //try to read in OLD aitool.exe.config or user.config files - they were
                        //unreliable because of strict versioning, etc
                        //
                        // NO NEED to add NEW settings to this area
                        //--------------------------------------------------------------------------------------------------------------------

                        List<FileInfo> filist = new List<FileInfo>();
                        FileInfo fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(Assembly.GetEntryAssembly().Location) + ".config"));
                        if (fi.Exists)
                            filist.Add(fi);
                        filist.AddRange(Global.GetFiles(Path.Combine(Environment.GetEnvironmentVariable("LOCALAPPDATA"), "WindowsFormsApp2"), "user.config"));
                        //sort by date
                        filist = filist.OrderByDescending((d) => d.LastWriteTime).ToList();

                        Global.Log("First time load, reading old config file: " + filist[0].FullName);

                        XDocument xmlfile = XDocument.Load(filist[0].FullName);
                        //<configuration>
                        //    <userSettings>
                        //        <WindowsFormsApp2.Properties.Settings>
                        //            <setting name="telegram_token" serializeAs="String">
                        //                <value />
                        //            </setting>
                        //            <setting name="telegram_chatid" serializeAs="String">
                        //                <value />
                        //            </setting>
                        //            <setting name="input_path" serializeAs="String">
                        //                <value>D:\BlueIrisStorage\AIInput</value>
                        //            </setting>

                        IEnumerable<XElement> els = xmlfile.XPathSelectElements("/configuration/userSettings/WindowsFormsApp2.Properties.Settings/setting");
                        if (els == null || els.Count() == 0)
                            els = xmlfile.XPathSelectElements("/configuration/applicationSettings/WindowsFormsApp2.Properties.Settings/setting");

                        int cnt = 0;
                        foreach (XElement el in els)
                        {
                            string val = el.Value;
                            if (!string.IsNullOrEmpty(val))
                            {
                                if (el.ToString().Contains("telegram_token")) { Settings.telegram_token = val; cnt += 1; }
                                if (el.ToString().Contains("telegram_chatids")) { Settings.telegram_chatids = Global.Split(val, ","); cnt += 1; }
                                if (el.ToString().Contains("input_path")) { Settings.input_path = val; cnt += 1; }
                                if (el.ToString().Contains("deepstack_url")) { Settings.deepstack_url = val; cnt += 1; }
                                if (el.ToString().Contains("log_everything")) { Settings.log_everything = Convert.ToBoolean(val); cnt += 1; }
                                if (el.ToString().Contains("send_errors")) { Settings.send_errors = Convert.ToBoolean(val); cnt += 1; }
                                if (el.ToString().Contains("close_instantly")) { Settings.close_instantly = Convert.ToInt32(val); cnt += 1; }

                            }
                        }

                        Resave = (cnt > 0);
                        Settings.SettingsValid = true;

                    }
                    else if (IsFileValid(AppSettings.Settings.SettingsFileName))
                    {
                        //Load regular settings file
                        Global.Log("Loading settings from " + AppSettings.Settings.SettingsFileName);
                        Settings = Global.ReadFromJsonFile<ClsSettings>(AppSettings.Settings.SettingsFileName);
                    }
                    else if (IsFileValid(AppSettings.Settings.SettingsFileName + ".bak"))
                    {
                        //revert to backup if its good
                        Global.Log("Reverting to backup settings file: " + AppSettings.Settings.SettingsFileName + ".bak");
                        Global.Log("Loading settings from " + AppSettings.Settings.SettingsFileName + ".bak");
                        Settings = Global.ReadFromJsonFile<ClsSettings>(AppSettings.Settings.SettingsFileName + ".bak");
                    }

                    else
                    {

                        //nothing valid
                        Global.Log("Settings file AND backup were missing or corrupt.");

                        if (File.Exists(AppSettings.Settings.SettingsFileName))
                            File.Delete(AppSettings.Settings.SettingsFileName);

                        if (File.Exists(AppSettings.Settings.SettingsFileName + ".bak"))
                            File.Delete(AppSettings.Settings.SettingsFileName + ".bak");

                    }

                    if (Settings != null)
                    {
                        //Ive had a case where MaskManager was null/corrupt to double check:
                        foreach (Camera cam in Settings.CameraList)
                        {
                            if (cam.maskManager == null)
                            {
                                cam.maskManager = new MaskManager();
                                Global.Log("Warning: Had to reset MaskManager for camera " + cam.name);
                            }

                            //update threshold in all masks if changed during session
                            cam.maskManager.Update(cam);

                            if (cam.trigger_url_cancels && !string.IsNullOrWhiteSpace(cam.cancel_urls_as_string))
                            {
                                cam.cancel_urls_as_string = cam.trigger_urls_as_string;
                                cam.cancel_urls = Global.Split(cam.cancel_urls_as_string, "\r\n|;,").ToArray();
                            }

                        }

                        //load cameras the old way if needed
                        if (Settings.CameraList.Count == 0)
                        {
                            string camerafolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cameras");
                            Global.Log("No cameras loaded in settings, trying to load old camera files from " + camerafolder);
                            List<FileInfo> files = Global.GetFiles(camerafolder, "*.txt"); //load all settings files in a string array
                                                                                           //Sort so more recent files are processed first - to make sure any dupes that are skipped are older
                                                                                           //I *think* this logic works?
                            files = files.OrderByDescending((d) => d.LastWriteTime).ToList();
                            //create a camera object for every camera settings file
                            int cnt = 0;
                            foreach (FileInfo file in files)
                            {

                                //check if camera with specified name or its prefix already exists. If yes, then abort.
                                bool fnd = false;
                                foreach (Camera c in AppSettings.Settings.CameraList)
                                {
                                    if (c.name.ToLower() == Path.GetFileNameWithoutExtension(file.FullName).ToLower())
                                    {
                                        fnd = true;
                                    }
                                    else if (c.prefix.ToLower() == System.IO.File.ReadAllLines(file.FullName)[2].Split('"')[1].ToLower())
                                    {
                                        fnd = true;
                                    }
                                }
                                if (!fnd)
                                {
                                    cnt++;
                                    Camera cam = new Camera(); //create new camera object
                                    cam.ReadConfig(file.FullName); //read camera's config from file
                                    AppSettings.Settings.CameraList.Add(cam); //add created camera object to CameraList
                                }
                                else
                                {
                                    Global.Log("Skipped duplicate camera: " + file);
                                }

                            }

                            Global.Log($"...Loaded {cnt} camera files.");

                            Resave = (cnt > 1);

                        }

                        //sort the camera list:
                        AppSettings.Settings.CameraList = AppSettings.Settings.CameraList.OrderBy((d) => d.name).ToList();


                        Ret = true;
                    }
                    else
                    {
                        Global.Log("Error: Could not load settings?");
                    }

                    if (Resave)
                    {
                        //we imported old settings, save them
                        Save();
                    }

                }

            }
            catch (Exception ex)
            {

                Global.Log("Error: Could not save settings: " + Global.ExMsg(ex));
            }

            return Ret;
        }

    }


}
