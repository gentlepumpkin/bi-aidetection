using MQTTnet.Client.Publishing;
using SixLabors.ImageSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using NPushover;
using NPushover.Exceptions;
using NPushover.RequestObjects;
using NPushover.ResponseObjects;
using static AITool.AITOOL;

namespace AITool
{
    public class ClsTriggerActionQueueItem
    {
        public TriggerType TType { get; set; } = TriggerType.Unknown;
        public Camera cam { get; set; } = null;
        public History Hist { get; set; } = null;
        public ClsImageQueueItem CurImg { get; set; } = null;
        public bool Trigger { get; set; } = true;
        public string Text { get; set; } = "";
        public DateTime AddedTime { get; set; } = DateTime.MinValue;
        public long QueueCount { get; set; } = 0;
        public long QueueWaitMS { get; set; } = 0;
        public bool IsQueued { get; set; } = false;
        public long ActionTimeMS { get; set; } = 0;
        public long TotalTimeMS { get; set; } = 0;
        public ClsTriggerActionQueueItem(TriggerType ttype, Camera cam, ClsImageQueueItem CurImg, History hist, bool Trigger, string Text, bool IsQueued)
        {
            this.cam = cam;
            this.TType = ttype;
            this.CurImg = CurImg;
            this.Trigger = Trigger;
            this.AddedTime = DateTime.Now;
            this.Text = Text;
            this.IsQueued = IsQueued;
            this.Hist = hist;
        }
    }

    public class ClsTriggerActionQueue
    {
        BlockingCollection<ClsTriggerActionQueueItem> TriggerActionQueue = new BlockingCollection<ClsTriggerActionQueueItem>();
        ConcurrentDictionary<string, ClsTriggerActionQueueItem> CancelActionDict = new ConcurrentDictionary<string, ClsTriggerActionQueueItem>();

        public ThreadSafe.Datetime last_telegram_trigger_time { get; set; } = new ThreadSafe.Datetime(DateTime.MinValue);
        public ThreadSafe.Datetime last_Pushover_trigger_time { get; set; } = new ThreadSafe.Datetime(DateTime.MinValue);
        public ThreadSafe.Datetime TelegramRetryTime { get; set; } = new ThreadSafe.Datetime(DateTime.MinValue);
        public ThreadSafe.Datetime PushoverRetryTime { get; set; } = new ThreadSafe.Datetime(DateTime.MinValue);
        public ClsURLItem _url { get; set; } = null;
        private String CurSrv = "";
        string ImgPath = "NoImage";
        public ThreadSafe.Integer Count { get; set; } = new ThreadSafe.Integer(0);
        public MovingCalcs QCountCalc { get; set; } = new MovingCalcs(250, "Action Queue Sizes", false);
        public MovingCalcs QTimeCalc { get; set; } = new MovingCalcs(250, "Action Queue Times", true);
        public MovingCalcs ActionTimeCalc { get; set; } = new MovingCalcs(250, "Actions", true);
        public MovingCalcs TotalTimeCalc { get; set; } = new MovingCalcs(250, "Actions", true);
        public ClsTriggerActionQueue()
        {
            Task.Run(this.TriggerActionJobQueueLoop);
            Task.Run(this.CancelActionJobQueueLoop);
        }

        public async Task<bool> AddTriggerActionAsync(TriggerType ttype, Camera cam, ClsImageQueueItem CurImg, History hist, bool Trigger, bool Wait, ClsURLItem ds_url, string Text)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool ret = false;
            this._url = ds_url;
            if (ds_url == null)
            {
                this.CurSrv = "NoServer";
            }
            if (CurImg != null)
            {
                this.ImgPath = CurImg.image_path;
            }

            ClsTriggerActionQueueItem AQI = new ClsTriggerActionQueueItem(ttype, cam, CurImg, hist, Trigger, Text, !Wait);

            //Make sure not to put cancel items in the queue if no cancel triggers are defined...

            bool NeedsToCancel = (cam.Action_mqtt_enabled && string.IsNullOrEmpty(cam.Action_mqtt_payload_cancel) ||
                                  Trigger && cam.cancel_urls.Count() > 0);

            //bool DoIt = (Trigger || (!Trigger && cam.cancel_urls.Count() > 0 || (cam.Action_mqtt_enabled && !string.IsNullOrEmpty(cam.Action_mqtt_payload_cancel))));
            bool DoIt = (Trigger || (!NeedsToCancel));

