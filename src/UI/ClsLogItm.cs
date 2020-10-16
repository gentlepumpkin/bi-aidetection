using NLog;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    //public enum EnumLogLevel
    //{
    //    Trace = 0,
    //    Debug = 1,
    //    Info = 2,
    //    Warn = 3,
    //    Error = 4,
    //    Fatal = 5,
    //    Off = 6,
    //    Unknown = 7
    //}

    //From NLOG - just trying to mimic this:
    
    //Each log entry has a level. And each logger is configured to include or ignore certain levels. 
    //A common configuration is to specify the minimum level where that level and higher levels are 
    //included. For example, if the minimum level is Info, then Info, Warn, Error and Fatal are 
    //logged, but Debug and Trace are ignored.

    //Level 	Typical Use
    //Fatal 	Something bad happened; application is going down
    //Error 	Something failed; application may or may not continue
    //Warn 	Something unexpected; application will continue
    //Info 	Normal behavior like mail sent, user updated profile etc.
    //Debug 	For debugging; executed query, user authenticated, session expired
    //Trace 	For trace debugging; begin method X, end method X


    public class ClsLogItm
    {
        public ClsLogItm(LogLevel level, DateTime time, string source, string func, string aiserver, string camera, string detail, int idx, int depth, string color, int threadid)
        {
            // "Date|Level|Source|Func|AIServer|Camera|Detail|Idx|Depth|Color|threadid"
            this.Level = level;
            this.Date = time;
            this.Source = source;
            this.Func = func;
            this.AIServer = aiserver;
            this.Camera = camera;
            this.Detail = detail;
            this.Idx = idx;
            this.Depth = depth;
            this.Color = color;
            this.ThreadID = threadid;
        }
        public ClsLogItm()
        {
        }
        public ClsLogItm(string LogEntry)
        {

            if (string.IsNullOrEmpty(LogEntry))
                return;

            if (LogEntry.StartsWith("["))  //old log format, ignore
                return;
            List<string> splt = Global.Split(LogEntry,"|",false,false);
            // "Date|Level|Source|Func|AIServer|Camera|Detail|Idx|Depth|Color|ThreadID"
            //  0    1     2      3    4        5      6      7   8     9     10

            if (splt.Count != 11)
                return;

            try
            {
                DateTime tdate = DateTime.MinValue;
                if (DateTime.TryParse(splt[0], out tdate))
                    this.Date = tdate;

                if (splt[1].ToLower() == "level")  //this must be a NEW header written part way down the file?
                    return;

                this.Level = LogLevel.FromString(splt[1]);
                this.Source = splt[2];
                this.Func = splt[3];
                this.AIServer = splt[4];
                this.Camera = splt[5];
                this.Detail = splt[6];
                this.Idx = Convert.ToInt32(splt[7]);
                this.Depth = Convert.ToInt32(splt[8]);
                this.Color = splt[9];
                this.ThreadID = Convert.ToInt32(splt[10]);

            }
            catch
            {
                this.Level = LogLevel.Off;
                return;
            }
        }

        public DateTime Date { get; set; } = DateTime.MinValue;
        public string Func { get; set; } = "";
        public string Detail { get; set; } = "";
        public NLog.LogLevel Level { get; set; } = NLog.LogLevel.Off;
        public string Source { get; set; } = "";
        public string AIServer { get; set; } = "";
        public string Camera { get; set; } = "";
        public int Idx { get; set; } = 0;
        public int Depth { get; set; } = 0;
        public string Color { get; set; } = "";
        public int ThreadID { get; set; } = 0;
        public bool FromFile { get; set; } = false;
        //public bool Displayed = false;
        public override string ToString()
        {
            //This is mainly meant for log output
            // "Date|Level|Source|Func|AIServer|Camera|Detail|Idx|Depth|Color"

            string str = $"{this.Date.ToString("yyyy-MM-dd HH:mm:ss.ffffff")}|{this.Level.ToString()}|{this.Source}|{this.Func}|{this.AIServer}|{this.Camera}|{this.Detail}|{this.Idx}|{this.Depth}|{this.Color}|{this.ThreadID}";
            return str;
        }
        public string ToDetailString()
        {
            //This is mainly meant for my application RTF log or normal log not output log
            string str = this.Level.ToString().ToUpper() + "> " + this.Detail.Trim();
            //Displayed = true;
            return str;
        }
    }
}
