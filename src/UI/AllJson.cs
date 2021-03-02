using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public class AllJson
    {
        public string fileName { get; set; } = "";
        public string baseName { get; set; } = "";
        public string summary { get; set; } = "";
        public string state { get; set; } = "";
        public long analysisDurationMS { get; set; } = 0;
        public DateTime Time { get; set; } = DateTime.MinValue;
        public string Camera { get; set; } = "";

        public ClsDeepstackDetection[] predictions { get; set; }

    }
}
