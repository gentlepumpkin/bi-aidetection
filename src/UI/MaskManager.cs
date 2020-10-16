using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Timers;
using static AITool.AITOOL;

namespace AITool
{
    public class MaskManager
    {
        public enum RemoveEvent
        {
             Timer,
             Detection
        }

        private bool _maskingEnabled = false;
        public bool MaskingEnabled
        {
            get => _maskingEnabled;
            set
            {
                _maskingEnabled = value;
                if (_maskingEnabled) _cleanMaskTimer.Start();
                else _cleanMaskTimer.Stop();
            }
        }

        public int MaskRemoveMins { get; set; } = 5;                    //counter for how long to keep masked objects. Each time not seen -1 from counter. If seen +1 counter until default max reached.
        public int HistorySaveMins { get; set; } = 5;                   //how long to store detected objects in history before purging list 
        public int HistoryThresholdCount { get; set; } = 2;             //number of times object is seen in same position before moving it to the masked_positions list
        public int MaskSaveMins { get; set; } = 2;
        public double PercentMatch { get; set; } = 85;                  //miniumn percentage match to be considered a match
        public int MaskRemoveThreshold { get; set; } = 1;
        public List<ObjectPosition> LastPositionsHistory { get; set; }  //list of last detected object positions during defined time period - history_save_mins
        public List<ObjectPosition> MaskedPositions { get; set; }       //stores dynamic masked object list (created in default constructor)
        
        private DateTime _lastDetectionDate;
        public DateTime LastDetectionDate {
            get => _lastDetectionDate;
            set
            {
                _lastDetectionDate = value;
                CleanUpExpiredMasks(RemoveEvent.Detection);
            }
        }

        public string Objects = "";
        public ObjectScale ScaleConfig { get; set; }

        [JsonIgnore]
        private object _maskLockObject = new object();
        [JsonIgnore]
        private Timer _cleanMaskTimer = new Timer();


        //I think JsonConstructor may not be needed, but adding anyway -Vorlon
        [JsonConstructor]
        public MaskManager()
        {
            LastPositionsHistory = new List<ObjectPosition>();
            MaskedPositions = new List<ObjectPosition>();
            ScaleConfig = new ObjectScale();

            //register event handler to run clean history every minute
            _cleanMaskTimer.Elapsed += new System.Timers.ElapsedEventHandler(cleanMaskEvent);
            _cleanMaskTimer.Interval = 60000; // 1min = 60,000ms
        }

        public void Update(Camera cam)
        {
            lock (_maskLockObject)
            {
                //This will run on save/load settings

                //lets store thresholdpercent as the same value the user sees in UI for easier troubleshooting in mask dialog
                if (this.PercentMatch < 1)
                {
                    this.PercentMatch = this.PercentMatch * 100;
                }

                if (cam.maskManager.ScaleConfig == null)
                    cam.maskManager.ScaleConfig = new ObjectScale();

                foreach (ObjectPosition op in this.LastPositionsHistory)
                {
                    //Update threshold since it could have been changed since mask created
                    op.PercentMatch = this.PercentMatch;
                    //update the camera name since it could have been renamed since the mask was created
                    op.CameraName = cam.name;
                    //update last seen date if hasnt been set
                    if (op.LastSeenDate == DateTime.MinValue)
                    {
                        op.LastSeenDate = op.CreateDate;
                    }
                    //if null image from older build, force to have the last image from the camera:
                    if (op.ImagePath == null || string.IsNullOrEmpty(op.ImagePath))
                    {
                        //first, set to empty string rather than null to fix bug I saw
                        op.ImagePath = "";
                        //next, fill with most recent image with detections
                        if (!string.IsNullOrEmpty(cam.last_image_file_with_detections))
                        {
                            op.ImagePath = cam.last_image_file_with_detections;
                        }
                        else if (!string.IsNullOrEmpty(cam.last_image_file))
                        {
                            op.ImagePath = cam.last_image_file;
                        }
                    }
                }

                foreach (ObjectPosition op in this.MaskedPositions)
                {
                    //Update threshold since it could have been changed since mask created
                    op.PercentMatch = this.PercentMatch;
                    //update the camera name since it could have been renamed since the mask was created
                    op.CameraName = cam.name;
                    //update last seen date if hasnt been set
                    if (op.LastSeenDate == DateTime.MinValue)
                    {
                        op.LastSeenDate = op.CreateDate;
                    }
                    //if null image from older build, force to have the last image from the camera:
                    if (op.ImagePath == null || string.IsNullOrEmpty(op.ImagePath))
                    {
                        //first, set to empty string rather than null to fix bug I saw
                        op.ImagePath = "";
                        //next, fill with most recent image with detections
                        if (!string.IsNullOrEmpty(cam.last_image_file_with_detections))
                        {
                            op.ImagePath = cam.last_image_file_with_detections;
                        }
                        else if (!string.IsNullOrEmpty(cam.last_image_file))
                        {
                            op.ImagePath = cam.last_image_file;
                        }
                    }
                }
            }
        }

