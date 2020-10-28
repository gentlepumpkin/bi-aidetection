using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using static AITool.AITOOL;

namespace AITool
{
    public class ObjectPosition : IEquatable<ObjectPosition>
    {
        public string Label { get; } = "";
        public DateTime CreateDate { get; set; } = DateTime.MinValue;
        public DateTime LastSeenDate { get; set; } = DateTime.MinValue;
        public int Counter { get; set; }
        public double PercentMatch { get; set; }
        public float LastPercentMatch { get; set; }
        public Boolean IsStatic { get; set; } = false;
        public int Xmin { get; }
        public int Ymin { get; }
        public int Xmax { get; }
        public int Ymax { get; }
        public long Key { get; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        private Rectangle _objRectangle;
        public string CameraName { get; set; } = "";
        public string ImagePath { get; set; } = "";

        //scaling of object based on size
        public int ScalePercent { get; set; }
        public double ObjectImagePercent { get; }

        private ObjectScale _scaleConfig;
        public ObjectScale ScaleConfig
        {
            get => this._scaleConfig;
            set
            {
                this._scaleConfig = value;
                this.ScalePercent = this.getImagePercentVariance();
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

            this._objRectangle = Rectangle.FromLTRB(this.Xmin, this.Ymin, this.Xmax, this.Ymax);

            this.ImageHeight = imageHeight;
            this.ImageWidth = imageWidth;

            //object percent of image area
            this.ObjectImagePercent = (this._objRectangle.Width * this._objRectangle.Height) / (float)(imageWidth * imageHeight) * 100;

            //starting x * y point + width * height of rectangle - used for debugging only
            this.Key = ((this.Xmin + 1) * (this.Ymin + 1) + (this._objRectangle.Width * this._objRectangle.Height));
        }

        /*Increases object variance percentage for smaller objects. 
        Due to thier size, smaller objects are more sensitive to slight changes in postion. 
        This settings allows for a custom percentage match value based on the object size.*/
        private int getImagePercentVariance()
        {
            int scalePercent = 0;

            if (this.ScaleConfig != null)
            {
                if (this.ScaleConfig.IsScaledObject)
                {
                    if (this.ObjectImagePercent < this.ScaleConfig.SmallObjectMaxPercent)
                    {
                        scalePercent = this.ScaleConfig.SmallObjectMatchPercent;
                    }
                    else if (this.ObjectImagePercent >= this.ScaleConfig.MediumObjectMinPercent
                            && this.ObjectImagePercent <= this.ScaleConfig.MediumObjectMaxPercent)
                    {
                        scalePercent = this.ScaleConfig.MediumObjectMatchPercent;
                    }
                }
            }
            return scalePercent;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ObjectPosition);
        }

        public bool Equals(ObjectPosition other)
        {
            if (other == null)
                return false;

            float percentageIntersect = AITOOL.GetObjIntersectPercent(this._objRectangle, other._objRectangle);

            if (percentageIntersect > other.LastPercentMatch)
            {
                this.LastPercentMatch = percentageIntersect;   //parent object highest match for this detection cycle
                other.LastPercentMatch = percentageIntersect;  //current object highest match
                Log($"Debug: Percentage Intersection of object: {percentageIntersect}% Current '{this.Label}' key={this.Key}, Tested '{other.Label}' key={other.Key}", "", other.CameraName, other.ImagePath);
            }

            double percentMatch = this.ScalePercent == 0 ? this.PercentMatch : this.ScalePercent;

            return percentageIntersect >= percentMatch;
        }

        public override int GetHashCode()
        {
            int hashCode = -853659638;
            hashCode = hashCode * -1521134295 + this.Xmin.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Ymin.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Xmax.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Ymax.GetHashCode();

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
            return this.Key;
        }

        public override string ToString()
        {
            string value = $"key={this.getKey()}, name={this.Label}, xmin={this.Xmin}, ymin={this.Ymin}, xmax={this.Xmax}, ymax={this.Ymax}, IsStatic={this.IsStatic}, counter={this.Counter}, camera={this.CameraName}, create date: {this.CreateDate}, imageName: {Path.GetFileName(this.ImagePath)}";

            return value;
        }
    }
}
