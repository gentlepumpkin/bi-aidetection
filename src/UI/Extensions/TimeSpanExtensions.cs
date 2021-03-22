using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public static class TimeSpanExtensions
    {

        public static string FormatTS(this TimeSpan span, bool shortformat)
        {
            string formatted = "";

            if (!shortformat)
                formatted = string.Format("{0}{1}{2}{3}{4}", span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.TotalDays, span.TotalDays == 1 ? String.Empty : "s") : string.Empty,
                                                                   span.Duration().Hours > 0 ? string.Format("{0:0} hr{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
                                                                   span.Duration().Minutes > 0 ? string.Format("{0:0} min{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
                                                                   span.Duration().Seconds > 0 ? string.Format("{0:0} sec{1}, ", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty,
                                                                   span.Duration().Milliseconds > 20 ? string.Format("{0:0} ms", span.Milliseconds) : string.Empty);
            else
                formatted = string.Format("{0}{1}{2}{3}{4}", span.Duration().Days > 0 ? string.Format("{0:0}d", span.Days) : string.Empty,
                                                                   span.Duration().Hours > 0 ? string.Format("{0:0}h", span.Hours) : string.Empty,
                                                                   span.Duration().Minutes > 0 ? string.Format("{0:0}m", span.Minutes) : string.Empty,
                                                                   span.Duration().Seconds > 0 ? string.Format("{0:0}s", span.Seconds) : string.Empty,
                                                                   span.Duration().Milliseconds > 20 ? string.Format("{0:0}ms", span.Milliseconds) : string.Empty);

            formatted = formatted.Trim(", ".ToCharArray());

            if (string.IsNullOrEmpty(formatted))
                formatted = "0 sec";

            return formatted;
        }
    }
}
