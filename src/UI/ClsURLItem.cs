using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string url { get; set; } = "";
        public bool Enabled { get; set; } = false;
        public int ErrCount { get; set; } = 0;
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
