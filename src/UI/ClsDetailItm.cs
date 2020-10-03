using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public enum DetailType
    {
        Info,  //stdout
        Error,
        Warning,
        Debug,
        Detail,
        Unknown
    }
    public class ClsDetailItm
    {

        public DetailType DType { get; set; } = DetailType.Unknown;
        public DateTime Time { get; set; } = DateTime.MinValue;
        public int Idx { get; set; } = 0;
        public string Detail { get; set; } = "";
        public string MemberName { get; set; } = "";
        //public bool Displayed = false;
        //public override string ToString()
        //{
        //    //This is mainly meant for log output
        //    string str = this.Time.ToString("hh:mm:ss.ffff") + " [" + this.Idx.ToString("000") + "]> " + Detail.Trim();
        //    return str;
        //}
        //public string ToDetailString()
        //{
        //    //This is mainly meant for my application RTF log or normal log not autocad output log
        //    string str = this.Type.ToString().ToUpper() + "> " + this.Detail.Trim();
        //    //Displayed = true;
        //    return str;
        //}
    }
}
