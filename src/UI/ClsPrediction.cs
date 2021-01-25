using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static AITool.AITOOL;

namespace AITool
{
    public enum ObjectType
    {
        Object,
        Person,
        Animal,
        Vehicle,
        Face,
        Unknown
    }

    public enum ResultType
    {
        Relevant = 1,
        DynamicMasked = 2,
        StaticMasked = 3,
        ImageMasked = 4,
        NoMask = 5,
        NoConfidence = 6,
        UnwantedObject = 7,
        CameraNotEnabled = 8,
        Error = 9,
        Unknown = 10
    }
    public class ClsPrediction : IEquatable<ClsPrediction>
    {
        private ObjectType _defaultObjType;
        private Camera _cam;
        private ClsImageQueueItem _curimg;
        private ClsURLItem _cururl;

        public string Label { get; set; } = "";
        public ResultType Result { get; set; } = ResultType.Unknown;
        public ObjectType ObjType { get; set; } = ObjectType.Unknown;
        public MaskType DynMaskType { get; set; } = MaskType.Unknown;
        public MaskResult DynMaskResult { get; set; } = MaskResult.Unknown;
        public float PercentMatch { get; set; } = 0;
        public MaskType ImgMaskType { get; set; } = MaskType.Unknown;
        public MaskResult ImgMaskResult { get; set; } = MaskResult.Unknown;
        public int DynamicThresholdCount { get; set; } = 0;
        public int ImagePointsOutsideMask { get; set; } = 0;

        public float Confidence { get; set; } = 0;
        public int YMin { get; set; } = 0;
        public int XMin { get; set; } = 0;
        public int YMax { get; set; } = 0;
        public int XMax { get; set; } = 0;
        public string Camera { get; set; } = "";
        public string BICamName { get; set; } = "";
        public int RectWidth { get; set; } = 0;
        public int RectHeight { get; set; } = 0;
        public int ImageWidth { get; set; } = 0;
        public int ImageHeight { get; set; } = 0;
        public int ObjectPriority { get; set; } = 0;
        public string Filename { get; set; } = "";
        public string Server { get; set; } = "";
        public DateTime Time { get; set; } = DateTime.MinValue;
        //private ClsDeepstackObject _imageObject;

        public ClsPrediction() { }
        public ClsPrediction(ObjectType defaultObjType, Camera cam, Amazon.Rekognition.Model.FaceDetail AiDetectionObject, ClsImageQueueItem curImg, ClsURLItem curURL)
        {
            this._defaultObjType = defaultObjType;
            this._cam = cam;
            this._cururl = curURL;
            this._curimg = curImg;
            this.Camera = cam.Name;
            this.BICamName = cam.BICamName;
            this.ImageHeight = curImg.Height;
            this.ImageWidth = curImg.Width;
            this.Server = curURL.CurSrv;
            this.Time = DateTime.Now;
            this.Filename = curImg.image_path;

            if (AiDetectionObject == null || cam == null)
            {
                Log("Error: Prediction or Camera was null?", "", this._cam.Name);
                this.Result = ResultType.Error;
                return;
            }

            this.RectHeight = Convert.ToInt32(Math.Round(curImg.Height * AiDetectionObject.BoundingBox.Height));
            this.RectWidth = Convert.ToInt32(Math.Round(curImg.Width * AiDetectionObject.BoundingBox.Width));

            double right = (curImg.Width * AiDetectionObject.BoundingBox.Left) + this.RectWidth;
            double left = curImg.Width * AiDetectionObject.BoundingBox.Left;

            double top = curImg.Height * AiDetectionObject.BoundingBox.Top;
            double bottom = (curImg.Height * AiDetectionObject.BoundingBox.Top) + this.RectHeight;

            this.XMin = Convert.ToInt32(Math.Round(left));
            this.YMin = Convert.ToInt32(Math.Round(top));

            this.XMax = Convert.ToInt32(Math.Round(right));
            this.YMax = Convert.ToInt32(Math.Round(bottom));

            //[{Global.UpperFirst(AiDetectionObject.Attributes.Gender)}, {AiDetectionObject.Attributes.Age}, {Global.UpperFirst(AiDetectionObject.Attributes.Emotion)}] 
            string emotions = "UnknownEmotion";
            List<Emotion> emotionslist = new List<Emotion>();

            if (AiDetectionObject.Emotions != null && AiDetectionObject.Emotions.Count > 0)
            {
                foreach (Emotion em in AiDetectionObject.Emotions)
                {
                    if (em.Confidence > .5)
                        emotionslist.Add(em);
                }
                if (emotionslist.Count > 0)
                {
                    //sort so highest conf is first
                    emotionslist = emotionslist.OrderByDescending(a => a.Confidence).ToList();
                    emotions = "";
                    int cnt = 0;
                    foreach (Emotion em in emotionslist)
                    {
                        if (em.Confidence > 10)
                           emotions += $"{em.Type};";
                        cnt++;
                        if (cnt > 3)
                            break;
                    }
                    emotions = emotions.Trim(" ;".ToCharArray());
                    if (string.IsNullOrEmpty(emotions))
                        emotions = "UnknownEmotions";
                }
            }

            string gender = "UnknownGender";
            if (AiDetectionObject.Gender != null)
                gender = AiDetectionObject.Gender.Value;

            string age = "UnknownAge";
            if (AiDetectionObject.AgeRange != null)
                age = $"{AiDetectionObject.AgeRange.Low}-{AiDetectionObject.AgeRange.High}";

            if (!gender.Contains("Unknown") || !emotions.Contains("Unknown") || !age.Contains("Unknown"))
                this.Label = $"Face [{gender}, {age}, {emotions}]";
            else
                this.Label = $"Face";

            this.Confidence = AiDetectionObject.Confidence;

            this.GetObjectType();
        }

