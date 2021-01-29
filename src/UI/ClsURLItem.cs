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
        DeepStack_Faces,
        DeepStack_Custom,
        DeepStack_Scene,
        DOODS,
        AWSRekognition_Objects,
        AWSRekognition_Faces,
        SightHound_Vehicle,
        SightHound_Person,
        Other,
        Unknown
    }
    public class ClsURLItem : IEquatable<ClsURLItem>
    {
        private bool isValid = false;

        public URLTypeEnum Type { get; set; } = URLTypeEnum.Unknown;
        public ThreadSafe.Boolean Enabled { get; set; } = new ThreadSafe.Boolean(true);
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
        public int Order { get; set; } = 0;
        public string url { get; set; } = "";
        [JsonIgnore]
        public ThreadSafe.Boolean InUse { get; set; } = new ThreadSafe.Boolean(false);
        public string ActiveTimeRange { get; set; } = "00:00:00-23:59:59";
        public string Cameras { get; set; } = "";
        public int MaxImagesPerMonth { get; set; } = 0;
        public string ImageAdjustProfile { get; set; } = "Default";
        public int Threshold_Lower { get; set; } = 0;   //override the cameras threshold since different AI servers may need to be tuned to different values
        public int Threshold_Upper { get; set; } = 100;
        public bool UseAsRefinementServer { get; set; } = false;
        public string RefinementObjects { get; set; } = "";
        [JsonIgnore]
        public bool RefinementUseCurrentlyValid { get; set; } = false;
        public bool LinkServerResults { get; set; } = false;
        public string LinkedResultsServerList { get; set; } = "";
        [JsonIgnore]
        public int CurOrder { get; set; } = 0;
        [JsonIgnore]
        public ThreadSafe.Integer CurErrCount { get; set; } = new ThreadSafe.Integer(0);
        [JsonIgnore]
        public ThreadSafe.Boolean ErrDisabled { get; set; } = new ThreadSafe.Boolean(false);
        public ThreadSafe.Integer ErrCount { get; set; } = new ThreadSafe.Integer(0);
        public ThreadSafe.Integer ErrsInRowCount { get; set; } = new ThreadSafe.Integer(0);
        public bool IsLocalHost { get; set; } = false;
        public bool IsLocalNetwork { get; set; } = false;
        public string HelpURL { get; set; } = "";
        public DateTime LastUsedTime { get; set; } = DateTime.MinValue;
        public bool LastResultSuccess { get; set; } = false;
        public string LastResultMessage { get; set; } = "";
        public long LastTimeMS { get; set; } = 0;
        public MovingCalcs AITimeCalcs { get; set; } = new MovingCalcs(250, "Images", true);   //store deepstack time calc in the url
        public string CurSrv { get; set; } = "";
        public int Port { get; set; } = 0;
        public int HttpClientTimeoutSeconds { get; set; } = 0;
        public string DefaultURL { get; set; } = "";
        //[JsonIgnore]
        //public Global.ClsProcess Process { get; set; } = null;
        [JsonIgnore]
        public HttpClient HttpClient { get; set; } = null;
        //public int Count { get; set; } = 0;
        public bool UrlFixed { get; set; } = false;
        public bool ExternalSettingsValid { get; set; } = false;

        public override string ToString()
        {
            return this.url;
        }
        public void IncrementError()
        {
            this.CurErrCount.AtomicIncrementAndGet();
            this.ErrCount.AtomicIncrementAndGet();
        }

        public TimeSpan GetTimeout()
        {
            if (this.HttpClientTimeoutSeconds > 0)
                return TimeSpan.FromSeconds(this.HttpClientTimeoutSeconds);
            else if (this.IsLocalNetwork)
                return TimeSpan.FromSeconds(AppSettings.Settings.HTTPClientLocalTimeoutSeconds);
            else
                return TimeSpan.FromSeconds(AppSettings.Settings.HTTPClientRemoteTimeoutSeconds);

        }

        [JsonConstructor]
        public ClsURLItem() { }

        public ClsURLItem(String url, int Order, URLTypeEnum type)
        {
            this.UrlFixed = false;
            this.url = url.Trim();
            this.Type = type;
            this.Order = Order;
            this.Update(true);
        }

        public bool Update(bool Init)
        {
            bool ret = false;
            this.UrlFixed = false;

            Uri uri = null;

            bool HasDoods = this.url.EndsWith("/detect", StringComparison.OrdinalIgnoreCase);
            bool HasAWSObj = this.url.Equals("amazon", StringComparison.OrdinalIgnoreCase) || this.url.Equals("amazon_objects", StringComparison.OrdinalIgnoreCase);
            bool HasAWSFac = this.url.Equals("amazon_faces", StringComparison.OrdinalIgnoreCase);
            bool HasSHPer = this.url.IndexOf("/v1/detections", StringComparison.OrdinalIgnoreCase) >= 0;
            bool HasSHVeh = this.url.IndexOf("/v1/recognition", StringComparison.OrdinalIgnoreCase) >= 0;
            bool HasDSFacRec = this.url.IndexOf("/v1/vision/face/recognize", StringComparison.OrdinalIgnoreCase) >= 0;  //Face Recognition
            bool HasDSFacDet = this.url.IndexOf("/v1/vision/face", StringComparison.OrdinalIgnoreCase) >= 0;  //Face Detections
            bool HasDSCus = this.url.IndexOf("/v1/vision/custom", StringComparison.OrdinalIgnoreCase) >= 0;
            bool HasDSScn = this.url.IndexOf("/v1/vision/scene", StringComparison.OrdinalIgnoreCase) >= 0;
            bool HasDSDet = this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0;


            bool ShouldInit = Init || !this.isValid || string.IsNullOrWhiteSpace(this.url) || (!this.url.Contains("/") && !this.url.Contains("_")) || this.Type == URLTypeEnum.Unknown;

            if (ShouldInit)
            {
                if (this.Type == URLTypeEnum.DOODS || HasDoods)
                {
                    this.DefaultURL = "http://127.0.0.1:8080/detect";
                    this.HelpURL = "https://github.com/snowzach/doods";
                    this.Type = URLTypeEnum.DOODS;
                }
                else if (this.Type == URLTypeEnum.AWSRekognition_Objects || HasAWSObj)
                {
                    this.DefaultURL = "Amazon_Objects";
                    this.HelpURL = "https://docs.aws.amazon.com/rekognition/latest/dg/setting-up.html";
                    this.Type = URLTypeEnum.AWSRekognition_Objects;
                    this.MaxImagesPerMonth = 5000;
                }
                else if (this.Type == URLTypeEnum.AWSRekognition_Faces || HasAWSFac)
                {
                    this.DefaultURL = "Amazon_Faces";
                    this.HelpURL = "https://docs.aws.amazon.com/rekognition/latest/dg/setting-up.html";
                    this.Type = URLTypeEnum.AWSRekognition_Faces;
                    this.UseAsRefinementServer = true;
                    this.RefinementObjects = "person,face";
                    this.MaxImagesPerMonth = 5000;
                }
                else if (this.Type == URLTypeEnum.SightHound_Vehicle || HasSHVeh)
                {
                    this.DefaultURL = "https://dev.sighthoundapi.com/v1/recognition?objectType=vehicle,licenseplate";
                    this.HelpURL = "https://docs.sighthound.com/cloud/recognition/";
                    this.Type = URLTypeEnum.SightHound_Vehicle;
                    this.MaxImagesPerMonth = 5000;
                    this.UseAsRefinementServer = true;
                    this.RefinementObjects = "car,truck,pickup truck,bus,suv,van,motorcycle";
                    this.IsLocalHost = false;
                    this.IsLocalNetwork = false;
                    this.HttpClient = null;
                }
                else if (this.Type == URLTypeEnum.SightHound_Person || HasSHPer)
                {
                    this.DefaultURL = "https://dev.sighthoundapi.com/v1/detections?type=face,person&faceOption=gender,age,emotion";
                    this.HelpURL = "https://docs.sighthound.com/cloud/detection/";
                    this.Type = URLTypeEnum.SightHound_Person;
                    this.UseAsRefinementServer = true;
                    this.RefinementObjects = "person,face";
                    this.IsLocalHost = false;
                    this.IsLocalNetwork = false;
                    this.HttpClient = null;
                    this.MaxImagesPerMonth = 5000;
                }
                else if (this.Type == URLTypeEnum.DeepStack_Faces || HasDSFacRec)
                {
                    this.DefaultURL = "http://127.0.0.1:80/v1/vision/face/recognize";
                    this.HelpURL = "https://docs.deepstack.cc/face-recognition/index.html";
                    this.Type = URLTypeEnum.DeepStack_Faces;
                    this.UseAsRefinementServer = true;
                    this.RefinementObjects = "person,face";
                }
                else if (this.Type == URLTypeEnum.DeepStack_Scene || HasDSScn)
                {
                    this.DefaultURL = "http://127.0.0.1:80/v1/vision/scene";
                    this.HelpURL = "https://docs.deepstack.cc/face-recognition/index.html";
                    this.Type = URLTypeEnum.DeepStack_Scene;
                    this.UseAsRefinementServer = true;
                    this.RefinementObjects = "*";
                }
                else if (this.Type == URLTypeEnum.DeepStack_Custom || HasDSCus) // assume deepstack //if (this.Type == URLTypeEnum.DeepStack || this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.DefaultURL = "http://127.0.0.1:80/v1/vision/custom/YOUR_CUSTOM_MODEL_NAME_HERE";
                    this.HelpURL = "https://docs.deepstack.cc/custom-models/index.html";
                    this.Type = URLTypeEnum.DeepStack_Custom;
                }
                else // assume deepstack //if (this.Type == URLTypeEnum.DeepStack || this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.DefaultURL = "http://127.0.0.1:80/v1/vision/detection";
                    this.HelpURL = "https://docs.deepstack.cc/object-detection/index.html";
                    this.Type = URLTypeEnum.DeepStack;
                }

            }

            if (string.IsNullOrWhiteSpace(this.url))
                this.url = this.DefaultURL;

            HasDoods = this.url.EndsWith("/detect", StringComparison.OrdinalIgnoreCase);
            HasAWSObj = this.url.Equals("amazon", StringComparison.OrdinalIgnoreCase) || this.url.Equals("amazon_objects", StringComparison.OrdinalIgnoreCase);
            HasAWSFac = this.url.Equals("amazon_faces", StringComparison.OrdinalIgnoreCase);
            HasSHPer = this.url.IndexOf("/v1/detections", StringComparison.OrdinalIgnoreCase) >= 0;
            HasSHVeh = this.url.IndexOf("/v1/recognition", StringComparison.OrdinalIgnoreCase) >= 0;
            HasDSFacRec = this.url.IndexOf("/v1/vision/face/recognize", StringComparison.OrdinalIgnoreCase) >= 0;  //Face Recognition
            HasDSFacDet = this.url.IndexOf("/v1/vision/face", StringComparison.OrdinalIgnoreCase) >= 0;  //Face Detections - Not using this for now
            HasDSCus = this.url.IndexOf("/v1/vision/custom", StringComparison.OrdinalIgnoreCase) >= 0;
            HasDSDet = this.url.IndexOf("/v1/vision/detection", StringComparison.OrdinalIgnoreCase) >= 0;
            HasDSScn = this.url.IndexOf("/v1/vision/scene", StringComparison.OrdinalIgnoreCase) >= 0;


            //================================================================================
            // Try to correct any servers without a full URL
            //================================================================================
            if (this.Type == URLTypeEnum.DOODS || HasDoods)
            {

                if (!this.url.Contains("://"))
                {
                    this.UrlFixed = true;
                    this.url = "http://" + this.url;
                }

                this.Type = URLTypeEnum.DOODS;

                if (!HasDoods)
                {
                    this.UrlFixed = true;
                    this.url = this.url + "/detect";
                }

            }
            else if (this.Type == URLTypeEnum.AWSRekognition_Objects || HasAWSObj)
            {

                this.url = "Amazon_Objects";
                this.Type = URLTypeEnum.AWSRekognition_Objects;
                this.UrlFixed = true;

            }
            else if (this.Type == URLTypeEnum.AWSRekognition_Faces || HasAWSFac)
            {

                this.Type = URLTypeEnum.AWSRekognition_Faces;
                this.url = "Amazon_Faces";
                this.UrlFixed = true;
            }
            else if (this.Type == URLTypeEnum.SightHound_Vehicle || HasSHVeh)
            {

                if (!this.url.Contains("://"))
                {
                    this.UrlFixed = true;
                    this.url = "https://" + this.url;
                }
                this.Type = URLTypeEnum.SightHound_Vehicle;

                if (!HasSHVeh)
                {
                    this.UrlFixed = true;
                    this.url = this.url + "/v1/recognition?objectType=vehicle,licenseplate";
                }

            }
            else if (this.Type == URLTypeEnum.SightHound_Person || HasSHPer)
            {
                if (!this.url.Contains("://"))
                {
                    this.UrlFixed = true;
                    this.url = "https://" + this.url;
                }

                this.Type = URLTypeEnum.SightHound_Person;

                if (!HasSHPer)
                {
                    this.UrlFixed = true;
                    this.url = this.url + "/v1/detections?type=face,person&faceOption=gender,age,emotion";
                }
            }
            else  // assume deepstack, default to detection
            {
                if (!this.url.Contains("://"))
                {
                    this.UrlFixed = true;
                    this.url = "http://" + this.url.Trim();
                }

                if (!HasDSCus && !HasDSDet && !HasDSScn && !HasDSFacRec && !HasDSFacDet)  //default to regular detection
                {
                    if (this.url.IsLike("*:#*"))
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + "/v1/vision/detection"; this.UrlFixed = true;
                    }
                    else
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + ":80/v1/vision/detection"; this.UrlFixed = true;
                    }

                    this.Type = URLTypeEnum.DeepStack;
                }
                else if (this.Type == URLTypeEnum.DeepStack_Custom && !HasDSCus)
                {
                    if (this.url.IsLike("*:#*"))
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + "/v1/vision/custom/YOUR_CUSTOM_MODEL_NAME_HERE"; this.UrlFixed = true;
                    }
                    else
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + ":80/v1/vision/custom/YOUR_CUSTOM_MODEL_NAME_HERE"; this.UrlFixed = true;
                    }
                }
                else if (this.Type != URLTypeEnum.DeepStack_Custom && HasDSCus)
                {
                    this.Type = URLTypeEnum.DeepStack_Custom;
                }
                else if (this.Type == URLTypeEnum.DeepStack_Faces && !HasDSFacRec && !HasDSFacDet)
                {
                    if (this.url.IsLike("*:#*"))
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + "/v1/vision/face/recognize"; this.UrlFixed = true;
                    }
                    else
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + ":80/v1/vision/face/recognize"; this.UrlFixed = true;
                    }
                }
                else if (this.Type != URLTypeEnum.DeepStack_Faces && HasDSFacRec)
                {
                    this.Type = URLTypeEnum.DeepStack_Faces;
                }
                else if (this.Type == URLTypeEnum.DeepStack && !HasDSDet)
                {
                    if (this.url.IsLike("*:#*"))
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + "/v1/vision/detection"; this.UrlFixed = true;
                    }
                    else
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + ":80/v1/vision/detection"; this.UrlFixed = true;
                    }
                }
                else if (this.Type != URLTypeEnum.DeepStack && HasDSDet)
                {
                    this.Type = URLTypeEnum.DeepStack;
                }

                else if (this.Type == URLTypeEnum.DeepStack_Scene && !HasDSScn)
                {
                    if (this.url.IsLike("*:#*"))
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + "/v1/vision/scene"; this.UrlFixed = true;
                    }
                    else
                    {
                        this.url = this.url.Trim(" /:".ToCharArray()) + ":80/v1/vision/scene"; this.UrlFixed = true;
                    }
                }
                else if (this.Type != URLTypeEnum.DeepStack_Scene && HasDSScn)
                {
                    this.Type = URLTypeEnum.DeepStack_Scene;
                }


            }


            //================================================================================
            // Do final validation tests
            //================================================================================

            bool IsAWS = this.Type == URLTypeEnum.AWSRekognition_Objects || this.Type == URLTypeEnum.AWSRekognition_Faces;

            if (!IsAWS)
            {
                if (Global.IsValidURL(this.url))
                {
                    uri = new Uri(this.url);


                    this.Port = uri.Port;
                    this.IsLocalHost = Global.IsLocalHost(uri.Host);
                    this.IsLocalNetwork = Global.IsLocalNetwork(uri.Host);

                    if (this.IsLocalHost && !uri.Host.Contains("127."))
                    {
                        //force it to always be 127.0.0.1 for localhost
                        AITOOL.Log($"Debug: Converting localhost from '{uri.Host}' to '127.0.0.1'.  Localhost and 0.0.0.0 do not seem to be reliable.");
                        uri = new Uri($"{uri.Scheme}://127.0.0.1:{uri.Port}{uri.PathAndQuery}");
                        this.url = uri.ToString();
                    }

                    if (this.Type == URLTypeEnum.DeepStack)
                    {
                        this.CurSrv = "Deepstack_Objects:" + uri.Host + ":" + uri.Port;
                    }
                    else if (this.Type == URLTypeEnum.DeepStack_Custom)
                    {
                        this.CurSrv = "Deepstack_Custom:" + uri.Host + ":" + uri.Port;
                    }
                    else if (this.Type == URLTypeEnum.DeepStack_Faces)
                    {
                        this.CurSrv = "Deepstack_Faces:" + uri.Host + ":" + uri.Port;
                    }
                    else if (this.Type == URLTypeEnum.DeepStack_Scene)
                    {
                        this.CurSrv = "Deepstack_Scene:" + uri.Host + ":" + uri.Port;
                    }
                    else if (this.Type == URLTypeEnum.DOODS)
                    {
                        this.CurSrv = "DOODS:" + uri.Host + ":" + uri.Port;
                    }
                    else if (this.Type == URLTypeEnum.SightHound_Person)
                    {
                        this.CurSrv = "SightHound_Person:" + uri.Host + ":" + uri.Port; this.IsLocalHost = false; this.IsLocalNetwork = false;
                    }
                    else if (this.Type == URLTypeEnum.SightHound_Vehicle)
                    {
                        this.CurSrv = "SightHound_Vehicle:" + uri.Host + ":" + uri.Port; this.IsLocalHost = false; this.IsLocalNetwork = false;
                    }
                    else
                    {
                        this.CurSrv = "Unknown:" + uri.Host + ":" + uri.Port; this.IsLocalHost = false; this.IsLocalNetwork = false;
                    }

                    ret = (this.Type != URLTypeEnum.Unknown && !string.IsNullOrEmpty(this.CurSrv) && !this.CurSrv.StartsWith("Unknown"));
                }
                else
                {
                    ret = false;
                }
            }
            else
            {
                if (IsAWS)
                {
                    this.CurSrv = this.url + ":" + AppSettings.Settings.AmazonRegionEndpoint;
                    this.IsLocalHost = false; this.IsLocalNetwork = false;
                    if (!this.ExternalSettingsValid || ShouldInit)
                    {
                        string error = AITOOL.UpdateAmazonSettings();
                        if (string.IsNullOrEmpty(error))
                        {
                            this.IsValid = true;
                            this.ExternalSettingsValid = true;
                            ret = true;
                        }
                        else
                        {
                            AITOOL.Log($"Error: {error}");
                            this.IsValid = false;
                            this.ExternalSettingsValid = false;
                            ret = false;
                            this.Enabled.WriteFullFence(false);
                        }
                    }
                    else
                    {
                        ret = true;
                    }

                }

            }

            this.IsValid = ret;

            //disable if needed, but never try reenable if the user disabled by themselves 
            if (!this.IsValid)
            {
                this.Enabled.WriteFullFence(false);
                AITOOL.Log($"Error: '{this.Type.ToString()}' URL is not known/valid: '{this.url}'");
            }

            if (!IsAWS && this.isValid && this.HttpClient == null)
            {
                this.HttpClient = new HttpClient();
                this.HttpClient.Timeout = this.GetTimeout();
            }

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

        public override bool Equals(object obj)
        {
            return Equals(obj as ClsURLItem);
        }

        public bool Equals(ClsURLItem other)
        {
            return other != null &&
                   (string.Equals(this.url, other.url, StringComparison.OrdinalIgnoreCase) &&
                   this.Type == other.Type &&
                   this.ActiveTimeRange == other.ActiveTimeRange &&
                   this.Cameras == other.Cameras &&
                   this.ImageAdjustProfile == other.ImageAdjustProfile &&
                   this.RefinementObjects == other.RefinementObjects);
        }

    }
}
