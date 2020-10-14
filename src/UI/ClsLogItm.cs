using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public enum LogType
    {
        Fatal,
        Error,
        Warn,
        Info,
        Debug,
        Trace,
        Off,
        Unknown
    }

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

        public LogType DType { get; set; } = LogType.Unknown;
        public DateTime Time { get; set; } = DateTime.MinValue;
        public string Func { get; set; } = "";
        public string AIServer { get; set; } = "";
        public string Camera { get; set; } = "";
        public string Detail { get; set; } = "";
        public int Idx { get; set; } = 0;
        public int Depth { get; set; } = 0;
        //public bool Displayed = false;
        public override string ToString()
        {
            //This is mainly meant for log output
            string str = this.Time.ToString("hh:mm:ss.ffff") + " [" + this.Idx.ToString("000") + "]> " + Detail.Trim();
            return str;
        }
        public string ToDetailString()
        {
            //This is mainly meant for my application RTF log or normal log not autocad output log
            string str = this.DType.ToString().ToUpper() + "> " + this.Detail.Trim();
            //Displayed = true;
            return str;
        }
    }
}
