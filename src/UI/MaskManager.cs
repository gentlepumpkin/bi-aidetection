using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Timers;

namespace AITool
{
    public class MaskManager
    {
        [JsonIgnore]
        private bool _masking_enabled = false;
        public bool masking_enabled
        {
            get => _masking_enabled;
            set
            {
                _masking_enabled = value;
                if(_masking_enabled) cleanHistoryTimer.Start();
                else cleanHistoryTimer.Stop();
            }
        }

        public int mask_counter_default { get; set; } = 15;               //counter for how long to keep masked objects. Each time not seen -1 from counter. If seen +1 counter until default max reached.
        public int history_save_mins { get; set; } = 5;                   //how long to store detected objects in history before purging list 
        public int history_threshold_count { get; set; } = 2;             //number of times object is seen in same position before moving it to the masked_positions list
        public double thresholdPercent { get; set; } = .14;               //what percent can the selection rectangle vary to be considered a match
        public List<ObjectPosition> last_positions_history { get; set; }  //list of last detected object positions during defined time period - history_save_mins
        public List<ObjectPosition> masked_positions { get; set; }        //stores dynamic masked object list (created in default constructor)

        [JsonIgnore]
        private Timer cleanHistoryTimer = new Timer();

        //[JsonIgnore]
        //private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public MaskManager()
        {
            last_positions_history = new List<ObjectPosition>();
            masked_positions = new List<ObjectPosition>();

            //register event handler to run clean history every minute
            cleanHistoryTimer.Elapsed += new System.Timers.ElapsedEventHandler(cleanHistoryEvent);
            cleanHistoryTimer.Interval =  60000; // 1min = 60,000ms
        }

        public void Update(Camera cam)
        {
            //This will run on save/load settings

            //lets store thresholdpercent as the same value the user sees in UI for easier troubleshooting in mask dialog
            if (this.thresholdPercent < 1)
            {
                this.thresholdPercent = this.thresholdPercent * 100;
            }

            foreach (ObjectPosition op in this.last_positions_history)
            {
                //Update threshold since it could have been changed since mask created
                op.thresholdPercent = this.thresholdPercent;
                //update the camera name since it could have been renamed since the mask was created
                op.cameraName = cam.name;
                //update last seen date if hasnt been set
                if (op.LastSeenDate == DateTime.MinValue)
                {
                    op.LastSeenDate = op.createDate;
                }
                //if null image from older build, force to have the last image from the camera:
                if (op.imagePath == null || string.IsNullOrEmpty(op.imagePath))
                {
                    //first, set to empty string rather than null to fix bug I saw
                    op.imagePath = "";
                    //next, fill with most recent image with detections
                    if (!string.IsNullOrEmpty(cam.last_image_file_with_detections))
                    {
                        op.imagePath = cam.last_image_file_with_detections;
                    }
                    else if (!string.IsNullOrEmpty(cam.last_image_file))
                    {
                        op.imagePath = cam.last_image_file;
                    }
                }
            }
            foreach (ObjectPosition op in this.masked_positions)
            {
                //Update threshold since it could have been changed since mask created
                op.thresholdPercent = this.thresholdPercent;
                //update the camera name since it could have been renamed since the mask was created
                op.cameraName = cam.name;
                //update last seen date if hasnt been set
                if (op.LastSeenDate == DateTime.MinValue)
                {
                    op.LastSeenDate = op.createDate;
                }
                //if null image from older build, force to have the last image from the camera:
                if (op.imagePath == null || string.IsNullOrEmpty(op.imagePath))
                {
                    //first, set to empty string rather than null to fix bug I saw
                    op.imagePath = "";
                    //next, fill with most recent image with detections
                    if (!string.IsNullOrEmpty(cam.last_image_file_with_detections))
                    {
                        op.imagePath = cam.last_image_file_with_detections;
                    }
                    else if (!string.IsNullOrEmpty(cam.last_image_file))
                    {
                        op.imagePath = cam.last_image_file;
                    }
                }
            }
        }