        public ClsPrediction(ObjectType defaultObjType, Camera cam, Amazon.Rekognition.Model.Label AiDetectionObject, int InstanceIdx, ClsImageQueueItem curImg, ClsURLItem curURL)
        {
            this._defaultObjType = defaultObjType;
            this._cam = cam;
            this._cururl = curURL;
            this._curimg = curImg;
            this.Camera = cam.Name;
            this.BICamName = cam.BICamName;
            this.ImageHeight = curImg.Height;
            this.ImageWidth = curImg.Width;
            this.Server = curURL.CurSrv;
            this.Time = DateTime.Now;
            this.Filename = curImg.image_path;

            if (AiDetectionObject == null || cam == null || string.IsNullOrWhiteSpace(AiDetectionObject.Name))
            {
                Log("Error: Prediction or Camera was null?", "", this._cam.Name);
                this.Result = ResultType.Error;
                return;
            }
            //"Name": "Car",
            //            "Confidence": 98.87621307373047,
            //            "Instances": [
            //                {
            //                    "BoundingBox": {
            //                        "Width": 0.10527367144823074,
            //                        "Height": 0.18472492694854736,
            //                        "Left": 0.0042892382480204105,
            //                        "Top": 0.5051581859588623
            //                    },
            //                    "Confidence": 98.87621307373047
            //                },

            //Rectangle(xmin, ymin, xmax - xmin, ymax - ymin)
            //          x,    y     Width,       Height

            this.RectHeight = Convert.ToInt32(Math.Round(curImg.Height * AiDetectionObject.Instances[InstanceIdx].BoundingBox.Height));
            this.RectWidth = Convert.ToInt32(Math.Round(curImg.Width * AiDetectionObject.Instances[InstanceIdx].BoundingBox.Width));

            double right = (curImg.Width * AiDetectionObject.Instances[InstanceIdx].BoundingBox.Left) + this.RectWidth;
            double left = curImg.Width * AiDetectionObject.Instances[InstanceIdx].BoundingBox.Left;

            double top = curImg.Height * AiDetectionObject.Instances[InstanceIdx].BoundingBox.Top;
            double bottom = (curImg.Height * AiDetectionObject.Instances[InstanceIdx].BoundingBox.Top) + this.RectHeight;

            this.XMin = Convert.ToInt32(Math.Round(left));
            this.YMin = Convert.ToInt32(Math.Round(top));

            this.XMax = Convert.ToInt32(Math.Round(right));
            this.YMax = Convert.ToInt32(Math.Round(bottom));

            //force first letter to always be capitalized 
            this.Label = Global.UpperFirst(AiDetectionObject.Name);

            this.Confidence = AiDetectionObject.Instances[InstanceIdx].Confidence;

            this.GetObjectType();
        }

