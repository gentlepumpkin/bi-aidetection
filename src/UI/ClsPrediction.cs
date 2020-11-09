using System;
using System.Collections.Generic;
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
    public class ClsPrediction
    {
        private ObjectType _defaultObjType;
        private Camera _cam;
        private ClsImageQueueItem _curimg;

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
        public int ImageWidth { get; set; } = 0;
        public int ImageHeight { get; set; } = 0;
        public int ObjectPriority { get; set; } = 0;
        public string Filename { get; set; } = "";
        //private ClsDeepstackObject _imageObject;

        public ClsPrediction() { }

        public ClsPrediction(ObjectType defaultObjType, Camera cam, ClsDeepstackDetection AiDetectionObject, ClsImageQueueItem curImg)
        {
            this._defaultObjType = defaultObjType;
            this._cam = cam;
            this._curimg = curImg;
            //this._imageObject = AiDetectionObject;
            this.Camera = cam.name;
            this.ImageHeight = curImg.Height;
            this.ImageWidth = curImg.Width;

            if (AiDetectionObject == null || cam == null || string.IsNullOrWhiteSpace(AiDetectionObject.label))
            {
                Log("Error: Prediction or Camera was null?", "", this._cam.name);
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

            this.GetObjectType();
        }

        public ClsPrediction(ObjectType defaultObjType, Camera cam, ClsDoodsDetection AiDetectionObject, ClsImageQueueItem curImg)
        {
            this._defaultObjType = defaultObjType;
            this._cam = cam;
            this._curimg = curImg;
            //this._imageObject = AiDetectionObject;
            this.Camera = cam.name;
            this.ImageHeight = curImg.Height;
            this.ImageWidth = curImg.Width;

            if (AiDetectionObject == null || cam == null || string.IsNullOrWhiteSpace(AiDetectionObject.Label))
            {
                Log("Error: Prediction or Camera was null?", "", this._cam.name);
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

            this.XMin = Convert.ToInt32(left);
            this.YMin = Convert.ToInt32(top);

            this.XMax = Convert.ToInt32(right);
            this.YMax = Convert.ToInt32(bottom);

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
                        if (this._cam.triggering_objects_as_string.IndexOf(this.Label, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // -> OBJECT IS RELEVANT

                            //if confidence limits are satisfied
                            if (this.Confidence >= this._cam.threshold_lower && this.Confidence <= this._cam.threshold_upper)
                            {
                                // -> OBJECT IS WITHIN CONFIDENCE LIMITS

                                //only if the object is outside of the masked area
                                result = AITOOL.Outsidemask(this._cam.name, this.XMin, this.XMax, this.YMin, this.YMax, this._curimg.Width, this._curimg.Height);
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
                                                                                          this._curimg.Width, this._curimg.Height, this._cam.name, this._curimg.image_path);
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
                                    Log($"Error: Masking error? '{this._cam.name}' ('{this.Label}') - DynMaskResult={this.DynMaskResult}, ImgMaskResult={this.ImgMaskResult}", "", this._cam.name, this._curimg.image_path);
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
                        Log($"Error: Camera does not have any objects enabled '{this._cam.name}' ('{this.Label}')", "", this._cam.name, this._curimg.image_path);
                        this.Result = ResultType.Error;
                    }

                }
                else
                {
                    Log($"Debug: Camera not enabled '{this._cam.name}' ('{this.Label}')", "", this._cam.name, this._curimg.image_path);
                    this.Result = ResultType.CameraNotEnabled;
                }

            }
            catch (Exception ex)
            {
                Log($"Error: Label '{this.Label}', Camera '{this._cam.name}': {Global.ExMsg(ex)}", "", this._cam.name, this._curimg.image_path);
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

            if (tmp.Equals("person"))
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
    }
}
