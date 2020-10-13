using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AITool.Properties;
using MQTTnet.Client.Publishing;
using SixLabors.ImageSharp;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.InputFiles;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Text;
using System.Security.AccessControl;

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

        public ThreadSafe.Datetime last_telegram_trigger_time { get; set; } = new ThreadSafe.Datetime(DateTime.MinValue);
        public ThreadSafe.Datetime TelegramRetryTime { get; set; } = new ThreadSafe.Datetime(DateTime.MinValue);
        public ClsURLItem _url { get; set; } = null;
        private String CurSrv = "";
        string ImgPath = "NoImage";
        public ThreadSafe.Integer Count { get; set; } = new ThreadSafe.Integer(0);
        public MovingCalcs QCountCalc { get; set; } = new MovingCalcs(250);
        public MovingCalcs QTimeCalc { get; set; } = new MovingCalcs(250);
        public MovingCalcs ActionTimeCalc { get; set; } = new MovingCalcs(250);
        public MovingCalcs TotalTimeCalc { get; set; } = new MovingCalcs(250);
        public ClsTriggerActionQueue()
        {
            Task.Run(TriggerActionJobQueueLoop);
        }

        public async Task<bool> AddTriggerActionAsync(TriggerType ttype, Camera cam, ClsImageQueueItem CurImg, History hist, bool Trigger, bool Wait, ClsURLItem ds_url, string Text)
        {
            bool ret = false;
            this._url = ds_url;
            if (ds_url != null)
            {
                Uri uri = new Uri(ds_url.url);
                this.CurSrv = uri.Host + ":" + uri.Port;
            }
            else
            {
                this.CurSrv = "NoServer";
            }
            if (CurImg != null)
            {
                ImgPath = CurImg.image_path;
            }

            ClsTriggerActionQueueItem AQI = new ClsTriggerActionQueueItem(ttype, cam, CurImg, hist, Trigger, Text, !Wait);

            if (Wait)  //not queued
            {
                ret = await RunTriggers(AQI);
            }
            else
            {
                if (this.TriggerActionQueue.Count <= AppSettings.Settings.MaxActionQueueSize)
                {
                    if (!this.TriggerActionQueue.TryAdd(AQI))
                    {
                        Global.Log($"{CurSrv} - Error: Action '{AQI.TType}' could not be added? {ImgPath}");
                    }
                    else
                    {
                        ret = true;
                    }
                }
                else
                {
                    Global.Log($"{CurSrv} - Error: Action '{AQI.TType}' could not be added because queue size is {TriggerActionQueue.Count} and the max is {AppSettings.Settings.MaxActionQueueSize} (MaxActionQueueSize) - {ImgPath}");
                }

            }

            return ret;
        }

        private async void TriggerActionJobQueueLoop()
        {

            //this runs forever and blocks if nothing is in the queue
            foreach (ClsTriggerActionQueueItem AQI in TriggerActionQueue.GetConsumingEnumerable())
            {
                try
                {
                    await RunTriggers(AQI);
                    Thread.Sleep(250); //very short wait between trigger events
                }
                catch (Exception ex)
                {

                    Global.Log($"{CurSrv} - Error: " + ex.ToString());
                }

            }

            Global.Log($"{CurSrv} - Error: Should not have left ActionQueueLoop?");

        }

        public async Task<bool> RunTriggers(ClsTriggerActionQueueItem AQI)
        {
            bool res = false;

            try
            {
                AQI.QueueWaitMS = Convert.ToInt64((DateTime.Now - AQI.AddedTime).TotalMilliseconds);
                this.QTimeCalc.AddToCalc(AQI.QueueWaitMS);
                bool WasSkipped = false;

                Stopwatch sw = Stopwatch.StartNew();

                if (this.TriggerActionQueue.Count == 0)
                    this.Count.WriteFullFence(1);
                else
                    this.Count.WriteFullFence(this.TriggerActionQueue.Count);

                AQI.QueueCount = this.Count.ReadFullFence();
                this.QCountCalc.AddToCalc(AQI.QueueCount);

                Global.SendMessage(MessageType.UpdateStatus);

                if (AQI.TType == TriggerType.TelegramText)
                {
                    if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
                        res = await TelegramText(AQI);
                    else
                        WasSkipped = true;
                }
                else if (AQI.TType == TriggerType.TelegramImageUpload)
                {
                    if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
                        res = await TelegramUpload(AQI);
                    else
                        WasSkipped = true;
                }
                else
                {
                    res = await Trigger(AQI);
                }

                this.Count.WriteFullFence(this.TriggerActionQueue.Count);

                AQI.ActionTimeMS = sw.ElapsedMilliseconds;
                AQI.TotalTimeMS = Convert.ToInt64((DateTime.Now - AQI.AddedTime).TotalMilliseconds);
                this.TotalTimeCalc.AddToCalc(AQI.TotalTimeMS);
                this.ActionTimeCalc.AddToCalc(AQI.ActionTimeMS);

                if (!WasSkipped)
                {
                    Global.Log($"{CurSrv} - Action '{AQI.TType}' done. Succeeded={res}, Trigger={AQI.Trigger}, Queued={AQI.IsQueued}, Queue Count={AQI.QueueCount} (Min={this.QCountCalc.Min}ms,Max={this.QCountCalc.Max}ms,Avg={this.QCountCalc.Average}ms), Total time={AQI.TotalTimeMS}ms (Min={this.TotalTimeCalc.Min}ms,Max={this.TotalTimeCalc.Max}ms,Avg={Convert.ToInt64(this.TotalTimeCalc.Average)}ms), Queue time={AQI.QueueWaitMS} (Min={this.QTimeCalc.Min}ms,Max={this.QTimeCalc.Max}ms,Avg={Convert.ToInt64(this.QTimeCalc.Average)}ms), Action Time={AQI.ActionTimeMS}ms (Min={this.ActionTimeCalc.Min}ms,Max={this.ActionTimeCalc.Max}ms,Avg={Convert.ToInt64(this.ActionTimeCalc.Average)}ms), Image={this.ImgPath}");
                }

                Global.SendMessage(MessageType.UpdateStatus);

            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));
            }

            return res;
        }

        //trigger actions
        public async Task<bool> Trigger(ClsTriggerActionQueueItem AQI)
        {
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
                    Global.Log($"{CurSrv} - Error: No image to process?");
                    return false;
                }
            }

            try
            {
                double cooltime = (DateTime.Now - AQI.cam.last_trigger_time.Read()).TotalMinutes;
                string tmpfile = "";

                //only trigger if cameras cooldown time since last detection has passed
                if (cooltime >= AQI.cam.cooldown_time)
                {

                    if (AQI.cam.Action_image_merge_detections && AQI.Trigger)
                    {
                        tmpfile = await MergeImageAnnotations(AQI);
                        
                        if (AQI.CurImg.image_path.ToLower() != tmpfile.ToLower() && System.IO.File.Exists(tmpfile))  //it wont exist if no detections or failure...
                            AQI.CurImg = new ClsImageQueueItem(tmpfile, 1);
                    }

                    if (AQI.cam.Action_image_copy_enabled && AQI.Trigger)
                    {
                        Global.Log($"{CurSrv} -    Copying image to network folder...");
                        string newimagepath = "";
                        if (!CopyImage(AQI.cam, AQI.CurImg, ref newimagepath))
                        {
                            ret = false;
                            Global.Log($"{CurSrv} -    -> Warning: Image could not be copied to network folder.");
                        }
                        else
                        {
                            Global.Log($"{CurSrv} -    -> Image copied to network folder.");
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
                            string tmp = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, url);
                            urls.Add(tmp);

                        }

                        bool result = await CallTriggerURLs(urls, AQI.Trigger);
                    }
                    else if (!AQI.Trigger && AQI.cam.cancel_urls.Count() > 0)
                    {
                        //replace url paramters with according values
                        List<string> urls = new List<string>();
                        //call urls
                        foreach (string url in AQI.cam.cancel_urls)
                        {
                            string tmp = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, url);
                            urls.Add(tmp);

                        }

                        bool result = await CallTriggerURLs(urls, AQI.Trigger);

                    }

                    //run external program
                    if (AQI.cam.Action_RunProgram && AQI.Trigger)
                    {
                        string run = "";
                        string param = "";
                        try
                        {
                            run = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, AQI.cam.Action_RunProgramString);
                            param = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, AQI.cam.Action_RunProgramArgsString);
                            Global.Log($"{CurSrv} -    Starting external app - Camera={AQI.cam.name} run='{run}', param='{param}'");
                            Process.Start(run, param);
                        }
                        catch (Exception ex)
                        {

                            ret = false;
                            Global.Log($"{CurSrv} - Error: while running program '{run}' with params '{param}', got: {Global.ExMsg(ex)}");
                        }
                    }

                    //Play sounds
                    if (AQI.cam.Action_PlaySounds && AQI.Trigger)
                    {
                        try
                        {

                            //object1, object2 ; soundfile.wav | object1, object2 ; anotherfile.wav | * ; defaultsound.wav
                            string snds = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, AQI.cam.Action_Sounds);

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
                                            if (detection.ToLower().Contains(objname.ToLower()) || (objname == "*"))
                                            {
                                                Global.Log($"{CurSrv} -    Playing sound because '{objname}' was detected: {soundfile}...");
                                                SoundPlayer sp = new SoundPlayer(soundfile);
                                                sp.Play();
                                                played++;
                                            }
                                        }
                                    }
                                }
                                if (played == 0)
                                {
                                    Global.Log($"{CurSrv} - No object matched sound to play or no detections.");
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                            ret = false;
                            Global.Log($"{CurSrv} - Error: while calling sound '{AQI.cam.Action_Sounds}', got: {Global.ExMsg(ex)}");
                        }
                    }

                    if (AQI.cam.Action_mqtt_enabled)
                    {
                        string topic = "";
                        string payload = "";
                        if (AQI.Trigger)
                        {
                            topic = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, AQI.cam.Action_mqtt_topic);
                            payload = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, AQI.cam.Action_mqtt_payload);
                        }
                        else
                        {
                            topic = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, AQI.cam.Action_mqtt_topic_cancel);
                            payload = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, AQI.cam.Action_mqtt_payload_cancel);
                        }

                        List<string> topics = Global.Split(topic, ";|");
                        List<string> payloads = Global.Split(payload, ";|");


                        for (int i = 0; i < topics.Count; i++)
                        {
                            MQTTClient mq = new MQTTClient();
                            MqttClientPublishResult pr = await mq.PublishAsync(topics[i], payloads[i], AQI.cam.Action_mqtt_retain_message);
                            if (pr == null || pr.ReasonCode != MqttClientPublishReasonCode.Success)
                                ret = false;

                        }


                    }

                    //upload to telegram
                    if (AQI.cam.telegram_enabled && AQI.Trigger)
                    {

                        string tmp = AITOOL.ReplaceParams(AQI.cam, AQI.CurImg, AQI.cam.telegram_caption);

                            if (!await TelegramUpload(AQI))
                            {
                                ret = false;
                                Global.Log($"{CurSrv} -    -> ERROR sending image to Telegram.");
                            }
                            else
                            {
                                Global.Log($"{CurSrv} -    -> Sent image to Telegram.");
                            }

                    }


                    if (AQI.Trigger)
                    {
                        AQI.cam.last_trigger_time.Write(DateTime.Now); //reset cooldown time every time an image contains something, even if no trigger was called (still in cooldown time)
                        Global.Log($"{CurSrv} - {AQI.cam.name} last triggered at {AQI.cam.last_trigger_time.Read()}.");
                        Global.UpdateLabel($"{CurSrv} - {AQI.cam.name} last triggered at {AQI.cam.last_trigger_time.Read()}.", "lbl_info");
                    }


                }
                else
                {
                    //log that nothing was done
                    Global.Log($"{CurSrv} -    Camera {AQI.cam.name} is still in cooldown. Trigger URL wasn't called and no image will be uploaded to Telegram. ({cooltime} of {AQI.cam.cooldown_time} minutes - See Cameras 'cooldown_time' in settings file)");
                }


                if (AQI.cam.Action_image_merge_detections && AQI.Trigger && !string.IsNullOrEmpty(tmpfile) && tmpfile.ToLower().Contains(Environment.GetEnvironmentVariable("TEMP").ToLower()) && System.IO.File.Exists(tmpfile))
                {
                    System.IO.File.Delete(tmpfile);
                    //Log($"Debug: Deleting tmp file {tmpfile}");
                }


            }
            catch (Exception ex)
            {

                Global.Log($"{CurSrv} - Error: " + Global.ExMsg(ex));
            }


            return ret;

        }

        
        public async Task<string> MergeImageAnnotations(ClsTriggerActionQueueItem AQI)
        {
            int countr = 0;
            string detections = "";
            string lasttext = "";
            string lastposition = "";
            string OutputImageFile = "";

            try
            {
                Global.Log("Merging image annotations: " + AQI.CurImg.image_path);

                if (System.IO.File.Exists(AQI.CurImg.image_path))
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    using (Bitmap img = new Bitmap(AQI.CurImg.image_path))
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

                                        int xmin = pred.xmin + AQI.cam.XOffset;
                                        int ymin = pred.ymin + AQI.cam.YOffset;
                                        int xmax = pred.xmax;
                                        int ymax = pred.ymax;

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

                                if (label.Contains("irrelevant") || label.Contains("confidence") || label.Contains("masked") || label.Contains("errors"))
                                {
                                    detections = detections.Split(':')[1]; //removes the "1x masked, 3x irrelevant:" before the actual detection, otherwise this would be displayed in the detection tags

                                    if (label.Contains("masked"))
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

                                    //Global.Log($"...{i}, LastText='{lasttext}' - LastPosition='{lastposition}'");
                                }

                            }


                            if (countr > 0)
                            {

                                GraphicsState gs = g.Save();

                                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

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

                                bool Success = true;

                                if (AQI.cam.Action_image_merge_detections_makecopy)
                                    OutputImageFile = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), Path.GetFileName(AQI.CurImg.image_path));
                                else
                                    OutputImageFile = AQI.CurImg.image_path;

                                if (System.IO.File.Exists(OutputImageFile))
                                {
                                    Success = await Global.WaitForFileAccessAsync(OutputImageFile, FileSystemRights.FullControl, FileShare.ReadWrite);
                                }

                                if (Success)
                                {
                                    img.Save(OutputImageFile, jpgEncoder, myEncoderParameters);
                                    Global.Log($"Merged {countr} detections in {sw.ElapsedMilliseconds}ms into image {OutputImageFile}");
                                }
                                else
                                {
                                    Global.Log($"Error: Could not gain access to write merged file {OutputImageFile}");
                                }

                            }
                            else
                            {
                                Global.Log($"No detections to merge.  Time={sw.ElapsedMilliseconds}ms, {OutputImageFile}");

                            }

                        }

                    }

                }
                else
                {
                    Global.Log("Error: could not find last image with detections: " + AQI.CurImg.image_path);
                }
            }
            catch (Exception ex)
            {

                Global.Log($"Error: Detections='{detections}', LastText='{lasttext}', LastPostions='{lastposition}' - " + Global.ExMsg(ex));
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

        public bool CopyImage(Camera cam, ClsImageQueueItem CurImg, ref string dest_path)
        {
            bool ret = false;

            try
            {
                string netfld = AITOOL.ReplaceParams(cam, CurImg, cam.Action_network_folder);

                string ext = Path.GetExtension(CurImg.image_path);
                string filename = AITOOL.ReplaceParams(cam, CurImg, cam.Action_network_folder_filename).Trim() + ext;

                dest_path = System.IO.Path.Combine(netfld, filename);

                Global.Log($"{CurSrv} -   File copying from {CurImg.image_path} to {dest_path}");

                if (!Directory.Exists(netfld))
                {
                    Directory.CreateDirectory(netfld);
                }

                System.IO.File.Copy(CurImg.image_path, dest_path, true);

                ret = true;


            }
            catch (Exception ex)
            {
                Global.Log($"{CurSrv} - ERROR: Could not copy image {CurImg.image_path} to network path {dest_path}: {Global.ExMsg(ex)}");
            }

            return ret;

        }
        //call trigger urls
        public async Task<bool> CallTriggerURLs(List<string> trigger_urls, bool Trigger)
        {

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
                        Global.Log($"{CurSrv} -    -> {type} URL called: {url}, response: '{content.Replace("\r\n", "\n").Replace("\n", " ")}'");
                    }
                    catch (Exception ex)
                    {
                        ret = false;
                        Global.Log($"{CurSrv} - ERROR: Could not {type} URL '{url}', please check if '{url}' is correct and reachable: {Global.ExMsg(ex)}");
                    }

                }

            }

            return ret;


        }

        //send image to Telegram
        public async Task<bool> TelegramUpload(ClsTriggerActionQueueItem AQI)
        {
            bool ret = false;
            string lastchatid = "";

            if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
            {
                //telegram upload sometimes fails
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    if (AppSettings.Settings.telegram_cooldown_minutes < 0.0333333)
                    {
                        AppSettings.Settings.telegram_cooldown_minutes = 0.0333333;  //force to be at least 2 seconds
                    }

                    if (TelegramRetryTime.Read() == DateTime.MinValue || DateTime.Now >= TelegramRetryTime.Read())
                    {
                        double cooltime = Math.Round((DateTime.Now - last_telegram_trigger_time.Read()).TotalMinutes, 4);
                        if (cooltime >= AppSettings.Settings.telegram_cooldown_minutes)
                        {
                            //in order to avoid hitting our limits when sending out mass notifications, consider spreading them over longer intervals, e.g. 8-12 hours. The API will not allow bulk notifications to more than ~30 users per second, if you go over that, you'll start getting 429 errors.


                            using (var image_telegram = System.IO.File.OpenRead(AQI.CurImg.image_path))
                            {
                                TelegramBotClient bot = new TelegramBotClient(AppSettings.Settings.telegram_token);

                                //upload image to Telegram servers and send to first chat
                                Global.Log($"{CurSrv} -       uploading image to chat \"{AppSettings.Settings.telegram_chatids[0]}\"");
                                lastchatid = AppSettings.Settings.telegram_chatids[0];
                                Message message = await bot.SendPhotoAsync(AppSettings.Settings.telegram_chatids[0], new InputOnlineFile(image_telegram, "image.jpg"), AQI.Text);

                                string file_id = message.Photo[0].FileId; //get file_id of uploaded image

                                //share uploaded image with all remaining telegram chats (if multiple chat_ids given) using file_id 
                                foreach (string chatid in AppSettings.Settings.telegram_chatids.Skip(1))
                                {
                                    Global.Log($"{CurSrv} -       uploading image to chat \"{chatid}\"...");
                                    lastchatid = chatid;
                                    await bot.SendPhotoAsync(chatid, file_id, AQI.Text);
                                }
                                ret = true;
                            }

                            last_telegram_trigger_time.Write(DateTime.Now);
                            TelegramRetryTime.Write(DateTime.MinValue);

                            if (AQI.IsQueued)
                            {
                                //add a minimum delay if we are in a queue to prevent minimum cooldown error
                                Global.Log($"{CurSrv} - Waiting {AppSettings.Settings.telegram_cooldown_minutes} minutes (telegram_cooldown_minutes)...");
                                await Task.Delay(TimeSpan.FromMinutes(AppSettings.Settings.telegram_cooldown_minutes));
                            }

                        }
                        else
                        {
                            //log that nothing was done
                            Global.Log($"{CurSrv} -    Still in TELEGRAM cooldown. No image will be uploaded to Telegram.  ({cooltime} of {AppSettings.Settings.telegram_cooldown_minutes} minutes - See 'telegram_cooldown_minutes' in settings file)");

                        }

                    }
                    else
                    {
                        Global.Log($"{CurSrv} -    Waiting {Math.Round((TelegramRetryTime.Read() - DateTime.Now).TotalSeconds, 1)} seconds ({TelegramRetryTime}) to retry TELEGRAM connection.  This is due to a previous telegram send error.");
                    }


                }
                catch (ApiRequestException ex)  //current version only gives webexception NOT this exception!  https://github.com/TelegramBots/Telegram.Bot/issues/891
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Global.Log($"{CurSrv} - ERROR: Could not upload image {AQI.CurImg.image_path} with chatid '{lastchatid}' to Telegram: {Global.ExMsg(ex)}");
                    TelegramRetryTime.Write(DateTime.Now.AddSeconds(ex.Parameters.RetryAfter));
                    Global.Log($"{CurSrv} - ...BOT API returned 'RetryAfter' value '{ex.Parameters.RetryAfter} seconds', so not retrying until {TelegramRetryTime}");
                    AppSettings.Settings.send_errors = se;
                    //store image that caused an error in ./errors/
                    if (!Directory.Exists("./errors/")) //if folder does not exist, create the folder
                    {
                        //create folder
                        DirectoryInfo di = Directory.CreateDirectory("./errors");
                        Global.Log($"{CurSrv} - ./errors/" + " dir created.");
                    }
                    //save error image
                    using (var image = await SixLabors.ImageSharp.Image.LoadAsync(AQI.CurImg.image_path))
                    {
                        await image.SaveAsync($"{CurSrv} - ./errors/" + "TELEGRAM-ERROR-" + Path.GetFileName(AQI.CurImg.image_path) + ".jpg");
                    }
                    Global.UpdateLabel($"{CurSrv} - Can't upload error message to Telegram!", "lbl_errors");

                }
                catch (Exception ex)  //As of version 
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Global.Log($"{CurSrv} - ERROR: Could not upload image {AQI.CurImg.image_path} to Telegram with chatid '{lastchatid}': {Global.ExMsg(ex)}");
                    TelegramRetryTime.Write(DateTime.Now.AddSeconds(AppSettings.Settings.Telegram_RetryAfterFailSeconds));
                    Global.Log($"{CurSrv} -...'Default' 'Telegram_RetryAfterFailSeconds' value was set to '{AppSettings.Settings.Telegram_RetryAfterFailSeconds}' seconds, so not retrying until {TelegramRetryTime}");
                    AppSettings.Settings.send_errors = se;
                    //store image that caused an error in ./errors/
                    if (!Directory.Exists("./errors/")) //if folder does not exist, create the folder
                    {
                        //create folder
                        DirectoryInfo di = Directory.CreateDirectory("./errors");
                        Global.Log($"{CurSrv} - ./errors/" + " dir created.");
                    }
                    //save error image
                    using (var image = await SixLabors.ImageSharp.Image.LoadAsync(AQI.CurImg.image_path))
                    {
                        await image.SaveAsync("./errors/" + "TELEGRAM-ERROR-" + Path.GetFileName(AQI.CurImg.image_path) + ".jpg");
                    }
                    Global.UpdateLabel($"{CurSrv} - Can't upload error message to Telegram!", "lbl_errors");

                }


                Global.Log($"{CurSrv} - ...Finished in {{yellow}}{sw.ElapsedMilliseconds}ms{{white}}");

            }
            else
            {
                Global.Log($"{CurSrv} - Error:  Telegram settings misconfigured. telegram_chatids.Count={AppSettings.Settings.telegram_chatids.Count} ({string.Join(",", AppSettings.Settings.telegram_chatids)}), telegram_token='{AppSettings.Settings.telegram_token}'");
            }

            return ret;

        }

        //send text to Telegram
        public async Task<bool> TelegramText(ClsTriggerActionQueueItem AQI)
        {
            bool ret = false;
            string lastchatid = "";
            if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
            {

                //telegram upload sometimes fails
                try
                {

                    if (AppSettings.Settings.telegram_cooldown_minutes < 0.0166667)
                    {
                        AppSettings.Settings.telegram_cooldown_minutes = 0.0166667;  //force to be at least 1 second
                    }

                    if (TelegramRetryTime.Read() == DateTime.MinValue || DateTime.Now >= TelegramRetryTime.Read())
                    {
                        double cooltime = Math.Round((DateTime.Now - last_telegram_trigger_time.Read()).TotalMinutes, 4);
                        if (cooltime >= AppSettings.Settings.telegram_cooldown_minutes)
                        {
                            TelegramBotClient bot = new Telegram.Bot.TelegramBotClient(AppSettings.Settings.telegram_token);
                            foreach (string chatid in AppSettings.Settings.telegram_chatids)
                            {
                                lastchatid = chatid;
                                Message msg = await bot.SendTextMessageAsync(chatid, AQI.Text);

                            }
                            last_telegram_trigger_time.Write(DateTime.Now);
                            TelegramRetryTime.Write(DateTime.MinValue);

                            if (AQI.IsQueued)
                            {
                                //add a minimum delay if we are in a queue to prevent minimum cooldown error
                                Global.Log($"Waiting {AppSettings.Settings.telegram_cooldown_minutes} minutes (telegram_cooldown_minutes)...");
                                await Task.Delay(TimeSpan.FromMinutes(AppSettings.Settings.telegram_cooldown_minutes));
                            }

                            ret = true;
                        }
                        else
                        {
                            //log that nothing was done
                            Global.Log($"{CurSrv} -    Still in TELEGRAM cooldown. No image will be uploaded to Telegram.  ({cooltime} of {AppSettings.Settings.telegram_cooldown_minutes} minutes - See 'telegram_cooldown_minutes' in settings file)");

                        }

                    }
                    else
                    {
                        Global.Log($"{CurSrv} -    Waiting {Math.Round((TelegramRetryTime.Read() - DateTime.Now).TotalSeconds, 1)} seconds ({TelegramRetryTime}) to retry TELEGRAM connection.  This is due to a previous telegram send error.");
                    }



                }
                catch (ApiRequestException ex)  //current version only gives webexception NOT this exception!  https://github.com/TelegramBots/Telegram.Bot/issues/891
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Global.Log($"{CurSrv} - ERROR: Could not upload text '{AQI.Text}' with chatid '{lastchatid}' to Telegram: {Global.ExMsg(ex)}");
                    TelegramRetryTime.Write(DateTime.Now.AddSeconds(ex.Parameters.RetryAfter));
                    Global.Log($"{CurSrv} - ...BOT API returned 'RetryAfter' value '{ex.Parameters.RetryAfter} seconds', so not retrying until {TelegramRetryTime}");
                    AppSettings.Settings.send_errors = se;
                    Global.UpdateLabel($"{CurSrv} - Can't upload error message to Telegram!", "lbl_errors");

                }
                catch (Exception ex)
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Global.Log($"{CurSrv} - ERROR: Could not upload image '{AQI.Text}' with chatid '{lastchatid}' to Telegram: {Global.ExMsg(ex)}");
                    TelegramRetryTime.Write(DateTime.Now.AddSeconds(AppSettings.Settings.Telegram_RetryAfterFailSeconds));
                    Global.Log($"{CurSrv} - ...'Default' 'Telegram_RetryAfterFailSeconds' value was set to '{AppSettings.Settings.Telegram_RetryAfterFailSeconds}' seconds, so not retrying until {TelegramRetryTime}");
                    AppSettings.Settings.send_errors = se;
                    Global.UpdateLabel($"{CurSrv} - Can't upload error message to Telegram!", "lbl_errors");
                }

            }
            else
            {
                Global.Log($"{CurSrv} - Error:  Telegram settings misconfigured. telegram_chatids.Count={AppSettings.Settings.telegram_chatids.Count} ({string.Join(",", AppSettings.Settings.telegram_chatids)}), telegram_token='{AppSettings.Settings.telegram_token}'");
            }


            return ret;
        }



    }
}
