using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using static AITool.AITOOL;

namespace AITool
{
    public class ObjectPosition:IEquatable<ObjectPosition>
    {
        public string Label { get; } = "";
        public DateTime CreateDate { get; set; } = DateTime.MinValue;
        public DateTime LastSeenDate { get; set; } = DateTime.MinValue;

        public int Counter { get; set; }
        public double PercentMatch { get; set; }
        public Boolean IsStatic { get; set; } = false;
        public int Xmin { get; }
        public int Ymin { get; }
        public int Xmax { get; }
        public int Ymax { get; }

        public long Key { get; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        private Rectangle ObjRectangle;

        public string CameraName { get; set; } = "";
        public string ImagePath { get; set; } = "";

        //scaling of object based on size
        public int ScalePercent { get; set; } 
        public double ObjectImagePercent { get; }

        private ObjectScale _scaleConfig;
        public ObjectScale ScaleConfig
        {
            get => _scaleConfig;
            set
            {
                _scaleConfig = value;
                ScalePercent = getImagePercentVariance();
            }
        }


        public ObjectPosition(int xmin, int xmax, int ymin, int ymax, string label, int imageWidth, int imageHeight, string cameraName, string imagePath)
        {
            this.CreateDate = DateTime.Now;
            this.LastSeenDate = DateTime.Now;
            this.CameraName = cameraName;
            this.ImagePath = imagePath;
            this.Label = label;
            this.Xmin = xmin;
            this.Ymin = ymin;
            this.Xmax = xmax;
            this.Ymax = ymax;

            ObjRectangle = Rectangle.FromLTRB(Xmin, Ymin, Xmax, Ymax);

            this.ImageHeight = imageHeight;
            this.ImageWidth = imageWidth;

            //object percent of image area
            ObjectImagePercent = (ObjRectangle.Width * ObjRectangle.Height) / (float)(imageWidth * imageHeight) * 100;

            //starting x * y point + width * height of rectangle - used for debugging only
            Key = ((Xmin + 1) * (Ymin + 1) + (ObjRectangle.Width * ObjRectangle.Height));
        }

        /*Increases object variance percentage for smaller objects. 
        Due to thier size, smaller objects are more sensitive to slight changes in postion. 
        This settings allows for a custom percentage match value based on the object size.*/
        private int getImagePercentVariance()
        {
            int scalePercent = 0;

            if (ScaleConfig != null)
            {
                if (ScaleConfig.IsScaledObject)
                {
                    if (ObjectImagePercent < ScaleConfig.SmallObjectMaxPercent)
                    {
                        scalePercent = ScaleConfig.SmallObjectMatchPercent;
                    }
                    else if (ObjectImagePercent >= ScaleConfig.MediumObjectMinPercent 
                            &&  ObjectImagePercent <= ScaleConfig.MediumObjectMaxPercent)
                    {
                        scalePercent = ScaleConfig.MediumObjectMatchPercent;
                    }
                }
            }
            return scalePercent;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ObjectPosition);
        }

        public bool Equals(ObjectPosition other)
        {
            if (other == null)
                return false;
            
            float percentageIntersect = AITOOL.getObjIntersectPercent(this.ObjRectangle, other.ObjRectangle);

            if (percentageIntersect > 0)
                Log($"Debug: Percentage Intersection of object: {percentageIntersect}% Current '{this.Label}' key={this.Key}, Tested '{other.Label}' key={other.Key}","",other.CameraName);

            double percentMatch = ScalePercent == 0 ? PercentMatch : ScalePercent;

            return percentageIntersect >= percentMatch;
        }

        public override int GetHashCode()
        {
            int hashCode = -853659638;
            hashCode = hashCode * -1521134295 + Xmin.GetHashCode();
            hashCode = hashCode * -1521134295 + Ymin.GetHashCode();
            hashCode = hashCode * -1521134295 + Xmax.GetHashCode();
            hashCode = hashCode * -1521134295 + Ymax.GetHashCode();

            return hashCode;
        }

        public static bool operator ==(ObjectPosition left, ObjectPosition right)
        {
            return EqualityComparer<ObjectPosition>.Default.Equals(left, right);
        }

        public static bool operator !=(ObjectPosition left, ObjectPosition right)
        {
            return !(left == right);
        }

        public long getKey()
        {
            return Key;
        }

        public override string ToString()
        {
            string value = $"key={getKey()}, name={Label}, xmin={Xmin}, ymin={Ymin}, xmax={Xmax}, ymax={Ymax}, IsStatic={this.IsStatic}, counter={Counter}, camera={CameraName}, create date: {CreateDate}, imageName: {Path.GetFileName(ImagePath)}";

            return value;
        }
    }
}
