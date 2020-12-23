using Amazon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class ClsURLItem : IEquatable<ClsURLItem>
    {
        private bool isValid = false;

        public URLTypeEnum Type { get; set; } = URLTypeEnum.Unknown;
        public ThreadSafe.Boolean Enabled { get; set; } = new ThreadSafe.Boolean(false);
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
        public int Order { get; set; } = 0;
        public string url { get; set; } = "";
        
        public ThreadSafe.Boolean InUse { get; set; } = new ThreadSafe.Boolean(false);
        public string ActiveTimeRange { get; set; } = "00:00:00-23:59:59";
        public string Cameras { get; set; } = "";
        public int MaxImagesPerMonth { get; set; } = 0;
        public string ImageAdjustProfile { get; set; } = "Default";
        public int Threshold_Lower { get; set; } = 0;   //override the cameras threshold since different AI servers may need to be tuned to different values
        public int Threshold_Upper { get; set; } = 100;
        [JsonIgnore]
        public int CurOrder { get; set; } = 0;
        [JsonIgnore]
        public ThreadSafe.Integer CurErrCount { get; set; } = new ThreadSafe.Integer(0);
        public ThreadSafe.Integer ErrCount { get; set; } = new ThreadSafe.Integer(0);
        public string HelpURL { get; set; } = "";
        public DateTime LastUsedTime { get; set; } = DateTime.MinValue;
        public string LastResultMessage { get; set; } = "";
        public long LastTimeMS { get; set; } = 0;
        public MovingCalcs AITimeCalcs { get; set; } = new MovingCalcs(250, "Images", true);   //store deepstack time calc in the url
        public string CurSrv { get; set; } = "";
        public string DefaultURL { get; set; } = "";
        [JsonIgnore]
        public HttpClient HttpClient { get; set; }
        //public int Count { get; set; } = 0;
        public bool UrlFixed = false;

        public override string ToString()
        {
            return this.url;
        }
        public void IncrementError()
        {
            this.CurErrCount.AtomicIncrementAndGet();
            this.ErrCount.AtomicIncrementAndGet();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClsURLItem);
        }

        public bool Equals(ClsURLItem other)
        {
            return other != null &&
                   string.Equals(url, other.url, StringComparison.OrdinalIgnoreCase);
        }

        [JsonConstructor]
        public ClsURLItem() { }

        public ClsURLItem(String url, int Order, URLTypeEnum type)
        {
            this.UrlFixed = false;

            if (string.IsNullOrWhiteSpace(url))
            {
                if (type == URLTypeEnum.DOODS)
                {
                    this.DefaultURL = "http://localhost:8080/detect";
                    url = this.DefaultURL;
                }
                else if (type == URLTypeEnum.AWSRekognition) // || this.url.Equals("aws", StringComparison.OrdinalIgnoreCase) || this.url.Equals("rekognition", StringComparison.OrdinalIgnoreCase))
                {
                    this.DefaultURL = "Amazon";
                    url = this.DefaultURL;
                }
                else // assume deepstack //if (this.Type == URLTypeEnum.DeepStack || this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.DefaultURL = "http://localhost:80/v1/vision/detection";
                    url = this.DefaultURL;
                }

            }

            this.url = url.Trim();

            this.Type = type;
            this.Order = Order;


            if (this.Type == URLTypeEnum.DOODS || this.url.EndsWith("/detect", StringComparison.OrdinalIgnoreCase))
            {
                this.DefaultURL = "http://127.0.0.1:8080/detect";
                this.HelpURL = "https://github.com/snowzach/doods";
                if (!this.url.Contains("://"))
                {
                    this.UrlFixed = true;
                    this.url = "http://" + this.url;
                }
                this.Type = URLTypeEnum.DOODS;
                if (!(this.url.IndexOf("/detect", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    this.UrlFixed = true;
                    this.url = this.url + "/detect";
                }

                Uri uri = new Uri(this.url);
                this.CurSrv = uri.Host + ":" + uri.Port;
                this.HttpClient = new HttpClient();
                this.HttpClient.Timeout = TimeSpan.FromSeconds(AppSettings.Settings.HTTPClientTimeoutSeconds);
                this.IsValid = true;
                this.Enabled.WriteFullFence(true);
            }
            else if (this.Type == URLTypeEnum.AWSRekognition || this.url.Equals("amazon", StringComparison.OrdinalIgnoreCase)) // || this.url.Equals("aws", StringComparison.OrdinalIgnoreCase) || this.url.Equals("rekognition", StringComparison.OrdinalIgnoreCase))
            {

                this.DefaultURL = "Amazon";

                this.HelpURL = "https://docs.aws.amazon.com/rekognition/latest/dg/setting-up.html";
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
                    this.IsValid = false;
                    this.Enabled.WriteFullFence(false);
                }

            }
            else // assume deepstack //if (this.Type == URLTypeEnum.DeepStack || this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                this.DefaultURL = "http://127.0.0.1:80/v1/vision/detection";
                this.HelpURL = "https://ipcamtalk.com/threads/tool-tutorial-free-ai-person-detection-for-blue-iris.37330/";

                if (!this.url.Contains("://"))
                {
                    this.UrlFixed = true;
                    this.url = "http://" + this.url;
                }


                this.Type = URLTypeEnum.DeepStack;

                bool valid = Global.IsValidURL(this.url);

                //only add path if none already given, for example /vision/custom/model-name
                if (!valid)
                {
                    this.UrlFixed = true;
                    this.url = this.url.Trim() + "/v1/vision/detection";
                }

                Uri uri = new Uri(this.url);
                this.CurSrv = uri.Host + ":" + uri.Port;
                this.HttpClient = new HttpClient();
                this.HttpClient.Timeout = TimeSpan.FromSeconds(AppSettings.Settings.HTTPClientTimeoutSeconds);
                this.IsValid = true;
                this.Enabled.WriteFullFence(true);
            }


            this.UpdateIsValid();
        }

        public bool UpdateIsValid()
        {
            bool ret = false;
            this.UrlFixed = false;

            Uri uri = null;

            if (this.Type == URLTypeEnum.DOODS)
            {
                if (!this.url.Contains("://"))
                {
                    this.UrlFixed = true;
                    this.url = "http://" + this.url;
                }

                if (!(this.url.IndexOf("/detect", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    this.UrlFixed = true;
                    this.url = this.url + "/detect";
                }

                if (Global.IsValidURL(this.url) && this.url.IndexOf("/detect", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    uri = new Uri(this.url);
                    if (uri.Port > 0)
                    {
                        ret = true;
                    }
                }
            }
            else if (this.Type == URLTypeEnum.AWSRekognition)
            {


                if (this.url.Equals("amazon", StringComparison.OrdinalIgnoreCase))
                {
                    ret = true;
                }

            }
            else // assume deepstack 
            {
                if (!this.url.Contains("://"))
                {
                    this.UrlFixed = true;
                    this.url = "http://" + this.url.Trim();
                }


                bool hasdet = this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0;
                bool hascus = this.url.IndexOf("/v1/vision/custom/", StringComparison.OrdinalIgnoreCase) >= 0;

                if (!hasdet && !hascus)
                {
                    this.UrlFixed = true;
                    this.url = this.url.Trim() + "/v1/vision/detection";
                }
                ///v1/vision/custom/catsanddogs
                if (Global.IsValidURL(this.url) && (hasdet || hascus))
                {
                    uri = new Uri(this.url);
                    if (uri.Port > 0)
                    {
                        ret = true;
                    }
                }

            }

            if (uri != null)
                this.CurSrv = uri.Host + ":" + uri.Port;

            if (!ret)
                AITOOL.Log($"Error: '{this.Type.ToString()}' URL is not valid: '{this.url}'");

            this.IsValid = ret;

            if (!this.isValid)
               this.Enabled.WriteFullFence(false);

            return ret;
        }

        public static bool operator ==(ClsURLItem left, ClsURLItem right)
        {
            return EqualityComparer<ClsURLItem>.Default.Equals(left, right);
        }

        public static bool operator !=(ClsURLItem left, ClsURLItem right)
        {
            return !(left == right);
        }
    }
}
