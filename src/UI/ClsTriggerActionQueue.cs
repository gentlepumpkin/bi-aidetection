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

namespace AITool
{
    public class ClsTriggerActionQueueItem
    {
        public TriggerType _ttype = TriggerType.Unknown;
        public Camera _cam = null;
        public ClsImageQueueItem _curmg = null;
        public bool _trigger = true;
        public string _text = "";
        public DateTime _addedTime = DateTime.MinValue;
        public ClsTriggerActionQueueItem(TriggerType ttype, Camera cam, ClsImageQueueItem CurImg, bool Trigger, string Text)
        {
            this._cam = cam;
            this._ttype = ttype;
            this._curmg = CurImg;
            this._trigger = Trigger;
            this._addedTime = DateTime.Now;
            this._text = Text;
        }
    }

    public class ClsTriggerActionQueue
    {
        BlockingCollection<ClsTriggerActionQueueItem> TriggerActionQueue = new BlockingCollection<ClsTriggerActionQueueItem>();

        public ThreadSafe.Datetime last_telegram_trigger_time { get; set; } = new ThreadSafe.Datetime(DateTime.MinValue);
        public ThreadSafe.Datetime TelegramRetryTime { get; set; } = new ThreadSafe.Datetime(DateTime.MinValue);
        public ClsURLItem _url { get; set; } = null;
        private String CurSrv = "";
        public ThreadSafe.Integer Count { get; set; } = new ThreadSafe.Integer(0);
        public ClsTriggerActionQueue()
        {
            Task.Run(TriggerActionJobQueueLoop);
        }

        public async Task<bool> AddTriggerActionAsync(TriggerType ttype, Camera cam, ClsImageQueueItem CurImg, bool Trigger, bool Wait, ClsURLItem ds_url, string Text)
        {
            bool ret = false;
            string imgpath = "NoImage";
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
                imgpath = CurImg.image_path;
            }

            ClsTriggerActionQueueItem AQI = new ClsTriggerActionQueueItem(ttype, cam, CurImg, Trigger, Text);


