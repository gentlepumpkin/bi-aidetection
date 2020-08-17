using System;
using System.Collections.Generic;

namespace AITool
{
    public class MaskManager
    {
        public bool masking_enabled { get; set; }
        public List<ObjectPosition> last_positions_history { get; set; }  //list of last detected object positions during defined time period (history_save_mins)
        public List<ObjectPosition> masked_positions { get; set;}         //stores dynamic masked object list
        public int mask_counter_default { get; set; }                     //counter for how long to keep masked objects. Each time not seen -1 from counter. If seen +1 counter until default max reached.
        public int history_save_mins { get; set; }                        //how long to store detected objects in history before purging list 
        public int history_threshold_count { get; set; }                  //number of times object is seen in same position before moving it to the masked_positions list
        
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public MaskManager()
        {
            last_positions_history = new List<ObjectPosition>();
            masked_positions = new List<ObjectPosition>();
        }
        public void CreateDynamicMask(ObjectPosition currentObject)
        {
            //Camera camera = currentObject.camera;
            log.Debug("*** Starting new object mask processing ***");
            log.Info("Current object detected: " + currentObject.ToString() + " on camera " + currentObject.camera.name);

            if (last_positions_history.Contains(currentObject))
            {
                //get index to prevent another search for removal if needed
                int indexLoc = last_positions_history.IndexOf(currentObject);
                ObjectPosition foundObject = last_positions_history[indexLoc];

                log.Debug("Found in last_positions_history: " + foundObject.ToString() + " for camera: " + currentObject.camera.name);

                if (foundObject.counter < history_threshold_count)
                {
                    foundObject.counter++;
                }
                else
                {
                    log.Info("History Threshold reached. Moving " + foundObject.ToString() + " to masked object list for camera: " + currentObject.camera.name);
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

                log.Debug("Found in masked_positions " + currentObject.ToString() + " for camera " + currentObject.camera.name);

                maskedObject.isVisible = true;
            }
            else
            {
                log.Info("+ New object found: " + currentObject.ToString() + ". Adding to last_positions_history for camera: " + currentObject.camera.name);
                last_positions_history.Add(currentObject);
            }
        }

        //remove objects from history if they have not been detected in defined time (history_save_mins) and found counter < history_threshold_count
        public void CleanUpExpiredHistory(String cameraName)
        {
            List<ObjectPosition> historyList = last_positions_history;

            log.Debug("### History objects summary for camera " + cameraName + " ###");

            if (historyList != null && historyList.Count > 0)
            {
                //scan backward through the list and remove by index. Not as easy to read but the fastest for removals at O(1)
                for (int x = historyList.Count - 1; x >= 0; x--)
                {
                    ObjectPosition historyObject = historyList[x];
                    TimeSpan ts = DateTime.Now - historyObject.createDate;
                    int minutes = ts.Minutes;

                    log.Debug("\t" + historyObject.ToString() + " existed for: " + ts.Minutes + " minutes");
                    if (minutes >= history_save_mins)
                    {
                        log.Debug("\t-Removing expired history: " + historyObject.ToString() + " for camera " + historyObject.camera.name);
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
                log.Debug("Searching for object masks to remove on Camera: " + cameraName);

                //scan backward through the list and remove by index. Not as easy to read as find by object but the fastest for removals at O(1)
                for (int x = maskedList.Count - 1; x >= 0; x--)
                {
                    ObjectPosition maskedObject = maskedList[x];
                    if (!maskedObject.isVisible)
                    {
                        log.Debug("Masked object NOT visible - " + maskedObject.ToString());
                        maskedObject.counter--;

                        if (maskedObject.counter <= 0)
                        {
                            log.Info("\t-Removing expired masked object: " + maskedObject.ToString() + " for camera " + cameraName);
                            maskedList.RemoveAt(x);
                        }
                    }
                    else
                    {
                        log.Debug("Masked object VISIBLE - " + maskedObject.ToString());
                        maskedObject.isVisible = false; //reset flag
                    }
                }
            }
        }

    }
}