        public ClsPrediction(ObjectType defaultObjType, Camera cam, SightHoundVehicleObject AiDetectionObject, ClsImageQueueItem curImg, ClsURLItem curURL, SightHoundImage SHImg)
        {
            //https://docs.sighthound.com/cloud/recognition/

            this._defaultObjType = defaultObjType;
            this._cam = cam;
            this._cururl = curURL;
            this._curimg = curImg;
            this.Camera = cam.Name;
            this.BICamName = cam.BICamName;
            this.Server = curURL.CurSrv;
            this.Time = DateTime.Now;
            this.Filename = curImg.image_path;


            if (AiDetectionObject == null || cam == null || string.IsNullOrWhiteSpace(AiDetectionObject.ObjectType))
            {
                Log("Error: Prediction or Camera was null?", "", this._cam.Name);
                this.Result = ResultType.Error;
                return;
            }

            this.ImageHeight = SHImg.Height; //curImg.Height;
            this.ImageWidth = SHImg.Width; //curImg.Width;

            if (curImg.Height != SHImg.Height)
            {
                Log($"Debug: Original image Height does not match returned height: Original={curImg.Height}, Returned={SHImg.Height}");
            }
            if (curImg.Width != SHImg.Width)
            {
                Log($"Debug: Original image Width does not match returned Width: Original={curImg.Width}, Returned={SHImg.Width}");
            }

            //{
            //    "image": { 
            //    "width":2016,
            //    "orientation":1,
            //    "height":1512
            //},
            //"objects":[  
            //    {  
            //        "vehicleAnnotation":{  
            //            "bounding":{  
            //                "vertices":[  
            //                    {  "x":430, "y":286 },
            //                    {  "x":835, "y":286 },
            //                    {  "x":835, "y":559 },
            //                    {  "x":430, "y":559 }
            //                ]
            //            },
            //            "attributes":{  
            //            "system":{  
            //                "color":{  
            //                    "confidence":0.9968,
            //                    "name":"silver/grey"
            //                },
            //                "make":{  
            //                    "confidence":0.8508,
            //                    "name":"BMW"
            //                },
            //                "model":{  
            //                    "confidence":0.8508,
            //                    "name":"3 Series"
            //                },
            //                "vehicleType":"car"
            //            }
            //            },
            //            "recognitionConfidence":0.8508
            //            "licenseplate":{
            //                "bounding":{
            //                    "vertices":[
            //                        { "x":617, "y":452 },
            //                        { "x":743, "y":452 },
            //                        { "x":743, "y":482 },
            //                        { "x":617, "y":482 }
            //                    ]
            //                },
            //                "attributes":{
            //                    "system":{
            //                        "region":{
            //                            "name":"Florida",
            //                            "confidence":0.9994
            //                        },
            //                        "string":{
            //                            "name":"RTB2",
            //                            "confidence":0.999
            //                        },
            //                        "characters":[
            //                            {
            //                                "bounding":{
            //                                    "vertices":[
            //                                        { "y":455, "x":637 },
            //                                        { "y":455, "x":649 },
            //                                        { "y":473, "x":649 },
            //                                        { "y":473, "x":637 }
            //                                    ]
            //                                },
            //                                "index":0,
            //                                "confidence":0.9999,
            //                                "character":"R"
            //                            },
            //                            {
            //                                "bounding":{
            //                                    "vertices":[
            //                                        { "y":455, "x":648 },
            //                                        { "y":455, "x":661 },
            //                                        { "y":473, "x":661 },
            //                                        { "y":473, "x":648 }
            //                                    ]
            //                                },
            //                                "index":1,
            //                                "confidence":0.9999,
            //                                "character":"T"
            //                            },
            //                            {
            //                                "bounding":{  
            //                                    "vertices":[  
            //                                        { "y":455, "x":671 },
            //                                        { "y":455, "x":684 },
            //                                        { "y":474, "x":684 },
            //                                        { "y":474, "x":671 }
            //                                    ]
            //                                },
            //                                "index":2,
            //                                "confidence":0.9996,
            //                                "character":"B"
            //                            },
            //                            {
            //                                "bounding":{
            //                                    "vertices":[
            //                                        { "y":456, "x":683 },
            //                                        { "y":456, "x":696 },
            //                                        { "y":474, "x":696 },
            //                                        { "y":474, "x":683 }
            //                                    ]
            //                                },
            //                                "index":3,
            //                                "confidence":0.9995,
            //                                "character":"2"
            //                            }
            //                        ]
            //                    }
            //                }
            //            },
            //        },
            //        "objectId":"_vehicle_f3c3d26b-c568-4d98-b6db-b96659fd7766",
            //        "objectType":"vehicle"
            //    }
            //],
            //"requestId":"d25b5e5d22f6431498065e9a25134d59"
            //}



            //Rectangle(xmin, ymin, xmax - xmin, ymax - ymin)
            //          x,    y     Width,       Height


            //
            //    vertices (array): A list of objects that define coordinates of the vertices that surround the Object
            //        x (integer): Horizontal pixel position of the vertex
            //        y (integer): Vertical pixel position of the vertex


            // get the bounding box from vertices 
            List< System.Drawing.Point> pts = new List<System.Drawing.Point>();
            foreach (var pt in AiDetectionObject.VehicleAnnotation.Bounding.Vertices)
                pts.Add(new System.Drawing.Point(pt.X, pt.Y));

            Rectangle rect = Global.RectFromVertices(pts);

            this.RectHeight = rect.Height;
            this.RectWidth = rect.Width;

            double right = rect.X + this.RectWidth;
            double left = rect.X;

            double top = rect.Y;
            double bottom = rect.Y + this.RectHeight;

            this.XMin = Convert.ToInt32(Math.Round(left));
            this.YMin = Convert.ToInt32(Math.Round(top));

            this.XMax = Convert.ToInt32(Math.Round(right));
            this.YMax = Convert.ToInt32(Math.Round(bottom));

            if (AiDetectionObject.VehicleAnnotation != null && !string.IsNullOrEmpty(AiDetectionObject.VehicleAnnotation.Attributes.System.VehicleType))
            {
                string type = Global.UpperFirst(AiDetectionObject.VehicleAnnotation.Attributes.System.VehicleType);
                string color = Global.UpperFirst(AiDetectionObject.VehicleAnnotation.Attributes.System.Color.Name);
                string make = Global.UpperFirst(AiDetectionObject.VehicleAnnotation.Attributes.System.Make.Name);
                string model = Global.UpperFirst(AiDetectionObject.VehicleAnnotation.Attributes.System.Model.Name);

                string plate = "Unknown";
                if (AiDetectionObject.VehicleAnnotation.Licenseplate != null && !string.IsNullOrEmpty(AiDetectionObject.VehicleAnnotation.Licenseplate.Attributes.System.String.Name))
                {
                    plate = $"{AiDetectionObject.VehicleAnnotation.Licenseplate.Attributes.System.Region.Name} {AiDetectionObject.VehicleAnnotation.Licenseplate.Attributes.System.String.Name}";
                }

                this.Label += $"{type} [{color}, {make}, {model}, Plate={plate}]";

                //this isnt exactly right, but sighthound doesnt give confidence for person/face, only gender, age
                this.Confidence = AiDetectionObject.VehicleAnnotation.RecognitionConfidence * 100;
            }
            else
            {
                this.Confidence = 100;
            }

            this.Filename = curImg.image_path;

            this.GetObjectType();
        }
        public ClsPrediction(ObjectType defaultObjType, Camera cam, SightHoundPersonObject AiDetectionObject, ClsImageQueueItem curImg, ClsURLItem curURL, SightHoundImage SHImg)
        {
            this._defaultObjType = defaultObjType;
            this._cam = cam;
            this._cururl = curURL;
            this._curimg = curImg;
            this.Camera = cam.Name;
            this.BICamName = cam.BICamName;
            this.Server = curURL.CurSrv;
            this.Time = DateTime.Now;
            this.Filename = curImg.image_path;


            if (AiDetectionObject == null || cam == null || string.IsNullOrWhiteSpace(AiDetectionObject.Type))
            {
                Log("Error: Prediction or Camera was null?", "", this._cam.Name);
                this.Result = ResultType.Error;
                return;
            }

                this.ImageHeight = SHImg.Height; //curImg.Height;
                this.ImageWidth = SHImg.Width; //curImg.Width;

            if (curImg.Height != SHImg.Height)
            {
                Log($"Debug: Original image Height does not match returned height: Original={curImg.Height}, Returned={SHImg.Height}");
            }
            if (curImg.Width != SHImg.Width)
            {
                Log($"Debug: Original image Width does not match returned Width: Original={curImg.Width}, Returned={SHImg.Width}");
            }

            //{
            //    "image": {
            //        "width": 1280, 
            //        "height": 960, 
            //        "orientation": 1
            //    },

            //    "objects": [

            //        {
            //            "type": "person",
            //            "boundingBox": { 
            //                "x": 363, 
            //                "y": 182, 
            //                "height": 778, 
            //                "width": 723
            //            } 
            //        },

            //        {
            //            "type": "face",
            //            "boundingBox": {
            //                "x": 508, 
            //                "y": 305, 
            //                "height": 406, 
            //                "width": 406
            //            },
            //            "attributes": {
            //                "gender": "male", 
            //                "genderConfidence": 0.9883, 
            //                "age":25,
            //                "ageConfidence": 0.2599,
            //                "emotion": "happiness",
            //                "emotionConfidence": 0.9943,
            //                "emotionsAll": {
            //                    "neutral": 0.0018,
            //                    "sadness": 0.0009,
            //                    "disgust": 0.0002,
            //                    "anger": 0.0003,
            //                    "surprise": 0,
            //                    "fear": 0.0022,
            //                    "happiness": 0.9943
            //                },
            //                "pose":{
            //                    "pitch":-18.4849,
            //                    "roll":0.854,
            //                    "yaw":-4.2123
            //                },
            //                "frontal": true
            //            },
            //            "landmarks": {
            //                "faceContour": [[515,447],[517,491]...[872,436]],
            //                "noseBridge": [[710,419],[711,441]...[712,487]],
            //                "noseBall": [[680,519],[696,522]...[742,518]],
            //                "eyebrowRight": [[736,387],[768,376]...[854,394]],
            //                "eyebrowLeft": [[555,413],[578,391]...[679,391]],
            //                "eyeRight": [[753,428],[774,414]...[777,432]],
            //                "eyeRightCenter": [[786,423]],
            //                "eyeLeft": [[597,435],[617,423]...[619,442]],
            //                "eyeLeftCenter": [[630,432]],
            //                "mouthOuter": [[650,590],[674,572]...[675,600]],
            //                "mouthInner": [[661,587],[697,580]...[697,584]]
            //            }
            //        }
            //    ]
            //}


            //Rectangle(xmin, ymin, xmax - xmin, ymax - ymin)
            //          x,    y     Width,       Height


            //boundingBox (object): An object containing x, y, width, and height values 
            //defining the location of the object in the image. The top left pixel of 
            //the image represents coordinate (0,0).

            this.RectHeight = AiDetectionObject.BoundingBox.Height;
            this.RectWidth = AiDetectionObject.BoundingBox.Width;

            double right = AiDetectionObject.BoundingBox.X + this.RectWidth;
            double left = AiDetectionObject.BoundingBox.X;

            double top = AiDetectionObject.BoundingBox.Y;
            double bottom = AiDetectionObject.BoundingBox.Y + this.RectHeight;

            this.XMin = Convert.ToInt32(Math.Round(left));
            this.YMin = Convert.ToInt32(Math.Round(top));

            this.XMax = Convert.ToInt32(Math.Round(right));
            this.YMax = Convert.ToInt32(Math.Round(bottom));

            //force first letter to always be capitalized 
            this.Label = Global.UpperFirst(AiDetectionObject.Type);

            if (AiDetectionObject.Attributes != null && !string.IsNullOrEmpty(AiDetectionObject.Attributes.Gender))
            {
                string emotions = Global.UpperFirst(AiDetectionObject.Attributes.Emotion);  //this is the highest confidence emotion 
                
                if (AiDetectionObject.Attributes.EmotionsAll != null)
                {
                    List<SightHoundEmotionItem> emotionslist = new List<SightHoundEmotionItem>();
                    if (AiDetectionObject.Attributes.EmotionsAll.Anger > .1)
                        emotionslist.Add(new SightHoundEmotionItem("Anger", AiDetectionObject.Attributes.EmotionsAll.Anger));
                    if (AiDetectionObject.Attributes.EmotionsAll.Disgust > .1)
                        emotionslist.Add(new SightHoundEmotionItem("Disgust", AiDetectionObject.Attributes.EmotionsAll.Disgust));
                    if (AiDetectionObject.Attributes.EmotionsAll.Fear > .1)
                        emotionslist.Add(new SightHoundEmotionItem("Fear", AiDetectionObject.Attributes.EmotionsAll.Fear));
                    if (AiDetectionObject.Attributes.EmotionsAll.Happiness > .1)
                        emotionslist.Add(new SightHoundEmotionItem("Happiness", AiDetectionObject.Attributes.EmotionsAll.Happiness));
                    if (AiDetectionObject.Attributes.EmotionsAll.Neutral > .1)
                        emotionslist.Add(new SightHoundEmotionItem("Neutral", AiDetectionObject.Attributes.EmotionsAll.Neutral));
                    if (AiDetectionObject.Attributes.EmotionsAll.Sadness > .1)
                        emotionslist.Add(new SightHoundEmotionItem("Sadness", AiDetectionObject.Attributes.EmotionsAll.Sadness));
                    if (AiDetectionObject.Attributes.EmotionsAll.Surprise > .1)
                        emotionslist.Add(new SightHoundEmotionItem("Surprise", AiDetectionObject.Attributes.EmotionsAll.Surprise));

                    //sort so highest conf is first
                    emotionslist = emotionslist.OrderByDescending(a => a.Confidence).ToList();
                    emotions = "";
                    int cnt = 0;
                    foreach (SightHoundEmotionItem em in emotionslist)
                    {
                        emotions += $"{em.ToString()};";
                        cnt++;
                        if (cnt > 3)
                            break;
                    }
                    emotions = emotions.Trim(" ;".ToCharArray());
                    if (string.IsNullOrEmpty(emotions))
                        emotions = "UnknownEmotions";
                }

                this.Label += $" [{Global.UpperFirst(AiDetectionObject.Attributes.Gender)}, {AiDetectionObject.Attributes.Age}, {emotions}]";

                //this isnt exactly right, but sighthound doesnt give confidence for person/face, only gender, age
                this.Confidence = AiDetectionObject.Attributes.GenderConfidence * 100;
            }
            else
            {
                this.Confidence = 100;
            }

            this.Filename = curImg.image_path;

            this.GetObjectType();
        }
        public ClsPrediction(ObjectType defaultObjType, Camera cam, ClsDeepstackDetection AiDetectionObject, ClsImageQueueItem curImg, ClsURLItem curURL)
        {
            this._defaultObjType = defaultObjType;
            this._cam = cam;
            this._curimg = curImg;
            this._cururl = curURL;
            this.Server = curURL.CurSrv;
            this.Time = DateTime.Now;


            //this._imageObject = AiDetectionObject;
            this.Camera = cam.Name;
            this.BICamName = cam.BICamName;
            this.ImageHeight = curImg.Height;
            this.ImageWidth = curImg.Width;
            this.Filename = curImg.image_path;

            if (AiDetectionObject == null || cam == null || string.IsNullOrWhiteSpace(AiDetectionObject.label))
            {
                Log("Error: Prediction or Camera was null?", "", this._cam.Name);
                this.Result = ResultType.Error;
                return;
            }

            //force first letter to always be capitalized 
            this.Label = Global.UpperFirst(AiDetectionObject.label);
            this.XMax = AiDetectionObject.x_max;
            this.YMax = AiDetectionObject.y_max;
            this.XMin = AiDetectionObject.x_min;
            this.YMin = AiDetectionObject.y_min;
            this.Confidence = AiDetectionObject.confidence * 100;  //store as whole number percent 
            this.Filename = curImg.image_path;
            this.RectHeight = this.XMax - this.YMin;
            this.RectWidth = this.XMax - this.XMin;

            this.GetObjectType();
        }