            if (Wait)
            {
                ret = await RunTriggers(AQI);
            }
            else
            {
                if (this.TriggerActionQueue.Count <= AppSettings.Settings.MaxActionQueueSize)
                {
                    if (!this.TriggerActionQueue.TryAdd(AQI))
                    {
                        Global.Log($"{CurSrv} - Error: Action '{AQI._ttype}' could not be added? {imgpath}");
                    }
                    else
                    {
                        ret = true;
                    }
                }
                else
                {
                    Global.Log($"{CurSrv} - Error: Action '{AQI._ttype}' could not be added because queue size is {TriggerActionQueue.Count} and the max is {AppSettings.Settings.MaxActionQueueSize} (MaxActionQueueSize) - {imgpath}");
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
                    Thread.Sleep(25); //very short wait between trigger events
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


            Stopwatch sw = Stopwatch.StartNew();

            if (this.TriggerActionQueue.Count == 0)
                this.Count.WriteFullFence(1);
            else
                this.Count.WriteFullFence(this.TriggerActionQueue.Count);

            Global.SendMessage(MessageType.UpdateStatus);

            if (AQI._ttype == TriggerType.TelegramText)
            {
                res = await TelegramText(AQI._text);
            }
            else if (AQI._ttype == TriggerType.TelegramImageUpload)
            {
                res = await TelegramUpload(AQI._curmg, AQI._text);
            }
            else
            {
                res = await Trigger(AQI._cam, AQI._curmg, AQI._trigger);
            }

            this.Count.WriteFullFence(this.TriggerActionQueue.Count);

            Global.Log($"{CurSrv} - Action completion time was {sw.ElapsedMilliseconds}ms.");

            Global.SendMessage(MessageType.UpdateStatus);

            return res;
        }

        //trigger actions
        public async Task<bool> Trigger(Camera cam, ClsImageQueueItem CurImg, bool Trigger)
        {
            bool ret = true;

            //mostly for testing when we dont have a current image...
            if (CurImg == null)
            {
                if (!string.IsNullOrEmpty(cam.last_image_file_with_detections))
                {
                    CurImg = new ClsImageQueueItem(cam.last_image_file_with_detections, 1);
                }
                else if (!string.IsNullOrEmpty(cam.last_image_file))
                {
                    CurImg = new ClsImageQueueItem(cam.last_image_file, 1);
                }
                else
                {
                    Global.Log($"{CurSrv} - Error: No image to process?");
                    return false;
                }
            }

            try
            {
                double cooltime = (DateTime.Now - cam.last_trigger_time).TotalMinutes;
                string tmpfile = CurImg.image_path;

                //only trigger if cameras cooldown time since last detection has passed
                if (cooltime >= cam.cooldown_time)
                {

                    if (cam.Action_image_merge_detections && Trigger)
                    {
                        if (cam.Action_image_merge_detections_makecopy)
                            tmpfile = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), Path.GetFileName(CurImg.image_path));

                        cam.MergeImageAnnotations(tmpfile, CurImg);

                        if (cam.Action_image_merge_detections_makecopy && System.IO.File.Exists(tmpfile))  //it wont exist if no detections or failure...
                            CurImg = new ClsImageQueueItem(tmpfile, 1);
                    }

                    if (cam.Action_image_copy_enabled && Trigger)
                    {
                        Global.Log($"{CurSrv} -    Copying image to network folder...");
                        string newimagepath = "";
                        if (!CopyImage(cam, CurImg, ref newimagepath))
                        {
                            ret = false;
                            Global.Log($"{CurSrv} -    -> Warning: Image could not be copied to network folder.");
                        }
                        else
                        {
                            Global.Log($"{CurSrv} -    -> Image copied to network folder.");
                            //set the image path to the new path so all imagename variable works
                            CurImg = new ClsImageQueueItem(newimagepath, 1);
                        }

                    }

                    //call trigger urls
                    if (Trigger && cam.trigger_urls.Count() > 0)
                    {
                        //replace url paramters with according values
                        List<string> urls = new List<string>();
                        //call urls
                        foreach (string url in cam.trigger_urls)
                        {
                            string tmp = AITOOL.ReplaceParams(cam, CurImg, url);
                            urls.Add(tmp);

                        }

                        bool result = await CallTriggerURLs(urls, Trigger);
                    }
                    else if (!Trigger && cam.cancel_urls.Count() > 0)
                    {
                        //replace url paramters with according values
                        List<string> urls = new List<string>();
                        //call urls
                        foreach (string url in cam.cancel_urls)
                        {
                            string tmp = AITOOL.ReplaceParams(cam, CurImg, url);
                            urls.Add(tmp);

                        }

                        bool result = await CallTriggerURLs(urls, Trigger);

                    }

                    //upload to telegram
                    if (cam.telegram_enabled && Trigger)
                    {

                        string tmp = AITOOL.ReplaceParams(cam, CurImg, cam.telegram_caption);
                        if (!await TelegramUpload(CurImg, tmp))
                        {
                            ret = false;
                            Global.Log($"{CurSrv} -    -> ERROR sending image to Telegram.");
                        }
                        else
                        {
                            Global.Log($"{CurSrv} -    -> Sent image to Telegram.");
                        }
                    }

                    //run external program
                    if (cam.Action_RunProgram && Trigger)
                    {
                        string run = "";
                        string param = "";
                        try
                        {
                            run = AITOOL.ReplaceParams(cam, CurImg, cam.Action_RunProgramString);
                            param = AITOOL.ReplaceParams(cam, CurImg, cam.Action_RunProgramArgsString);
                            Global.Log($"{CurSrv} -    Starting external app - Camera={cam.name} run='{run}', param='{param}'");
                            Process.Start(run, param);
                        }
                        catch (Exception ex)
                        {

                            ret = false;
                            Global.Log($"{CurSrv} - Error: while running program '{run}' with params '{param}', got: {Global.ExMsg(ex)}");
                        }
                    }

                    //Play sounds
                    if (cam.Action_PlaySounds && Trigger)
                    {
                        try
                        {

                            //object1, object2 ; soundfile.wav | object1, object2 ; anotherfile.wav | * ; defaultsound.wav
                            string snds = AITOOL.ReplaceParams(cam, CurImg, cam.Action_Sounds);

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
                                        foreach (string detection in cam.last_detections)
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
                            Global.Log($"{CurSrv} - Error: while calling sound '{cam.Action_Sounds}', got: {Global.ExMsg(ex)}");
                        }
                    }

                    if (cam.Action_mqtt_enabled)
                    {
                        string topic = "";
                        string payload = "";
                        if (Trigger)
                        {
                            topic = AITOOL.ReplaceParams(cam, CurImg, cam.Action_mqtt_topic);
                            payload = AITOOL.ReplaceParams(cam, CurImg, cam.Action_mqtt_payload);
                        }
                        else
                        {
                            topic = AITOOL.ReplaceParams(cam, CurImg, cam.Action_mqtt_topic_cancel);
                            payload = AITOOL.ReplaceParams(cam, CurImg, cam.Action_mqtt_payload_cancel);
                        }

                        List<string> topics = Global.Split(topic, ";|");
                        List<string> payloads = Global.Split(payload, ";|");


                        for (int i = 0; i < topics.Count; i++)
                        {
                            MQTTClient mq = new MQTTClient();
                            MqttClientPublishResult pr = await mq.PublishAsync(topics[i], payloads[i], cam.Action_mqtt_retain_message);
                            if (pr == null || pr.ReasonCode != MqttClientPublishReasonCode.Success)
                                ret = false;

                        }


                    }


                    if (Trigger)
                    {
                        cam.last_trigger_time = DateTime.Now; //reset cooldown time every time an image contains something, even if no trigger was called (still in cooldown time)
                        Global.Log($"{CurSrv} - {cam.name} last triggered at {cam.last_trigger_time}.");
                        Global.UpdateLabel($"{CurSrv} - {cam.name} last triggered at {cam.last_trigger_time}.", "lbl_info");
                    }


                }
                else
                {
                    //log that nothing was done
                    Global.Log($"{CurSrv} -    Camera {cam.name} is still in cooldown. Trigger URL wasn't called and no image will be uploaded to Telegram. ({cooltime} of {cam.cooldown_time} minutes - See Cameras 'cooldown_time' in settings file)");
                }


                if (cam.Action_image_merge_detections && Trigger && cam.Action_image_merge_detections_makecopy && !string.IsNullOrEmpty(tmpfile) && System.IO.File.Exists(tmpfile))
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
        public async Task<bool> TelegramUpload(ClsImageQueueItem CurImg, string img_caption)
        {
            bool ret = false;

            if (AppSettings.Settings.telegram_chatids.Count > 0 && AppSettings.Settings.telegram_token != "")
            {
                //telegram upload sometimes fails
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    if (AppSettings.Settings.telegram_cooldown_minutes < 0.0333333)
                    {
                        AppSettings.Settings.telegram_cooldown_minutes = 0.0333333;  //force to be at least 1 second
                    }

                    if (TelegramRetryTime.Read() == DateTime.MinValue || DateTime.Now >= TelegramRetryTime.Read())
                    {
                        double cooltime = Math.Round((DateTime.Now - last_telegram_trigger_time.Read()).TotalMinutes, 4);
                        if (cooltime >= AppSettings.Settings.telegram_cooldown_minutes)
                        {
                            //in order to avoid hitting our limits when sending out mass notifications, consider spreading them over longer intervals, e.g. 8-12 hours. The API will not allow bulk notifications to more than ~30 users per second, if you go over that, you'll start getting 429 errors.


                            using (var image_telegram = System.IO.File.OpenRead(CurImg.image_path))
                            {
                                TelegramBotClient bot = new TelegramBotClient(AppSettings.Settings.telegram_token);

                                //upload image to Telegram servers and send to first chat
                                Global.Log($"{CurSrv} -       uploading image to chat \"{AppSettings.Settings.telegram_chatids[0]}\"");
                                Message message = await bot.SendPhotoAsync(AppSettings.Settings.telegram_chatids[0], new InputOnlineFile(image_telegram, "image.jpg"), img_caption);

                                string file_id = message.Photo[0].FileId; //get file_id of uploaded image

                                //share uploaded image with all remaining telegram chats (if multiple chat_ids given) using file_id 
                                foreach (string chatid in AppSettings.Settings.telegram_chatids.Skip(1))
                                {
                                    Global.Log($"{CurSrv} -       uploading image to chat \"{chatid}\"...");
                                    await bot.SendPhotoAsync(chatid, file_id, img_caption);
                                }
                                ret = true;
                            }

                            last_telegram_trigger_time.Write(DateTime.Now);
                            TelegramRetryTime.Write(DateTime.MinValue);
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
                    Global.Log($"{CurSrv} - ERROR: Could not upload image {CurImg.image_path} to Telegram: {Global.ExMsg(ex)}");
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
                    using (var image = SixLabors.ImageSharp.Image.Load(CurImg.image_path))
                    {
                        image.Save($"{CurSrv} - ./errors/" + "TELEGRAM-ERROR-" + Path.GetFileName(CurImg.image_path) + ".jpg");
                    }
                    Global.UpdateLabel($"{CurSrv} - Can't upload error message to Telegram!", "lbl_errors");

                }
                catch (Exception ex)  //As of version 
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Global.Log($"{CurSrv} -ERROR: Could not upload image {CurImg.image_path} to Telegram: {Global.ExMsg(ex)}");
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
                    using (var image = SixLabors.ImageSharp.Image.Load(CurImg.image_path))
                    {
                        image.Save("./errors/" + "TELEGRAM-ERROR-" + Path.GetFileName(CurImg.image_path) + ".jpg");
                    }
                    Global.UpdateLabel($"{CurSrv} - Can't upload error message to Telegram!", "lbl_errors");

                }


                Global.Log($"{CurSrv} - ...Finished in {{yellow}}{sw.ElapsedMilliseconds}ms{{white}}");

            }
            else
            {
                Global.Log($"{CurSrv} - Error:  Telegram settings misconfigured.  telegram_chatids.Count={AppSettings.Settings.telegram_chatids.Count}, telegram_token='{AppSettings.Settings.telegram_token}'");
            }

            return ret;

        }

        //send text to Telegram
        public async Task<bool> TelegramText(string text)
        {
            bool ret = false;
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
                                Message msg = await bot.SendTextMessageAsync(chatid, text);

                            }
                            last_telegram_trigger_time.Write(DateTime.Now);
                            TelegramRetryTime.Write(DateTime.MinValue);
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
                    Global.Log($"{CurSrv} - ERROR: Could not upload text '{text}' to Telegram: {Global.ExMsg(ex)}");
                    TelegramRetryTime.Write(DateTime.Now.AddSeconds(ex.Parameters.RetryAfter));
                    Global.Log($"{CurSrv} - ...BOT API returned 'RetryAfter' value '{ex.Parameters.RetryAfter} seconds', so not retrying until {TelegramRetryTime}");
                    AppSettings.Settings.send_errors = se;
                    Global.UpdateLabel($"{CurSrv} - Can't upload error message to Telegram!", "lbl_errors");

                }
                catch (Exception ex)
                {
                    bool se = AppSettings.Settings.send_errors;
                    AppSettings.Settings.send_errors = false;
                    Global.Log($"{CurSrv} - ERROR: Could not upload image '{text}' to Telegram: {Global.ExMsg(ex)}");
                    TelegramRetryTime.Write(DateTime.Now.AddSeconds(AppSettings.Settings.Telegram_RetryAfterFailSeconds));
                    Global.Log($"{CurSrv} - ...'Default' 'Telegram_RetryAfterFailSeconds' value was set to '{AppSettings.Settings.Telegram_RetryAfterFailSeconds}' seconds, so not retrying until {TelegramRetryTime}");
                    AppSettings.Settings.send_errors = se;
                    Global.UpdateLabel($"{CurSrv} - Can't upload error message to Telegram!", "lbl_errors");
                }

            }

            return ret;
        }



    }
}
