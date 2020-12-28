using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        MQTT
    }
    public class CameraTriggerAction
    {
        public TriggerType Type = TriggerType.Unknown;
        public string Description = "";
        public string LastResponse = "";
    }
    public class Camera
    {
        public string Name = "";
        public string Prefix = "";
        public string BICamName = "";
        public string MaskFileName = "";
        public string triggering_objects_as_string = "Person";
        public string additional_triggering_objects_as_string = "Spiny Lumpsucker, Pink Fairy Armadillo, Tasselled Wobbegong";
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
        public bool enabled = true;
        public double cooldown_time = 0;
        public int cooldown_time_seconds = 5;
        public int threshold_lower = 0;
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

        public bool IsRelevant(string ObjectName)
        {
            return Global.IsInList(ObjectName, this.triggering_objects_as_string, TrueIfEmpty:false) || Global.IsInList(ObjectName, this.additional_triggering_objects_as_string, TrueIfEmpty:false);
        }
        public Camera(string Name = "")
        {

            this.Name = Name;
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