        public ClsPrediction(ObjectType defaultObjType, Camera cam, ClsDoodsDetection AiDetectionObject, ClsImageQueueItem curImg, ClsURLItem curURL)
        {
            this._defaultObjType = defaultObjType;
            this._cam = cam;
            this._curimg = curImg;
            this._cururl = curURL;

            //this._imageObject = AiDetectionObject;
            this.Camera = cam.Name;
            this.BICamName = cam.BICamName;
            this.ImageHeight = curImg.Height;
            this.ImageWidth = curImg.Width;
            this.Server = curURL.CurSrv;
            this.Time = DateTime.Now;
            this.Filename = curImg.image_path;


            if (AiDetectionObject == null || cam == null || string.IsNullOrWhiteSpace(AiDetectionObject.Label))
            {
                Log("Error: Prediction or Camera was null?", "", this._cam.Name);
                this.Result = ResultType.Error;
                return;
            }

            //force first letter to always be capitalized 
            this.Label = Global.UpperFirst(AiDetectionObject.Label);

            //{
            //         "top":0.09833781,
            //         "left":0.62415826,
            //         "bottom":0.14295554,
            //         "right":0.7029755,
            //         "label":"car",
            //         "confidence":63.28125
            //      }

            // convert percentage values from doods to pixels

            //System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
            //System.Drawing.Rectangle rect = new System.Drawing.Rectangle(x,    y,    width,       height);

            //x,    y    = - coordinate of the UPPER LEFT corner of the rectangle
            //xmin, ymin

            double right = curImg.Width * AiDetectionObject.Right;
            double left = curImg.Width * AiDetectionObject.Left;

            double top = curImg.Height * AiDetectionObject.Top;
            double bottom = curImg.Height * AiDetectionObject.Bottom;

            this.XMin = Convert.ToInt32(Math.Round(left));
            this.YMin = Convert.ToInt32(Math.Round(top));

            this.XMax = Convert.ToInt32(Math.Round(right));
            this.YMax = Convert.ToInt32(Math.Round(bottom));

            this.RectHeight = this.XMax - this.YMin;
            this.RectWidth = this.XMax - this.XMin;

            this.Confidence = Convert.ToSingle(AiDetectionObject.Confidence);
            this.Filename = curImg.image_path;

            this.GetObjectType();
        }

