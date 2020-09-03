using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{

    public class ClsImageQueueItem
    {
        public string image_path { get; set; }
        public DateTime TimeAdded { get; set; }
        public long QueueWaitMS { get; set; }
        public long TotalTimeMS { get; set; }
        public long DeepStackTimeMS { get; set; }
        public long FileLockMS { get; set; }
        public long CurQueueSize { get; set; }
        public int ErrCount { get; set; }
        public string ResultMessage { get; set; }
        public ClsImageQueueItem(String FileName, long CurQueueSize)
        {
            this.image_path = FileName;
            this.TimeAdded = DateTime.Now;
            this.CurQueueSize = CurQueueSize;
        }

    }
}
