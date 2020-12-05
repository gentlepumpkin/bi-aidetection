﻿using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using OSVersionExtension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
using Rectangle = System.Drawing.Rectangle;

namespace AITool
{
    public static class AITOOL
    {
        // =============================================================
        // ALL FUNCTIONS HERE THAT MAY EVENTUALLY BE USED IN A SERVICE
        // NO direct UI interaction
        // =============================================================

        public static DeepStack DeepStackServerControl = null;
        //public static RichTextBoxEx RTFLogger = null;
        //public static LogFileWriter LogWriter = null;
        //public static LogFileWriter HistoryWriter = null;

        public static BlueIris BlueIrisInfo = null;
        //public static List<ClsURLItem> DeepStackURLList = new List<ClsURLItem>();

        //keep track of timing
        //moving average will be faster for long running process with 1000's of samples
        public static MovingCalcs tcalc = new MovingCalcs(250, "Images", true);
        public static MovingCalcs fcalc = new MovingCalcs(250, "Images", true);
        public static MovingCalcs qcalc = new MovingCalcs(250, "Images", true);
        public static MovingCalcs qsizecalc = new MovingCalcs(250, "Queue Size", false);

        //public static ClsLogManager errors = new ClsLogManager();

        public static ClsLogManager LogMan = null;

        public static ConcurrentQueue<ClsImageQueueItem> ImageProcessQueue = new ConcurrentQueue<ClsImageQueueItem>();

        public static ConcurrentQueue<ClsLogItm> TmpHistQueue = new ConcurrentQueue<ClsLogItm>();  //For before the logger gets fully initialized

        //The sqlite db connection
        public static SQLiteHistory HistoryDB = null;
        public static ClsTriggerActionQueue TriggerActionQueue = null;

        public static object FileWatcherLockObject = new object();
        public static object ImageLoopLockObject = new object();

        //thread safe dictionary to prevent more than one file being processed at one time
        public static ConcurrentDictionary<string, ClsImageQueueItem> detection_dictionary = new ConcurrentDictionary<string, ClsImageQueueItem>();
                

        public static Dictionary<string, ClsFileSystemWatcher> watchers = new Dictionary<string, ClsFileSystemWatcher>();
        //public static ThreadSafe.Boolean AIURLSettingsChanged = new ThreadSafe.Boolean(true);


        public static ThreadSafe.Boolean IsClosing = new ThreadSafe.Boolean(false);
        public static ThreadSafe.Boolean IsLoading = new ThreadSafe.Boolean(true);
        public static ThreadSafe.Datetime LastImageBackupTime = new ThreadSafe.Datetime(DateTime.MinValue);

        public static CancellationTokenSource MasterCTS = new CancellationTokenSource();

        public static System.Timers.Timer FileSystemErrorCheckTimer = new System.Timers.Timer();


        public static string srv = "";

        public static async Task InitializeBackend()
        {

            try
            {
                using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

                Global.JSONContractResolver = new DefaultContractResolver();
                Global.JSONContractResolver.NamingStrategy = new CamelCaseNamingStrategy();

                //initialize log manager with basic settings so we can start getting output if needed
                if (Global.IsService)
                    srv = ".SERVICE.";
                else
                    srv = ".";

                string exe = $"AITOOLS{srv}EXE";

                //initialize logging as early as we can...  Write to the temp folder since we dont know the log location yet
                int TempDefSize = ((1024 * 1024) * 20); //20mb
                LogMan = new ClsLogManager(!Global.IsService, exe);

                await LogMan.UpdateNLog(LogLevel.Debug, Path.Combine(Environment.GetEnvironmentVariable("TEMP"), Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location) + $"{srv}LOG"), TempDefSize, 120, AppSettings.Settings.MaxGUILogItems);

                //load settings
                await AppSettings.LoadAsync();

                //reset log settings if different:
                await LogMan.UpdateNLog(LogLevel.FromString(AppSettings.Settings.LogLevel), AppSettings.Settings.LogFileName, AppSettings.Settings.MaxLogFileSize, AppSettings.Settings.MaxLogFileAgeDays, AppSettings.Settings.MaxGUILogItems);

                //HistoryWriter.MaxLogFileAgeDays = AppSettings.Settings.MaxLogFileAgeDays;
                //HistoryWriter.MaxLogSize = AppSettings.Settings.MaxLogFileSize;

                Assembly CurAssm = Assembly.GetExecutingAssembly();
                string AssemNam = CurAssm.GetName().Name;
                string AssemVer = CurAssm.GetName().Version.ToString();

                Log("");
                Log("");
                Log("");
                Log($"Starting {AssemNam} Version {AssemVer} built on {Global.RetrieveLinkerTimestamp()}");

                try  //just in case some weird issue comes up with older os version...
                {
                    OSVersionExt.VersionInfo vi = OSVersion.GetOSVersion();
                    OSVersionExtension.OperatingSystem ov = OSVersion.GetOperatingSystem();

                    Log($"Debug:   Installed NET Framework version '{Global.GetFrameworkVersion()}', Target version '{AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName}'");
                    Log($"Debug:   Windows '{ov.ToString()}', version '{vi.Version.ToString()}' Release ID '{OSVersion.MajorVersion10Properties().ReleaseId}', 64Bit={OSVersion.Is64BitOperatingSystem}, Workstation={OSVersion.IsWorkstation}, Server={OSVersion.IsServer}, SERVICE={Global.IsService}");

                }
                catch (Exception ex)
                {

                    Log("Error: Problem getting OS version: " + Global.ExMsg(ex));
                }


                if (AppSettings.AlreadyRunning)
                {
                    Log("*** Warning: Another instance is already running *** ");
                    Log(" --- Files will not be monitored from within this session ");
                    Log(" --- Log tab will not display output from service instance. You will need to directly open log file for that ");
                    Log(" --- Changes made here to settings will require that you stop/start the service ");
                }
                if (Global.IsAdministrator())
                {
                    Log("Debug: *** Running as administrator ***");
                }
                else
                {
                    Log("Debug: Not running as administrator.");
                }

                if (string.Equals(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), Directory.GetCurrentDirectory().TrimEnd('\\'), StringComparison.OrdinalIgnoreCase))
                {
                    Log($"Debug: *** Start in/current directory is the same as where the EXE is running from: {Directory.GetCurrentDirectory()} ***");
                }
                else
                {
                    try
                    {
                        Log($"Debug: *** Changing Start in/current directory from '{Directory.GetCurrentDirectory().TrimEnd('\\')}' to '{AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')}' ***");
                        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                    }
                    catch (Exception ex)
                    {

                        string msg = $"Error: The Start in/current directory is NOT the same as where the EXE is running from: \r\n{Directory.GetCurrentDirectory()}\r\n{AppDomain.CurrentDomain.BaseDirectory}";
                        Log(msg);
                        Log($"...this may prevent DLL files from loading from the wrong folder.  '{ex.Message}'");
                    }
                }

                //initialize blueiris info class to get camera names, clip paths, etc
                BlueIrisInfo = new BlueIris();

                await BlueIrisInfo.RefreshBIInfoAsync(AppSettings.Settings.BlueIrisServer);

                if (BlueIrisInfo.Result == BlueIrisResult.Valid)
                {
                    Log($"Debug: BlueIris path is '{BlueIrisInfo.AppPath}', with {BlueIrisInfo.Users.Count} users, {BlueIrisInfo.Cameras.Count()} cameras and {BlueIrisInfo.ClipPaths.Count()} clip folder paths configured.");
                    if (BlueIrisInfo.Users.Count > 0 && string.IsNullOrEmpty(AppSettings.Settings.DefaultUserName) || string.Equals(AppSettings.Settings.DefaultUserName, "username", StringComparison.OrdinalIgnoreCase))
                    {
                        AppSettings.Settings.DefaultUserName = BlueIrisInfo.Users[0].Name;
                        AppSettings.Settings.DefaultPasswordEncrypted = Global.EncryptString(BlueIrisInfo.Users[0].Password);
                    }
                }
                else
                {
                    Log($"Debug: BlueIris not detected.");
                }


                //initialize the deepstack class - it collects info from running deepstack processes, detects install location, and
                //allows for stopping and starting of its service
                DeepStackServerControl = new DeepStack(AppSettings.Settings.deepstack_adminkey, AppSettings.Settings.deepstack_apikey, AppSettings.Settings.deepstack_mode, AppSettings.Settings.deepstack_sceneapienabled, AppSettings.Settings.deepstack_faceapienabled, AppSettings.Settings.deepstack_detectionapienabled, AppSettings.Settings.deepstack_port);


                //Load the database, and migrate any old csv lines if needed
                HistoryDB = new SQLiteHistory(AppSettings.Settings.HistoryDBFileName, AppSettings.AlreadyRunning);

                await HistoryDB.Initialize();

                TriggerActionQueue = new ClsTriggerActionQueue();


                await UpdateWatchers(false);

                FileSystemErrorCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimerCheckFileSystemWatchers);
                FileSystemErrorCheckTimer.Interval = AppSettings.Settings.FileSystemWatcherRetryOnErrorTimeMS;
                FileSystemErrorCheckTimer.Enabled = true;
                FileSystemErrorCheckTimer.Start();

                //Start the thread that watches for the file queue
                if (!AppSettings.AlreadyRunning)
                    Task.Run(ImageQueueLoop);


