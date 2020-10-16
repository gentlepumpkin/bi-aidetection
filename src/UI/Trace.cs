using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    class Trace:IDisposable
    {
        private bool disposedValue;
        private string _memberName = "";
        private Stopwatch _sw;
        public Trace([CallerMemberName()] string memberName = null)
        {
            
            this._memberName = memberName;
            this._sw = Stopwatch.StartNew();
            if (AITOOL.LogMan != null)
                AITOOL.LogMan.Enter(memberName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (AITOOL.LogMan != null)
                        AITOOL.LogMan.Exit(this._memberName, this._sw.ElapsedMilliseconds);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Trace()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
