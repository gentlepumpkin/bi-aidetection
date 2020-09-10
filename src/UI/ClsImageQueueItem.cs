using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AITool
{

    public class ClsImageQueueItem
    {
        private int errCount;
        private int retryCount;

        public string image_path { get; set; }
        public DateTime TimeAdded { get; set; }
        public long QueueWaitMS { get; set; }
        public long TotalTimeMS { get; set; }
        public long DeepStackTimeMS { get; set; }
        public long FileLockMS { get; set; }
        public long CurQueueSize { get; set; }
        public int ErrCount { get => errCount; set => errCount = value; }
        public void IncrementErrCount()
        {
            //if we try to increment class.ErrCount directly you get 'A property or indexer may not be passed as an out or ref parameter' - Workaround:
            Interlocked.Increment(ref this.errCount);
        }
        public int RetryCount { get => retryCount; set => retryCount = value; }
        public void IncrementRetryCount()
        {
            Interlocked.Increment(ref this.retryCount);
        }
        public string ResultMessage { get; set; }
        public ClsImageQueueItem(String FileName, long CurQueueSize)
        {
            this.image_path = FileName;
            this.TimeAdded = DateTime.Now;
            this.CurQueueSize = CurQueueSize;
        }

    }
}