                if (AppSettings.LastShutdownState.StartsWith("checkpoint") && !AppSettings.AlreadyRunning)
                    Log($"Error: Program did not shutdown gracefully.  Last log entry was '{AppSettings.LastLogEntry}', '{AppSettings.LastShutdownState}'");



            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }

        }
        //just an alias to make things easier
        public static void Log(string Detail, string AIServer = "", string Camera = "", string Image = "", string Source = "", int Depth = 0, LogLevel Level = null, Nullable<DateTime> Time = default(DateTime?), [CallerMemberName()] string memberName = null)
        {
            if (LogMan != null && LogMan.Enabled)
            {
                //flush any entries from before logman initialized
                while (!TmpHistQueue.IsEmpty)
                {
                    ClsLogItm cli;
                    if (TmpHistQueue.TryDequeue(out cli))
                        LogMan.Log(cli.Detail, cli.AIServer, cli.Camera, cli.Image, cli.Source, cli.Depth, cli.Level, cli.Date, cli.Func);
                }

                LogMan.Log(Detail, AIServer, Camera, Image, Source, Depth, Level, Time, memberName);
            }
            else
            {
                TmpHistQueue.Enqueue(new ClsLogItm(null, DateTime.Now, Source, memberName, AIServer, Camera, Image, Detail, 0, Depth, "", 0));
                //Console.WriteLine($"Error: Wrote to log before initialized? '{Detail}'");
            }
        }

        public static string UpdateAmazonSettings()
        {
            string error = "";

            RegionEndpoint endpoint = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(AppSettings.Settings.AmazonRegionEndpoint))
                    endpoint = RegionEndpoint.GetBySystemName(AppSettings.Settings.AmazonRegionEndpoint);
            }
            catch (Exception ex)
            {
                error = $"Could not set Amazon region endpoint to '{AppSettings.Settings.AmazonRegionEndpoint}' (JSON setting=AmazonRegionEndpoint): {ex.Message}";
            }

            if (endpoint != null)
            {

                AITOOL.Log($"Debug: Amazon RegionEndpoint DisplayName={endpoint.DisplayName}, PartitionDnsSuffix={endpoint.PartitionDnsSuffix}, PartitionName={endpoint.PartitionName}, SystemName={endpoint.SystemName}");
                //always try to extract latest from rootkey.csv if found
                string pth = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), "rootkey.csv");
                if (File.Exists(pth))
                {
                    string csv = File.ReadAllText(pth);
                    if (!string.IsNullOrWhiteSpace(csv))
                    {
                        AITOOL.Log($"Debug: Extracting AWSAccessKeyId and AWSSecretKey from {pth}");
                        string tid = Global.GetWordBetween(csv, "AWSAccessKeyId=", "\r|\n");
                        string tsid = Global.GetWordBetween(csv, "AWSSecretKey=", "\r|\n|");
                        if (!string.IsNullOrEmpty(tid) && tid != AppSettings.Settings.AmazonAccessKeyId)
                            AppSettings.Settings.AmazonAccessKeyId = tid;
                        if (!string.IsNullOrEmpty(tsid) && tsid != AppSettings.Settings.AmazonSecretKey)
                            AppSettings.Settings.AmazonSecretKey = tsid;

                    }
                    else
                    {
                        error = $"Error: Empty file '{pth}'";
                    }

                }
                else
                {
                    error = $"Could not find AWS credentials file '{pth}'";
                }

                if (string.IsNullOrWhiteSpace(AppSettings.Settings.AmazonAccessKeyId) || string.IsNullOrWhiteSpace(AppSettings.Settings.AmazonSecretKey))
                {
                    error = "Please download 'rootkey.csv' and place it in AITOOL _SETTINGS folder.  1) Sign up for AWS, 2) Create user 3) Export rootkey.csv when prompted.  https://docs.aws.amazon.com/rekognition/latest/dg/setting-up.html  Please note that you have to CREATE NEW in order to see rootkey.csv if one already exists: https://console.aws.amazon.com/iam/home#/security_credentials";
                }
            }
            else
            {
                error = "Please close AITOOL and set 'AmazonRegionEndpoint' in AITOOL.SETTTINGS.JSON to a region code near you such as 'us-east-1': https://docs.aws.amazon.com/general/latest/gr/rande.html";
            }

            return error;

        }

        public static void UpdateAIURLList(bool Force = false)
        {
            //Check to see if we need to get updated URL list
            if (AppSettings.Settings.AIURLList.Count == 0 || Force)
            {
                Log("Debug: Updating/Resetting AI URL list...");
                List<string> SpltURLs = Global.Split(AppSettings.Settings.deepstack_url, "|;,");

                //I want to reuse any object that already exists for the url but make sure to get the right order if it changes
                Dictionary<string, ClsURLItem> tmpdic = new Dictionary<string, ClsURLItem>();

                foreach (ClsURLItem url in AppSettings.Settings.AIURLList)
                {
                    string ur = url.ToString().ToLower();
                    if (!tmpdic.ContainsKey(ur))
                    {
                        tmpdic.Add(ur, url);
                    }
                    else
                    {
                        Log($"Debug: ---- (duplicate url configured - {ur})");
                    }
                }

                AppSettings.Settings.AIURLList.Clear();

                for (int i = 0; i < SpltURLs.Count; i++)
                {
                    ClsURLItem url = new ClsURLItem(SpltURLs[i], i + 1, SpltURLs.Count, URLTypeEnum.Unknown);

                    if (url != null && url.IsValid)
                    {
                        //if it already exists, use it, otherwise add a new one
                        if (tmpdic.ContainsKey(url.ToString().ToLower()))
                        {
                            url = tmpdic[url.ToString().ToLower()];
                            AppSettings.Settings.AIURLList.Add(url);
                            url.OrigOrder = i + 1;
                            //url.InUse.WriteFullFence(false);
                            url.CurErrCount.WriteFullFence(0);
                            url.Enabled.WriteFullFence(true);
                            Log($"Debug: ----   #{url.OrigOrder}: Re-added known URL: {url}");
                        }
                        else
                        {
                            AppSettings.Settings.AIURLList.Add(url);
                            Log($"Debug: ----   #{url.OrigOrder}: Added new URL: {url}");
                        }

                    }
                    else
                    {
                        Log($"Debug: ----   #{url.OrigOrder}: Skipped INVALID URL: {url}");

                    }
                }
                Log($"Debug: ...Found {AppSettings.Settings.AIURLList.Count} AI URL's in settings.");

                //AIURLSettingsChanged.WriteFullFence(false);

            }

        }

        public static async Task<ClsURLItem> WaitForNextURL(Camera cam)
        {
            //lets wait in here forever until a URL is available...

            ClsURLItem ret = null;

            DateTime LastWaitingLog = DateTime.MinValue;
            bool displayedbad = false;
            bool displayedretry = false;

            while (ret == null)
            {
                int disabled = 0;
                int inuse = 0;
                int incorrectcam = 0;
                int notintimerange = 0;
                try
                {

                    UpdateAIURLList();

                    List<ClsURLItem> sorted = new List<ClsURLItem>();

                    if (AppSettings.Settings.deepstack_urls_are_queued)
                    {
                        //always use oldest first
                        sorted = AppSettings.Settings.AIURLList.OrderBy((d) => d.LastUsedTime).ToList();
                    }
                    else
                    {
                        //use original order
                        sorted.AddRange(AppSettings.Settings.AIURLList);
                    }
                    //sort by oldest last used

                    for (int i = 0; i < sorted.Count; i++)
                    {
                        if (sorted[i].Enabled.ReadFullFence())
                        {
                            if (!sorted[i].InUse.ReadFullFence())
                            {
                                if (Global.IsInList(cam.name, sorted[i].Cameras, TrueIfEmpty: true))
                                {
                                    DateTime now = DateTime.Now;

                                    if (Global.IsTimeBetween(now, sorted[i].ActiveTimeRange))
                                    {
                                        if (sorted[i].CurErrCount.ReadFullFence() == 0)
                                        {
                                            ret = sorted[i];
                                            ret.CurOrder = i + 1;
                                            break;
                                        }
                                        else
                                        {
                                            double secs = Math.Round((now - sorted[i].LastUsedTime).TotalSeconds, 0);
                                            if (secs >= AppSettings.Settings.MinSecondsBetweenFailedURLRetry)
                                            {
                                                ret = sorted[i];
                                                ret.CurOrder = i + 1;
                                                if (!displayedretry)  //if we get in a long loop waiting for URL
                                                {
                                                    Log($"---- Trying previously failed URL again after {secs} seconds. (ErrCount={sorted[i].CurErrCount.ReadFullFence()}, Setting 'MinSecondsBetweenFailedURLRetry'={AppSettings.Settings.MinSecondsBetweenFailedURLRetry}): {sorted[i]}");
                                                    displayedretry = true;
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                if (!displayedbad)  //if we get in a long loop waiting for URL
                                                {
                                                    Log($"---- Waiting {AppSettings.Settings.MinSecondsBetweenFailedURLRetry - secs} seconds before retrying bad URL. (ErrCount={sorted[i].CurErrCount.ReadFullFence()} of {AppSettings.Settings.MaxQueueItemRetries}, Setting 'MinSecondsBetweenFailedURLRetry'={AppSettings.Settings.MinSecondsBetweenFailedURLRetry}): {sorted[i]}");
                                                    displayedbad = true;
                                                }
                                            }

                                        }
                                    }
                                    else
                                    {
                                        notintimerange++;
                                    }
                                }
                                else
                                {
                                    incorrectcam++;
                                }

                            }
                            else
                            {
                                inuse++;
                            }
                        }
                        //disabled, but check to see if we need to reenable
                        else 
                        {
                            disabled++;
                            if ((DateTime.Now - sorted[i].LastUsedTime).TotalMinutes >= AppSettings.Settings.URLResetAfterDisabledMinutes)
                            {
                                //check to see if can be re-enabled yet
                                sorted[i].Enabled.WriteFullFence(true);
                                sorted[i].CurErrCount.WriteFullFence(0);
                                sorted[i].InUse.WriteFullFence(false);
                                Log($"---- Re-enabling disabled URL because {AppSettings.Settings.URLResetAfterDisabledMinutes} (URLResetAfterDisabledMinutes) minutes have passed: " + sorted[i]);
                                ret = sorted[i];
                                ret.CurOrder = i + 1;
                                break;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Log("Error: getting next URL: " + ex.ToString());
                }

                if (ret != null)
                {
                    ret.InUse.WriteFullFence(true);
                    ret.LastUsedTime = DateTime.Now;
                    break;
                }

                if ((DateTime.Now - LastWaitingLog).TotalMinutes >= 10)
                {
                    Log($"---- All URL's are in use, disabled, camera name doesnt match or time range was not met.  ({inuse} inuse, {disabled} disabled, {incorrectcam} wrong camera, {notintimerange} not in time range) Waiting...");
                    LastWaitingLog = DateTime.Now;
                }

                //wait quarter a second for other url's to become available
                await Task.Delay(250);

            }

            return ret;


        }
        public static async Task ImageQueueLoop()
        {
            //This runs in another thread, waiting for items to appear in the queue and process them one at a time
            try
            {
                //Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;

                ClsImageQueueItem CurImg;

                DateTime LastCleanDupesTime = DateTime.MinValue;


                //lets wait 5 seconds to let the UI settle down a bit
                await Task.Delay(5000);

                //Start infinite loop waiting for images to come into queue

                while (true)
                {
                    if (MasterCTS.IsCancellationRequested)
                        break;

                    while (!ImageProcessQueue.IsEmpty)
                    {

                        int ProcImgCnt = 0;
                        int ErrCnt = 0;
                        int TskCnt = 0;

                        while (!ImageProcessQueue.IsEmpty)
                        {
                            //tiny delay to conserve cpu and allow more images to come in the queue if needed
                            //await Task.Delay(250);

                            //get the next image

                            if (ImageProcessQueue.TryDequeue(out CurImg))
                            {
                                Camera cam = GetCamera(CurImg.image_path, true);

                                if (cam == null)
                                {
                                    Log($"Error: Camera could not be found to match this image: {CurImg.image_path}");
                                    continue;
                                }

                                //skip the image if its been in the queue too long
                                if ((DateTime.Now - CurImg.TimeAdded).TotalMinutes >= AppSettings.Settings.MaxImageQueueTimeMinutes)
                                {
                                    Log($"...Taking image OUT OF QUEUE because it has been in there over 'MaxImageQueueTimeMinutes'. (QueueTime={(DateTime.Now - CurImg.TimeAdded).TotalMinutes.ToString("###0.0")}, Image ErrCount={CurImg.ErrCount}, Image RetryCount={CurImg.RetryCount}, ImageProcessQueue.Count={ImageProcessQueue.Count}: '{CurImg.image_path}'", "None", cam.name, CurImg.image_path);
                                    continue;
                                }

                                Stopwatch sw = Stopwatch.StartNew();

                                //wait for the next url to become available...
                                ClsURLItem url = await WaitForNextURL(cam);

                                sw.Stop();

                                double lastsecs = Math.Round((DateTime.Now - url.LastUsedTime).TotalSeconds, 0);

                                Log($"Debug: Adding task for file '{Path.GetFileName(CurImg.image_path)}' (Image QueueTime='{(DateTime.Now - CurImg.TimeAdded).TotalMinutes.ToString("###0.0")}' mins, URL Queue wait='{sw.ElapsedMilliseconds}ms', URLOrder={url.CurOrder} of {url.Count}, URLOriginalOrder={url.OrigOrder}) on URL '{url}'", url.CurSrv, cam.name, CurImg.image_path);

                                Interlocked.Increment(ref TskCnt);

                                Task.Run(async () =>
                                {


                                    Global.SendMessage(MessageType.BeginProcessImage, CurImg.image_path);

                                    bool success = await DetectObjects(CurImg, url); //ai process image

                                    Global.SendMessage(MessageType.EndProcessImage, CurImg.image_path);


                                    if (!success)
                                    {
                                        Interlocked.Increment(ref ErrCnt);

                                        if (url.CurErrCount.ReadFullFence() > 0)
                                        {
                                            if (url.CurErrCount.ReadFullFence() < AppSettings.Settings.MaxQueueItemRetries)
                                            {
                                                //put url back in queue when done
                                                Log($"...Problem with AI URL: '{url}' (URL ErrCount={url.CurErrCount}, max allowed of {AppSettings.Settings.MaxQueueItemRetries})", url.CurSrv, cam.name);
                                            }
                                            else
                                            {
                                                url.Enabled.WriteFullFence(false);
                                                Log($"...Error: AI URL for '{url.Type}' failed '{url.CurErrCount}' times.  Disabling: '{url}'", url.CurSrv, cam.name);
                                            }

                                        }

                                        CurImg.RetryCount.AtomicIncrementAndGet();  //even if there was not an error directly accessing the image

                                        if (CurImg.ErrCount.ReadFullFence() <= AppSettings.Settings.MaxQueueItemRetries && CurImg.RetryCount.ReadFullFence() <= AppSettings.Settings.MaxQueueItemRetries)
                                        {
                                            //put back in queue to be processed by another deepstack server
                                            Log($"...Putting image back in queue due to URL '{url}' problem (QueueTime={(DateTime.Now - CurImg.TimeAdded).TotalMinutes.ToString("###0.0")}, Image ErrCount={CurImg.ErrCount}, Image RetryCount={CurImg.RetryCount}, URL ErrCount={url.CurErrCount}): '{CurImg.image_path}', ImageProcessQueue.Count={ImageProcessQueue.Count}", url.CurSrv, cam.name, CurImg.image_path);
                                            ImageProcessQueue.Enqueue(CurImg);
                                        }
                                        else
                                        {
                                            cam.stats_skipped_images++;
                                            cam.stats_skipped_images_session++;

                                            Log($"...Error: Removing image from queue. Image RetryCount={CurImg.RetryCount}, URL ErrCount='{url.CurErrCount}': {url}', Image: '{CurImg.image_path}', ImageProcessQueue.Count={ImageProcessQueue.Count}, Skipped this session={cam.stats_skipped_images_session }", url.CurSrv, cam.name, CurImg.image_path);
                                            Global.CreateHistoryItem(new History().Create(CurImg.image_path, DateTime.Now, cam.name, $"Skipped image, {CurImg.RetryCount.ReadFullFence()} errors processing.", "", false, "", url.CurSrv));

                                        }
                                    }
                                    else
                                    {
                                        Interlocked.Increment(ref ProcImgCnt);
                                        //reset error count
                                        url.CurErrCount.WriteFullFence(0);
                                    }

                                    url.InUse.WriteFullFence(false);

                                    Interlocked.Decrement(ref TskCnt);

                                });


                            }
                            else
                            {
                                //Log("No Images left in the queue!");
                                break;
                            }

                        }

                        if (TskCnt > 0)
                        {
                            Log($"Debug: Done adding {TskCnt} total threads, ErrCnt={ErrCnt}, ImageProcessQueue.Count={ImageProcessQueue.Count}");
                        }

                        //Clean up old images in the dupe check dic
                        if ((DateTime.Now - LastCleanDupesTime).TotalMinutes >= 60)
                        {
                            int cnt = 0;
                            foreach (KeyValuePair<string, ClsImageQueueItem> kvPair in detection_dictionary)
                            {
                                if ((DateTime.Now - kvPair.Value.TimeAdded).TotalMinutes >= 30)
                                {   // Remove expired item.
                                    cnt++;
                                    ClsImageQueueItem removedItem;
                                    detection_dictionary.TryRemove(kvPair.Key, out removedItem);
                                }
                            }

                        }

                    }

                    //Only loop 10 times a second conserve cpu
                    await Task.Delay(100);
                }

                Log("Debug: ImageQueueLoop canceled.");

            }
            catch (Exception ex)
            {
                //if we get here its the end of the world as we know it
                Log("Error: * '...Human sacrifice, dogs and cats living together – mass hysteria!' * - " + Global.ExMsg(ex));
            }
        }

        //EVENT: new image added to input_path -> START AI DETECTION
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            AddImageToQueue(e.FullPath);
        }

        public static void AddImageToQueue(string Filename)
        {

            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            lock (FileWatcherLockObject)
            {
                try
                {
                    //make sure we are not processing a duplicate file...
                    if (detection_dictionary.ContainsKey(Filename.ToLower()))
                    {
                        Log("Skipping image because of duplicate Created File Event: " + Filename);
                    }
                    else
                    {
                        Camera cam = GetCamera(Filename);
                        if (cam != null)  //only put in queue if we can match to camera (even default)
                        {

                            if (cam.enabled)
                            {
                                //Note:  Interwebz says ConCurrentQueue.Count may be slow for large number of items but I dont think we have to worry here in most cases
                                int qsize = ImageProcessQueue.Count + 1;
                                if (qsize > AppSettings.Settings.MaxImageQueueSize)
                                {
                                    Log("");
                                    Log($"Error: Skipping image because queue ({qsize}) is greater than '{AppSettings.Settings.MaxImageQueueSize}'. (Adjust 'MaxImageQueueSize' in .JSON file if needed): " + Filename, "", cam.name, Filename);
                                }
                                else
                                {
                                    Log("Debug: ");
                                    Log($"Debug: ====================== Adding new image to queue (Count={ImageProcessQueue.Count + 1}): " + Filename, "", cam.name, Filename);
                                    ClsImageQueueItem CurImg = new ClsImageQueueItem(Filename, qsize);
                                    detection_dictionary.TryAdd(Filename.ToLower(), CurImg);
                                    ImageProcessQueue.Enqueue(CurImg);
                                    qsizecalc.AddToCalc(qsize);
                                    Global.SendMessage(MessageType.ImageAddedToQueue);
                                }

                            }
                            else
                            {
                                Log($"Error: Skipping image because camera '{cam.name}' is DISABLED " + Filename, "", cam.name, Filename);
                            }
                        }
                        else
                        {
                            Log("Error: Skipping image because no camera found for new image " + Filename, "", cam.name, Filename);
                        }


                    }

                }
                catch (Exception ex)
                {
                    Log("Error: " + Global.ExMsg(ex));
                }

            }


        }
        //event: image in input_path renamed
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Global.DeleteHistoryItem(e.OldFullPath);
        }

        //event: image in input path deleted
        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            Global.DeleteHistoryItem(e.FullPath);
        }

        private static void OnError(object sender, System.IO.ErrorEventArgs e)
        {
            //Too many changes at once in directory:C:\BlueIris\aiinput.
            //File watcher  The specified network name is no longer available
            string path = ((FileSystemWatcher)sender).Path;
            Log("Error: File watcher error: " + e.GetException().Message + $" on path '{path}'");
            UpdateWatchers(true);

        }

        public static void TimerCheckFileSystemWatchers(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (FileWatcherHasError)
            {
                Log($"Debug: Re-checking bad File System Watcher Paths ('FileSystemWatcherRetryOnErrorTimeMS' = {AppSettings.Settings.FileSystemWatcherRetryOnErrorTimeMS}ms)...");
                UpdateWatchers(true);
            }
        }

        public static async Task UpdateWatchers(bool Reset)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            try
            {


                if (AppSettings.AlreadyRunning)
                {
                    Log("*** Another instance is already running, skip watching for changed files ***");
                    return;
                }

                FileWatcherHasError = false;

                //first add all the names and paths to check...
                List<string> names = new List<string>();
                Dictionary<string, string> paths = new Dictionary<string, string>();

                string pths = AppSettings.Settings.input_path.Trim().TrimEnd(@"\".ToCharArray());
                names.Add($"INPUT_PATH|{pths}|{AppSettings.Settings.input_path_includesubfolders}");
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    if (cam.enabled && !String.IsNullOrWhiteSpace(cam.input_path))
                    {
                        pths = cam.input_path.Trim().TrimEnd(@"\".ToCharArray());
                        names.Add($"{cam.name}|{pths}|{cam.input_path_includesubfolders}");
                    }
                }

                if (Reset)
                {
                    foreach (ClsFileSystemWatcher watcher1 in watchers.Values)
                    {
                        if (watcher1 != null && watcher1.watcher != null)
                        {
                            try
                            {
                                watcher1.watcher.EnableRaisingEvents = false;
                                watcher1.watcher.Dispose();
                                watcher1.watcher = null;
                            }
                            catch (Exception ex)
                            {

                                Log($"Error: Failed to reset/clear watcher for folder '{watcher1.Name}' - {watcher1.Path}: {ex.Message}");
                            }
                        }
                    }
                    watchers.Clear();
                }

                //check each one to see if needs to be added
                foreach (string item in names)
                {
                    List<string> splt = Global.Split(item, "|", false);
                    string name = splt[0];
                    string path = splt[1];
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        bool include = Convert.ToBoolean(splt[2]);
                        if (!paths.ContainsKey(path.ToLower()))
                        {
                            paths.Add(path.ToLower(), path);

                            if (!watchers.ContainsKey(name.ToLower()))
                            {
                                //this will return null if the path is invalid...
                                FileSystemWatcher curwatch = await CreateFileWatcherAsync(path, include);
                                if (curwatch != null)
                                {
                                    ClsFileSystemWatcher mywtc = new ClsFileSystemWatcher(name, path, curwatch, include);
                                    //add even if null to keep track of things
                                    watchers.Add(name.ToLower(), mywtc);
                                }
                            }
                            else
                            {
                                //update path if needed, even to empty
                                watchers[name.ToLower()].Path = path;
                                if (watchers[name.ToLower()].watcher == null)
                                {
                                    //could be null if path is bad
                                    watchers[name.ToLower()].watcher = await CreateFileWatcherAsync(path, include);
                                }
                            }
                        }
                        else
                        {
                            Log($"Debug: Skipping duplicate path for '{name}': '{path}'");
                        }

                    }


                }

                //check to see if any need disabling - a camera was deleted
                foreach (ClsFileSystemWatcher watcher1 in watchers.Values)
                {
                    bool fnd = false;
                    foreach (string item in names)
                    {
                        List<string> splt = Global.Split(item);
                        string name = splt[0];
                        if (string.Equals(name, watcher1.Name, StringComparison.OrdinalIgnoreCase))
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

                foreach (ClsFileSystemWatcher watcher in watchers.Values)
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
                                    Log($"Debug: Watcher '{watcher.Name}' changed from '{watcher.watcher.Path}' to '{watcher.Path}'.");
                                }

                                if (watcher.IncludeSubdirectories != watcher.watcher.IncludeSubdirectories)
                                {
                                    watcher.watcher.IncludeSubdirectories = watcher.IncludeSubdirectories;
                                    Log($"Debug: Watcher '{watcher.Name}' IncludeSubdirectories changed from '{watcher.watcher.IncludeSubdirectories}' to '{watcher.IncludeSubdirectories}'.");
                                }

                                if (watcher.watcher.EnableRaisingEvents != true)
                                {
                                    enabledcnt++;
                                    watcher.watcher.EnableRaisingEvents = true;
                                    dupes.Add(watcher.Path.ToLower(), watcher.Path);
                                    Log($"Debug: Watcher '{watcher.Name}' is now watching '{watcher.Path}'");
                                }

                            }
                            else
                            {
                                Log($"Debug: Watcher '{watcher.Name}' has a duplicate path, skipping '{watcher.Path}'");
                            }
                        }
                        else
                        {
                            //make sure it is disabled
                            disabledcnt++;

                            watcher.watcher.EnableRaisingEvents = false;
                            watcher.watcher.Dispose();
                            watcher.watcher = null;
                            Log($"Debug: Watcher '{watcher.Name}' has an empty path, just disabled.");
                        }

                    }
                    else if (!string.IsNullOrEmpty(watcher.Path))
                    {
                        Log($"Error: Watcher '{watcher.Name}' disabled. INVALID PATH='{watcher.Path}'");
                    }
                    else
                    {
                        //Log($"Watcher '{watcher.Name}' already disabled.");
                    }


                }

                if (watchers.Count() == 0)
                {
                    Log("Debug: No FileSystemWatcher input folders defined yet.");
                }
                else
                {
                    if (enabledcnt == 0)
                    {
                        Log("Debug: No NEW FileSystemWatcher input folders found.");
                    }
                    else
                    {
                        Log($"Debug: Enabled {enabledcnt} FileSystemWatchers.");
                    }
                }

            }
            catch (Exception ex)
            {
                FileWatcherHasError = true;
                Log($"Error: {Global.ExMsg(ex)}");
            }

        }

        static bool FileWatcherHasError = false;

        public static async Task<FileSystemWatcher> CreateFileWatcherAsync(string path, bool IncludeSubdirectories = false, string filter = "*.jpg")
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            FileSystemWatcher watcher = null;

            try
            {
                // Be aware: https://stackoverflow.com/questions/1764809/filesystemwatcher-changed-event-is-raised-twice

                if (!String.IsNullOrWhiteSpace(path))
                {
                    if (await Global.DirectoryExistsAsync(path))
                    {
                        watcher = new FileSystemWatcher(path);
                        watcher.Path = path;
                        watcher.Filter = filter;
                        watcher.IncludeSubdirectories = IncludeSubdirectories;

                        //The 'default' is the bitwise OR combination of LastWrite, FileName, and DirectoryName'
                        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

                        //fswatcher events
                        watcher.Created += new FileSystemEventHandler(OnCreated);
                        watcher.Renamed += new RenamedEventHandler(OnRenamed);
                        watcher.Deleted += new FileSystemEventHandler(OnDeleted);
                        watcher.Error += new ErrorEventHandler(OnError);


                    }
                    else
                    {
                        FileWatcherHasError = true;
                        Log("Error: Path does not exist: " + path);
                    }
                }
            }
            catch (Exception ex)
            {
                FileWatcherHasError = true;
                Log($"Error: {Global.ExMsg(ex)}");
            }

            return watcher;
        }

        public static ClsImageAdjust GetImageAdjustProfileByName(string name)
        {
            ClsImageAdjust ret = null;
            ClsImageAdjust def = null;

            foreach (ClsImageAdjust ia in AppSettings.Settings.ImageAdjustProfiles)
            {
                if (string.Equals(name.Trim(), ia.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    ret = ia;
                }
                if (string.Equals("Default", ia.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    def = ia;
                }
            }

            if (ret == null)
                ret = def;

            if (ret == null)
            {
                ret = new ClsImageAdjust("Default");
                Log($"Error: Could not find Image Adjust profile that matches '{name}'");
            }

            return ret;
        }

        public static bool HasImageAdjustProfile(string name)
        {
            bool ret = false;

            foreach (ClsImageAdjust ia in AppSettings.Settings.ImageAdjustProfiles)
            {
                if (string.Equals(name.Trim(), ia.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return ret;
        }
        public static bool IsValidImage(ClsImageQueueItem CurImg)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool ret = false;

            try
            {
                if (System.IO.File.Exists(CurImg.image_path))
                {
                    if (new FileInfo(CurImg.image_path).Length >= 1024)
                    {
                        using (System.Drawing.Image test = System.Drawing.Image.FromFile(CurImg.image_path))
                        {
                            ret = (test.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg));

                            if (!ret)
                            {
                                Log($"Error: Image file is not jpeg? ({test.RawFormat}): {CurImg.image_path}");
                            }
                            else
                            {
                                CurImg.Width = test.Width;
                                CurImg.Height = test.Height;
                                Log($"Debug: Image file is valid: {Path.GetFileName(CurImg.image_path)}");
                            }
                        }
                    }
                    else
                    {
                        Log($"Error: Image file is too small, less than 1024 bytes: {CurImg.image_path}");
                    }

                }
                else
                {
                    Log($"Error: Image file does not exist: {CurImg.image_path}");
                }
            }
            catch (NotSupportedException ex)
            {
                // System.NotSupportedException:
                // No imaging component suitable to complete this operation was found.
                Log($"Error: Image file not valid {CurImg.image_path}: {Global.ExMsg(ex)}");
            }
            catch (Exception ex)
            {
                Log($"Error: Image file not valid {CurImg.image_path}: {Global.ExMsg(ex)}");
            }

            return ret;
        }

        public class ClsAIServerResponse
        {
            public List<ClsPrediction> Predictions = new List<ClsPrediction>();
            public bool Success = false;
            public string JsonString = "";
            public string Error = "";
            public long SWPostTime = 0;
            public HttpStatusCode StatusCode = HttpStatusCode.Unused;
        }

        public static async Task<ClsAIServerResponse> GetDetectionsFromAIServer(ClsImageQueueItem CurImg, ClsURLItem AiUrl, Camera cam)
        {
            ClsAIServerResponse ret = new ClsAIServerResponse();


            //==============================================================================================================
            //==============================================================================================================
            //==============================================================================================================
            if (AiUrl.Type == URLTypeEnum.DeepStack)
            {
                Stopwatch swposttime = new Stopwatch();

                try
                {
                    long FileSize = new FileInfo(CurImg.image_path).Length;

                    using FileStream image_data = System.IO.File.OpenRead(CurImg.image_path);

                    using MultipartFormDataContent request = new MultipartFormDataContent();

                    request.Add(new StreamContent(image_data), "image", Path.GetFileName(CurImg.image_path));

                    Log($"Debug: (1/6) Uploading a {FileSize} byte image to '{AiUrl.Type}' AI Server at {AiUrl}", AiUrl.CurSrv, cam.name, CurImg.image_path);

                    swposttime.Restart();

                    using HttpResponseMessage output = await AiUrl.HttpClient.PostAsync(AiUrl.ToString(), request, MasterCTS.Token);

                    swposttime.Stop();
                    ret.StatusCode = output.StatusCode;

                    if (output.IsSuccessStatusCode)
                    {
                        ret.JsonString = await output.Content.ReadAsStringAsync();

                        if (ret.JsonString != null && !string.IsNullOrWhiteSpace(ret.JsonString))
                        {
                            string cleanjsonString = Global.CleanString(ret.JsonString);

                            ClsDeepStackResponse response = null;

                            try
                            {
                                //This can throw an exception
                                response = JsonConvert.DeserializeObject<ClsDeepStackResponse>(ret.JsonString);

                                if (response != null)
                                {
                                    if (response.predictions != null)
                                    {
                                        if (!response.success)
                                        {
                                            ret.Error = $"ERROR: Failure response from '{AiUrl.Type.ToString()}'. JSON: '{cleanjsonString}'";
                                            AiUrl.IncrementError();
                                            AiUrl.LastResultMessage = ret.Error;
                                        }
                                        else
                                        {

                                            if (response.predictions.Count() > 0)
                                            {

                                                foreach (ClsDeepstackDetection DSObj in response.predictions)
                                                {
                                                    ClsPrediction pred = new ClsPrediction(ObjectType.Object, cam, DSObj, CurImg);

                                                    ret.Predictions.Add(pred);

                                                }


                                            }

                                            ret.Success = true;
                                            AiUrl.LastResultMessage = $"{ret.Predictions.Count()} predictions found.";

                                        }

                                    }
                                    else
                                    {
                                        ret.Error = $"ERROR: No predictions?  JSON: '{cleanjsonString}')";
                                        AiUrl.IncrementError();
                                        AiUrl.LastResultMessage = ret.Error;
                                    }


                                }
                                else if (string.IsNullOrEmpty(ret.Error))
                                {
                                    //deserialization did not cause exception, it just gave a null response in the object?
                                    //probably wont happen but just making sure
                                    ret.Error = $"ERROR: Deserialization of 'Response' from DeepStack failed. response is null. JSON: '{cleanjsonString}'";
                                    AiUrl.IncrementError();
                                    AiUrl.LastResultMessage = ret.Error;
                                }
                            }
                            catch (Exception ex)
                            {
                                ret.Error = $"ERROR: Deserialization of 'Response' from '{AiUrl.Type.ToString()}' failed: {Global.ExMsg(ex)}, JSON: '{cleanjsonString}'";
                                AiUrl.IncrementError();
                                AiUrl.LastResultMessage = ret.Error;
                            }
                        }
                        else
                        {
                            ret.Error = $"ERROR: Empty string returned from HTTP post.";
                            AiUrl.IncrementError();
                            AiUrl.LastResultMessage = ret.Error;
                        }


                    }
                    else
                    {
                        ret.Error = $"ERROR: Got http status code '{output.StatusCode}' ({Convert.ToInt32(output.StatusCode)}) in {swposttime.ElapsedMilliseconds}ms: {output.ReasonPhrase}";
                        AiUrl.IncrementError();
                        AiUrl.LastResultMessage = ret.Error;
                    }

                }
                catch (Exception ex)
                {
                    swposttime.Stop();

                    ret.Error = $"ERROR: {Global.ExMsg(ex)}";
                    AiUrl.IncrementError();
                    AiUrl.LastResultMessage = ret.Error;
                }
                finally
                {
                    ret.SWPostTime = swposttime.ElapsedMilliseconds;
                }

            }
            //==============================================================================================================
            //==============================================================================================================
            //==============================================================================================================
            else if (AiUrl.Type == URLTypeEnum.DOODS)
            {
                Stopwatch swposttime = new Stopwatch();

                try
                {
                    ClsDoodsRequest cdr = new ClsDoodsRequest();

                    //We could prevent doods from giving back ALL of its results but then we couldnt fully see how it was working:
                    //So many things come back from Doods, cluttering up the db, we really need to limit at the source
                    if (AppSettings.Settings.HistoryRestrictMinThresholdAtSource)
                        cdr.Detect.MinPercentMatch = cam.threshold_lower;

                    cdr.DetectorName = AppSettings.Settings.DOODSDetectorName;

                    //string testjson = JsonConvert.SerializeObject(cdr);

                    long FileSize = new FileInfo(CurImg.image_path).Length;

                    using (FileStream image_data = System.IO.File.OpenRead(CurImg.image_path))
                    {
                        cdr.Data = image_data.ConvertToBase64();
                    }

                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, AiUrl.ToString()))
                    {

                        using HttpContent httpContent = Global.CreateHttpContentString(cdr);

                        request.Content = httpContent;

                        Log($"Debug: (1/6) Uploading a {FileSize} byte image to '{AiUrl.Type}' AI Server at {AiUrl}", AiUrl.CurSrv, cam.name, CurImg.image_path);


                        //  Got http status code 'RequestEntityTooLarge' (413) in 42ms: Request Entity Too Large
                        swposttime.Restart();

                        using HttpResponseMessage output = await AiUrl.HttpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, MasterCTS.Token);

                        swposttime.Stop();
                        ret.StatusCode = output.StatusCode;


                        if (output.IsSuccessStatusCode)
                        {
                            ret.JsonString = await output.Content.ReadAsStringAsync();

                            if (ret.JsonString != null && !string.IsNullOrWhiteSpace(ret.JsonString))
                            {
                                string cleanjsonString = Global.CleanString(ret.JsonString);

                                ClsDoodsResponse response = null;

                                try
                                {
                                    //This can throw an exception
                                    response = JsonConvert.DeserializeObject<ClsDoodsResponse>(ret.JsonString);

                                    if (response != null)
                                    {
                                        if (response.Detections != null)
                                        {
                                            if (response.Detections.Count() > 0)
                                            {

                                                foreach (ClsDoodsDetection DSObj in response.Detections)
                                                {
                                                    ClsPrediction pred = new ClsPrediction(ObjectType.Object, cam, DSObj, CurImg);

                                                    ret.Predictions.Add(pred);

                                                }


                                            }

                                            ret.Success = true;
                                            AiUrl.LastResultMessage = $"{ret.Predictions.Count()} predictions found.";


                                        }
                                        else
                                        {
                                            ret.Error = $"ERROR: No predictions?  JSON: '{cleanjsonString}')";
                                            AiUrl.IncrementError();
                                            AiUrl.LastResultMessage = ret.Error;
                                        }


                                    }
                                    else if (string.IsNullOrEmpty(ret.Error))
                                    {
                                        //deserialization did not cause exception, it just gave a null response in the object?
                                        //probably wont happen but just making sure
                                        ret.Error = $"ERROR: Deserialization of 'Response' from DeepStack failed. response is null. JSON: '{cleanjsonString}'";
                                        AiUrl.IncrementError();
                                        AiUrl.LastResultMessage = ret.Error;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ret.Error = $"ERROR: Deserialization of 'Response' from '{AiUrl.Type.ToString()}' failed: {Global.ExMsg(ex)}, JSON: '{cleanjsonString}'";
                                    AiUrl.IncrementError();
                                    AiUrl.LastResultMessage = ret.Error;
                                }
                            }
                            else
                            {
                                ret.Error = $"ERROR: Empty string returned from HTTP post.";
                                AiUrl.IncrementError();
                                AiUrl.LastResultMessage = ret.Error;
                            }


                        }
                        else
                        {
                            ret.Error = $"ERROR: Got http status code '{output.StatusCode}' ({Convert.ToInt32(output.StatusCode)}) in {swposttime.ElapsedMilliseconds}ms: {output.ReasonPhrase}";
                            AiUrl.IncrementError();
                            AiUrl.LastResultMessage = ret.Error;
                        }

                    }



                }
                catch (Exception ex)
                {
                    swposttime.Stop();

                    ret.Error = $"ERROR: {Global.ExMsg(ex)}";
                    AiUrl.IncrementError();
                    AiUrl.LastResultMessage = ret.Error;
                }
                finally
                {
                    ret.SWPostTime = swposttime.ElapsedMilliseconds;
                }

            }
            //==============================================================================================================
            //==============================================================================================================
            //==============================================================================================================
            else if (AiUrl.Type == URLTypeEnum.AWSRekognition)
            {
                Stopwatch swposttime = new Stopwatch();

                try
                {

                    //string testjson = JsonConvert.SerializeObject(cdr);

                    long FileSize = new FileInfo(CurImg.image_path).Length;
                    //https://docs.aws.amazon.com/general/latest/gr/rande.html


                    RegionEndpoint endpoint = RegionEndpoint.GetBySystemName(AppSettings.Settings.AmazonRegionEndpoint);
                    AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(new BasicAWSCredentials(AppSettings.Settings.AmazonAccessKeyId, AppSettings.Settings.AmazonSecretKey), endpoint);

                    DetectLabelsRequest dlr = new DetectLabelsRequest();

                    dlr.MaxLabels = AppSettings.Settings.AmazonMaxLabels;
                    dlr.MinConfidence = AppSettings.Settings.AmazonMinConfidence;

                    if (AppSettings.Settings.HistoryRestrictMinThresholdAtSource)
                        dlr.MinConfidence = cam.threshold_lower;

                    Amazon.Rekognition.Model.Image rekognitionImgage = new Amazon.Rekognition.Model.Image();

                    byte[] data = null;

                    using (FileStream fileStream = new FileStream(CurImg.image_path, FileMode.Open, FileAccess.Read))
                    {
                        data = new byte[fileStream.Length];
                        await fileStream.ReadAsync(data, 0, (int)fileStream.Length);
                    }

                    rekognitionImgage.Bytes = new MemoryStream(data);

                    dlr.Image = rekognitionImgage;


                    Log($"Debug: (1/6) Uploading a {FileSize} byte image to '{AiUrl.Type}' AI Server at {AiUrl}", AiUrl.CurSrv, cam.name, CurImg.image_path);

                    swposttime.Restart();

                    DetectLabelsResponse response = await rekognitionClient.DetectLabelsAsync(dlr);

                    swposttime.Stop();

                    if (response != null)
                    {
                        ret.StatusCode = response.HttpStatusCode;
                        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (response.Labels.Count() > 0)
                            {

                                foreach (Label lbl in response.Labels)
                                {
                                    //not sure if there will ever be more than one instance
                                    for (int i = 0; i < lbl.Instances.Count; i++)
                                    {
                                        ClsPrediction pred = new ClsPrediction(ObjectType.Object, cam, lbl, i, CurImg);

                                        ret.Predictions.Add(pred);

                                    }

                                }


                            }

                            ret.Success = true;
                            AiUrl.LastResultMessage = $"{ret.Predictions.Count()} predictions found.";
                        }
                        else 
                        {
                            ret.Error = $"ERROR: Amazon Rekognition 'HttpStatusCode' is '{response.HttpStatusCode}' ({Convert.ToInt32(response.HttpStatusCode)}).";
                            AiUrl.IncrementError();
                            AiUrl.LastResultMessage = ret.Error;
                        }
                    }
                    else
                    {
                        ret.Error = $"ERROR: Amazon Rekognition 'Response' is null.";
                        AiUrl.IncrementError();
                        AiUrl.LastResultMessage = ret.Error;
                    }


                }
                catch (Exception ex)
                {
                    swposttime.Stop();

                    ret.Error = $"ERROR: {Global.ExMsg(ex)}";
                    AiUrl.IncrementError();
                    AiUrl.LastResultMessage = ret.Error;
                }
                finally
                {
                    ret.SWPostTime = swposttime.ElapsedMilliseconds;
                }

            }
            else
            {
                ret.Error = $"Error: URL type not supported yet: '{AiUrl.Type}'";
                AiUrl.LastResultMessage = ret.Error;
            }

            return ret;

        }
        //analyze image with AI
        public static async Task<bool> DetectObjects(ClsImageQueueItem CurImg, ClsURLItem AiUrl)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            //Only set error when there IS an error...
            string error = ""; //if code fails at some point, the last text of the error string will be posted in the log

            string filename = Path.GetFileName(CurImg.image_path);

            CurImg.QueueWaitMS = (long)(DateTime.Now - CurImg.TimeAdded).TotalMilliseconds;

            Stopwatch sw = Stopwatch.StartNew();

            Camera cam = AITOOL.GetCamera(CurImg.image_path);
            cam.last_image_file = CurImg.image_path;

            History hist = null;

            // check if camera is still in the first half of the cooldown. If yes, don't analyze to minimize cpu load.
            //only analyze if 50% of the cameras cooldown time since last detection has passed
            double secs = (DateTime.Now - cam.last_trigger_time.Read()).TotalSeconds;
            double halfcool = cam.cooldown_time_seconds / 2;
            ClsAIServerResponse asr = new ClsAIServerResponse();

            if (secs >= halfcool)
            {
                try
                {
                    Log($"Debug: Starting analysis of {CurImg.image_path}...", AiUrl.CurSrv, cam.name, CurImg.image_path);

                    // Wait up to 30 seconds to gain access to the file that was just created.This should
                    //prevent the need to retry in the detection routine
                    sw.Restart();

                    Global.WaitFileAccessResult result = await Global.WaitForFileAccessAsync(CurImg.image_path, FileSystemRights.Read, FileShare.Read, 30000, 20);

                    sw.Stop();

                    CurImg.FileLockMS = sw.ElapsedMilliseconds;

                    if (result.Success)
                    {

                        string fldr = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), "LastCamImages");
                        string file = Path.Combine(fldr, $"{cam.name}-Last.jpg");
                        //Create a copy of the current image for use in mask manager when the original image was deleted
                        if ((DateTime.Now - LastImageBackupTime.Read()).TotalMinutes >= 60 || !File.Exists(file))
                        {
                            if (!Directory.Exists(fldr))
                                Directory.CreateDirectory(fldr);
                            File.Copy(CurImg.image_path, file, true);
                            LastImageBackupTime.Write(DateTime.Now);
                        }


                        if (IsValidImage(CurImg))
                        {
                            asr = await GetDetectionsFromAIServer(CurImg, AiUrl, cam);
                        }
                        else
                            asr.Error = $"Error: Invalid image file: {filename}";

                        if (asr.Success)  //returns success if we get a valid response back from AI server EVEN if no detections
                        {

                            Log($"Debug: (2/6) Posted in {asr.SWPostTime}ms, StatusCode='{asr.StatusCode}', Received a {asr.JsonString.Length} byte JSON response: '{asr.JsonString.Truncate(32, true)}'", AiUrl.CurSrv, cam.name, CurImg.image_path);
                            Log($"Debug: (3/6) Processing results...", AiUrl.CurSrv, cam.name, CurImg.image_path);


                            List<string> objects = new List<string>(); //list that will be filled with all objects that were detected and are triggering_objects for the camera
                            List<float> objects_confidence = new List<float>(); //list containing ai confidence value of object at same position in List objects
                            List<string> objects_position = new List<string>(); //list containing object positions (xmin, ymin, xmax, ymax)

                            List<string> irrelevant_objects = new List<string>(); //list that will be filled with all irrelevant objects
                            List<float> irrelevant_objects_confidence = new List<float>(); //list containing ai confidence value of irrelevant object at same position in List objects
                            List<string> irrelevant_objects_position = new List<string>(); //list containing irrelevant object positions (xmin, ymin, xmax, ymax)


                            int masked_counter = 0; //this value is incremented if an object is in a masked area
                            int threshold_counter = 0; // this value is incremented if an object does not satisfy the confidence limit requirements
                            int irrelevant_counter = 0; // this value is incremented if an irrelevant (but not masked or out of range) object is detected
                            int error_counter = 0;

                            //if we are not using the local deepstack windows version, this means nothing:
                            DeepStackServerControl.IsActivated = true;

                            if (asr.Predictions.Count() > 0)
                            {
                                //print every detected object with the according confidence-level
                                Log($"Debug:    Detected objects:", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                foreach (ClsPrediction pred in asr.Predictions)
                                {
                                    pred.AnalyzePrediction();

                                    string clr = "";
                                    if (pred.Result != ResultType.Error)
                                        DeepStackServerControl.VisionDetectionRunning = true;

                                    if (pred.Result == ResultType.Relevant)
                                    {
                                        objects.Add(pred.Label);
                                        objects_confidence.Add(pred.Confidence);
                                        objects_position.Add($"{pred.XMin},{pred.YMin},{pred.XMax},{pred.YMax}");
                                        clr = "{" + AppSettings.Settings.RectRelevantColor.Name + "}";
                                    }
                                    else
                                    {
                                        clr = "{" + AppSettings.Settings.RectIrrelevantColor.Name + "}";
                                        irrelevant_objects.Add(pred.Label);
                                        irrelevant_objects_confidence.Add(pred.Confidence);
                                        string position = $"{pred.XMin},{pred.YMin},{pred.XMax},{pred.YMax}";
                                        irrelevant_objects_position.Add(position);

                                        if (pred.Result == ResultType.NoConfidence)
                                        {
                                            threshold_counter++;
                                        }
                                        else if (pred.Result == ResultType.ImageMasked || pred.Result == ResultType.DynamicMasked || pred.Result == ResultType.StaticMasked)
                                        {
                                            clr = "{" + AppSettings.Settings.RectMaskedColor.Name + "}";
                                            masked_counter++;
                                        }
                                        else if (pred.Result == ResultType.UnwantedObject)
                                        {
                                            irrelevant_counter++;
                                        }
                                        else if (pred.Result == ResultType.Error)
                                        {
                                            clr = "{red}";
                                            error_counter++;
                                        }
                                    }

                                    if (pred.Result == ResultType.Relevant || pred.Result == ResultType.Error)
                                        Log($"     {clr}Result='{pred.Result}', Detail='{pred.ToString()}', ObjType='{pred.ObjType}', DynMaskResult='{pred.DynMaskResult}', DynMaskType='{pred.DynMaskType}', ImgMaskResult='{pred.ImgMaskResult}', ImgMaskType='{pred.ImgMaskType}'", AiUrl.CurSrv, cam.name, CurImg.image_path);
                                    else
                                        Log($"Debug:     {clr}Result='{pred.Result}', Detail='{pred.ToString()}', ObjType='{pred.ObjType}', DynMaskResult='{pred.DynMaskResult}', DynMaskType='{pred.DynMaskType}', ImgMaskResult='{pred.ImgMaskResult}', ImgMaskType='{pred.ImgMaskType}'", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                }

                                //mark the end of AI detection for the current image
                                cam.maskManager.LastDetectionDate = DateTime.Now;

                                //sort predictions so most important are at the top
                                asr.Predictions = asr.Predictions.OrderBy(p => p.Result == ResultType.Relevant ? 1 : 999).ThenBy(p => p.ObjectPriority).ThenByDescending(p => p.Confidence).ToList();

                                string PredictionsJSON = Global.GetJSONString(asr.Predictions);

                                //if one or more objects were detected, that are 1. relevant, 2. within confidence limits and 3. outside of masked areas
                                if (objects.Count() > 0)
                                {
                                    //store these last detections for the specific camera
                                    cam.last_detections = objects;
                                    cam.last_confidences = objects_confidence;
                                    cam.last_positions = objects_position;
                                    cam.last_image_file_with_detections = CurImg.image_path;

                                    //the new way


                                    //create summary string for this detection
                                    StringBuilder detectionsTextSb = new StringBuilder();
                                    for (int i = 0; i < objects.Count(); i++)
                                    {
                                        detectionsTextSb.Append($"{objects[i]} {String.Format(AppSettings.Settings.DisplayPercentageFormat, objects_confidence[i])}; "); // String.Format("{0} ({1}%) | ", objects[i], Math.Round((objects_confidence[i] * 100), 2)));
                                    }

                                    cam.last_detections_summary = detectionsTextSb.ToString().Trim(" ;".ToCharArray());

                                    //create text string objects and confidences
                                    string objects_and_confidences = "";
                                    string object_positions_as_string = "";
                                    //for (int i = 0; i < objects.Count; i++)
                                    //{
                                    //    objects_and_confidences += $"{objects[i]} {String.Format(AppSettings.Settings.DisplayPercentageFormat, objects_confidence[i])}; ";
                                    //    object_positions_as_string += $"{objects_position[i]};";
                                    //}

                                    foreach (ClsPrediction pred in asr.Predictions)
                                    {
                                        if (pred.Result != ResultType.Relevant && AppSettings.Settings.HistoryOnlyDisplayRelevantObjects)
                                            continue;

                                        objects_and_confidences += $"{pred.ToString()}; ";
                                        object_positions_as_string += $"{pred.PositionString()};";
                                    }

                                    objects_and_confidences = objects_and_confidences.Trim(" ;".ToCharArray());

                                    Log($"Debug: The summary:" + cam.last_detections_summary, AiUrl.CurSrv, cam.name, CurImg.image_path);

                                    Log($"Debug: (5/6) Performing alert actions:", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                    hist = new History().Create(CurImg.image_path, DateTime.Now, cam.name, objects_and_confidences, object_positions_as_string, true, PredictionsJSON, AiUrl.CurSrv);

                                    await TriggerActionQueue.AddTriggerActionAsync(TriggerType.All, cam, CurImg, hist, true, !cam.Action_queued, AiUrl, ""); //make TRIGGER

                                    cam.IncrementAlerts(); //stats update
                                    Log($"Debug: (6/6) SUCCESS.", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                    //add to history list
                                    //Log($"Debug: Adding detection to history list.", AiUrl.CurSrv, cam.name);
                                    Global.CreateHistoryItem(hist);

                                }
                                //if no object fulfills all 3 requirements but there are other objects: 
                                else if (irrelevant_objects.Count() > 0)
                                {
                                    //IRRELEVANT ALERT

                                    //retrieve confidences and positions
                                    string objects_and_confidences = "";
                                    string object_positions_as_string = "";

                                    //for (int i = 0; i < irrelevant_objects.Count; i++)
                                    //{
                                    //    objects_and_confidences += $"{irrelevant_objects[i]} {String.Format(AppSettings.Settings.DisplayPercentageFormat, irrelevant_objects_confidence[i])}; "; // ({Math.Round((irrelevant_objects_confidence[i] * 100), 0)}%); ";
                                    //    object_positions_as_string += $"{irrelevant_objects_position[i]};";
                                    //}

                                    foreach (ClsPrediction pred in asr.Predictions)
                                    {
                                        //if (pred.Result != ResultType.Relevant)
                                        //{
                                        objects_and_confidences += $"{pred.ToString()}; ";
                                        object_positions_as_string += $"{pred.PositionString()};";
                                        //}
                                    }


                                    objects_and_confidences = objects_and_confidences.Trim(" ;".ToCharArray());

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
                                    if (error_counter > 0) //if other irrelevant objects, add them
                                    {
                                        text += $"{error_counter}x errors; ";
                                    }

                                    if (text != "") //remove last ";"
                                    {
                                        text = text.Remove(text.Length - 2);
                                    }

                                    Log($"Debug: {text}, so it's an irrelevant alert.", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                    Log($"Debug: (5/6) Performing CANCEL actions:", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                    hist = new History().Create(CurImg.image_path, DateTime.Now, cam.name, $"{text} : {objects_and_confidences}", object_positions_as_string, false, PredictionsJSON, AiUrl.CurSrv);

                                    await TriggerActionQueue.AddTriggerActionAsync(TriggerType.All, cam, CurImg, hist, false, !cam.Action_queued, AiUrl, ""); //make TRIGGER

                                    cam.IncrementIrrelevantAlerts(); //stats update
                                    Log($"Debug: (6/6) Camera {cam.name} caused an irrelevant alert.", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                    //add to history list
                                    Global.CreateHistoryItem(hist);
                                }
                            }
                            else
                            {
                                Log($"Debug:      ((NO DETECTED OBJECTS))", AiUrl.CurSrv, cam.name, CurImg.image_path);
                                // FALSE ALERT

                                cam.IncrementFalseAlerts(); //stats update

                                Log($"Debug: (5/6) Performing CANCEL actions:", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                hist = new History().Create(CurImg.image_path, DateTime.Now, cam.name, "false alert", "", false, "", AiUrl.CurSrv);

                                await TriggerActionQueue.AddTriggerActionAsync(TriggerType.All, cam, CurImg, hist, false, !cam.Action_queued, AiUrl, ""); //make TRIGGER

                                Log($"Debug: (6/6) Camera {cam.name} caused a false alert, nothing detected.", AiUrl.CurSrv, cam.name, CurImg.image_path);

                                //add to history list
                                Global.CreateHistoryItem(hist);
                            }


                        }
                        else
                        {
                            error = asr.Error;
                            AiUrl.IncrementError();
                            AiUrl.LastResultMessage = error;
                            Log(error, AiUrl.CurSrv, cam.name, CurImg.image_path);
                        }

                    }
                    else
                    {
                        //could not access the file for 30 seconds??   Or unexpected error
                        error = $"Error: Could not gain access to {CurImg.image_path} for {result.TimeMS}ms, with {result.ErrRetryCnt} retries, giving up.";
                        CurImg.ErrCount.AtomicIncrementAndGet();
                        CurImg.ResultMessage = error;
                        Log(error, AiUrl.CurSrv, cam.name, CurImg.image_path);
                    }

                }
                catch (Exception ex)
                {
                    error = $"ERROR: {Global.ExMsg(ex)}";
                    AiUrl.IncrementError();
                    AiUrl.LastResultMessage = error;
                    Log(error, AiUrl.CurSrv, cam.name, CurImg.image_path);
                }
                //exitfunction:
                if (!string.IsNullOrEmpty(error) && AppSettings.Settings.send_errors == true && cam.telegram_enabled)
                {
                    //bool success = await TelegramUpload(CurImg, "Error");
                    if (hist == null)
                    {
                        hist = new History().Create(CurImg.image_path, DateTime.Now, cam.name, "error", "", false, "", AiUrl.CurSrv);
                    }
                    await TriggerActionQueue.AddTriggerActionAsync(TriggerType.TelegramImageUpload, cam, CurImg, hist, false, !cam.Action_queued, AiUrl, "Error"); //make TRIGGER
                }


                //I notice deepstack takes a lot longer the very first run?

                CurImg.TotalTimeMS = (long)(DateTime.Now - CurImg.TimeAdded).TotalMilliseconds; //sw.ElapsedMilliseconds + CurImg.QueueWaitMS + CurImg.FileLockMS;
                CurImg.DeepStackTimeMS = asr.SWPostTime;
                AiUrl.LastTimeMS = asr.SWPostTime;
                tcalc.AddToCalc(CurImg.TotalTimeMS);
                AiUrl.AITimeCalcs.AddToCalc(CurImg.DeepStackTimeMS);
                qcalc.AddToCalc(CurImg.QueueWaitMS);
                fcalc.AddToCalc(CurImg.FileLockMS);

                Log($"Debug:          Total Time:  {CurImg.TotalTimeMS}ms (Count={tcalc.Count}, Min={tcalc.Min}ms, Max={tcalc.Max}ms, Avg={tcalc.Average.ToString("#####")}ms)", AiUrl.CurSrv, cam.name, CurImg.image_path);
                Log($"Debug:       AI (URL) Time:  {CurImg.DeepStackTimeMS}ms (Count={AiUrl.AITimeCalcs.Count}, Min={AiUrl.AITimeCalcs.Min}ms, Max={AiUrl.AITimeCalcs.Max}ms, Avg={AiUrl.AITimeCalcs.Average.ToString("#####")}ms)", AiUrl.CurSrv, cam.name, CurImg.image_path);
                Log($"Debug:      File lock Time:  {CurImg.FileLockMS}ms (Count={fcalc.Count}, Min={fcalc.Min}ms, Max={fcalc.Max}ms, Avg={fcalc.Average.ToString("#####")}ms)", AiUrl.CurSrv, cam.name, CurImg.image_path);
                Log($"Debug:    Image Queue Time:  {CurImg.QueueWaitMS}ms (Count={qcalc.Count}, Min={qcalc.Min}ms, Max={qcalc.Max}ms, Avg={qcalc.Average.ToString("#####")}ms)", AiUrl.CurSrv, cam.name, CurImg.image_path);
                Log($"Debug:   Image Queue Depth:  {CurImg.CurQueueSize} (Count={qsizecalc.Count}, Min={qsizecalc.Min}, Max={qsizecalc.Max}, Avg={qsizecalc.Average.ToString("#####")})", AiUrl.CurSrv, cam.name, CurImg.image_path);

            }
            else
            {
                cam.stats_skipped_images++;
                cam.stats_skipped_images_session++;
                Log($"Skipping detection for '{filename}' because cooldown has not been met for camera '{cam.name}':  '{secs.ToString("#######0.000")}' of '{halfcool.ToString("#######0.000")}' seconds (half of trigger cooldown time), Session Skip Count={cam.stats_skipped_images_session}", AiUrl.CurSrv, cam.name, CurImg.image_path);
                Global.CreateHistoryItem(new History().Create(CurImg.image_path, DateTime.Now, cam.name, $"Skipped image, cooldown was '{secs.ToString("#######0.000")}' of '{halfcool.ToString("#######0.000")}' seconds.", "", false, "", AiUrl.CurSrv));
            }

            return (error == "");

        }



        //check if detected object is outside the mask for the specific camera
        //TODO: refacotor png, bmp mask logic later. This is just a starting point. 
        public static string GetMaskFile(string cameraname)
        {
            string ret = "";
            try
            {

                //we are not using cameras folder any longer

                List<string> files = new List<string>();

                //this is for future support of storing all settings files in one folder such as AppData, or simply \SETTINGS
                files.Add(Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), $"{cameraname}.bmp"));
                files.Add(Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), $"{cameraname}.png"));
                //original cameras folder
                //files.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cameras", $"{cameraname}.bmp"));
                //files.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cameras", $"{cameraname}.png"));

                foreach (string fil in files)
                {
                    if (System.IO.File.Exists(fil))
                    {
                        ret = fil;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(ret))
                {
                    ret = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), $"{cameraname}.bmp");
                }


            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }
            return ret;

        }
        public static MaskResultInfo Outsidemask(string cameraname, double xmin, double xmax, double ymin, double ymax, int width, int height)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            //Log($"      Checking if object is outside privacy mask of {cameraname}:");
            //Log("         Loading mask file...");
            MaskResultInfo ret = new MaskResultInfo();
            string fileType = "";
            string foundfile = "";
            try
            {

                foundfile = GetMaskFile(cameraname);

                if (System.IO.File.Exists(foundfile))
                {
                    Log($"Debug:     ->Using found mask file {foundfile}...");
                    fileType = Path.GetExtension(foundfile).ToLower();
                }
                else
                {
                    Log("Debug:     ->Camera has no mask, the object is OUTSIDE of the masked area.");
                    ret.IsMasked = false;
                    ret.MaskType = MaskType.None;
                    ret.Result = MaskResult.NoMaskImageFile;
                    return ret;
                }

                //load mask file (in the image all places that have color (transparency > 9 [0-255 scale]) are masked)
                using (var mask_img = new Bitmap(foundfile))
                {
                    //if any coordinates of the object are outside of the mask image, th mask image must be too small.
                    if (mask_img.Width != width || mask_img.Height != height)
                    {
                        Log($"ERROR: The resolution of the mask '{foundfile}' does not equal the resolution of the processed image. Skipping privacy mask feature. Image: {width}x{height}, Mask: {mask_img.Width}x{mask_img.Height}");
                        ret.IsMasked = false;
                        ret.MaskType = MaskType.Image;
                        ret.Result = MaskResult.Error;
                        return ret;
                    }

                    //relative x and y locations of the 9 detection points
                    double[] x_factor = new double[] { 0.25, 0.5, 0.75, 0.25, 0.5, 0.75, 0.25, 0.5, 0.75 };
                    double[] y_factor = new double[] { 0.25, 0.25, 0.25, 0.5, 0.5, 0.5, 0.75, 0.75, 0.75 };

                    //int result = 0; //counts how many of the 9 points are outside of masked area(s)

                    //check the transparency of the mask image in all 9 detection points
                    for (int i = 0; i < 9; i++)
                    {
                        //get image point coordinates (and converting double to int)
                        int x = (int)(xmin + (xmax - xmin) * x_factor[i]);
                        int y = (int)(ymin + (ymax - ymin) * y_factor[i]);

                        // Get the color of the pixel
                        System.Drawing.Color pixelColor = mask_img.GetPixel(x, y);

                        if (fileType == ".png")
                        {
                            //if the pixel is transparent (A refers to the alpha channel), the point is outside of masked area(s)
                            if (pixelColor.A < 10)
                            {
                                ret.ImagePointsOutsideMask++;
                            }
                        }
                        else
                        {
                            if (pixelColor.A == 0)  // object is in a transparent section of the image (not masked)
                            {
                                ret.ImagePointsOutsideMask++;
                            }
                        }

                    }

                    if (ret.ImagePointsOutsideMask > 4) //if 5 or more of the 9 detection points are outside of masked areas, the majority of the object is outside of masked area(s)
                    {
                        if (ret.ImagePointsOutsideMask == 9)
                        {
                            Log($"Debug:      ->ALL of the object is OUTSIDE of masked area(s). ({ret.ImagePointsOutsideMask} of 9 points)");
                            ret.IsMasked = false;
                            ret.MaskType = MaskType.Image;
                            ret.Result = MaskResult.MajorityOutsideMask;
                            return ret;
                        }
                        else
                        {
                            Log($"Debug:      ->Most of the object is OUTSIDE of masked area(s). ({ret.ImagePointsOutsideMask} of 9 points)");
                            ret.IsMasked = false;
                            ret.MaskType = MaskType.Image;
                            ret.Result = MaskResult.CompletlyOutsideMask;
                            return ret;
                        }
                    }

                    else //if 4 or less of 9 detection points are outside, then 5 or more points are in masked areas and the majority of the object is so too
                    {
                        if (ret.ImagePointsOutsideMask == 0)
                        {
                            Log($"Debug:      ->All of the object is INSIDE a masked area. ({ret.ImagePointsOutsideMask} of 9 points)");
                            ret.IsMasked = true;
                            ret.MaskType = MaskType.Image;
                            ret.Result = MaskResult.CompletlyInsideMask;
                            return ret;

                        }
                        else
                        {
                            Log($"Debug:      ->Most of the object is INSIDE a masked area. ({ret.ImagePointsOutsideMask} of 9 points)");
                            ret.IsMasked = true;
                            ret.MaskType = MaskType.Image;
                            ret.Result = MaskResult.MajorityInsideMask;
                            return ret;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Log($"ERROR while loading the mask file {foundfile}: {Global.ExMsg(ex)}");
                ret.IsMasked = false;
                ret.MaskType = MaskType.Image;
                ret.Result = MaskResult.Error;
                return ret;
            }

        }

        public static string ReplaceParams(Camera cam, History hist, ClsImageQueueItem CurImg, string instr)
        {
            string ret = instr;

            try
            {
                string camname = "TESTCAMERANAME";
                string prefix = "TESTCAMERANAMEPREFIX";
                string imgpath = "C:\\TESTFILE.jpg";

                if (cam != null)
                {
                    camname = cam.name;
                    prefix = cam.prefix;
                }

                if (CurImg != null)
                {
                    imgpath = CurImg.image_path;
                }
                else if (hist != null)
                {
                    imgpath = hist.Filename;
                }
                else if (cam != null)
                {
                    imgpath = cam.last_image_file;
                }

                //handle environment variables too:
                ret = Environment.ExpandEnvironmentVariables(ret);

                //handle date and time:
                ret = Global.ReplaceCaseInsensitive(ret, "%date%", DateTime.Now.ToString("d"));
                ret = Global.ReplaceCaseInsensitive(ret, "%time%", DateTime.Now.ToString("t"));
                ret = Global.ReplaceCaseInsensitive(ret, "%datetime%", DateTime.Now.ToString(AppSettings.Settings.DateFormat));


                //handle custom variables
                ret = Global.ReplaceCaseInsensitive(ret, "[camera]", camname);
                ret = Global.ReplaceCaseInsensitive(ret, "[prefix]", prefix);
                ret = Global.ReplaceCaseInsensitive(ret, "[imagepath]", imgpath); //gives the full path of the image that caused the trigger
                ret = Global.ReplaceCaseInsensitive(ret, "[imagepathescaped]", Uri.EscapeUriString(imgpath)); //gives the full path of the image that caused the trigger
                ret = Global.ReplaceCaseInsensitive(ret, "[imagefilename]", Path.GetFileName(imgpath)); //gives the image name of the image that caused the trigger
                ret = Global.ReplaceCaseInsensitive(ret, "[imagefilenamenoext]", Path.GetFileNameWithoutExtension(imgpath)); //gives the image name of the image that caused the trigger

                ret = Global.ReplaceCaseInsensitive(ret, "[username]", AppSettings.Settings.DefaultUserName); //gives the image name of the image that caused the trigger
                ret = Global.ReplaceCaseInsensitive(ret, "[password]", Global.DecryptString(AppSettings.Settings.DefaultPasswordEncrypted)); //gives the image name of the image that caused the trigger
                ret = Global.ReplaceCaseInsensitive(ret, "[blueirisserverip]", AppSettings.Settings.BlueIrisServer.Trim()); //gives the image name of the image that caused the trigger
                ret = Global.ReplaceCaseInsensitive(ret, "[blueirisurl]", BlueIrisInfo.URL); //gives the image name of the image that caused the trigger


                if (hist != null)
                {
                    List<ClsPrediction> preds = Global.SetJSONString<List<ClsPrediction>>(hist.PredictionsJSON);

                    if (preds != null && preds.Count > 0)
                    {
                        string detections = "";
                        string confidences = "";
                        foreach (ClsPrediction pred in preds)
                        {
                            if (pred.Result != ResultType.Relevant && AppSettings.Settings.HistoryOnlyDisplayRelevantObjects)
                                continue;

                            confidences += pred.ConfidenceString() + ",";
                            detections += pred.ToString() + ",";
                        }
                        ret = Global.ReplaceCaseInsensitive(ret, "[summarynonescaped]", hist.Detections); //summary text including all detections and confidences, p.e."person (91,53%)"
                        ret = Global.ReplaceCaseInsensitive(ret, "[summary]", Uri.EscapeUriString(hist.Detections)); //summary text including all detections and confidences, p.e."person (91,53%)"
                        ret = Global.ReplaceCaseInsensitive(ret, "[detection]", preds[0].Label); //only gives first detection (maybe not most relevant one)
                        ret = Global.ReplaceCaseInsensitive(ret, "[position]", preds[0].PositionString());
                        ret = Global.ReplaceCaseInsensitive(ret, "[confidence]", preds[0].ConfidenceString());
                        ret = Global.ReplaceCaseInsensitive(ret, "[detections]", detections);
                        ret = Global.ReplaceCaseInsensitive(ret, "[confidences]", confidences);
                    }
                    else
                    {
                        ret = Global.ReplaceCaseInsensitive(ret, "[summarynonescaped]", "No Summary Found");
                        ret = Global.ReplaceCaseInsensitive(ret, "[summary]", "No Summary Found"); //summary text including all detections and confidences, p.e."person (91,53%)"
                        ret = Global.ReplaceCaseInsensitive(ret, "[detection]", "No Detection Found"); //only gives first detection (maybe not most relevant one)
                        ret = Global.ReplaceCaseInsensitive(ret, "[position]", "No Position Found");
                        ret = Global.ReplaceCaseInsensitive(ret, "[confidence]", "No Confidence Found");
                        ret = Global.ReplaceCaseInsensitive(ret, "[detections]", "No Detections Found");
                        ret = Global.ReplaceCaseInsensitive(ret, "[confidences]", "No Confidences Found");
                    }
                }
                else if (cam != null)
                {
                    if (cam.last_detections != null && cam.last_detections.Count > 0)
                    {
                        ret = Global.ReplaceCaseInsensitive(ret, "[summarynonescaped]", cam.last_detections_summary); //summary text including all detections and confidences, p.e."person (91,53%)"
                        ret = Global.ReplaceCaseInsensitive(ret, "[summary]", Uri.EscapeUriString(cam.last_detections_summary)); //summary text including all detections and confidences, p.e."person (91,53%)"
                        ret = Global.ReplaceCaseInsensitive(ret, "[detection]", cam.last_detections.ElementAt(0)); //only gives first detection (maybe not most relevant one)
                        ret = Global.ReplaceCaseInsensitive(ret, "[position]", cam.last_positions.ElementAt(0));
                        ret = Global.ReplaceCaseInsensitive(ret, "[confidence]", cam.last_confidences.ElementAt(0).ToString());
                        ret = Global.ReplaceCaseInsensitive(ret, "[detections]", string.Join(",", cam.last_detections));
                        ret = Global.ReplaceCaseInsensitive(ret, "[confidences]", string.Join(",", cam.last_confidences));
                    }
                    else
                    {
                        ret = Global.ReplaceCaseInsensitive(ret, "[summarynonescaped]", "No Summary Found");
                        ret = Global.ReplaceCaseInsensitive(ret, "[summary]", "No Summary Found"); //summary text including all detections and confidences, p.e."person (91,53%)"
                        ret = Global.ReplaceCaseInsensitive(ret, "[detection]", "No Detection Found"); //only gives first detection (maybe not most relevant one)
                        ret = Global.ReplaceCaseInsensitive(ret, "[position]", "No Position Found");
                        ret = Global.ReplaceCaseInsensitive(ret, "[confidence]", "No Confidence Found");
                        ret = Global.ReplaceCaseInsensitive(ret, "[detections]", "No Detections Found");
                        ret = Global.ReplaceCaseInsensitive(ret, "[confidences]", "No Confidences Found");
                    }

                }

            }
            catch (Exception ex)
            {

                Log($"Error: {Global.ExMsg(ex)}");
            }

            return ret;

        }


        public static Camera GetCamera(String ImageOrNameOrPrefix, bool ReturnDefault = true)
        {
            Camera cam = null;
            try
            {
                ImageOrNameOrPrefix = ImageOrNameOrPrefix.Trim();

                string pth = "";
                string fname = "";
                int index = -1;
                //search by path or filename prefix if we are passed a full path to image file
                if (ImageOrNameOrPrefix.Contains("\\"))
                {
                    pth = Path.GetDirectoryName(ImageOrNameOrPrefix);
                    fname = Path.GetFileNameWithoutExtension(ImageOrNameOrPrefix);
                    
                    //&CAM.%Y%m%d_%H%M%S
                    //AIFOSCAMDRIVEWAY.20200827_131840312.jpg
                    //sgrtgrdg - Kopie (2).jpg
                    if (fname.Contains(".") || fname.Contains("-"))
                    {

                        string fileprefix = "";

                        if (fname.Contains("."))
                        {
                            fileprefix = Path.GetFileNameWithoutExtension(ImageOrNameOrPrefix).Split('.')[0].Trim(); //get prefix of inputted file
                        }
                        else if (fname.Contains("-"))
                        {
                            fileprefix = Path.GetFileNameWithoutExtension(ImageOrNameOrPrefix).Split('-')[0].Trim(); //get prefix of inputted file
                        }

                        index = AppSettings.Settings.CameraList.FindIndex(x => string.Equals(x.prefix, fileprefix, StringComparison.OrdinalIgnoreCase)); //get index of camera with same prefix, is =-1 if no camera has the same prefix 

                        if (index > -1)
                        {
                            //found
                            cam = AppSettings.Settings.CameraList[index];
                        }
                        else
                        {
                            //fall back to camera name, prefix and optional wildcard use
                            //find by name or prefix
                            //allow to use wildcards
                            if (ImageOrNameOrPrefix.Contains("*") || ImageOrNameOrPrefix.Contains("?"))
                            {
                                foreach (Camera ccam in AppSettings.Settings.CameraList)
                                {
                                    if (Regex.IsMatch(ccam.name, Global.WildCardToRegular(ImageOrNameOrPrefix), RegexOptions.IgnoreCase) || Regex.IsMatch(ccam.prefix, Global.WildCardToRegular(ImageOrNameOrPrefix), RegexOptions.IgnoreCase))
                                    {
                                        cam = ccam;
                                        break;
                                    }
                                }

                            }
                            else  //find by exact name or prefix
                            {
                                foreach (Camera ccam in AppSettings.Settings.CameraList)
                                {
                                    if (string.Equals(ccam.name, ImageOrNameOrPrefix, StringComparison.OrdinalIgnoreCase) || string.Equals(ccam.prefix, ImageOrNameOrPrefix, StringComparison.OrdinalIgnoreCase))
                                    {
                                        cam = ccam;
                                        break;
                                    }
                                }

                            }
                        }
                    }

                    
                    if (index > -1 && cam == null)
                    {
                        //found
                        cam = AppSettings.Settings.CameraList[index];
                    }

                }
                else
                {
                    //find by name or prefix
                    //allow to use wildcards
                    if (ImageOrNameOrPrefix.Contains("*") || ImageOrNameOrPrefix.Contains("?"))
                    {
                        foreach (Camera ccam in AppSettings.Settings.CameraList)
                        {
                            if (Regex.IsMatch(ccam.name, Global.WildCardToRegular(ImageOrNameOrPrefix),RegexOptions.IgnoreCase) || Regex.IsMatch(ccam.prefix, Global.WildCardToRegular(ImageOrNameOrPrefix), RegexOptions.IgnoreCase))
                            {
                                cam = ccam;
                                break;
                            }
                        }

                    }
                    else  //find by exact name or prefix
                    {
                        foreach (Camera ccam in AppSettings.Settings.CameraList)
                        {
                            if (string.Equals(ccam.name, ImageOrNameOrPrefix, StringComparison.OrdinalIgnoreCase) || string.Equals(ccam.prefix, ImageOrNameOrPrefix, StringComparison.OrdinalIgnoreCase))
                            {
                                cam = ccam;
                                break;
                            }
                        }

                    }

                }

                //if it is not found, search by the camera input path
                if (cam == null && !string.IsNullOrEmpty(pth))
                {
                    foreach (Camera ccam in AppSettings.Settings.CameraList)
                    {
                        //If the watched path is c:\bi\cameraname but the full path of found file is 
                        //                       c:\bi\cameraname\date\time\randomefilename.jpg 
                        //we just check the beginning of the path
                        if (!String.IsNullOrWhiteSpace(ccam.input_path) && ccam.input_path.Trim().StartsWith(pth, StringComparison.OrdinalIgnoreCase))
                        {
                            //found
                            cam = ccam;
                            break;

                        }
                    }

                }

                //if we didnt find a camera see if there is a default camera name we can use without a prefix
                if (cam == null)
                {
                    Log($"Debug: No enabled camera with the same filename, cameraname, or prefix found for '{ImageOrNameOrPrefix}'");
                    //check if there is a default camera which accepts any prefix, select it
                    if (ReturnDefault)
                    {
                        if (AppSettings.Settings.CameraList.Exists(x => x.prefix.Trim() == ""))
                        {
                            int i = AppSettings.Settings.CameraList.FindIndex(x => x.prefix.Trim() == "");
                            cam = AppSettings.Settings.CameraList[i];
                            Log($"(   Found a default camera: '{cam.name}')");
                        }
                        else
                        {
                            Log("WARNING: No default camera found. Aborting.");
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                Log(Global.ExMsg(ex));
            }

            if (cam == null)
            {
                Log($"Debug: Cannot match '{ImageOrNameOrPrefix}' to an existing camera.");
            }

            return cam;

        }

        public static float GetObjIntersectPercent(Rectangle masterRectangle, Rectangle compareRectangle)
        {

            Rectangle objIntersect = Rectangle.Intersect(masterRectangle, compareRectangle);

            float percentage = (((objIntersect.Width * objIntersect.Height) * 2) * 100f) /
                   ((compareRectangle.Width * compareRectangle.Height) + (compareRectangle.Width * masterRectangle.Height));

            return percentage;
        }

    }
}
