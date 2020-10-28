using System;
using System.Net.Http;

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
        public int Order { get; set; } = 0;
        public int CurOrder { get; set; } = 0;
        public int Count { get; set; } = 0;
        public string CurSrv = "";
        public HttpClient HttpClient { get; set; }
        public ThreadSafe.Boolean Enabled { get; set; } = new ThreadSafe.Boolean(false);
        public ThreadSafe.Boolean InUse { get; set; } = new ThreadSafe.Boolean(false);
        public DateTime LastUsedTime { get; set; } = DateTime.MinValue;
        public MovingCalcs dscalc { get; set; } = new MovingCalcs(250);   //store deepstack time calc in the url
        public long DeepStackTimeMS { get; set; } = 0;
        public ThreadSafe.Integer ErrCount { get; set; } = new ThreadSafe.Integer(0);

        public string ResultMessage { get; set; } = "";
        public URLTypeEnum Type { get; set; } = URLTypeEnum.Other;
        public override string ToString()
        {
            return this.url;
        }
        public ClsURLItem(String url, int Order, int Count, URLTypeEnum type = URLTypeEnum.DeepStack)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                this.Enabled.WriteFullFence(true);
                this.url = url.Trim();

                this.Type = type;
                this.Order = Order;
                this.Count = Count;

                if (this.Type == URLTypeEnum.DeepStack)
                {
                    if (!this.url.Contains("://"))
                        this.url = "http://" + this.url;
                    if (!this.url.ToLower().Contains("/v1/vision/detection"))
                        this.url = this.url + "/v1/vision/detection";
                }

                Uri uri = new Uri(this.url);
                this.CurSrv = uri.Host + ":" + uri.Port;

                this.HttpClient = new HttpClient();
                //set httpclient timeout:
                this.HttpClient.Timeout = TimeSpan.FromSeconds(AppSettings.Settings.HTTPClientTimeoutSeconds);
            }
        }

    }
}
