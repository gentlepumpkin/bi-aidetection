using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AITool
{

    public enum TriggerType
    {
        Unknown,
        All,
        DownloadURL,
        PostURL,
        TelegramImageUpload,
        TelegramText,
        Sound,
        Run,
        MQTT,
        Pushover
    }
    public class CameraTriggerAction
    {
        public TriggerType Type = TriggerType.Unknown;
        public string Description = "";
        public string LastResponse = "";
    }

    public class ImageResItem : IEquatable<ImageResItem>
    {
        public int Width = 0;
        public int Height = 0;
        public long Count = 0;
        public string LastFileName = "";
        public long LastFileSize = 0;
        public float LastFileDPI = 0;
        public DateTime LastSeenDate = DateTime.MinValue;

        public ImageResItem() { }
        public ImageResItem(ClsImageQueueItem CurImg)
        {
            this.Count = 1;
            this.Height = CurImg.Height;
            this.Width = CurImg.Width;
            this.LastFileName = CurImg.image_path;
            this.LastSeenDate = CurImg.TimeCreated;
            this.LastFileSize = CurImg.FileSize;
            this.LastFileDPI = CurImg.DPI;

        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ImageResItem);
        }

        public bool Equals(ImageResItem other)
        {
            return other != null &&
                   Width == other.Width &&
                   Height == other.Height;
        }

        public override int GetHashCode()
        {
            int hashCode = 859600377;
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{this.Width}x{this.Height}";
        }

        public static bool operator ==(ImageResItem left, ImageResItem right)
        {
            return EqualityComparer<ImageResItem>.Default.Equals(left, right);
        }

        public static bool operator !=(ImageResItem left, ImageResItem right)
        {
            return !(left == right);
        }
    }
    public class Camera
    {
        public string Name = "";
        public string Prefix = "";
        public string BICamName = "";
        public string MaskFileName = "";
        public string triggering_objects_as_string = "person, bear, elephant, car, truck, bicycle, motorcycle, bus, dog, horse, boat, train, airplane, zebra, giraffe, cow, sheep, cat, bird";
        public string additional_triggering_objects_as_string = "Chicken Turtle, Hummingbird Hawk-moth, Goblin Shark";
        public string[] triggering_objects = new string[0];
        public string trigger_urls_as_string = "";
        public string[] trigger_urls = new string[0];
        public string cancel_urls_as_string = "";
        public string[] cancel_urls = new string[0];
        //public List<CameraTriggerAction> trigger_action_list = new List<CameraTriggerAction>();
        public bool trigger_url_cancels = false;
        public bool telegram_enabled = false;
        public string telegram_caption = "[camera] - [SummaryNonEscaped]";  //cam.name + " - " + cam.last_detections_summary
        public string telegram_triggering_objects = "";
        public string telegram_chatid = "";
        public string telegram_active_time_range = "00:00:00-23:59:59";
        public bool enabled = true;
        public double cooldown_time = 0;
        public int cooldown_time_seconds = 5;
        public int threshold_lower = 1;
        public int threshold_upper = 100;

        //watch folder for each camera
        public string input_path = "";
        public bool input_path_includesubfolders = false;

        public bool Action_image_copy_enabled = false;
        public bool Action_image_merge_detections = false;
        public bool Action_image_merge_detections_makecopy = true;
        public long Action_image_merge_jpegquality = 90;
        public string Action_network_folder = "";
        public string Action_network_folder_filename = "[ImageFilenameNoExt]";
        public int Action_network_folder_purge_older_than_days = 30;
        public bool Action_RunProgram = false;
        public string Action_RunProgramString = "";
        public string Action_RunProgramArgsString = "";
        public bool Action_PlaySounds = false;
        public string Action_Sounds = @"person ; C:\example\YOYO.WAV | bird ; C:\example\TWEET.WAV";

        public bool Action_mqtt_enabled = false;
        public string Action_mqtt_topic = "ai/[camera]/motion";
        public string Action_mqtt_payload = "[detections]";
        public string Action_mqtt_topic_cancel = "ai/[camera]/motioncancel";
        public string Action_mqtt_payload_cancel = "cancel";
        public bool Action_mqtt_retain_message = false;
        public bool Action_mqtt_send_image = false;
        public bool Action_queued = false;

        public bool Action_pushover_enabled = false;
        public string Action_pushover_title = "AI Detection - [camera]";
        public string Action_pushover_message = "[SummaryNonEscaped]";
        public string Action_pushover_device = "";
        public string Action_pushover_triggering_objects = "";
        public string Action_pushover_Priority = "Normal";
        public string Action_pushover_Sound = "pushover";
        public int Action_pushover_retry_seconds = 60;
        public int Action_pushover_expire_seconds = 10800;
        public string Action_pushover_retrycallback_url = "";
        public string Action_pushover_SupplementaryUrl = "";
        public string Action_pushover_active_time_range = "00:00:00-23:59:59";

        [JsonIgnore]
        public bool Action_Cancel_Timer_Enabled = false;
        [JsonIgnore]
        public DateTime Action_Cancel_Start_Time = DateTime.MinValue;

        public MaskManager maskManager = new MaskManager();
        public int mask_brush_size = 35;

        //stats
        public int stats_alerts = 0; //alert image contained relevant object counter
        public int stats_false_alerts = 0; //alert image contained no object counter
        public int stats_irrelevant_alerts = 0; //alert image contained irrelevant object counter

        public int stats_skipped_images = 0; //Images that were skipped due to cooldown or retry count

        [JsonIgnore]
        public int stats_skipped_images_session = 0; //Images that were skipped due to cooldown or retry count


        public string last_image_file = "";
        public string last_image_file_with_detections = "";

        public int XOffset = 0;   //these are for when deepstack is having a problem with detection rectangle being in the wrong location
        public int YOffset = 0;   //  Can be negative numbers

        public string ImageAdjustProfile = "Default";

        //Keep a list of image resolutions and the last image file name with that resolution.  This will help us keep track of which image mask to use
        public List<ImageResItem> ImageResolutions = new List<ImageResItem>();

        [JsonIgnore]
        public ThreadSafe.Datetime last_trigger_time = new ThreadSafe.Datetime(DateTime.MinValue);
        [JsonIgnore]
        public List<string> last_detections = new List<string>(); //stores objects that were detected last
        [JsonIgnore]
        public List<float> last_confidences = new List<float>(); //stores last objects confidences
        [JsonIgnore]
        public List<string> last_positions = new List<string>(); //stores last objects positions
        [JsonIgnore]
        public String last_detections_summary; //summary text of last detection
        [JsonIgnore]
        private object CamLock = new object();

        public string GetMaskFile(bool MustExist, ClsImageQueueItem CurImg = null, ImageResItem ir = null)
        {
            string ret = "";

            lock (CamLock)
            {
                try
                {
                    String resstr = "";

                    if (CurImg != null)
                    {
                        resstr = $"_{CurImg.Width}x{CurImg.Height}";
                    }
                    else if (ir == null && this.ImageResolutions.Count > 0)
                    {
                        ir = this.ImageResolutions[0];  //the first one should be the most recent image processed because of the sort.
                        resstr = $"_{ir.Width}x{ir.Height}";
                    }
                    else if (ir != null)
                    {
                        resstr = $"_{ir.Width}x{ir.Height}";
                    }

                    string CamMaskFile = "";
                    if (!string.IsNullOrEmpty(this.MaskFileName))
                    {
                        if (this.MaskFileName.Contains("\\") && this.MaskFileName.Contains("."))
                            CamMaskFile = this.MaskFileName;
                        else if (this.MaskFileName.Contains("."))
                            CamMaskFile = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), $"{this.MaskFileName}");
                        else
                            CamMaskFile = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), $"{this.MaskFileName}.bmp");

                        //Add WidthxHeight to filename
                        string ResFile = Path.Combine(Path.GetDirectoryName(CamMaskFile), $"{Path.GetFileNameWithoutExtension(CamMaskFile)}{resstr}.bmp");

                        bool resempty = string.IsNullOrEmpty(resstr);
                        bool cammaskexist = File.Exists(CamMaskFile);
                        bool resexist = File.Exists(ResFile);
                        if (!resempty && cammaskexist && !Path.GetFileName(CamMaskFile).Contains("_") && !resexist)
                        {
                            //lets rename it to appropriate ResFile name
                            string tmpresstr = "";
                            using (FileStream fileStream = new FileStream(CamMaskFile, FileMode.Open, FileAccess.Read))
                            {
                                using Image img = Image.FromStream(fileStream, false, false);
                                tmpresstr = $"_{img.Width}x{img.Height}";
                            }
                            if (tmpresstr == resstr)
                            {
                                AITOOL.Log($"Debug: Renaming mask file from '{CamMaskFile}' to '{ResFile}'...");
                                File.Move(CamMaskFile, ResFile);
                            }
                            else
                            {
                                AITOOL.Log($"Debug: Cannot rename mask file because it does not match the current image resolution of '{resstr}' (!={tmpresstr}):  MaskFile='{CamMaskFile}'...");
                            }
                        }
                        else
                        {
                            //AITOOL.Log($"Debug: ResEmpty={resempty}, CamMaskExist={cammaskexist}, ResMaskFileExist={resexist}, CurRes={resstr}, MaskRes={resstr}, CamMaskFile='{CamMaskFile}', ResMaskFile={ResFile}...");
                        }

                        if (MustExist)
                        {
                            if (File.Exists(ResFile))
                            {
                                return ResFile;
                            }
                            else
                            {
                                return "";
                            }
                        }
                        else
                        {
                            return ResFile;
                        }

                    }

                }
                catch (Exception ex)
                {

                    AITOOL.Log("Error: " + Global.ExMsg(ex));
                }

            }

            return ret;

        }
        public bool UpdateImageResolutions(ClsImageQueueItem CurImg)
        {

            bool ret = false;

            if (!CurImg.IsValid())
                return ret;

            lock (CamLock)
            {
                ImageResItem newri = new ImageResItem(CurImg);

                int idx = this.ImageResolutions.IndexOf(newri);
                bool updated = false;

                if (idx > -1)
                {
                    if (CurImg.TimeCreated > this.ImageResolutions[idx].LastSeenDate)
                    {
                        updated = true;
                        this.ImageResolutions[idx].LastFileName = CurImg.image_path;
                        this.ImageResolutions[idx].LastSeenDate = CurImg.TimeCreated;
                        this.ImageResolutions[idx].LastFileSize = CurImg.FileSize;
                        this.ImageResolutions[idx].LastFileDPI = CurImg.DPI;
                        this.ImageResolutions[idx].Count++;
                    }
                }
                else
                {
                    ret = true;
                    this.ImageResolutions.Add(newri);
                }

                //sort so most recent is at top of list

                if (ret || updated)
                    this.ImageResolutions = this.ImageResolutions.OrderByDescending(x => x.LastSeenDate).ToList();

            }

            return ret;

        }
        public async Task ScanImagesAsync(int MaxFiles = 3000, int MaxTimeScanningMS = 60000, int MaxDaysOld = -4)
        {
            await Task.Run(() => ScanImages(MaxFiles,MaxTimeScanningMS,MaxDaysOld));
        }
        public void ScanImages(int MaxFiles = 3000, int MaxTimeScanningMS = 60000, int MaxDaysOld = -4)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            List<FileInfo> files = new List<FileInfo>();

            Stopwatch fscansw = Stopwatch.StartNew();

            if (!string.IsNullOrEmpty(this.input_path) && Directory.Exists(this.input_path))
            {
                List<FileInfo> newfiles = Global.GetFiles(this.input_path, $"{this.Prefix}*.jpg", this.input_path_includesubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, DateTime.Now.AddDays(MaxDaysOld), DateTime.Now, MaxFiles);
                files.AddRange(newfiles);
                AITOOL.Log($"Debug: Found {newfiles.Count} {this.Prefix}*.jpg files in {this.input_path}");
            }

            if (files.Count < MaxFiles && !string.IsNullOrEmpty(AppSettings.Settings.input_path) && AppSettings.Settings.input_path != this.input_path && Directory.Exists(AppSettings.Settings.input_path))
            {
                List<FileInfo> newfiles = Global.GetFiles(AppSettings.Settings.input_path, $"{this.Prefix}*.jpg", AppSettings.Settings.input_path_includesubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly, DateTime.Now.AddDays(MaxDaysOld), DateTime.Now, MaxFiles);
                files.AddRange(newfiles);
                AITOOL.Log($"Debug: Found {newfiles.Count} {this.Prefix}*.jpg files in {AppSettings.Settings.input_path}");
            }

            if (files.Count < MaxFiles && !string.IsNullOrEmpty(this.Action_network_folder) && Directory.Exists(this.Action_network_folder)) 
            {
                List<FileInfo> newfiles = Global.GetFiles(this.Action_network_folder, $"{this.Prefix}*.jpg", SearchOption.TopDirectoryOnly, DateTime.Now.AddDays(MaxDaysOld), DateTime.Now, MaxFiles);
                files.AddRange(newfiles);
                AITOOL.Log($"Debug: Found {newfiles.Count} {this.Prefix}*.jpg files in {this.Action_network_folder}");
            }

            if (!string.IsNullOrEmpty(this.last_image_file) && File.Exists(this.last_image_file))
            {
                if (!files.Any(x => string.Equals(x.FullName, this.last_image_file, StringComparison.OrdinalIgnoreCase)))
                {
                    files.Add(new FileInfo(this.last_image_file));
                }
            }

            files = files.OrderByDescending(t => t.CreationTime).ToList();

            fscansw.Stop();


            int cnt = 0;
            int updated = 0;
            int invalid = 0;

            AITOOL.Log($"Debug: Found {files.Count} images in {fscansw.ElapsedMilliseconds} ms. Scanning images...");

            Stopwatch sw = Stopwatch.StartNew();

            foreach (FileInfo fi in files)
            {

                try
                {
                    ClsImageQueueItem img = new ClsImageQueueItem(fi.FullName, 0, true);
                    if (img.IsValid())
                    {
                        if (this.UpdateImageResolutions(img))
                            updated++;

                        cnt++;
                    }
                    else
                    {
                        invalid++;
                    }
                }
                catch (Exception ex) 
                {
                    invalid++;
                    AITOOL.Log($"Debug: {fi.Name}: {ex.Message}");
                }

                if (sw.ElapsedMilliseconds >= MaxTimeScanningMS)
                {
                    AITOOL.Log($"Debug: Max search time exceeded: {sw.ElapsedMilliseconds} ms >= {MaxTimeScanningMS} ms");
                    break;
                }

            }

            sw.Stop();

            string reseseses = "";
            foreach (ImageResItem res in this.ImageResolutions)
            {
                reseseses += $"{res.ToString()};";
            }

            if (string.IsNullOrEmpty(this.last_image_file) && files.Count > 0)
                this.last_image_file = files[0].FullName;

            AITOOL.Log($"Debug: {cnt} of {files.Count} image files processed, {updated} new resolutions found ({invalid} invalid) in {sw.ElapsedMilliseconds} ms (Max={MaxTimeScanningMS} ms), {this.ImageResolutions.Count()} different image resolutions found: {reseseses}");

        }

        public bool IsRelevant(string ObjectName)
        {
            return Global.IsInList(ObjectName, this.triggering_objects_as_string, TrueIfEmpty: false) || Global.IsInList(ObjectName, this.additional_triggering_objects_as_string, TrueIfEmpty: false);
        }
        public Camera(string Name = "")
        {

            this.Name = Name;
            this.BICamName = Name;
            this.Prefix = Name;
        }

        public void ReadConfig(string config_path)
        {
            //retrieve whole config file content
            string[] content = System.IO.File.ReadAllLines(config_path);

            //import config data into variables, cut out relevant data between " "
            this.Name = Path.GetFileNameWithoutExtension(config_path);
            this.Prefix = content[2].Split('"')[1];

            //read triggering objects
            this.triggering_objects_as_string = content[1].Split('"')[1].Replace(" ", ""); //take the second line, split it between every ", take the part after the first ", remove every " " in this part
            this.triggering_objects = Global.Split(this.triggering_objects_as_string, ",").ToArray(); //split the row of triggering objects between every ','

            //input_path = AppSettings.Settings.input_path;

            //read trigger urls
            this.trigger_urls_as_string = content[0].Split('"')[1]; //takes the first line, cuts out everything between the first and the second " marker; all trigger urls in one string, ! still contains possible spaces etc.
            this.trigger_urls = this.trigger_urls_as_string.Replace(" ", "").Split(','); //all trigger urls in an array
            this.trigger_urls = this.trigger_urls.Except(new string[] { "" }).ToArray(); //remove empty entries

            //rewrite trigger_urls_as_string without possible empty entires
            int i = 0;
            this.trigger_urls_as_string = "";
            foreach (string c in this.trigger_urls)
            {
                this.trigger_urls_as_string += c;
                if (i < (this.trigger_urls.Length - 1))
                {
                    this.trigger_urls_as_string += ", ";
                }
                i++;
            }

            //read telegram enabled
            if (content[3].Split('"')[1].Replace(" ", "") == "yes")
            {
                this.telegram_enabled = true;
            }
            else
            {
                this.telegram_enabled = false;
            }

            //read enabled
            if (content[4].Split('"')[1].Replace(" ", "") == "yes")
            {
                this.enabled = true;
            }
            else
            {
                this.enabled = false;
            }

            Double.TryParse(content[5].Split('"')[1], out this.cooldown_time); //read cooldown time

            //read lower and upper threshold. Only load if line containing threshold values already exists (>version 1.58).
            if (content[6] != "")
            {
                Int32.TryParse(content[6].Split('"')[1].Split(',')[0], out this.threshold_lower); //read lower threshold
                Int32.TryParse(content[6].Split('"')[1].Split(',')[1], out this.threshold_upper); //read upper threshold
            }
            else //if config file from older version, set values to 0% and 100%
            {
                this.threshold_lower = 0;
                this.threshold_upper = 100;
            }


            //read stats
            Int32.TryParse(content[7].Split('"')[1].Split(',')[0], out this.stats_alerts); //bedeutet: Zeile 7 (6+1), aufgetrennt an ", 2tes (1+1) Resultat, aufgeteilt an ',', davon 1. Resultat  
            Int32.TryParse(content[7].Split('"')[1].Split(',')[1], out this.stats_irrelevant_alerts);
            Int32.TryParse(content[7].Split('"')[1].Split(',')[2], out this.stats_false_alerts);
        }


        //one correct alarm counter
        public void IncrementAlerts()
        {
            this.stats_alerts++;
            AppSettings.SaveAsync();
            //WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper);
        }

        //one alarm that contained no objects counter
        public void IncrementFalseAlerts()
        {
            this.stats_false_alerts++;
            AppSettings.SaveAsync();
            //WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper);
        }

        //one alarm that contained irrelevant objects counter
        public void IncrementIrrelevantAlerts()
        {
            this.stats_irrelevant_alerts++;
            AppSettings.SaveAsync();
            //WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper);
        }


    }
}
