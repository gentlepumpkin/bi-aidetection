using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public static class DoubleExtensions
    {
        public static int ToInt(this double val)
        {
            try
            {
                if (!val.IsNull())
                    return Convert.ToInt32(val);  //I believe ToInt32 rounds up so 1.5 is 2.  (int) just truncates the decimal
            }
            catch { }

            return 0;
        }
        public static float ToFloat(this double val)
        {
            try
            {
                if (!val.IsNull())
                    return Convert.ToSingle(val);
            }
            catch { }

            return 0;
        }
        public static double Round(this double val, int Places = 2)
        {
            try
            {
                if (!val.IsNull())
                    return Math.Round(val, Places);
            }
            catch { }

            return 0;
        }
        public static string ToPercent(this double val, int Places = 2)
        {
            string chars = "";
            if (Places > 0)
                chars = "." + new string('#', Places);

            return val.Round(Places).ToString("##0" + chars) + "%";

        }

    }
}
