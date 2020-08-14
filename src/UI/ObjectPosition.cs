using System;
using System.Collections.Generic;

namespace AITool
{
    public class ObjectPosition : IEquatable<ObjectPosition>
    {
        public DateTime createDate;
        public Boolean isVisible=false;
        public int counter;
        public int xmin;
        public int ymin;
        public int xmax;
        public int ymax;
        public long key;
        public string label;
        public Camera camera;

        //object position +- thresholdMaxRange to determine positive match. 
        //Threshold variable due to variations in object position between detections.
        public int thresholdMaxRange = 35;

        public ObjectPosition(int xmin, int ymin, int xmax, int ymax, string label, Camera camera)
        {
            //todo: convert 0's to 1 to prevent 0 key; Currently, Adds 1 to avoid zero key
            createDate = DateTime.Now;
            this.camera = camera;

            this.xmin = xmin;
            this.ymin = ymin;
            this.xmax = xmax;
            this.ymax = ymax;
            this.label = label; 

            key = ((xmin+1) * (ymin+1) * (xmax+1) * (ymax+1));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ObjectPosition);
        }

        public bool Equals(ObjectPosition other)
        {
            return (other != null &&
                   (xmin.Between(other.xmin - thresholdMaxRange, other.xmin + thresholdMaxRange)) &&
                   (ymin.Between(other.ymin - thresholdMaxRange, other.ymin + thresholdMaxRange)) &&
                   (xmax.Between(other.xmax - thresholdMaxRange, other.xmax + thresholdMaxRange)) &&
                   (ymax.Between(other.ymax - thresholdMaxRange, other.ymax + thresholdMaxRange)));
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
