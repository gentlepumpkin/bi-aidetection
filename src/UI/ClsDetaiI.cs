using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    

    public class ClsDetail 
    {
        private int _LastIDX = 0;
        private int _NotDisplayedCount = 0;
        private object _LockObj = new object();
        public List<ClsDetailItm> Values = new List<ClsDetailItm>();
        private ClsDetailItm _LastDetailItm = new ClsDetailItm();

        

        public ClsDetail()
        {
        }

        public void Clear()
        {
            this.Values.Clear();
            this._LastDetailItm = new ClsDetailItm();
            this._LastIDX = 0;

        }
               

        public void Add(ClsDetailItm CDI)
        {
            lock (this._LockObj)
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

        public void AddRange(List<ClsDetailItm> CDIList)
        {
            lock (this._LockObj)
            {
                foreach (ClsDetailItm CDI in CDIList)
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
        public string Add(string Detail, DetailType Type = DetailType.Unknown, Nullable<DateTime> Time = default(DateTime?), [CallerMemberName()] string memberName = null)
        {
            lock (this._LockObj)
            {
                string det = Detail.Trim();
                if (string.IsNullOrWhiteSpace(det))
                    return "";
                this._LastIDX += 1;
                this._LastDetailItm = new ClsDetailItm();
                this._LastDetailItm.Detail = det;
                if (Time.HasValue)
                    this._LastDetailItm.Time = Time.Value;
                else
                    this._LastDetailItm.Time = DateTime.Now;
                this._LastDetailItm.MemberName = memberName;
                if (Type == DetailType.Unknown)
                {
                    if (this._LastDetailItm.Detail.ToLower().Contains("error:"))
                        Type = DetailType.Error;
                    else if (this._LastDetailItm.Detail.ToLower().Contains("warning:"))
                        Type = DetailType.Warning;
                    else if (this._LastDetailItm.Detail.ToLower().Contains("info:"))
                        Type = DetailType.Info;
                    else if (this._LastDetailItm.Detail.ToLower().Contains("debug:"))
                        Type = DetailType.Debug;
                    else
                        Type = DetailType.Detail;
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
