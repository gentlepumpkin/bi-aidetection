using BrightIdeasSoftware;
using System;
using System.Collections.Generic;

namespace AITool
{
    public class ObjectPosition:IEquatable<ObjectPosition>
    {
        public string label { get; }
        public DateTime createDate { get; set; }
        public int counter { get; set; }
        public int xmin { get; }
        public int ymin { get; }
        public int xmax { get; }
        public int ymax { get; }
        public int height { get; }
        public int width { get; }
        public double thresholdPercent { get; set; }
        public Boolean isStatic { get; set; } = false;
        public long key { get; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }
        public Boolean isVisible { get; set; } = false;

        public Camera camera { get; set; }


        public ObjectPosition(int xmin, int ymin, int xmax, int ymax, string label, int imageWidth, int imageHeight, Camera camera)
        {
            createDate = DateTime.Now;
            this.camera = camera;
            this.label = label;

            this.xmin = xmin;
            this.ymin = ymin;
            this.xmax = xmax;
            this.ymax = ymax;
            this.imageHeight = imageHeight;
            this.imageWidth = imageWidth;

            //calc width and height of box
            this.width = xmax - xmin;
            this.height = ymax - ymin;

            //starting x * y point + width * height of rectangle - used for debugging only
            key = ((xmin + 1) * (ymin + 1) + (width * height));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ObjectPosition);
        }

        public bool Equals(ObjectPosition other)
        {
            //calculate the percentage variance for width and height of selected object
            double widthVariance = (double)Math.Abs(this.width - other.width) / this.width;
            double heightVariance = (double)Math.Abs(this.height - other.height) / this.height;

            //calculate percentage change in starting corner of the x,y axis (upper-left corner of the rectangle)
            double xPercentVariance = (double)(Math.Abs(this.xmin - other.xmin)) / imageWidth;
            double yPercentVariance = (double)(Math.Abs(this.ymin - other.ymin)) / imageHeight;

            return (other != null &&
                   (xPercentVariance <= thresholdPercent) &&
                   (yPercentVariance <= thresholdPercent) &&
                   (widthVariance <= thresholdPercent) &&
                   (heightVariance <= thresholdPercent));
        }

        public override int GetHashCode()
        {
            int hashCode = -853659638;
            hashCode = hashCode * -1521134295 + xmin.GetHashCode();
            hashCode = hashCode * -1521134295 + ymin.GetHashCode();
            hashCode = hashCode * -1521134295 + xmax.GetHashCode();
            hashCode = hashCode * -1521134295 + ymax.GetHashCode();
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
            return key;
        }

        public override string ToString()
        {
            string value = "key=" + getKey() + ", name=" + label + ", xmin=" + xmin + ", ymin=" + ymin + ", xmax=" + xmax + ", ymax=" + ymax + ", counter=" + counter + ", create date: " + createDate;

            return value;
        }
    }
}
