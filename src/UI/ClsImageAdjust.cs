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

        public ClsImageAdjust(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClsImageAdjust);
        }

        public bool Equals(ClsImageAdjust other)
        {
            return other != null &&
                   string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
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
