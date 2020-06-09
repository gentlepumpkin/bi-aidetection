using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace WindowsFormsApp2
{
    class Camera
    {
        public string name;
        public string prefix;
        public string triggering_objects_as_string;
        public string[] triggering_objects;
        public string trigger_urls_as_string;
        public string[] trigger_urls;
        public bool telegram_enabled;
        public bool enabled;
        public DateTime last_trigger_time;
        public double cooldown_time;
        public int threshold_lower;
        public int threshold_upper;

        public List<string> last_detections = new List<string>(); //stores objects that were detected last
        public List<float> last_confidences = new List<float>(); //stores last objects confidences
        public List<string> last_positions = new List<string>(); //stores last objects positions
        public String last_detections_summary;


        //stats
        public int stats_alerts; //alert image contained relevant object counter
        public int stats_false_alerts; //alert image contained no object counter
        public int stats_irrelevant_alerts; //alert image contained irrelevant object counter

        //write config to file
        public void WriteConfig(string _name, string _prefix, string _triggering_objects_as_string, string _trigger_urls_as_string, bool _telegram_enabled, bool _enabled, double _cooldown_time, int _threshold_lower, int _threshold_upper)
        {
            //if camera name (= settings file name) changed, the old settings file must be deleted
            if(name != _name)
            {
                System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + $"/cameras/{ name }.txt");
            }

            //write config file
            using (StreamWriter sw = System.IO.File.CreateText(AppDomain.CurrentDomain.BaseDirectory + $"/cameras/{ _name }.txt"))
            {
                name = _name;
                prefix = _prefix;
                triggering_objects_as_string = _triggering_objects_as_string;
                trigger_urls_as_string = _trigger_urls_as_string;
                telegram_enabled = _telegram_enabled;
                enabled = _enabled;
                cooldown_time = _cooldown_time;
                threshold_lower = _threshold_lower;
                threshold_upper = _threshold_upper;


                triggering_objects = triggering_objects_as_string.Split(','); //split the row of triggering objects between every ','

                trigger_urls = trigger_urls_as_string.Replace(" ", "").Split(','); //all trigger urls in an array
                trigger_urls = trigger_urls.Except(new string[] { "" }).ToArray(); //remove empty entries

                //rewrite trigger_urls_as_string without possible empty entires
                int i = 0;
                trigger_urls_as_string = "";
                foreach (string c in trigger_urls)
                {
                    trigger_urls_as_string += c;
                    if(i < (trigger_urls.Length - 1))
                    {
                        trigger_urls_as_string += ", ";
                    }
                    i++;
                }

                sw.WriteLine($"Trigger URL(s): \"{trigger_urls_as_string.Replace(", ,", "")}\" (input one or multiple urls, leave empty to disable; format: \"url, url, url\", example: \"http://192.168.1.133:80/admin?trigger&camera=frontyard&user=admin&pw=secretpassword, http://google.com\")");
                sw.WriteLine($"Relevant objects: \"{triggering_objects_as_string}\" (format: \"object, object, ...\", options: see below, example: \"person, bicycle, car\")");
                sw.WriteLine($"Input file begins with: \"{prefix}\" (only analyze images which names start with this text, leave empty to disable the feature, example: \"backyardcam\")");
                if (telegram_enabled == true)
                {
                    sw.WriteLine("Send images to Telegram: \"yes\"(options: yes, no)");
                }
                else
                {
                    sw.WriteLine("Send images to Telegram: \"no\"(options: yes, no)");
                }

                if (enabled == true)
                {
                    sw.WriteLine("ai detection enabled?: \"yes\"(options: yes, no)");
                }
                else
                {
                    sw.WriteLine("ai detection enabled?: \"no\"(options: yes, no)");
                }
                sw.WriteLine($"Cooldown time: \"{cooldown_time}\" minutes (How many minutes must have passed since the last detection. Used to separate event to ensure that every event only causes one alert.)");
                sw.WriteLine($"Certainty threshold: \"{threshold_lower},{threshold_upper}\" (format: \"lower % limit, upper % limit\")");
                sw.WriteLine($"STATS: alerts,irrelevant alerts,false alerts: \"{stats_alerts.ToString()}, {stats_irrelevant_alerts.ToString()}, {stats_false_alerts.ToString()}\" ");


            }
        }

        //delete config file
        public void Delete()
        {
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + $"/cameras/{ this.name }.txt");
        }

        //read config from file
        public void ReadConfig(string config_path)
        {
            //retrieve whole config file content
            string[] content = System.IO.File.ReadAllLines(config_path);

            //import config data into variables, cut out relevant data between " "
            name = Path.GetFileNameWithoutExtension(config_path);
            prefix = content[2].Split('"')[1];

            //read triggering objects
            triggering_objects_as_string = content[1].Split('"')[1].Replace(" ", ""); //take the second line, split it between every ", take the part after the first ", remove every " " in this part
            triggering_objects = triggering_objects_as_string.Split(','); //split the row of triggering objects between every ','
            

            //read trigger urls
            trigger_urls_as_string = content[0].Split('"')[1]; //takes the first line, cuts out everything between the first and the second " marker; all trigger urls in one string, ! still contains possible spaces etc.
            trigger_urls = trigger_urls_as_string.Replace(" ", "").Split(','); //all trigger urls in an array
            trigger_urls = trigger_urls.Except(new string[] { "" }).ToArray(); //remove empty entries

            //rewrite trigger_urls_as_string without possible empty entires
            int i = 0;
            trigger_urls_as_string = "";
            foreach (string c in trigger_urls)
            {
                trigger_urls_as_string += c;
                if (i < (trigger_urls.Length - 1))
                {
                    trigger_urls_as_string += ", ";
                }
                i++;
            }

            //read telegram enabled
            if (content[3].Split('"')[1].Replace(" ", "") == "yes")
            {
                telegram_enabled = true;
            }
            else
            {
                telegram_enabled = false;
            }

            //read enabled
            if (content[4].Split('"')[1].Replace(" ", "") == "yes")
            {
                enabled = true;
            }
            else
            {
                enabled = false;
            }

            Double.TryParse(content[5].Split('"')[1], out cooldown_time); //read cooldown time

            //read lower and upper threshold. Only load if line containing threshold values already exists (>version 1.58).
            if (content[6] != "")
            {
                Int32.TryParse(content[6].Split('"')[1].Split(',')[0], out threshold_lower); //read lower threshold
                Int32.TryParse(content[6].Split('"')[1].Split(',')[1], out threshold_upper); //read upper threshold
            }
            else //if config file from older version, set values to 0% and 100%
            {
                threshold_lower = 0;
                threshold_upper = 100;
            }
            

            //read stats
            Int32.TryParse(content[7].Split('"')[1].Split(',')[0], out stats_alerts); //bedeutet: Zeile 7 (6+1), aufgetrennt an ", 2tes (1+1) Resultat, aufgeteilt an ',', davon 1. Resultat  
            Int32.TryParse(content[7].Split('"')[1].Split(',')[1], out stats_irrelevant_alerts);
            Int32.TryParse(content[7].Split('"')[1].Split(',')[2], out stats_false_alerts);
        }


        //one correct alarm counter
        public void IncrementAlerts()
        {
            stats_alerts++;
            WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper);
        }

        //one alarm that contained no objects counter
        public void IncrementFalseAlerts()
        {
            stats_false_alerts++;
            WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper);
        }

        //one alarm that contained irrelevant objects counter
        public void IncrementIrrelevantAlerts()
        {
            stats_irrelevant_alerts++;
            WriteConfig(name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper);
        }


    }
}
