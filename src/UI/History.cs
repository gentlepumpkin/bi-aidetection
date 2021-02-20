using System;
using System.Collections.Generic;
using static AITool.AITOOL;

namespace AITool
{
    public class History
    {
        //filename|date and time|camera|detections|positions of detections|success
        //D:\BlueIrisStorage\AIInput\AIFOSCAMDRIVEWAY\AIFOSCAMDRIVEWAY.20200901_073513579.jpg|01.09.20, 07:35:15|AIFOSCAMDRIVEWAY|false alert||false
        //D:\BlueIrisStorage\AIInput\AILOREXBACK\AILOREXBACK.20200901_073516231.jpg|
        //      01.09.20, 07:35:17|
        //      AILOREXBACK|
        //      1x not in confidence range; 6x irrelevant : fire hydrant (47%); dog (42%); umbrella (77%); chair (98%); chair (86%); chair (60%); chair (59%); |
        //      1541,870,1690,1097;1316,1521,1583,1731;226,214,1231,637;451,1028,689,1321;989,769,1225,1145;882,616,1089,938;565,647,701,814;|
        //      false

        public DateTime Date { get; set; } = DateTime.MinValue;
        public string Camera { get; set; } = "";
        public bool Success { get; set; } = false;
        public string Detections { get; set; } = "";
        public bool IsPerson { get; set; } = false;
        public bool IsVehicle { get; set; } = false;
        public bool IsAnimal { get; set; } = false;
        public bool WasMasked { get; set; } = false;
        public bool WasSkipped { get; set; } = false;
        public bool HasError { get; set; } = false;
        public bool IsFace { get; set; } = false;
        public bool IsKnownFace { get; set; } = false;
        [SQLite.PrimaryKey, SQLite.Unique]  //cannot have indexed here also - pk auto creates index I think
        public string Filename { get; set; } = "";
        public string Positions { get; set; } = "";
        public string PredictionsJSON { get; set; } = "";
        public string AIServer { get; set; } = "";
        public long analysisDurationMS { get; set; } = 0;
        public string state { get; set; } = "";

        public List<ClsPrediction> Predictions()
        {
            List<ClsPrediction> ret = new List<ClsPrediction>();
            if (!string.IsNullOrEmpty(this.PredictionsJSON))
            {
                //I think we were storing predictions as json either due to something with sqlite db or for compatibility with original AITOOL
                ret = Global.SetJSONString<List<ClsPrediction>>(this.PredictionsJSON);
                if (ret != null)
                {
                    foreach (var pred in ret)
                    {
                        pred.UpdatePercent();
                    }
                }
                else
                {
                    ret = new List<ClsPrediction>();
                }
            }
            return ret;
        }


