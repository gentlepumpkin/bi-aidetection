using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Arch.CMessaging.Client.Core.Utils;

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
        public ThreadSafe.Integer ErrCount { get; set; } = new ThreadSafe.Integer(0);
        public ThreadSafe.Integer RetryCount { get; set; } = new ThreadSafe.Integer(0); 
        public string ResultMessage { get; set; }
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public ClsImageQueueItem(String FileName, long CurQueueSize)
        {
            this.image_path = FileName;
            this.TimeAdded = DateTime.Now;
            this.CurQueueSize = CurQueueSize;
        }

    }
}
