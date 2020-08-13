using System;
using System.Collections.Generic;

namespace WindowsFormsApp2
{
    public class MaskManager
    {
        public bool masking_enabled = false;
        public List<ObjectPosition> last_positions_history = new List<ObjectPosition>(); //list of last detected object positions during defined time period (history_save_mins)
        public List<ObjectPosition> masked_positions = new List<ObjectPosition>();      //stores dynamic masked object list
        public int mask_counter_default = 15;                                           //counter for how long to keep masked objects. Each time not seen -1 from counter. If seen +1 counter until default max reached.
        public int history_save_mins = 5;                                               //How long to store detected objects in history before purging list 
        public int history_threshold_count = 2;                                         //Number of times object is seen in same position before moving it to the masked_positions list


        public void CreateDynamicMask(ObjectPosition currentObject)
        {
            //Camera camera = currentObject.camera;
            //log("*** Starting new object mask processing ***");
            //log("Current object detected: " + currentObject.ToString() + " on camera " + currentObject.camera.name);

            if (last_positions_history.Contains(currentObject))
            {
                //get index to prevent another search for removal if needed
                int indexLoc = last_positions_history.IndexOf(currentObject);
                ObjectPosition foundObject = last_positions_history[indexLoc];

                //log("Found in last_positions_history: " + foundObject.ToString() + " for camera: " + currentObject.camera.name);

                if (foundObject.counter < history_threshold_count)
                {
                    foundObject.counter++;
                }
                else
                {
                    //log("History Threshold reached. Moving " + foundObject.ToString() + " to masked object list for camera: " + currentObject.camera.name);
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

                //log("Found in masked_positions " + currentObject.ToString() + " for camera " + currentObject.camera.name);

                maskedObject.isVisible = true;
            }
            else
            {
                //log("+ New object found: " + currentObject.ToString() + ". Adding to last_positions_history for camera: " + currentObject.camera.name);
                last_positions_history.Add(currentObject);
            }
        }

        //remove objects from history if they have not been detected in defined time (history_save_mins) and found counter < history_threshold_count
        public void CleanUpExpiredHistory(String cameraName)
        {
            List<ObjectPosition> historyList = last_positions_history;

            //log("### History objects summary for camera " + cameraName + " ###");

            if (historyList != null && historyList.Count > 0)
            {
                //scan backward through the list and remove by index. Not as easy to read but the fastest for removals at O(1)
                for (int x = historyList.Count - 1; x >= 0; x--)
                {
                    ObjectPosition historyObject = historyList[x];
                    TimeSpan ts = DateTime.Now - historyObject.createDate;
                    int minutes = ts.Minutes;

                    //log("\t" + historyObject.ToString() + " existed for: " + ts.Minutes + " minutes");
                    if (minutes >= history_save_mins)
                    {
                        //log("\t-Removing expired history: " + historyObject.ToString() + " for camera " + historyObject.camera.name);
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
                //log("Searching for object masks to remove on Camera: " + cameraName);

                //scan backward through the list and remove by index. Not as easy to read as find by object but the fastest for removals at O(1)
                for (int x = maskedList.Count - 1; x >= 0; x--)
                {
                    ObjectPosition maskedObject = maskedList[x];
                    if (!maskedObject.isVisible)
                    {
                        //log("Masked object NOT visible - " + maskedObject.ToString());
                        maskedObject.counter--;

                        if (maskedObject.counter <= 0)
                        {
                            //log("\t-Removing expired masked object: " + maskedObject.ToString() + " for camera " + cameraName);
                            maskedList.RemoveAt(x);
                        }
                    }
                    else
                    {
                        //log("Masked object VISIBLE - " + maskedObject.ToString());
                        maskedObject.isVisible = false; //reset flag
                    }
                }
            }
        }

    }
}