        public void AnalyzePrediction()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            MaskResultInfo result = new MaskResultInfo();

            try
            {
                if (this._cam.enabled)
                {
                    if (!string.IsNullOrWhiteSpace(this._cam.triggering_objects_as_string))
                    {
                        if (this._cam.IsRelevant(this.Label))
                        {
                            // -> OBJECT IS RELEVANT

                            //if confidence limits are satisfied
                            bool OverrideThreshold = this._cururl !=null && (this._cururl.Threshold_Lower > 0 || (this._cururl.Threshold_Upper > 0 && this._cururl.Threshold_Upper < 100));

                            if ((!OverrideThreshold && this.Confidence >= this._cam.threshold_lower && this.Confidence <= this._cam.threshold_upper) ||
                                (OverrideThreshold && this.Confidence >= this._cururl.Threshold_Lower && this.Confidence <= this._cururl.Threshold_Upper))
                            {
                                // -> OBJECT IS WITHIN CONFIDENCE LIMITS

                                //only if the object is outside of the masked area
                                result = AITOOL.Outsidemask(this._cam, this.XMin, this.XMax, this.YMin, this.YMax, this._curimg.Width, this._curimg.Height);
                                this.ImgMaskResult = result.Result;
                                this.ImgMaskType = result.MaskType;
                                this.ImagePointsOutsideMask = result.ImagePointsOutsideMask;

                                if (!result.IsMasked)
                                {
                                    this.Result = ResultType.Relevant;
                                }
                                else if (result.MaskType == MaskType.None) //if the object is in a masked area
                                {
                                    this.Result = ResultType.NoMask;
                                }
                                else if (result.MaskType == MaskType.Image) //if the object is in a masked area
                                {
                                    this.Result = ResultType.ImageMasked;
                                }

                                //check the mask manager if the image mask didnt flag anything
                                if (!result.IsMasked)
                                {
                                    //check the dynamic or static masks
                                    if (this._cam.maskManager.MaskingEnabled)
                                    {
                                        ObjectPosition currentObject = new ObjectPosition(this.XMin, this.XMax, this.YMin, this.YMax, this.Label,
                                                                                          this._curimg.Width, this._curimg.Height, this._cam.Name, this._curimg.image_path);
                                        //creates history and masked lists for objects returned
                                        result = this._cam.maskManager.CreateDynamicMask(currentObject);
                                        this.DynMaskResult = result.Result;
                                        this.DynMaskType = result.MaskType;
                                        this.DynamicThresholdCount = result.DynamicThresholdCount;
                                        this.PercentMatch = result.PercentMatch;

                                        //there is a dynamic or static mask
                                        if (result.MaskType == MaskType.Dynamic)
                                            this.Result = ResultType.DynamicMasked;
                                        else if (result.MaskType == MaskType.Static)
                                            this.Result = ResultType.StaticMasked;
                                    }

                                }

                                if (result.Result == MaskResult.Error || result.Result == MaskResult.Unknown)
                                {
                                    this.Result = ResultType.Error;
                                    Log($"Error: Masking error? '{this._cam.Name}' ('{this.Label}') - DynMaskResult={this.DynMaskResult}, ImgMaskResult={this.ImgMaskResult}", "", this._cam.Name, this._curimg.image_path);
                                }

                            }
                            else //if confidence limits are not satisfied
                            {
                                this.Result = ResultType.NoConfidence;
                            }

                        }
                        else
                        {
                            this.Result = ResultType.UnwantedObject;
                        }

                    }
                    else
                    {
                        Log($"Error: Camera does not have any objects enabled '{this._cam.Name}' ('{this.Label}')", "", this._cam.Name, this._curimg.image_path);
                        this.Result = ResultType.Error;
                    }

                }
                else
                {
                    Log($"Debug: Camera not enabled '{this._cam.Name}' ('{this.Label}')", "", this._cam.Name, this._curimg.image_path);
                    this.Result = ResultType.CameraNotEnabled;
                }

            }
            catch (Exception ex)
            {
                Log($"Error: Label '{this.Label}', Camera '{this._cam.Name}': {Global.ExMsg(ex)}", "", this._cam.Name, this._curimg.image_path);
                this.Result = ResultType.Error;
            }

        }

