using SixLabors.ImageSharp.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AITool
{
    public class ObjectPosition : IEquatable<ObjectPosition>
    {
        public DateTime createDate { get; }
        public Boolean isVisible { get; set; } = false;
        public int counter { get; set; }
        public int xmin { get; }
        public int ymin { get; }
        public int xmax { get; }
        public int ymax { get; }
        public int height { get; }
        public int width { get; }
        public int xMaxVariance { get; }
        public int yMaxVariance { get; }
        public long key { get; }
        public string label { get; }
        public Camera camera { get; set; }

        //object position +- threshold max variances to determine positive match. 
        //Threshold percentage variable - percentage variation in object position between detections.
        private double thresholdPercent { get; set; } = .05;

        public ObjectPosition(int xmin, int ymin, int xmax, int ymax, string label, Camera camera)
        {
            createDate = DateTime.Now;
            this.camera = camera;
            this.label = label;

            this.xmin = xmin;
            this.ymin = ymin;
            this.xmax = xmax;
            this.ymax = ymax;

            //calc width and height of box
            this.width = xmax - xmin;
            this.height = ymax - ymin;

            //calculate percentage position variances used in equality comparisons
            xMaxVariance = Convert.ToInt32(this.width * thresholdPercent);
            yMaxVariance = Convert.ToInt32(this.height * thresholdPercent);

            key = ((xmin+1) * (ymin+1) * (xmax+1) * (ymax+1));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ObjectPosition);
        }

        public bool Equals(ObjectPosition other)
        {
            return (other != null &&
                   (xmin.Between(other.xmin - other.xMaxVariance, other.xmin + other.xMaxVariance)) &&
                   (ymin.Between(other.ymin - other.yMaxVariance, other.ymin + other.yMaxVariance)) &&
                   (width.Between(other.width - other.xMaxVariance, other.width + other.xMaxVariance)) &&
                   (height.Between(other.height - other.yMaxVariance, other.height + other.yMaxVariance)));
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
