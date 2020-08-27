using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AITool
{
    public class MaskManager
    {
        public bool masking_enabled { get; set; } = false;
        public int mask_counter_default { get; set; } = 15;                    //counter for how long to keep masked objects. Each time not seen -1 from counter. If seen +1 counter until default max reached.
        public int history_save_mins { get; set; } = 5;                        //how long to store detected objects in history before purging list 
        public int history_threshold_count { get; set; } = 2;                 //number of times object is seen in same position before moving it to the masked_positions list
        public double thresholdPercent { get; set; } = .08;
        public List<ObjectPosition> last_positions_history { get; set; }  //list of last detected object positions during defined time period (history_save_mins)
        public List<ObjectPosition> masked_positions { get; set; }        //stores dynamic masked object list

        //[JsonIgnore]
        //private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public MaskManager()
        {
            last_positions_history = new List<ObjectPosition>();
            masked_positions = new List<ObjectPosition>();
        }

        public bool CreateDynamicMask(ObjectPosition currentObject)
        {
            bool maskExists = false;

            Global.Log("*** Starting new object mask processing ***");
            Global.Log("Current object detected: " + currentObject.ToString() + " on camera " + currentObject.camera.name);

            currentObject.thresholdPercent = thresholdPercent;

            if (last_positions_history.Contains(currentObject))
            {
                //get index to prevent another search for removal if needed
                int indexLoc = last_positions_history.IndexOf(currentObject);
                ObjectPosition foundObject = last_positions_history[indexLoc];

                Global.Log("Found in last_positions_history: " + foundObject.ToString() + " for camera: " + currentObject.camera.name);

                if (foundObject.counter < history_threshold_count)
                {
                    foundObject.counter++;
                }
                else
                {
                    Global.Log("History Threshold reached. Moving " + foundObject.ToString() + " to masked object list for camera: " + currentObject.camera.name);
                    last_positions_history.RemoveAt(indexLoc);
                    foundObject.isVisible = true;
                    foundObject.counter = mask_counter_default;
                    masked_positions.Add(foundObject);
                }
            }
            else if (masked_positions.Contains(currentObject))
            {
                ObjectPosition maskedObject = (ObjectPosition)masked_positions[masked_positions.IndexOf(currentObject)];
                if (maskedObject.counter < mask_counter_default)
                {
                    maskedObject.counter++;
                }

                Global.Log("Found in masked_positions " + maskedObject.ToString() + " for camera " + currentObject.camera.name);

                maskedObject.isVisible = true;
                maskExists = true;
            }
            else
            {
                Global.Log("+ New object found: " + currentObject.ToString() + ". Adding to last_positions_history for camera: " + currentObject.camera.name);
                last_positions_history.Add(currentObject);
            }

            return maskExists;
        }

        //remove objects from history if they have not been detected in defined time (history_save_mins) and found counter < history_threshold_count
        public void CleanUpExpiredHistory(String cameraName)
        {
            List<ObjectPosition> historyList = last_positions_history;

            //Global.Log("### History objects summary for camera " + cameraName + " ###");

            if (historyList != null && historyList.Count > 0)
            {
                //scan backward through the list and remove by index. Not as easy to read but the faster for removals
                for (int x = historyList.Count - 1; x >= 0; x--)
                {
                    ObjectPosition historyObject = historyList[x];
                    TimeSpan ts = DateTime.Now - historyObject.createDate;
                    int minutes = ts.Minutes;

                    //Global.Log("\t" + historyObject.ToString() + " existed for: " + ts.Minutes + " minutes");
                    if (minutes >= history_save_mins)
                    {
                        Global.Log("Removing expired history: " + historyObject.ToString() + " for camera " + historyObject.camera.name + " which existed for " + ts.Minutes + " minutes.");
                        historyList.RemoveAt(x);
                    }
                }
            }
        }

        public void CleanUpExpiredMasks(String cameraName)
        {
            List<ObjectPosition> maskedList = masked_positions;

            if (maskedList != null && maskedList.Count > 0)
            {
                //Global.Log("Searching for object masks to remove on Camera: " + cameraName);

                //scan backward through the list and remove by index. Not as easy to read as find by object but the faster for removals
                for (int x = maskedList.Count - 1; x >= 0; x--)
                {
                    ObjectPosition maskedObject = maskedList[x];
                    if (!maskedObject.isVisible)
                    {
                        Global.Log("Masked object NOT visible - " + maskedObject.ToString());
                        maskedObject.counter--;

                        if (maskedObject.counter <= 0)
                        {
                            Global.Log("Removing expired masked object: " + maskedObject.ToString() + " for camera " + cameraName);
                            maskedList.RemoveAt(x);
                        }
                    }
                    else
                    {
                        Global.Log("Masked object VISIBLE - " + maskedObject.ToString());
                        maskedObject.isVisible = false; //reset flag
                    }
                }
            }
        }

    }
}
