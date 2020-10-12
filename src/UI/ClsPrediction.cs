using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Telegram.Bot.Requests;

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
        Relevant,
        UnwantedObject,
        NoConfidence,
        DynamicMasked,
        StaticMasked,
        ImageMasked,
        NoMask,
        CameraNotEnabled,
        Error,
        Unknown
    }
    public class ClsPrediction
    {
        private ObjectType _DefaultObjType;
        private Camera _cam;
        private ClsImageQueueItem _curimg;

        public string Label { get; set; } = "";
        public ResultType Result { get; set; } = ResultType.Unknown;
        public ObjectType ObjType { get; set; } = ObjectType.Unknown;
        public MaskType DynMaskType { get; set; } = MaskType.Unknown;
        public MaskResult DynMaskResult { get; set; } = MaskResult.Unknown;
        public MaskType ImgMaskType { get; set; } = MaskType.Unknown;
        public MaskResult ImgMaskResult { get; set; } = MaskResult.Unknown;
        public int Dynamic_Threshold_Count { get; set; } = 0;
        public int Image_PointsOutsideMask { get; set; } = 0;

        public float Confidence { get; set; } = 0;
        public int ymin { get; set; } = 0;
        public int xmin { get; set; } = 0;
        public int ymax { get; set; } = 0;
        public int xmax { get; set; } = 0;
        public string Filename { get; set; } = "";

        public ClsPrediction()
        { 

        }

        public ClsPrediction(ObjectType DefaultObjType, Camera cam, Object user, ClsImageQueueItem CurImg) 
        {
            this._DefaultObjType = DefaultObjType;
            this._cam = cam;
            this._curimg = CurImg;

            if (user == null || cam == null || string.IsNullOrWhiteSpace(user.label))
            {
                Global.Log("Error: Prediction or Camera was null?");
                this.Result = ResultType.Error;
                return;
            }

            //force first letter to always be capitalized 
            this.Label = Global.UpperFirst(user.label);
            this.xmax = user.x_max;
            this.ymax = user.y_max;
            this.xmin = user.x_min;
            this.ymin = user.y_min;
            this.Confidence = user.confidence * 100;  //store as whole number percent 
            this.Filename = CurImg.image_path;

            this.GetObjectType();
            this.AnalyzePrediction();

        }

        

        public void AnalyzePrediction()
        {
            MaskResultInfo result = new MaskResultInfo();

            try
            {
                if (this._cam.enabled)
                {
                    if (!string.IsNullOrWhiteSpace(this._cam.triggering_objects_as_string))
                    {
                        if (this._cam.triggering_objects_as_string.ToLower().Contains(this.Label.ToLower()))
                        {
                            // -> OBJECT IS RELEVANT

                            //if confidence limits are satisfied
                            if (this.Confidence >= this._cam.threshold_lower && this.Confidence  <= this._cam.threshold_upper)
                            {
                                // -> OBJECT IS WITHIN CONFIDENCE LIMITS

                                //only if the object is outside of the masked area
                                result = AITOOL.Outsidemask(this._cam.name, this.xmin, this.xmax, this.ymin, this.ymax, this._curimg.Width, this._curimg.Height);
                                this.ImgMaskResult = result.Result;
                                this.ImgMaskType = result.MaskType;
                                this.Image_PointsOutsideMask = result.Image_PointsOutsideMask;

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
                                    if (this._cam.maskManager.masking_enabled)
                                    {
                                        ObjectPosition currentObject = new ObjectPosition(this.xmin, this.ymin, this.xmax, this.ymax, this.Label,
                                                                                          this._curimg.Width, this._curimg.Height, this._cam.name, this._curimg.image_path);
                                        //creates history and masked lists for objects returned
                                        result = this._cam.maskManager.CreateDynamicMask(currentObject);
                                        this.DynMaskResult = result.Result;
                                        this.DynMaskType = result.MaskType;
                                        this.Dynamic_Threshold_Count = result.Dynamic_Threshold_Count;
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
                                    Global.Log($"Error: Masking error? '{this._cam.name}' ('{this.Label}') - DynMaskResult={this.DynMaskResult}, ImgMaskResult={this.ImgMaskResult}");
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
                        Global.Log($"Error: Camera does not have any objects enabled '{this._cam.name}' ('{this.Label}')");
                        this.Result = ResultType.Error;
                    }

                }
                else
                {
                    Global.Log($"Info: Camera not enabled '{this._cam.name}' ('{this.Label}')");
                    this.Result = ResultType.CameraNotEnabled;
                }

            }
            catch (Exception ex)
            {
                Global.Log($"Error: Label '{this.Label}', Camera '{this._cam.name}': {Global.ExMsg(ex)}");
                this.Result = ResultType.Error;
            }

            

        }

        public override string ToString()
        {
            return $"{this.Label} {String.Format(AppSettings.Settings.DisplayPercentageFormat, this.Confidence)}";
        }
        private void GetObjectType()
        {
            string tmp = this.Label.Trim().ToLower();

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
                this.ObjType = this._DefaultObjType;

        }
    }
}
