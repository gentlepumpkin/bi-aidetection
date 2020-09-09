using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public class ClsHistoryItem
    {
        //filename|date and time|camera|detections|positions of detections|success
        //D:\BlueIrisStorage\AIInput\AIFOSCAMDRIVEWAY\AIFOSCAMDRIVEWAY.20200901_073513579.jpg|01.09.20, 07:35:15|AIFOSCAMDRIVEWAY|false alert||false
        //D:\BlueIrisStorage\AIInput\AILOREXBACK\AILOREXBACK.20200901_073516231.jpg|
        //      01.09.20, 07:35:17|
        //      AILOREXBACK|
        //      1x not in confidence range; 6x irrelevant : fire hydrant (47%); dog (42%); umbrella (77%); chair (98%); chair (86%); chair (60%); chair (59%); |
        //      1541,870,1690,1097;1316,1521,1583,1731;226,214,1231,637;451,1028,689,1321;989,769,1225,1145;882,616,1089,938;565,647,701,814;|
        //      false

        public string Filename { get; set; } = "";
        public string Date { get; set; } = "";
        public string Camera { get; set; } = "";
        public string Detections { get; set; } = "";   //TODO: should this be a list in the db?
        public string Positions { get; set; } = "";
        public bool Success { get; set; } = false;

        public ClsHistoryItem(string filename, string date, string camera, string objects_and_confidence, string object_positions)
        {
            this.Filename = filename;
            this.Date = date;
            this.Camera = camera;
            this.Detections = objects_and_confidence;
            this.Positions = object_positions;
        }

    }
}