        public MaskResultInfo CreateDynamicMask(ObjectPosition currentObject)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            MaskResultInfo returnInfo = new MaskResultInfo();

            try
            {
                lock (_maskLockObject)  //moved this up, trying to figure out why IsMasked isnt returning correctly
                {
                    List<string> objects = Global.Split(this.Objects, "|;,");

                    if (objects.Count > 0)
                    {
                        bool fnd = false;
                        foreach (string objname in objects)
                        {
                            if (currentObject.Label.Trim().ToLower() == objname.ToLower())
                                fnd = true;
                        }
                        if (!fnd)
                        {
                            Log($"Skipping mask creation because '{currentObject.Label}' is not one of the configured objects: '{this.Objects}'");
                            returnInfo.SetResults(MaskType.Unknown, MaskResult.Unwanted);
                            return returnInfo;
                        }
                    }

                    Log("*** Starting new object mask processing ***");
                    Log("Current object detected: " + currentObject.ToString() + " on camera " + currentObject.CameraName);

                    currentObject.ScaleConfig = ScaleConfig;
                    currentObject.PercentMatch = PercentMatch;

                    int historyIndex = LastPositionsHistory.IndexOf(currentObject);

                    if (historyIndex > -1)
                    {
                        ObjectPosition foundObject = LastPositionsHistory[historyIndex];

                        foundObject.LastSeenDate = DateTime.Now;

                        //Update last image that has same detection, and camera name found for existing mask
                        foundObject.ImagePath = currentObject.ImagePath;
                        foundObject.CameraName = currentObject.CameraName;

                        Log("Found in last_positions_history: " + foundObject.ToString() + " for camera: " + currentObject.CameraName);

                        if (foundObject.Counter < HistoryThresholdCount)
                        {
                            foundObject.Counter++;
                            returnInfo.SetResults(MaskType.History, MaskResult.ThresholdNotMet, foundObject.Counter);
                        }
                        else
                        {
                            Log("History Threshold reached. Moving " + foundObject.ToString() + " to masked object list for camera: " + currentObject.CameraName);
                            
                            LastPositionsHistory.RemoveAt(historyIndex);
                            foundObject.CreateDate = DateTime.Now;     //reset create date as history object is converted to a mask
                            foundObject.Counter = MaskRemoveThreshold; //sets the number of detections not visiable before being eligable to remove by timer
                            MaskedPositions.Add(foundObject);
                            returnInfo.SetResults(MaskType.Dynamic, MaskResult.NewDynamicCreated, foundObject.Counter);
                        }
                        return returnInfo;
                    }

                    int maskIndex = MaskedPositions.IndexOf(currentObject);

                    if (maskIndex > -1)
                    {
                        ObjectPosition maskedObject = (ObjectPosition)MaskedPositions[maskIndex];

                        maskedObject.LastSeenDate = DateTime.Now;

                        //increase threashold counter when object is visible
                        if (maskedObject.Counter < MaskRemoveThreshold)
                        {
                            maskedObject.Counter++;
                        }

                        //Update last image that has same detection, and camera name found for existing mask
                        maskedObject.ImagePath = currentObject.ImagePath;
                        maskedObject.CameraName = currentObject.CameraName;

                        Log("Found in masked_positions " + maskedObject.ToString() + " for camera " + currentObject.CameraName);

                        MaskType type = maskedObject.IsStatic ? MaskType.Static : MaskType.Dynamic;
                        returnInfo.SetResults(type, MaskResult.Found, maskedObject.Counter);
                    }
                    else
                    {
                        Log("+ New object found: " + currentObject.ToString() + ". Adding to last_positions_history for camera: " + currentObject.CameraName);
                        LastPositionsHistory.Add(currentObject);
                        returnInfo.SetResults(MaskType.History, MaskResult.New, currentObject.Counter);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Error: {Global.ExMsg(ex)}");
                returnInfo.Result = MaskResult.Error;
            }

            return returnInfo;
        }

        //remove objects from history if they have not been detected in defined time (history_save_mins) and found counter < history_threshold_count
        private void CleanUpExpiredHistory()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            lock (_maskLockObject)
            {
                try
                {
                    if (LastPositionsHistory != null && LastPositionsHistory.Count > 0)
                    {
                        //scan backward through the list and remove by index. Not as easy to read but the faster for removals
                        for (int x = LastPositionsHistory.Count - 1; x >= 0; x--)
                        {
                            ObjectPosition historyObject = LastPositionsHistory[x];
                            TimeSpan ts = DateTime.Now - historyObject.CreateDate;
                            double minutes = ts.TotalMinutes;

                            //Log("\t" + historyObject.ToString() + " existed for: " + ts.Minutes + " minutes");
                            if (minutes >= HistorySaveMins)
                            {
                                Log($"Removing expired history: {historyObject.ToString()} which existed for {minutes.ToString("#######0.0")} minutes. (max={HistorySaveMins})");
                                LastPositionsHistory.RemoveAt(x);
                            }
                        }
                    }
                    else if (LastPositionsHistory == null)
                    {
                        Log("Error: historyList is null?");
                    }
                }
                catch (Exception ex)
                {
                    Log("Error: " + Global.ExMsg(ex));
                }
            }
        }

        public void CleanUpExpiredMasks(RemoveEvent trigger)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            lock (_maskLockObject)
            {
                try
                {
                    if (MaskedPositions != null && MaskedPositions.Count > 0)
                    {
                        //scan backward through the list and remove by index. Not as easy to read as find by object but the faster for removals
                        for (int x = MaskedPositions.Count - 1; x >= 0; x--)
                        {
                            ObjectPosition maskedObject = MaskedPositions[x];

                            TimeSpan ts = LastDetectionDate - maskedObject.LastSeenDate;
                            double minutes = ts.TotalMinutes;

                            switch (trigger)
                            {
                                case RemoveEvent.Timer:
                                    if (minutes >= MaskSaveMins && !maskedObject.IsStatic && maskedObject.Counter == 0)
                                    {
                                        Log($"Removing expired ({minutes.ToString("####0.0")} mins, max={MaskSaveMins}) masked object by timer thread: " + maskedObject.ToString());
                                        MaskedPositions.RemoveAt(x);
                                    }
                                    break;
                                case RemoveEvent.Detection:
                                    if (minutes > 1 && !maskedObject.IsStatic)  //if not visiable and not marked as a static mask
                                    {
                                        if (maskedObject.Counter == 0)
                                        {
                                            Log($"Removing expired ({minutes.ToString("####0.0")} mins, max={MaskSaveMins}) masked object after detection: " + maskedObject.ToString());
                                            MaskedPositions.RemoveAt(x);
                                        }
                                        else maskedObject.Counter--;
                                    }
                                    break;
                            }
                        }
                    }
                    else if (MaskedPositions == null)
                    {
                        Log("Error: Maskedlist is null?");
                    }
                }
                catch (Exception ex)
                {
                    Log("Error: " + Global.ExMsg(ex));
                }
            }
        }

        private void cleanMaskEvent(object sender, EventArgs e)
        {
            CleanUpExpiredHistory();
            CleanUpExpiredMasks(RemoveEvent.Timer);
        }
    }

    public class ObjectScale
    {
        //empty default constructor required by Newtonsoft deserialization for new objects
        public ObjectScale() {}

        public bool IsScaledObject { get; set; } = false;
        public int SmallObjectMaxPercent { get; set; } = 2;
        public int SmallObjectMatchPercent { get; set; } = 78;
        public int MediumObjectMinPercent { get; set; } = 2;
        public int MediumObjectMaxPercent { get; set; } = 5;
        public int MediumObjectMatchPercent { get; set; } = 82;
    }

}
