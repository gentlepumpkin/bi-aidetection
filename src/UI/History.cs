using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

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

        [Indexed]
        public DateTime Date { get; set; } = DateTime.MinValue;
        [Indexed]
        public string Camera { get; set; } = "";
        public bool Success { get; set; } = false;
        public string Detections { get; set; } = "";
        public bool IsPerson { get; set; } = false;
        public bool IsVehicle { get; set; } = false;
        public bool IsAnimal { get; set; } = false;
        public bool WasMasked { get; set; } = false;
        public bool IsFace { get; set; } = false;
        public bool IsKnownFace { get; set; } = false;
        [PrimaryKey, Indexed]
        public string Filename { get; set; } = "";
        public string Positions { get; set; } = "";


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
            this.Filename = sp[0].Trim();
            this.Positions = sp[4].Trim();

            bool suc = false;
            //20.09.20, 11:51:24

            if (bool.TryParse(sp[5],out suc))
            {
                this.Success = suc;
            }
            else
            {
                Global.Log($"Error: Invalid bool in csv '{sp[5]}'");
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
                    Global.Log($"Error: Invalid date in csv '{sp[1]}'");
                }

            }


            this.GetObjects();

            return this;

        }
        public History Create(string filename, DateTime date, string camera, string objects_and_confidence, string object_positions, bool Success)
        {
            this.Filename = filename.Trim().ToLower();
            this.Date = date;
            this.Camera = camera.Trim();
            this.Detections = objects_and_confidence.Trim();
            this.Positions = object_positions.Trim();

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


            this.IsPerson = tmp.Contains("person");

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
        }

    }
}
