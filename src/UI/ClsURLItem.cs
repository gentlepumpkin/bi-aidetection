using Amazon;
using System;
using System.IO;
using System.Net.Http;

namespace AITool
{
    public enum URLTypeEnum
    {
        DeepStack,
        DOODS,
        AWSRekognition,
        Other,
        Unknown
    }
    public class ClsURLItem
    {

        public ThreadSafe.Boolean Enabled { get; set; } = new ThreadSafe.Boolean(false);
        public URLTypeEnum Type { get; set; } = URLTypeEnum.Unknown;
        public ThreadSafe.Integer CurErrCount { get; set; } = new ThreadSafe.Integer(0);
        public ThreadSafe.Integer ErrCount { get; set; } = new ThreadSafe.Integer(0);
        public int OrigOrder { get; set; } = 0;
        public int CurOrder { get; set; } = 0;
        public string url { get; set; } = "";
        public ThreadSafe.Boolean InUse { get; set; } = new ThreadSafe.Boolean(false);
        public string ActiveTimeRange { get; set; } = "00:00:00-23:59:59";
        public string Cameras { get; set; } = "";
        public int MaxImagesPerMonth = 0;
        public DateTime LastUsedTime { get; set; } = DateTime.MinValue;
        public string LastResultMessage { get; set; } = "";
        public long LastTimeMS { get; set; } = 0;
        public MovingCalcs AITimeCalcs { get; set; } = new MovingCalcs(250, "Images", true);   //store deepstack time calc in the url
        public string CurSrv { get; set; } = "";
        public HttpClient HttpClient { get; set; }
        public int Count { get; set; } = 0;

        public bool IsValid { get; set; } = false;
        public override string ToString()
        {
            return this.url;
        }
        public void IncrementError()
        {
            this.CurErrCount.AtomicIncrementAndGet();
            this.ErrCount.AtomicIncrementAndGet();
        }
        public ClsURLItem(String url, int Order, int Count, URLTypeEnum type)
        {
            
            if (!string.IsNullOrWhiteSpace(url))
            {
                this.url = url.Trim();

                this.Type = type;
                this.OrigOrder = Order;
                this.Count = Count;


                if (this.Type == URLTypeEnum.DOODS || this.url.EndsWith("/detect", StringComparison.OrdinalIgnoreCase))
                {
                    if (!this.url.Contains("://"))
                        this.url = "http://" + this.url;
                    this.Type = URLTypeEnum.DOODS;
                    if (!(this.url.IndexOf("/detect", StringComparison.OrdinalIgnoreCase) >= 0))
                        this.url = this.url + "/detect";
                    Uri uri = new Uri(this.url);
                    this.CurSrv = uri.Host + ":" + uri.Port;
                    this.HttpClient = new HttpClient();
                    this.HttpClient.Timeout = TimeSpan.FromSeconds(AppSettings.Settings.HTTPClientTimeoutSeconds);
                    this.IsValid = true;
                    this.Enabled.WriteFullFence(true);
                }
                else if (this.Type == URLTypeEnum.AWSRekognition || this.url.Equals("amazon", StringComparison.OrdinalIgnoreCase)) // || this.url.Equals("aws", StringComparison.OrdinalIgnoreCase) || this.url.Equals("rekognition", StringComparison.OrdinalIgnoreCase))
                {

                    this.Type = URLTypeEnum.AWSRekognition;

                    string error = AITOOL.UpdateAmazonSettings();

                    if (string.IsNullOrEmpty(error))
                    {
                        this.CurSrv = "Amazon:" + AppSettings.Settings.AmazonRegionEndpoint;
                        this.IsValid = true;
                        this.Enabled.WriteFullFence(true);
                        this.MaxImagesPerMonth = 5000;
                    }
                    else
                    {
                        AITOOL.Log($"Error: {error}");
                    }

                }
                else // assume deepstack //if (this.Type == URLTypeEnum.DeepStack || this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!this.url.Contains("://"))
                        this.url = "http://" + this.url;
                    this.Type = URLTypeEnum.DeepStack;
                    if (!(this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0))
                        this.url = this.url.Trim() + "/v1/vision/detection";
                    Uri uri = new Uri(this.url);
                    this.CurSrv = uri.Host + ":" + uri.Port;
                    this.HttpClient = new HttpClient();
                    this.HttpClient.Timeout = TimeSpan.FromSeconds(AppSettings.Settings.HTTPClientTimeoutSeconds);
                    this.IsValid = true;
                    this.Enabled.WriteFullFence(true);
                }

            }
        }

    }
}
