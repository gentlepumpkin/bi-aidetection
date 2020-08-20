using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using Newtonsoft.Json;

namespace AITool
{
    public class Camera
    {
        public string name;
        public string prefix;
        public string[] triggering_objects;
        public string[] trigger_urls;
        public bool telegram_enabled;
        public bool enabled;
        public double cooldown_time;
        public int threshold_lower;
        public int threshold_upper;

        // Use this class also as a base to save and reload camera settings. [JsonIgnore] is used to ignore fields you don't want saved to json file. 
        [JsonIgnore]
        public DateTime last_trigger_time;

        [JsonIgnore] 
        public List<string> last_detections = new List<string>();   //stores objects that were detected last
    
        [JsonIgnore]
        public List<float> last_confidences = new List<float>();    //stores last objects confidences
        
        [JsonIgnore]
        public List<string> last_positions = new List<string>();    //stores last objects positions

        [JsonIgnore]
        public String last_detections_summary;                      //summary text of last detection

        public MaskManager maskManager = new MaskManager();

        //stats
        public int stats_alerts; //alert image contained relevant object counter
        public int stats_false_alerts; //alert image contained no object counter
        public int stats_irrelevant_alerts; //alert image contained irrelevant object counter


        //Serialize this object to json file
        public void WriteConfig()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + $"/cameras/{ name }.json", jsonString);
        }


        //Updates the state of the current camera object with any changes from UI
        public void UpdateCamera(Camera updateCamera)
        {
            //if camera name (= settings file name) changed, the old settings file must be deleted
            if (name != updateCamera.name)
            {
                System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + $"/cameras/{ name }.json");
            }

            name = updateCamera.name;
            prefix = updateCamera.prefix;
            triggering_objects = updateCamera.triggering_objects;
            trigger_urls = updateCamera.trigger_urls.Except(new string[] { "" }).ToArray();  //remove empty entries
            telegram_enabled = updateCamera.telegram_enabled;
            enabled = updateCamera.enabled;
            cooldown_time = updateCamera.cooldown_time;
            threshold_lower = updateCamera.threshold_lower;
            threshold_upper = updateCamera.threshold_upper;

            maskManager.history_save_mins = updateCamera.maskManager.history_save_mins;
            maskManager.history_threshold_count = updateCamera.maskManager.history_threshold_count;
            maskManager.mask_counter_default = updateCamera.maskManager.mask_counter_default;
            maskManager.masking_enabled = updateCamera.maskManager.masking_enabled;
            maskManager.thresholdPercent = updateCamera.maskManager.thresholdPercent;

            WriteConfig();
        }


        //delete config file
        public void Delete()
        {
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + $"/cameras/{ this.name }.json");
        }


        //read config by deserializing json file into Camera object
        public void ReadConfig(string config_path)
        {
           
            String jsonString = File.ReadAllText(config_path);
            Camera camera = JsonConvert.DeserializeObject<Camera>(jsonString);

            name = Path.GetFileNameWithoutExtension(config_path);
            prefix = camera.prefix;
            triggering_objects = camera.triggering_objects;

            //read trigger urls
            trigger_urls = camera.trigger_urls.Except(new string[] { "" }).ToArray(); //remove empty entries

            telegram_enabled = camera.telegram_enabled;
            enabled = camera.enabled;
            cooldown_time = camera.cooldown_time;
            threshold_lower = camera.threshold_lower;
            threshold_upper = camera.threshold_upper; 

            maskManager.masking_enabled = camera.maskManager.masking_enabled;
            maskManager.history_save_mins = camera.maskManager.history_save_mins;
            maskManager.history_threshold_count = camera.maskManager.history_threshold_count;
            maskManager.mask_counter_default = camera.maskManager.mask_counter_default;
            maskManager.thresholdPercent = camera.maskManager.thresholdPercent;

            //read stats
            stats_alerts = camera.stats_alerts;
            stats_irrelevant_alerts = camera.stats_irrelevant_alerts;
            stats_false_alerts = camera.stats_false_alerts;
        }


        //one correct alarm counter
        public void IncrementAlerts()
        {
            stats_alerts++;
            WriteConfig();        
        }

        //one alarm that contained no objects counter
        public void IncrementFalseAlerts()
        {
            stats_false_alerts++;
            WriteConfig();
        }

        //one alarm that contained irrelevant objects counter
        public void IncrementIrrelevantAlerts()
        {
            stats_irrelevant_alerts++;
            WriteConfig();
        }
    }
}