            if (DoIt)
            {
                if (Wait)  //not queued
                {
                    ret = await this.RunTriggers(AQI);
                }
                else
                {
                    if (this.TriggerActionQueue.Count <= AppSettings.Settings.MaxActionQueueSize)
                    {
                        if (!this.TriggerActionQueue.TryAdd(AQI))
                        {
                            Log($"Error: Action '{AQI.TType}' could not be added? {this.ImgPath}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }
                        else
                        {
                            this.Count.WriteFullFence(this.TriggerActionQueue.Count());
                            AQI.QueueCount = this.Count.ReadFullFence();

                            ret = true;
                            Log($"Debug: Action '{AQI.TType}' ADDED to queue. Trigger={AQI.Trigger}, Queued={AQI.IsQueued}, Queue Count={AQI.QueueCount}, Image={this.ImgPath}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);

                        }
                    }
                    else
                    {
                        Log($"Error: Action '{AQI.TType}' could not be added because queue size is {this.TriggerActionQueue.Count} and the max is {AppSettings.Settings.MaxActionQueueSize} (MaxActionQueueSize) - {this.ImgPath}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    }

                }
            }

            return ret;
        }

        private async Task TriggerActionJobQueueLoop()
        {

            //this runs forever and blocks if nothing is in the queue
            foreach (ClsTriggerActionQueueItem AQI in this.TriggerActionQueue.GetConsumingEnumerable())
            {
                if (MasterCTS.IsCancellationRequested)
                    break;

                try
                {
                    await this.RunTriggers(AQI);
                    await Task.Delay(250); //very short wait between trigger events
                }
                catch (Exception ex)
                {

                    Log($"Error: " + ex.ToString(), this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                }

            }

            Log($"Error: Should not have left ActionQueueLoop?");

        }

        private async Task CancelActionJobQueueLoop()
        {
            while (true)
            {
                if (MasterCTS.IsCancellationRequested)
                    break;

                if (!this.CancelActionDict.IsEmpty)
                {

                    foreach (ClsTriggerActionQueueItem AQI in CancelActionDict.Values)
                    {

                        try
                        {
                            if (AQI.cam.Action_Cancel_Timer_Enabled)
                            {
                                if ((DateTime.Now - AQI.cam.Action_Cancel_Start_Time).TotalSeconds >= AppSettings.Settings.ActionCancelSeconds)
                                {
                                    await this.RunTriggers(AQI);
                                    AQI.cam.Action_Cancel_Timer_Enabled = false;  // will be deleted next time the loop goes through
                                }
                            }
                            else
                            {
                                CancelActionDict.TryRemove(AQI.cam.Name.ToLower(), out ClsTriggerActionQueueItem removedItem);
                                Log($"Debug: Removed cancel action in queue for camera '{AQI.cam.Name}', after {(DateTime.Now - AQI.cam.Action_Cancel_Start_Time).TotalSeconds} seconds", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);

                            }

                        }
                        catch (Exception ex)
                        {

                            Log($"Error: " + ex.ToString(), this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }
                    }

                }

                await Task.Delay(1000); //Only check every second
            }

        }

        public async Task<bool> RunTriggers(ClsTriggerActionQueueItem AQI)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool res = false;

            try
            {
                AQI.QueueWaitMS = Convert.ToInt64((DateTime.Now - AQI.AddedTime).TotalMilliseconds);
                this.QTimeCalc.AddToCalc(AQI.QueueWaitMS);
                bool WasSkipped = false;

                Stopwatch sw = Stopwatch.StartNew();

                this.Count.WriteFullFence(this.TriggerActionQueue.Count);

                this.QCountCalc.AddToCalc(AQI.QueueCount);

                Global.SendMessage(MessageType.UpdateStatus);

                if (AQI.TType == TriggerType.TelegramText)
                {
                    if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
                        res = await this.TelegramText(AQI);
                    else
                        WasSkipped = true;
                }
                else if (AQI.TType == TriggerType.TelegramImageUpload)
                {
                    if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
                        res = await this.TelegramUpload(AQI);
                    else
                        WasSkipped = true;
                }
                else
                {
                    res = await this.Trigger(AQI);
                }


                bool HasCancelAction = ((AQI.cam.Action_mqtt_enabled && string.IsNullOrEmpty(AQI.cam.Action_mqtt_payload_cancel)) || (AQI.cam.cancel_urls.Count() > 0));

                if (HasCancelAction)
                {
                    if (AQI.Trigger == false)  //If this is a CANCEL anyway...
                    {
                        //if we already did a cancel, set flag to delete the queued cancel item if exists
                        AQI.cam.Action_Cancel_Timer_Enabled = false;
                    }
                    else //If this is another TRIGGER...
                    {
                        if (this.CancelActionDict.ContainsKey(AQI.cam.Name.ToLower()))
                        {
                            //if already in queue, update date
                            AQI.cam.Action_Cancel_Start_Time = DateTime.Now;
                            Log($"Debug: EXTENDING cancel action time for camera '{AQI.cam.Name}', waiting {AppSettings.Settings.ActionCancelSeconds} seconds...", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }
                        else  //add it to the queue
                        {
                            AQI.cam.Action_Cancel_Start_Time = DateTime.Now;
                            AQI.cam.Action_Cancel_Timer_Enabled = true;
                            AQI.Trigger = false;  //set to be a cancel
                            this.CancelActionDict.TryAdd(AQI.cam.Name.ToLower(), AQI);
                            Log($"Debug: Cancel action queued for camera '{AQI.cam.Name}', waiting {AppSettings.Settings.ActionCancelSeconds} seconds...", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }
                    }
                }

                this.Count.WriteFullFence(this.TriggerActionQueue.Count);

                AQI.ActionTimeMS = sw.ElapsedMilliseconds;
                AQI.TotalTimeMS = Convert.ToInt64((DateTime.Now - AQI.AddedTime).TotalMilliseconds);
                this.TotalTimeCalc.AddToCalc(AQI.TotalTimeMS);
                this.ActionTimeCalc.AddToCalc(AQI.ActionTimeMS);

                if (!WasSkipped)
                {
                    Log($"Debug: Action '{AQI.TType}' done. Succeeded={res}, Trigger={AQI.Trigger}, Queued={AQI.IsQueued}, Queue Count={AQI.QueueCount} (Min={this.QCountCalc.Min},Max={this.QCountCalc.Max},Avg={this.QCountCalc.Average}), Total time={AQI.TotalTimeMS}ms (Min={this.TotalTimeCalc.Min}ms,Max={this.TotalTimeCalc.Max}ms,Avg={Convert.ToInt64(this.TotalTimeCalc.Average)}ms), Queue time={AQI.QueueWaitMS} (Min={this.QTimeCalc.Min}ms,Max={this.QTimeCalc.Max}ms,Avg={Convert.ToInt64(this.QTimeCalc.Average)}ms), Action Time={AQI.ActionTimeMS}ms (Min={this.ActionTimeCalc.Min}ms,Max={this.ActionTimeCalc.Max}ms,Avg={Convert.ToInt64(this.ActionTimeCalc.Average)}ms), Image={this.ImgPath}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                }

                Global.SendMessage(MessageType.UpdateStatus);

            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex), this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
            }

            return res;
        }

        //trigger actions
        public async Task<bool> Trigger(ClsTriggerActionQueueItem AQI)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool ret = true;

            //mostly for testing when we dont have a current image...
            if (AQI.CurImg == null)
            {
                if (!string.IsNullOrEmpty(AQI.cam.last_image_file_with_detections))
                {
                    AQI.CurImg = new ClsImageQueueItem(AQI.cam.last_image_file_with_detections, 1);
                }
                else if (!string.IsNullOrEmpty(AQI.cam.last_image_file))
                {
                    AQI.CurImg = new ClsImageQueueItem(AQI.cam.last_image_file, 1);
                }
                else
                {
                    Log($"Error: No image to process?", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    return false;
                }
            }

            try
            {
                double cooltime = (DateTime.Now - AQI.cam.last_trigger_time.Read()).TotalSeconds;
                string tmpfile = "";

                //only trigger if cameras cooldown time since last detection has passed
                if (cooltime >= AQI.cam.cooldown_time_seconds)
                {

                    if (AQI.cam.Action_image_merge_detections && AQI.Trigger)
                    {
                        tmpfile = await this.MergeImageAnnotations(AQI);

                        if (!string.Equals(AQI.CurImg.image_path, tmpfile, StringComparison.OrdinalIgnoreCase) && System.IO.File.Exists(tmpfile))  //it wont exist if no detections or failure...
                            AQI.CurImg = new ClsImageQueueItem(tmpfile, 1);
                    }

                    if (AQI.cam.Action_image_copy_enabled && AQI.Trigger)
                    {
                        Log($"Debug:   Copying image to network folder...", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        string newimagepath = "";
                        if (!this.CopyImage(AQI, ref newimagepath))
                        {
                            ret = false;
                            Log($"Warn:   -> Warning: Image could not be copied to network folder.", AQI.CurImg.image_path);
                        }
                        else
                        {
                            Log($"Debug:   -> Image copied to network folder.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                            //set the image path to the new path so all imagename variable works
                            AQI.CurImg = new ClsImageQueueItem(newimagepath, 1);
                        }

                    }

                    //call trigger urls
                    if (AQI.Trigger && AQI.cam.trigger_urls.Count() > 0)
                    {
                        //replace url paramters with according values
                        List<string> urls = new List<string>();
                        //call urls
                        foreach (string url in AQI.cam.trigger_urls)
                        {
                            string tmp = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, url);
                            urls.Add(tmp);

                        }

                        bool result = await this.CallTriggerURLs(urls, AQI.Trigger);
                    }
                    else if (!AQI.Trigger && AQI.cam.cancel_urls.Count() > 0)
                    {
                        //replace url paramters with according values
                        List<string> urls = new List<string>();
                        //call urls
                        foreach (string url in AQI.cam.cancel_urls)
                        {
                            string tmp = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, url);
                            urls.Add(tmp);

                        }

                        bool result = await this.CallTriggerURLs(urls, AQI.Trigger);

                    }

                    //run external program
                    if (AQI.cam.Action_RunProgram && AQI.Trigger)
                    {
                        string run = "";
                        string param = "";
                        try
                        {
                            run = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_RunProgramString);
                            param = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_RunProgramArgsString);
                            Log($"Debug:   Starting external app - Camera={AQI.cam.Name} run='{run}', param='{param}'", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                            Process.Start(run, param);
                        }
                        catch (Exception ex)
                        {

                            ret = false;
                            Log($"Error: while running program '{run}' with params '{param}', got: {Global.ExMsg(ex)}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }
                    }

                    //Play sounds
                    if (AQI.cam.Action_PlaySounds && AQI.Trigger)
                    {
                        try
                        {

                            //object1, object2 ; soundfile.wav | object1, object2 ; anotherfile.wav | * ; defaultsound.wav
                            string snds = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_Sounds);

                            List<string> items = Global.Split(snds, "|");

                            foreach (string itm in items)
                            {
                                //object1, object2 ; soundfile.wav
                                int played = 0;
                                List<string> prms = Global.Split(itm, "|");
                                foreach (string prm in prms)
                                {
                                    //prm0 - object1, object2
                                    //prm1 - soundfile.wav
                                    List<string> splt = Global.Split(prm, ";");
                                    string soundfile = splt[1];
                                    List<string> objects = Global.Split(splt[0], ",");
                                    foreach (string objname in objects)
                                    {
                                        foreach (string detection in AQI.cam.last_detections)
                                        {
                                            if (detection.IndexOf(objname, StringComparison.OrdinalIgnoreCase) >= 0 || (objname == "*"))
                                            {
                                                Log($"Debug:   Playing sound because '{objname}' was detected: {soundfile}...", AQI.CurImg.image_path);
                                                SoundPlayer sp = new SoundPlayer(soundfile);
                                                sp.Play();
                                                played++;
                                            }
                                        }
                                    }
                                }
                                if (played == 0)
                                {
                                    Log($"Debug: No object matched sound to play or no detections.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                            ret = false;
                            Log($"Error: while calling sound '{AQI.cam.Action_Sounds}', got: {Global.ExMsg(ex)}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }
                    }

                    if (AQI.cam.Action_mqtt_enabled)
                    {
                        string topic = "";
                        string payload = "";

                        if (AQI.Trigger)
                        {
                            topic = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_mqtt_topic);
                            payload = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_mqtt_payload);
                        }
                        else
                        {
                            topic = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_mqtt_topic_cancel);
                            payload = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_mqtt_payload_cancel);
                        }

                        //Log($"Debug: [SummaryNonEscaped]='{AQI.Hist.Detections}', After replacement Topic='{topic}', Payload='{payload}'");

                        List<string> topics = Global.Split(topic, "|");
                        List<string> payloads = Global.Split(payload, "|");

                        ClsImageQueueItem ci = null;

                        for (int i = 0; i < topics.Count; i++)
                        {
                            if (AQI.cam.Action_mqtt_send_image && topics[i].IndexOf("/image", StringComparison.OrdinalIgnoreCase) >= 0)
                                ci = AQI.CurImg;
                            else
                                ci = null;
                            MqttClientPublishResult pr = await AITOOL.mqttClient.PublishAsync(topics[i], payloads[i], AQI.cam.Action_mqtt_retain_message, ci);
                            if (pr == null || pr.ReasonCode != MqttClientPublishReasonCode.Success)
                                ret = false;

                        }


                    }

                    //upload to telegram
                    if (AQI.cam.Action_pushover_enabled && AQI.Trigger)
                    {

                        if (!await this.PushoverUpload(AQI))
                        {
                            ret = false;
                            Log($"Error:   -> ERROR sending image to Pushover", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }
                        else
                        {
                            Log($"Debug:   -> Sent image to Pushover.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }

                    }



                    //upload to telegram
                    if (AQI.cam.telegram_enabled && AQI.Trigger)
                    {

                        if (!await this.TelegramUpload(AQI))
                        {
                            ret = false;
                            Log($"Error:   -> ERROR sending image to Telegram.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }
                        else
                        {
                            Log($"Debug:   -> Sent image to Telegram.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        }

                    }


                    if (AQI.Trigger)
                    {
                        AQI.cam.last_trigger_time.Write(DateTime.Now); //reset cooldown time every time an image contains something, even if no trigger was called (still in cooldown time)
                        Log($"Debug: {AQI.cam.Name} last triggered at {AQI.cam.last_trigger_time.Read()}.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                        Global.UpdateLabel($"{AQI.cam.Name} last triggered at {AQI.cam.last_trigger_time.Read()}.", "lbl_info");
                    }


                }
                else
                {
                    //log that nothing was done
                    Log($"   Camera {AQI.cam.Name} is still in cooldown. Trigger URL wasn't called and no image will be uploaded to Telegram. ({cooltime} of {AQI.cam.cooldown_time_seconds} seconds - See Cameras 'cooldown_time_seconds' in settings file)", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                }


                if (AQI.cam.Action_image_merge_detections && AQI.Trigger && !string.IsNullOrEmpty(tmpfile) && tmpfile.IndexOf(Environment.GetEnvironmentVariable("TEMP"), StringComparison.OrdinalIgnoreCase) >= 0 && System.IO.File.Exists(tmpfile))
                {
                    System.IO.File.Delete(tmpfile);
                    //Log($"Debug: Deleting tmp file {tmpfile}");
                }


            }
            catch (Exception ex)
            {

                Log($"Error: " + Global.ExMsg(ex), this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
            }


            return ret;

        }


        public async Task<string> MergeImageAnnotations(ClsTriggerActionQueueItem AQI)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            int countr = 0;
            string detections = "";
            string lasttext = "";
            string lastposition = "";
            string OutputImageFile = "";

            try
            {
                Log($"Debug: Merging image annotations: " + AQI.CurImg.image_path, "", "", AQI.CurImg.image_path);

                if (AQI.CurImg.IsValid())
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    using (Bitmap img = new Bitmap(AQI.CurImg.ToStream()))
                    {
                        using (Graphics g = Graphics.FromImage(img))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            //http://csharphelper.com/blog/2014/09/understand-font-aliasing-issues-in-c/
                            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;


                            System.Drawing.Color color = new System.Drawing.Color();

                            if (AQI.Hist != null && !string.IsNullOrEmpty(AQI.Hist.PredictionsJSON))
                            {
                                List<ClsPrediction> predictions = new List<ClsPrediction>();

                                predictions = Global.SetJSONString<List<ClsPrediction>>(AQI.Hist.PredictionsJSON);

                                foreach (var pred in predictions)
                                {
                                    bool Merge = false;

                                    if (AppSettings.Settings.HistoryOnlyDisplayRelevantObjects && pred.Result == ResultType.Relevant)
                                        Merge = true;
                                    else if (!AppSettings.Settings.HistoryOnlyDisplayRelevantObjects)
                                        Merge = true;

                                    if (Merge)
                                    {
                                        if (pred.Result == ResultType.Relevant)
                                        {
                                            color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectRelevantColorAlpha, AppSettings.Settings.RectRelevantColor);
                                        }
                                        else if (pred.Result == ResultType.DynamicMasked || pred.Result == ResultType.ImageMasked || pred.Result == ResultType.StaticMasked)
                                        {
                                            color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectMaskedColorAlpha, AppSettings.Settings.RectMaskedColor);
                                        }
                                        else
                                        {
                                            color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectIrrelevantColorAlpha, AppSettings.Settings.RectIrrelevantColor);
                                        }

                                        int xmin = pred.XMin + AQI.cam.XOffset;
                                        int ymin = pred.YMin + AQI.cam.YOffset;
                                        int xmax = pred.XMax;
                                        int ymax = pred.YMax;

                                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);

                                        using (Pen pen = new Pen(color, AppSettings.Settings.RectBorderWidth))
                                        {
                                            g.DrawRectangle(pen, rect); //draw rectangle
                                        }

                                        //we need this since people can change the border width in the json file
                                        int halfbrd = AppSettings.Settings.RectBorderWidth / 2;

                                        //object name text below rectangle
                                        rect = new System.Drawing.Rectangle(xmin - halfbrd, ymax + halfbrd, img.Width, img.Height); //sets bounding box for drawn text

                                        Brush brush = new SolidBrush(color); //sets background rectangle color

                                        lasttext = pred.ToString();

                                        System.Drawing.SizeF size = g.MeasureString(lasttext, new Font(AppSettings.Settings.RectDetectionTextFont, AppSettings.Settings.RectDetectionTextSize)); //finds size of text to draw the background rectangle
                                        g.FillRectangle(brush, xmin - halfbrd, ymax + halfbrd, size.Width, size.Height); //draw grey background rectangle for detection text
                                        g.DrawString(lasttext, new Font(AppSettings.Settings.RectDetectionTextFont, AppSettings.Settings.RectDetectionTextSize), Brushes.Black, rect); //draw detection text

                                        g.Flush();

                                        countr++;
                                    }

                                }

                            }
                            else
                            {
                                //Use the old way -this code really doesnt need to be here but leaving just to make sure
                                detections = AQI.cam.last_detections_summary;
                                if (string.IsNullOrEmpty(detections))
                                    detections = "";

                                string label = Global.GetWordBetween(detections, "", ":");

                                if (label.IndexOf("irrelevant", StringComparison.OrdinalIgnoreCase) >= 0 || label.IndexOf("confidence", StringComparison.OrdinalIgnoreCase) >= 0 || label.IndexOf("masked", StringComparison.OrdinalIgnoreCase) >= 0 || label.IndexOf("errors", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    detections = detections.Split(':')[1]; //removes the "1x masked, 3x irrelevant:" before the actual detection, otherwise this would be displayed in the detection tags

                                    if (label.IndexOf("masked", StringComparison.OrdinalIgnoreCase) >= 0)
                                    {
                                        color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectMaskedColorAlpha, AppSettings.Settings.RectMaskedColor);
                                    }
                                    else
                                    {
                                        color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectIrrelevantColorAlpha, AppSettings.Settings.RectIrrelevantColor);
                                    }
                                }
                                else
                                {
                                    color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectRelevantColorAlpha, AppSettings.Settings.RectRelevantColor);
                                }

                                //List<string> detectlist = Global.Split(detections, "|;");
                                countr = AQI.cam.last_detections.Count();

                                //display a rectangle around each relevant object


                                for (int i = 0; i < countr; i++)
                                {
                                    //({ Math.Round((user.confidence * 100), 2).ToString() }%)
                                    lasttext = $"{AQI.cam.last_detections[i]} {String.Format(AppSettings.Settings.DisplayPercentageFormat, AQI.cam.last_confidences[i])}";
                                    lastposition = AQI.cam.last_positions[i];  //load 'xmin,ymin,xmax,ymax' from third column into a string

                                    //store xmin, ymin, xmax, ymax in separate variables
                                    Int32.TryParse(lastposition.Split(',')[0], out int xmin);
                                    Int32.TryParse(lastposition.Split(',')[1], out int ymin);
                                    Int32.TryParse(lastposition.Split(',')[2], out int xmax);
                                    Int32.TryParse(lastposition.Split(',')[3], out int ymax);

                                    xmin = xmin + AQI.cam.XOffset;
                                    ymin = ymin + AQI.cam.YOffset;

                                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);


                                    using (Pen pen = new Pen(color, AppSettings.Settings.RectBorderWidth))
                                    {
                                        g.DrawRectangle(pen, rect); //draw rectangle
                                    }

                                    //we need this since people can change the border width in the json file
                                    int halfbrd = AppSettings.Settings.RectBorderWidth / 2;

                                    //object name text below rectangle
                                    rect = new System.Drawing.Rectangle(xmin - halfbrd, ymax + halfbrd, img.Width, img.Height); //sets bounding box for drawn text


                                    Brush brush = new SolidBrush(color); //sets background rectangle color

                                    System.Drawing.SizeF size = g.MeasureString(lasttext, new Font(AppSettings.Settings.RectDetectionTextFont, AppSettings.Settings.RectDetectionTextSize)); //finds size of text to draw the background rectangle
                                    g.FillRectangle(brush, xmin - halfbrd, ymax + halfbrd, size.Width, size.Height); //draw grey background rectangle for detection text
                                    g.DrawString(lasttext, new Font(AppSettings.Settings.RectDetectionTextFont, AppSettings.Settings.RectDetectionTextSize), Brushes.Black, rect); //draw detection text

                                    g.Flush();

                                    //Log($"...{i}, LastText='{lasttext}' - LastPosition='{lastposition}'");
                                }

                            }


                            if (countr > 0)
                            {

                                GraphicsState gs = g.Save();

                                ImageCodecInfo jpgEncoder = this.GetEncoder(ImageFormat.Jpeg);

                                // Create an Encoder object based on the GUID  
                                // for the Quality parameter category.  
                                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

                                // Create an EncoderParameters object.  
                                // An EncoderParameters object has an array of EncoderParameter  
                                // objects. In this case, there is only one  
                                // EncoderParameter object in the array.  
                                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, AQI.cam.Action_image_merge_jpegquality);  //100=least compression, largest file size, best quality
                                myEncoderParameters.Param[0] = myEncoderParameter;

                                Global.WaitFileAccessResult result = new Global.WaitFileAccessResult();
                                result.Success = true; //assume true

                                if (AQI.cam.Action_image_merge_detections_makecopy)
                                    OutputImageFile = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), Path.GetFileName(AQI.CurImg.image_path));
                                else
                                    OutputImageFile = AQI.CurImg.image_path;

                                if (System.IO.File.Exists(OutputImageFile))
                                {
                                    result = await Global.WaitForFileAccessAsync(OutputImageFile, FileAccess.ReadWrite, FileShare.None);
                                }

                                if (result.Success)
                                {
                                    img.Save(OutputImageFile, jpgEncoder, myEncoderParameters);
                                    Log($"Debug: Merged {countr} detections in {sw.ElapsedMilliseconds}ms into image {OutputImageFile}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                                }
                                else
                                {
                                    Log($"Error: Could not gain access to write merged file {OutputImageFile}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                                }

                            }
                            else
                            {
                                Log($"Debug: No detections to merge.  Time={sw.ElapsedMilliseconds}ms, {OutputImageFile}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);

                            }

                        }

                    }

                }
                else
                {
                    Log($"Error: could not find last image with detections: " + AQI.CurImg.image_path, this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                }
            }
            catch (Exception ex)
            {

                Log($"Error: Detections='{detections}', LastText='{lasttext}', LastPostions='{lastposition}' - " + Global.ExMsg(ex), this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
            }

            return OutputImageFile;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public bool CopyImage(ClsTriggerActionQueueItem AQI, ref string dest_path)
        {
            bool ret = false;

            try
            {
                string netfld = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_network_folder);

                string ext = Path.GetExtension(AQI.CurImg.image_path);
                string filename = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_network_folder_filename).Trim() + ext;

                dest_path = System.IO.Path.Combine(netfld, filename);

                Log($"Debug:  File copying from {AQI.CurImg.image_path} to {dest_path}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);


                if (AQI.CurImg.CopyFileTo(dest_path))
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                ret = false;
                Log($"ERROR: Could not copy image {AQI.CurImg.image_path} to network path {dest_path}: {Global.ExMsg(ex)}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
            }

            return ret;

        }
        //call trigger urls
        public async Task<bool> CallTriggerURLs(List<string> trigger_urls, bool Trigger)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool ret = true;

            using (WebClient client = new WebClient())
            {
                string type = "trigger";
                if (!Trigger)
                    type = "cancel";

                foreach (string url in trigger_urls)
                {
                    try
                    {
                        string content = await client.DownloadStringTaskAsync(url);
                        Log($"Debug:   -> {type} URL called: {url}, response: '{content.Replace("\r\n", "\n").Replace("\n", " ")}'");
                    }
                    catch (Exception ex)
                    {
                        ret = false;
                        Log($"ERROR: Could not {type} URL '{url}', please check if '{url}' is correct and reachable: {Global.ExMsg(ex)}");
                    }

                }

            }

            return ret;


        }

        public async Task<bool> PushoverUpload(ClsTriggerActionQueueItem AQI)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool ret = false;

            if (!string.IsNullOrEmpty(AppSettings.Settings.pushover_APIKey) && !string.IsNullOrEmpty(AppSettings.Settings.pushover_UserKey))
            {

                //make sure it is a matching object
                if (!string.IsNullOrEmpty(AQI.cam.Action_pushover_triggering_objects))
                {

                    List<ClsPrediction> preds = Global.SetJSONString<List<ClsPrediction>>(AQI.Hist.PredictionsJSON);
                    if (preds != null && preds.Count > 0)
                    {
                        //find at least one thing in the triggered objects list in order to send
                        string bad = "";
                        bool fnd = false;
                        foreach (ClsPrediction pred in preds)
                        {
                            if (Global.IsInList(pred.Label, AQI.cam.Action_pushover_triggering_objects))
                                fnd = true;
                            else
                                bad += pred.Label + ",";
                        }
                        if (!fnd)
                        {
                            Log($"Debug: Skipping pushover because object(s) '{bad}' not in trigger objects list '{AQI.cam.Action_pushover_triggering_objects}'", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                            return false;
                        }
                    }
                }

                if (AppSettings.Settings.pushover_cooldown_seconds < 2)
                {
                    AppSettings.Settings.pushover_cooldown_seconds = 2;  //force to be at least 2 seconds
                }

                if (this.PushoverRetryTime.Read() == DateTime.MinValue || DateTime.Now >= this.PushoverRetryTime.Read())
                {
                    double cooltime = Math.Round((DateTime.Now - this.last_Pushover_trigger_time.Read()).TotalSeconds, 4);
                    if (cooltime >= AppSettings.Settings.pushover_cooldown_seconds)
                    {


                        string title = "";
                        string message = "";
                        string device = "";

                        if (AQI.Trigger)
                        {
                            title = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_pushover_title);
                            message = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_pushover_message);
                            device = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_pushover_device);
                        }
                        else  //TODO: Add cancel if requested
                        {
                            title = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_pushover_title);
                            message = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_pushover_message);
                            device = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.Action_pushover_device);
                        }


                        List<string> titles = Global.Split(title, "|");
                        List<string> messages = Global.Split(message, "|");
                        List<string> devices = Global.Split(device, "|");

                        if (AITOOL.pushoverClient == null)
                        {
                            AITOOL.pushoverClient = new NPushover.Pushover(AppSettings.Settings.pushover_APIKey); //new PushoverClient.Pushover(, AppSettings.Settings.pushover_UserKey);
                        }

                        for (int i = 0; i < titles.Count; i++)
                        {
                            PushoverUserResponse response = null;

                            Stopwatch sw = Stopwatch.StartNew();

                            try
                            {
                                string pushtitle = titles[i];
                                string pushmessage = messages[i];
                                string pushdevice = "";
                                string attachment = "";
                                if (i <= devices.Count - 1)
                                    pushdevice = devices[i];

                                //MemoryStream ms = AQI.CurImg.ToStream();
                                //if (ms != null && ms.Length > 32)
                                //    attachment = $"(\"image.jpg\", {ms.ConvertToBase64()}, \"image/jpeg\")";
                                
                                NPushover.RequestObjects.Message msg = new NPushover.RequestObjects.Message()
                                {
                                    Title = pushtitle,
                                    Body = pushmessage,
                                    Timestamp = AQI.CurImg.TimeCreated,
                                    Priority = (NPushover.RequestObjects.Priority)Enum.Parse(typeof(NPushover.RequestObjects.Priority), AQI.cam.Action_pushover_Priority),
                                    Sound = AQI.cam.Action_pushover_Sound,
                                    Attachment = attachment,
                                    //RetryOptions = new RetryOptions
                                    //{
                                    //    RetryEvery = TimeSpan.FromSeconds(30),
                                    //    RetryPeriod = TimeSpan.FromHours(3)
                                    //},
                                    //SupplementaryUrl = new SupplementaryURL
                                    //{
                                    //    Uri = new Uri("http://robiii.me"),
                                    //    Title = "Awesome dude!"
                                    //},
                                };

                                sw.Restart();

                                response = await AITOOL.pushoverClient.SendMessageAsync(msg, AppSettings.Settings.pushover_UserKey, pushdevice);

                                sw.Stop();
                            }
                            catch (Exception ex)
                            {

                                sw.Stop();
                                ret = false;
                                Log($"Error: Pushover: After {sw.ElapsedMilliseconds}ms, got: " + Global.ExMsg(ex), this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                            }

                            if (response != null)
                            {
                                string rateinfo = "";
                                if (response.RateLimitInfo != null)
                                {
                                    rateinfo = $"(Monthly Limit={response.RateLimitInfo.Limit}, Remaining={response.RateLimitInfo.Remaining}, ResetDate={response.RateLimitInfo.Reset})";
                                }

                                if (response.IsOk)
                                {
                                    ret = true;
                                    Log($"Debug: Pushover success in {sw.ElapsedMilliseconds}ms {rateinfo}");
                                }
                                else
                                {
                                    string errs = "";
                                    if (response.HasErrors)
                                        errs = string.Join(";", response.Errors);
                                    ret = false;
                                    Log($"Error: Pushover response code={response.Status} in {sw.ElapsedMilliseconds}ms, Errs='{errs}' {rateinfo}");
                                }
                            }
                            else
                            {
                                ret = false;
                                Log($"Error: Pushover failed to return a response in {sw.ElapsedMilliseconds}ms?", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                            }

                            if (!ret)
                                this.PushoverRetryTime.Write(DateTime.Now.AddSeconds(AppSettings.Settings.Pushover_RetryAfterFailSeconds));
                            else
                                this.PushoverRetryTime.Write(DateTime.MinValue);

                        }
                    }
                    else
                    {
                        //log that nothing was done
                        Log($"Debug:   Still in PUSHOVER cooldown. No image will be uploaded to Pushover.  ({cooltime} of {AppSettings.Settings.pushover_cooldown_seconds} seconds - See 'pushover_cooldown_seconds' in settings file)", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);

                    }
                }
                else
                {
                    Log($"Debug:   Waiting {Math.Round((this.PushoverRetryTime.Read() - DateTime.Now).TotalSeconds, 1)} seconds ({this.PushoverRetryTime}) to retry PUSHOVER connection.  This is due to a previous pushover send error.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                }

            }
            else
            {
                ret = false;
                Log("Error: Pushover API key not set.");
            }


            return ret;
        }
        //send image to Telegram
        public async Task<bool> TelegramUpload(ClsTriggerActionQueueItem AQI)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool ret = false;
            string lastchatid = "";

            if ((!string.IsNullOrWhiteSpace(AQI.cam.telegram_chatid) || AppSettings.Settings.telegram_chatids.Count > 0) && AppSettings.Settings.telegram_token != "")
            {
                //telegram upload sometimes fails
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    if (AppSettings.Settings.telegram_cooldown_seconds < 2)
                    {
                        AppSettings.Settings.telegram_cooldown_seconds = 2;  //force to be at least 2 seconds
                    }

                    string Caption = "";

                    if (!string.IsNullOrEmpty(AQI.Text))
                        Caption = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.Text);
                    else
                        Caption = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.cam.telegram_caption);



                    //make sure it is a matching object
                    if (!string.IsNullOrEmpty(AQI.cam.telegram_triggering_objects))
                    {
                        List<ClsPrediction> preds = Global.SetJSONString<List<ClsPrediction>>(AQI.Hist.PredictionsJSON);
                        if (preds != null && preds.Count > 0)
                        {
                            //find at least one thing in the triggered objects list in order to send
                            string bad = "";
                            bool fnd = false;
                            foreach (ClsPrediction pred in preds)
                            {
                                if (Global.IsInList(pred.Label, AQI.cam.telegram_triggering_objects))
                                    fnd = true;
                                else
                                    bad += pred.Label + ",";
                            }
                            if (!fnd)
                            {
                                Log($"Debug: Skipping telegram because object(s) '{bad}' not in trigger objects list '{AQI.cam.telegram_triggering_objects}'", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                                return false;
                            }
                        }
                    }

                    if (this.TelegramRetryTime.Read() == DateTime.MinValue || DateTime.Now >= this.TelegramRetryTime.Read())
                    {
                        double cooltime = Math.Round((DateTime.Now - this.last_telegram_trigger_time.Read()).TotalSeconds, 4);
                        if (cooltime >= AppSettings.Settings.telegram_cooldown_seconds)
                        {
                            //in order to avoid hitting our limits when sending out mass notifications, consider spreading them over longer intervals, e.g. 8-12 hours. The API will not allow bulk notifications to more than ~30 users per second, if you go over that, you'll start getting 429 errors.

                            //using (var image_telegram = System.IO.File.OpenRead(AQI.CurImg.image_path))
                            //{

                            if (AITOOL.telegramBot == null)
                                AITOOL.telegramBot = new TelegramBotClient(AppSettings.Settings.telegram_token);

                            string chatid = "";
                            bool overrideid = (!string.IsNullOrWhiteSpace(AQI.cam.telegram_chatid));
                            if (overrideid)
                                chatid = AQI.cam.telegram_chatid.Trim();
                            else
                                chatid = AppSettings.Settings.telegram_chatids[0];

                            //upload image to Telegram servers and send to first chat
                            Log($"Debug:      uploading image to chat \"{chatid}\"", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                            lastchatid = chatid;
                            Telegram.Bot.Types.Message message = await AITOOL.telegramBot.SendPhotoAsync(chatid, new InputOnlineFile(AQI.CurImg.ToStream(), "image.jpg"), Caption);

                            string file_id = message.Photo[0].FileId; //get file_id of uploaded image

                            if (!overrideid)
                            {
                                //share uploaded image with all remaining telegram chats (if multiple chat_ids given) using file_id 
                                foreach (string curchatid in AppSettings.Settings.telegram_chatids.Skip(1))
                                {
                                    Log($"Debug:      uploading image to chat \"{curchatid}\"...", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                                    lastchatid = curchatid;
                                    await AITOOL.telegramBot.SendPhotoAsync(curchatid, file_id, Caption);
                                }
                            }
                            ret = true;
                            //}

                            this.last_telegram_trigger_time.Write(DateTime.Now);
                            this.TelegramRetryTime.Write(DateTime.MinValue);

                            if (AQI.IsQueued)
                            {
                                //add a minimum delay if we are in a queue to prevent minimum cooldown error
                                Log($"Debug: Waiting {AppSettings.Settings.telegram_cooldown_seconds} seconds (telegram_cooldown_seconds)...", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                                await Task.Delay(TimeSpan.FromSeconds(AppSettings.Settings.telegram_cooldown_seconds));
                            }

                        }
                        else
                        {
                            //log that nothing was done
                            Log($"Debug:   Still in TELEGRAM cooldown. No image will be uploaded to Telegram.  ({cooltime} of {AppSettings.Settings.telegram_cooldown_seconds} seconds - See 'telegram_cooldown_seconds' in settings file)", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);

                        }

                    }
                    else
                    {
                        Log($"Debug:   Waiting {Math.Round((this.TelegramRetryTime.Read() - DateTime.Now).TotalSeconds, 1)} seconds ({this.TelegramRetryTime}) to retry TELEGRAM connection.  This is due to a previous telegram send error.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    }


                }
                catch (ApiRequestException ex)  //current version only gives webexception NOT this exception!  https://github.com/TelegramBots/Telegram.Bot/issues/891
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Log($"ERROR: Could not upload image {AQI.CurImg.image_path} with chatid '{lastchatid}' to Telegram: {Global.ExMsg(ex)}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    this.TelegramRetryTime.Write(DateTime.Now.AddSeconds(ex.Parameters.RetryAfter));
                    Log($"...BOT API returned 'RetryAfter' value '{ex.Parameters.RetryAfter} seconds', so not retrying until {this.TelegramRetryTime}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    AppSettings.Settings.send_errors = se;
                    //store image that caused an error in ./errors/
                    if (!Directory.Exists("./errors/")) //if folder does not exist, create the folder
                    {
                        //create folder
                        DirectoryInfo di = Directory.CreateDirectory("./errors");
                        Log($"./errors/" + " dir created.");
                    }
                    //save error image
                    using (var image = await SixLabors.ImageSharp.Image.LoadAsync(AQI.CurImg.image_path))
                    {
                        await image.SaveAsync($"./errors/" + "TELEGRAM-ERROR-" + Path.GetFileName(AQI.CurImg.image_path) + ".jpg");
                    }
                    Global.UpdateLabel($"Can't upload error message to Telegram!", "lbl_errors");

                }
                catch (Exception ex)  //As of version 
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Log($"ERROR: Could not upload image {AQI.CurImg.image_path} to Telegram with chatid '{lastchatid}': {Global.ExMsg(ex)}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    this.TelegramRetryTime.Write(DateTime.Now.AddSeconds(AppSettings.Settings.Telegram_RetryAfterFailSeconds));
                    Log($"Debug: ...'Default' 'Telegram_RetryAfterFailSeconds' value was set to '{AppSettings.Settings.Telegram_RetryAfterFailSeconds}' seconds, so not retrying until {this.TelegramRetryTime}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    AppSettings.Settings.send_errors = se;
                    //store image that caused an error in ./errors/
                    if (!Directory.Exists("./errors/")) //if folder does not exist, create the folder
                    {
                        //create folder
                        DirectoryInfo di = Directory.CreateDirectory("./errors");
                        Log($"./errors/" + " dir created.");
                    }
                    //save error image
                    using (var image = await SixLabors.ImageSharp.Image.LoadAsync(AQI.CurImg.image_path))
                    {
                        await image.SaveAsync("./errors/" + "TELEGRAM-ERROR-" + Path.GetFileName(AQI.CurImg.image_path) + ".jpg");
                    }
                    Global.UpdateLabel($"Can't upload error message to Telegram!", "lbl_errors");

                }


                Log($"Debug: ...Finished in {sw.ElapsedMilliseconds}ms", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);

            }
            else
            {
                Log($"Error:  Telegram settings misconfigured. telegram_chatids.Count={AppSettings.Settings.telegram_chatids.Count} ({string.Join(",", AppSettings.Settings.telegram_chatids)}), telegram_token='{AppSettings.Settings.telegram_token}'", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
            }

            return ret;

        }

        //send text to Telegram
        public async Task<bool> TelegramText(ClsTriggerActionQueueItem AQI)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool ret = false;
            string lastchatid = "";
            if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
            {

                //telegram upload sometimes fails
                try
                {

                    if (AppSettings.Settings.telegram_cooldown_seconds < 2)
                    {
                        AppSettings.Settings.telegram_cooldown_seconds = 2;  //force to be at least 1 second
                    }

                    string Caption = AITOOL.ReplaceParams(AQI.cam, AQI.Hist, AQI.CurImg, AQI.Text);

                    if (this.TelegramRetryTime.Read() == DateTime.MinValue || DateTime.Now >= this.TelegramRetryTime.Read())
                    {
                        double cooltime = Math.Round((DateTime.Now - this.last_telegram_trigger_time.Read()).TotalSeconds, 4);
                        if (cooltime >= AppSettings.Settings.telegram_cooldown_seconds)
                        {

                            string chatid = "";
                            bool overrideid = (!string.IsNullOrWhiteSpace(AQI.cam.telegram_chatid));
                            if (overrideid)
                                chatid = AQI.cam.telegram_chatid.Trim();
                            else
                                chatid = AppSettings.Settings.telegram_chatids[0];

                            if (AITOOL.telegramBot == null)
                                AITOOL.telegramBot = new TelegramBotClient(AppSettings.Settings.telegram_token);

                            if (overrideid)
                            {
                                lastchatid = chatid;
                                Telegram.Bot.Types.Message msg = await AITOOL.telegramBot.SendTextMessageAsync(chatid, Caption);

                            }
                            else
                            {
                                foreach (string curchatid in AppSettings.Settings.telegram_chatids)
                                {
                                    lastchatid = curchatid;
                                    Telegram.Bot.Types.Message msg = await AITOOL.telegramBot.SendTextMessageAsync(curchatid, Caption);

                                }

                            }
                            this.last_telegram_trigger_time.Write(DateTime.Now);
                            this.TelegramRetryTime.Write(DateTime.MinValue);

                            if (AQI.IsQueued)
                            {
                                //add a minimum delay if we are in a queue to prevent minimum cooldown error
                                Log($"Waiting {AppSettings.Settings.telegram_cooldown_seconds} seconds (telegram_cooldown_seconds)...", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                                await Task.Delay(TimeSpan.FromSeconds(AppSettings.Settings.telegram_cooldown_seconds));
                            }

                            ret = true;
                        }
                        else
                        {
                            //log that nothing was done
                            Log($"   Still in TELEGRAM cooldown. No image will be uploaded to Telegram.  ({cooltime} of {AppSettings.Settings.telegram_cooldown_seconds} seconds - See 'telegram_cooldown_seconds' in settings file)", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);

                        }

                    }
                    else
                    {
                        Log($"   Waiting {Math.Round((this.TelegramRetryTime.Read() - DateTime.Now).TotalSeconds, 1)} seconds ({this.TelegramRetryTime}) to retry TELEGRAM connection.  This is due to a previous telegram send error.", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    }



                }
                catch (ApiRequestException ex)  //current version only gives webexception NOT this exception!  https://github.com/TelegramBots/Telegram.Bot/issues/891
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Log($"ERROR: Could not upload text '{AQI.Text}' with chatid '{lastchatid}' to Telegram: {Global.ExMsg(ex)}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    this.TelegramRetryTime.Write(DateTime.Now.AddSeconds(ex.Parameters.RetryAfter));
                    Log($"...BOT API returned 'RetryAfter' value '{ex.Parameters.RetryAfter} seconds', so not retrying until {this.TelegramRetryTime}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    AppSettings.Settings.send_errors = se;
                    Global.UpdateLabel($"Can't upload error message to Telegram!", "lbl_errors");

                }
                catch (Exception ex)
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Log($"ERROR: Could not upload image '{AQI.Text}' with chatid '{lastchatid}' to Telegram: {Global.ExMsg(ex)}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    this.TelegramRetryTime.Write(DateTime.Now.AddSeconds(AppSettings.Settings.Telegram_RetryAfterFailSeconds));
                    Log($"...'Default' 'Telegram_RetryAfterFailSeconds' value was set to '{AppSettings.Settings.Telegram_RetryAfterFailSeconds}' seconds, so not retrying until {this.TelegramRetryTime}", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
                    AppSettings.Settings.send_errors = se;
                    Global.UpdateLabel($"Can't upload error message to Telegram!", "lbl_errors");
                }

            }
            else
            {
                Log($"Error:  Telegram settings misconfigured. telegram_chatids.Count={AppSettings.Settings.telegram_chatids.Count} ({string.Join(",", AppSettings.Settings.telegram_chatids)}), telegram_token='{AppSettings.Settings.telegram_token}'", this.CurSrv, AQI.cam.Name, AQI.CurImg.image_path);
            }

            return ret;
        }



    }
}