        public History()
        {
        }
        public History CreateFromCSV(string CSVLine)
        {
            //"filename|date and time|camera|detections|positions of detections|success"
            // 0        1             2      3          4                       5
            //d:\blueirisstorage\aiinput\aifoscamdriveway\aifoscamdriveway.20200919_195956172.jpg|19.09.20, 19:59:57|FOSCAMDRIVEWAY|false alert||False
            //d:\blueirisstorage\aiinput\aisunroom\aisunroom.20200920_102321242.jpg|20.09.20, 10:23:21|SUNROOM|Person 65%|336,587,513,717;|True
            //d:\blueirisstorage\aiinput\ailorexback\ailorexback.20200920_102343589.jpg|20.09.20, 10:23:44|LOREXBACK|2x irrelevant : Umbrella 72%; Dining table 43%|188,231,1174,583;471,765,881,988;|False


            string[] sp = CSVLine.Split('|');

            this.Camera = sp[2].Trim();
            this.Detections = sp[3];
            this.Filename = sp[0].Trim().ToLower();
            this.Positions = sp[4].Trim();

            bool suc = false;
            //20.09.20, 11:51:24

            if (bool.TryParse(sp[5], out suc))
            {
                this.Success = suc;
            }
            else
            {
                Log($"Error: Invalid bool in csv '{sp[5]}'");
            }

            //seems we are trying pretty hard here to get this non standard date back in a real DateTime...
            DateTime date1;

            suc = DateTime.TryParseExact(sp[1], AppSettings.Settings.DateFormat, null, System.Globalization.DateTimeStyles.None, out date1);
            if (suc)
            {
                this.Date = date1;
            }
            else
            {
                if (AppSettings.Settings.DateFormat != "dd.MM.yy, HH:mm:ss")
                {
                    suc = DateTime.TryParseExact(sp[1], "dd.MM.yy, HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out date1);
                    if (suc)
                    {
                        this.Date = date1;
                    }
                }

            }

            if (!suc)
            {
                suc = DateTime.TryParse(sp[1], out date1);
                if (suc)
                {
                    this.Date = date1;
                }
                else
                {
                    Log($"Error: Invalid date in csv '{sp[1]}'");
                }

            }


            this.GetObjects();

            return this;

        }
        public History Create(string filename, DateTime date, string camera, string objects_and_confidence, string object_positions, bool Success, string PredictionsJSON, string AIServer, long analysisDurationMS, bool Trigger)
        {
            this.Filename = filename.Trim().ToLower();
            this.Date = date;
            this.Camera = camera.Trim();
            this.Detections = objects_and_confidence.Trim();
            this.Positions = object_positions.Trim();
            this.PredictionsJSON = PredictionsJSON;
            this.AIServer = AIServer;
            this.analysisDurationMS = analysisDurationMS;
            if (Trigger)
                this.state = "on";
            else
                this.state = "off";

            this.Success = Success; //this.Detections.Contains("%") && !this.Detections.Contains(':');

            this.GetObjects();

            return this;
        }
        private void GetObjects()
        {
            string tmp = this.Detections.ToLower();

            //person,   bicycle,   car,   motorcycle,   airplane,
            //bus,   train,   truck,   boat,   traffic light,   fire hydrant,   stop_sign,
            //parking meter,   bench,   bird,   cat,   dog,   horse,   sheep,   cow,   elephant,
            //bear,   zebra, giraffe,   backpack,   umbrella,   handbag,   tie,   suitcase,
            //frisbee,   skis,   snowboard, sports ball,   kite,   baseball bat,   baseball glove,
            //skateboard,   surfboard,   tennis racket, bottle,   wine glass,   cup,   fork,
            //knife,   spoon,   bowl,   banana,   apple,   sandwich,   orange, broccoli,   carrot,
            //hot dog,   pizza,   donot,   cake,   chair,   couch,   potted plant,   bed, dining table,
            //toilet,   tv,   laptop,   mouse,   remote,   keyboard,   cell phone,   microwave,
            //oven,   toaster,   sink,   refrigerator,   book,   clock,   vase,   scissors,   teddy bear,
            //hair dryer, toothbrush.


            this.IsPerson = tmp.Contains("person") || tmp.Contains("face");

            this.IsVehicle = tmp.Contains("car") ||
                             tmp.Contains("truck") ||
                             tmp.Contains("bus") ||
                             tmp.Contains("bicycle") ||
                             tmp.Contains("motorcycle") ||
                             tmp.Contains("horse") ||
                             tmp.Contains("boat") ||
                             tmp.Contains("airplane");

            this.IsAnimal = tmp.Contains("dog") ||
                            tmp.Contains("cat") ||
                            tmp.Contains("bird") ||
                            tmp.Contains("bear") ||
                            tmp.Contains("sheep") ||
                            tmp.Contains("cow") ||
                            tmp.Contains("horse") ||
                            tmp.Contains("elephant") ||
                            tmp.Contains("zebra") ||
                            tmp.Contains("giraffe");

            this.WasMasked = tmp.Contains("mask");

            this.WasSkipped = tmp.Contains("skipped");

            this.HasError = tmp.Contains("error");
        }

    }
}
