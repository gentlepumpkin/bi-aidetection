using System;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipeWrapper.Threading
{
    class Worker
    {
        private readonly TaskScheduler _callbackThread;

        private static TaskScheduler CurrentTaskScheduler
        {
            get
            {
                return (SynchronizationContext.Current != null
                            ? TaskScheduler.FromCurrentSynchronizationContext()
                            : TaskScheduler.Default);
            }
        }

        public event WorkerSucceededEventHandler Succeeded;
        public event WorkerExceptionEventHandler Error;

        public Worker() : this(CurrentTaskScheduler)
        {
        }

        public Worker(TaskScheduler callbackThread)
        {
            this._callbackThread = callbackThread;
        }

        public void DoWork(Action action)
        {
            new Task(this.DoWorkImpl, action, CancellationToken.None, TaskCreationOptions.LongRunning).Start();
        }

        private void DoWorkImpl(object oAction)
        {
            var action = (Action)oAction;
            try
            {
                action();
                this.Callback(this.Succeed);
            }
            catch (Exception e)
            {
                this.Callback(() => this.Fail(e));
            }
        }

        private void Succeed()
        {
            if (Succeeded != null)
                Succeeded();
        }

        private void Fail(Exception exception)
        {
            if (Error != null)
                Error(exception);
        }

        private void Callback(Action action)
        {
            Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, this._callbackThread);
        }
    }

    internal delegate void WorkerSucceededEventHandler();
    internal delegate void WorkerExceptionEventHandler(Exception exception);
}
