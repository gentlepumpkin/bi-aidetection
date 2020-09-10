using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AITool
{
    public enum URLTypeEnum
    {
        DeepStack,
        Other
    }
    public class ClsURLItem
    {
        private int _ErrCount = 0;

        public string url { get; set; } = "";
        public bool Enabled { get; set; } = false;
        public DateTime LastUsedTime = DateTime.MinValue;
        public MovingCalcs dscalc = new MovingCalcs(250);   //store deepstack time calc in the url
        public long DeepStackTimeMS = 0;
        public int ErrCount { get => _ErrCount; set => _ErrCount = value; }
        public void IncrementErrCount()
        {
            //if we try to increment class.ErrCount directly you get 'A property or indexer may not be passed as an out or ref parameter' - Workaround:
            Interlocked.Increment(ref this._ErrCount);
        }

        public string ResultMessage { get; set; } = "";
        public URLTypeEnum Type { get; set; } = URLTypeEnum.Other;
        public override string ToString()
        {
            return this.url;
        }
        public ClsURLItem(String url, URLTypeEnum type = URLTypeEnum.DeepStack)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                this.Enabled = true;
                this.url = url.Trim();
                this.Type = type;
                if (this.Type == URLTypeEnum.DeepStack)
                {
                    if (!this.url.Contains("://"))
                        this.url = "http://" + this.url;
                    if (!this.url.ToLower().Contains("/v1/vision/detection"))
                        this.url = this.url + "/v1/vision/detection";
                }
            }
        }

    }
}
