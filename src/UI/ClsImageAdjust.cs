using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public class ClsImageAdjust : IEquatable<ClsImageAdjust>
    {
        public string Name { get; set; } = "";
        public int JPEGQualityPercent { get; set; } = 90;
        public int ImageSizePercent { get; set; } = 100;
        public int ImageWidth { get; set; } = -1; //-1=unchanged
        public int ImageHeight { get; set; } = -1; //-1=unchanged
        public int Brightness { get; set; } = 1;  //1=unchanged
        public int Contrast { get; set; } = 1;  //1=unchanged

        [JsonConstructor]
        public ClsImageAdjust()
        {

        }

        public ClsImageAdjust(string name)
        {
            this.Name = name.Trim();
        }
        public ClsImageAdjust(string name, string jpegqualitypercent, string imagesizepercent, string imagewidth, string imageheight, string brightness, string contrast)
        {
            this.Update(name, jpegqualitypercent, imagesizepercent, imagewidth, imageheight, brightness, contrast);
        }

        public void Update(string name, string jpegqualitypercent, string imagesizepercent, string imagewidth, string imageheight, string brightness, string contrast)
        {
            this.Name = name.Trim();

            if (string.IsNullOrWhiteSpace(jpegqualitypercent))
                jpegqualitypercent = this.JPEGQualityPercent.ToString();

            if (string.IsNullOrWhiteSpace(imagesizepercent))
                imagesizepercent = this.ImageSizePercent.ToString();

            if (string.IsNullOrWhiteSpace(imagewidth))
                imagewidth = this.ImageWidth.ToString();

            if (string.IsNullOrWhiteSpace(imageheight))
                imageheight = this.ImageHeight.ToString();

            if (string.IsNullOrWhiteSpace(brightness))
                brightness = this.Brightness.ToString();

            if (string.IsNullOrWhiteSpace(contrast))
                contrast = this.Contrast.ToString();

            this.JPEGQualityPercent = Convert.ToInt32(jpegqualitypercent);
            this.ImageSizePercent = Convert.ToInt32(imagesizepercent);
            this.ImageWidth = Convert.ToInt32(imagewidth);
            this.ImageHeight = Convert.ToInt32(imageheight);
            this.Brightness = Convert.ToInt32(brightness);
            this.Contrast = Convert.ToInt32(contrast);


        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClsImageAdjust);
        }

        public bool Equals(ClsImageAdjust other)
        {
            return other != null &&
                   string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 59;
                // Suitable nullity checks etc, of course :)
                hash = hash * 61 + this.Name.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(ClsImageAdjust left, ClsImageAdjust right)
        {
            return EqualityComparer<ClsImageAdjust>.Default.Equals(left, right);
        }

        public static bool operator !=(ClsImageAdjust left, ClsImageAdjust right)
        {
            return !(left == right);
        }
    }
}
