using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AITool
{

    //this is for UI logging only, not file logging directly
    public class ClsLogManager 
    {
        public List<ClsLogItm> Values { get; set; } = new List<ClsLogItm>();
        public int ErrorCount { get; set; } = 0;
        private int _LastIDX = 0;
        private int _NotDisplayedCount = 0;
        private object _LockObj = new object();
        private ClsLogItm _LastDetailItm = new ClsLogItm();
               

        public ClsLogManager()
        {
        }

        public void Clear()
        {
            this.Values.Clear();
            this._LastDetailItm = new ClsLogItm();
            this._LastIDX = 0;

        }
               

        public void Add(ClsLogItm CDI)
        {
            lock (this._LockObj)
            {
                this._LastIDX += 1;
                this._NotDisplayedCount += 1;
                this._LastDetailItm = CDI;
                CDI.Idx = this._LastIDX;
                this.Values.Add(this._LastDetailItm);
            }
        }

        public void AddRange(List<ClsLogItm> CDIList)
        {
            lock (this._LockObj)
            {
                foreach (ClsLogItm CDI in CDIList)
                {
                    if (!this.Values.Contains(CDI))
                    {
                        this._LastIDX += 1;
                        this._NotDisplayedCount += 1;
                        this._LastDetailItm = CDI;
                        CDI.Idx = this._LastIDX;
                        this.Values.Add(this._LastDetailItm);
                    }
                }
            }
        }
        public string Add(string Detail, string AIServer, string Camera, LogType Type = LogType.Unknown, Nullable<DateTime> Time = default(DateTime?), [CallerMemberName()] string memberName = null)
        {
            lock (this._LockObj)
            {
                string det = Detail.Trim();
                if (string.IsNullOrWhiteSpace(det))
                    return "";
                this._LastIDX += 1;
                this._LastDetailItm = new ClsLogItm();
                this._LastDetailItm.Detail = det;

                if (Camera==null || string.IsNullOrWhiteSpace(Camera))
                    this._LastDetailItm.Camera = "None";
                else
                    this._LastDetailItm.Camera = Camera;

                if (AIServer == null || string.IsNullOrWhiteSpace(AIServer))
                    this._LastDetailItm.AIServer = "None";
                else
                    this._LastDetailItm.AIServer = Camera;

                if (Time.HasValue)
                    this._LastDetailItm.Time = Time.Value;
                else
                    this._LastDetailItm.Time = DateTime.Now;

                this._LastDetailItm.Func = memberName;
                
                if (Type == LogType.Unknown)
                {
                    if (this._LastDetailItm.Detail.IndexOf("fatal:", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Type = LogType.Fatal;
                    }
                    else if (this._LastDetailItm.Detail.IndexOf("error:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Type = LogType.Error;
                    else if (this._LastDetailItm.Detail.IndexOf("warning:", StringComparison.OrdinalIgnoreCase) >= 0 || this._LastDetailItm.Detail.IndexOf("warn:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Type = LogType.Warn;
                    else if (this._LastDetailItm.Detail.IndexOf("info:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Type = LogType.Info;
                    else if (this._LastDetailItm.Detail.IndexOf("debug:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Type = LogType.Debug;
                    else if (this._LastDetailItm.Detail.IndexOf("trace:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Type = LogType.Trace;
                    else
                        Type = LogType.Info;
                }

                //remove tags
                if (Type == LogType.Error)
                {
                    this.ErrorCount++;
                    this._LastDetailItm.Detail = Global.ReplaceCaseInsensitive(this._LastDetailItm.Detail, "error:", "");
                }
                else if (Type == LogType.Fatal)
                {
                    this.ErrorCount++;
                    this._LastDetailItm.Detail = Global.ReplaceCaseInsensitive(this._LastDetailItm.Detail, "fatal:", "");
                }
                else if (Type == LogType.Warn)
                {
                    this.ErrorCount++;
                    this._LastDetailItm.Detail = Global.ReplaceCaseInsensitive(this._LastDetailItm.Detail, "warn:", "");
                    this._LastDetailItm.Detail = Global.ReplaceCaseInsensitive(this._LastDetailItm.Detail, "warning:", "");
                }
                else if (Type == LogType.Info)
                {
                    this._LastDetailItm.Detail = Global.ReplaceCaseInsensitive(this._LastDetailItm.Detail, "info:", "");
                }
                else if (Type == LogType.Trace)
                {
                    this._LastDetailItm.Detail = Global.ReplaceCaseInsensitive(this._LastDetailItm.Detail, "trace:", "");
                }
                else if (Type == LogType.Debug)
                {
                    this._LastDetailItm.Detail = Global.ReplaceCaseInsensitive(this._LastDetailItm.Detail, "debug:", "");
                }

                this._LastDetailItm.DType = Type;
                this._LastDetailItm.Idx = _LastIDX;

                this.Values.Add(this._LastDetailItm);
            }
            return this._LastDetailItm.Detail;
        }
        public void Sort()
        {
            lock (this._LockObj)
                this.Values = this.Values.OrderBy(c => c.Time).ThenBy(c => c.Idx).ToList();
        }

    }

}
