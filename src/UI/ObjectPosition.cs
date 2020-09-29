using BrightIdeasSoftware;
using System;
using System.Collections.Generic;

namespace AITool
{
    public class ObjectPosition:IEquatable<ObjectPosition>
    {
        public string label { get; } = "";
        public DateTime createDate { get; set; } = DateTime.MinValue;
        public DateTime LastSeenDate { get; set; } = DateTime.MinValue;

        public int counter { get; set; }
        public double thresholdPercent { get; set; }
        public Boolean isStatic { get; set; } = false;
        public int xmin { get; }
        public int ymin { get; }
        public int xmax { get; }
        public int ymax { get; }
        public int height { get; }
        public int width { get; }
        public long key { get; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }

        public string cameraName { get; set; } = "";
        public string imagePath { get; set; } = "";

        //store the calcs for troubleshooting
        public bool LastMatched { get; set; } = false;
        public double LastWidthPercentVariance { get; set; } = 0;
        public double LastHeightPercentVariance { get; set; } = 0;

        //calculate percentage change in starting corner of the x,y axis (upper-left corner of the rectangle)
        public double LastxPercentVariance { get; set; } = 0;
        public double LastyPercentVariance { get; set; } = 0;

        //scaling of object based on size
        public int scalePercent { get; set; } 
        public double objectImagePercent { get; }

        private ObjectScale _scaleConfig;
        public ObjectScale scaleConfig
        {
            get => _scaleConfig;
            set
            {
                _scaleConfig = value;
                scalePercent = getImagePercentVariance();
            }
        }


        public ObjectPosition(int xmin, int ymin, int xmax, int ymax, string label, int imageWidth, int imageHeight, string cameraName, string imagePath)
        {
            this.createDate = DateTime.Now;
            this.LastSeenDate = DateTime.Now;
            this.cameraName = cameraName;
            this.imagePath = imagePath;
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

            //object percent of image area
            objectImagePercent = (width * height) / (float)(imageWidth * imageHeight) * 100;

            //starting x * y point + width * height of rectangle - used for debugging only
            key = ((xmin + 1) * (ymin + 1) + (width * height));
        }

        /*Increases object variance percentage for smaller objects. 
        Due to thier size, smaller objects are more sensitive to slight changes in postion. 
        This settings allows sensivity adjustments based on the object size.*/
        private int getImagePercentVariance()
        {
            int scalePercent = 0;

            if (scaleConfig != null)
            {
                if (scaleConfig.isScaledObject)
                {
                    if (objectImagePercent <= scaleConfig.smallObjectMaxPercent)
                    {
                        scalePercent = scaleConfig.smallObjectScalePercent;
                    }
                    else if (objectImagePercent >= scaleConfig.mediumObjectMinPercent 
                            &&  objectImagePercent <= scaleConfig.mediumObjectMaxPercent)
                    {
                        scalePercent = scaleConfig.mediumObjectScalePercent;
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

            //calculate the percentage variance for width and height of selected object
            this.LastWidthPercentVariance = (double)Math.Abs(this.width - other.width) / this.width * 100;
            this.LastHeightPercentVariance = (double)Math.Abs(this.height - other.height) / this.height * 100;

            //calculate percentage change in starting corner of the x,y axis (upper-left corner of the rectangle)
            this.LastxPercentVariance = (double)(Math.Abs(this.xmin - other.xmin)) / imageWidth * 100;
            this.LastyPercentVariance = (double)(Math.Abs(this.ymin - other.ymin)) / imageHeight * 100;
            double percent = thresholdPercent + scalePercent;

            this.LastMatched = (this.LastxPercentVariance <= percent) &&
                               (this.LastyPercentVariance <= percent) &&
                               (this.LastWidthPercentVariance <= percent) &&
                               (this.LastHeightPercentVariance <= percent);

            return this.LastMatched;
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
            string value = "key=" + getKey() + ", name=" + label + ", xmin=" + xmin + ", ymin=" + ymin + ", xmax=" + xmax + ", ymax=" + ymax + ", counter=" + counter + ", camera=" + cameraName + ", create date: " + createDate;

            return value;
        }
    }
}