        public override string ToString()
        {
            return $"{this.Label} {String.Format(AppSettings.Settings.DisplayPercentageFormat, this.Confidence)}";
        }
        public string PositionString()
        {
            return $"{this.XMin},{this.YMin},{this.XMax},{this.YMax}";
        }
        public string ConfidenceString()
        {
            return $"{String.Format(AppSettings.Settings.DisplayPercentageFormat, this.Confidence)}";
        }

        private void GetObjectType()
        {
            string tmp = this.Label.Trim().ToLower();

            if (tmp.Contains("["))
                tmp = Global.GetWordBetween(tmp, "", "[").Trim();

            List<string> pris = Global.Split(AppSettings.Settings.ObjectPriority, ",", ToLower: true);
            this.ObjectPriority = pris.IndexOf(tmp);
            if (this.ObjectPriority == -1)
                this.ObjectPriority = 999;

            //person,   bicycle,   car,   motorcycle,   airplane,
            //bus,   train,   truck,   boat,   traffic light,   fire hydrant,   stop_sign,
            //parking meter,   bench,   bird,   cat,   dog,   horse,   sheep,   cow,   elephant,
            //bear,   zebra, giraffe,   backpack,   umbrella,   handbag,   tie,   suitcase,
            //frisbee,   skis,   snowboard, sports ball,   kite,   baseball bat,   baseball glove,
            //skateboard,   surfboard,   tennis racket, bottle,   wine glass,   cup,   fork,
            //knife,   spoon,   bowl,   banana,   apple,   sandwich,   orange, broccoli,   carrot,
            //hot dog,   pizza,   donot,   cake,   chair,   couch,   potted plant,   bed, dining table,
            //toilet,   tv,   laptop,   mouse,   remote,   keyboard,   cell phone,   microwave,
            //oven,   toaster,   sink,   refrigerator,   book,   clock,   vase,   scissors,   teddy bear,
            //hair dryer, toothbrush.

            if (tmp.Equals("person") || tmp.Equals("face"))
                this.ObjType = ObjectType.Person;
            else if (tmp.Equals("dog") ||
                     tmp.Equals("cat") ||
                     tmp.Equals("bird") ||
                     tmp.Equals("bear") ||
                     tmp.Equals("sheep") ||
                     tmp.Equals("cow") ||
                     tmp.Equals("horse") ||
                     tmp.Equals("elephant") ||
                     tmp.Equals("zebra") ||
                     tmp.Equals("giraffe"))
                this.ObjType = ObjectType.Animal;
            else if (tmp.Equals("car") ||
                     tmp.Equals("truck") ||
                     tmp.Equals("bus") ||
                     tmp.Equals("van") ||
                     tmp.Equals("suv") ||
                     tmp.Equals("bicycle") ||
                     tmp.Equals("motorcycle") ||
                     tmp.Equals("horse") ||
                     tmp.Equals("boat") ||
                     tmp.Equals("train") ||
                     tmp.Equals("airplane"))
                this.ObjType = ObjectType.Vehicle;
            else
                this.ObjType = this._defaultObjType;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClsPrediction);
        }

        public bool Equals(ClsPrediction other)
        {
            if (other == null)
                return false;

            string tmp1 = this.Label.Trim().ToLower();
            string tmp2 = other.Label.Trim().ToLower();

            if (tmp1.Contains("["))
                tmp1 = Global.GetWordBetween(tmp1, "", "[").Trim();

            if (tmp2.Contains("["))
                tmp2 = Global.GetWordBetween(tmp2, "", "[").Trim();

            return tmp1 == tmp2;
        }

        public static bool operator ==(ClsPrediction left, ClsPrediction right)
        {
            return EqualityComparer<ClsPrediction>.Default.Equals(left, right);
        }

        public static bool operator !=(ClsPrediction left, ClsPrediction right)
        {
            return !(left == right);
        }
    }
}