        public bool CreateDynamicMask(ObjectPosition currentObject)
        {
            bool maskExists = false;

            Global.Log("*** Starting new object mask processing ***");
            Global.Log("Current object detected: " + currentObject.ToString() + " on camera " + currentObject.cameraName);

            currentObject.thresholdPercent = thresholdPercent;

            if (last_positions_history.Contains(currentObject))
            {
                //get index to prevent another search for removal if needed
                int indexLoc = last_positions_history.IndexOf(currentObject);
                ObjectPosition foundObject = last_positions_history[indexLoc];

                foundObject.LastSeenDate = DateTime.Now;

                //Update last image that has same detection, and camera name found for existing mask
                foundObject.imagePath = currentObject.imagePath;
                foundObject.cameraName = currentObject.cameraName;

                Global.Log("Found in last_positions_history: " + foundObject.ToString() + " for camera: " + currentObject.cameraName);

                if (foundObject.counter < history_threshold_count)
                {
                    foundObject.counter++;
                }
                else
                {
                    Global.Log("History Threshold reached. Moving " + foundObject.ToString() + " to masked object list for camera: " + currentObject.cameraName);
                    last_positions_history.RemoveAt(indexLoc);
                    foundObject.isVisible = true;
                    foundObject.counter = mask_counter_default;
                    masked_positions.Add(foundObject);
                }
            }
            else if (masked_positions.Contains(currentObject))
            {
                ObjectPosition maskedObject = (ObjectPosition)masked_positions[masked_positions.IndexOf(currentObject)];

                maskedObject.LastSeenDate = DateTime.Now;

                //Update last image that has same detection, and camera name found for existing mask
                maskedObject.imagePath = currentObject.imagePath;
                maskedObject.cameraName = currentObject.cameraName;

                if (maskedObject.counter < mask_counter_default)
                {
                    maskedObject.counter++;
                }

                Global.Log("Found in masked_positions " + maskedObject.ToString() + " for camera " + currentObject.cameraName);

                maskedObject.isVisible = true;
                maskExists = true;
            }
            else
            {
                Global.Log("+ New object found: " + currentObject.ToString() + ". Adding to last_positions_history for camera: " + currentObject.cameraName);
                last_positions_history.Add(currentObject);
            }

            return maskExists;
        }

        //remove objects from history if they have not been detected in defined time (history_save_mins) and found counter < history_threshold_count
        private void CleanUpExpiredHistory()
        {
            try
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
                            Global.Log("Removing expired history: " + historyObject.ToString() + " which existed for " + ts.Minutes + " minutes.");
                            historyList.RemoveAt(x);
                        }
                    }
                }
                else if (historyList == null)
                {
                    Global.Log("Error: historyList is null?");
                }
            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));
            }
        }

        public void CleanUpExpiredMasks()
        {
            try
            {
                List<ObjectPosition> maskedList = masked_positions;

                if (maskedList != null && maskedList.Count > 0)
                {
                    //Global.Log("Searching for object masks to remove on Camera: " + cameraName);

                    //scan backward through the list and remove by index. Not as easy to read as find by object but the faster for removals
                    for (int x = maskedList.Count - 1; x >= 0; x--)
                    {
                        ObjectPosition maskedObject = maskedList[x];
                        if (!maskedObject.isVisible && !maskedObject.isStatic)
                        {
                            //Global.Log("Masked object NOT visible - " + maskedObject.ToString());
                            maskedObject.counter--;

                            if (maskedObject.counter <= 0)
                            {
                                Global.Log("Removing expired masked object: " + maskedObject.ToString());
                                maskedList.RemoveAt(x);
                            }
                        }
                        else
                        {
                            //Global.Log("Masked object VISIBLE - " + maskedObject.ToString());
                            maskedObject.isVisible = false; //reset flag
                        }
                    }
                }
                else if (maskedList == null)
                {
                    Global.Log("Error: Maskedlist is null?");
                }

            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));
            }
        }

        private void cleanHistoryEvent(object sender, EventArgs e)
        {
            CleanUpExpiredHistory();
        }

    }
}
