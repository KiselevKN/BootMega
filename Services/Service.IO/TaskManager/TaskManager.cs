using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.IO.TaskManager
{
    public class TaskManager<TResult, TProgressValue> : ITaskManager<TResult, TProgressValue>
    {
        #region fields

        private bool isStarted;
        private CancellationTokenSource cancelSource;
        private CancellationToken token;
        private SynchronizationContext syncContext;
        private Func<IInTaskManager<TProgressValue>, TResult> function;
        private object sync = new object();

        #endregion

        #region ctor

        public TaskManager(Func<IInTaskManager<TProgressValue>, TResult> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");
            
            this.function = function;
            
            isStarted = false;
            syncContext = SynchronizationContext.Current;
            
        }

        #endregion

        #region methods

        protected virtual Task<TResult> _Run(Func<IInTaskManager<TProgressValue>, TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();
            
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnStarted(this, new TaskStartedEventArgs());
                    tcs.SetResult(function(this));
                }
                catch (OperationCanceledException)
                {
                    tcs.SetCanceled();
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }

            }, token);

            return tcs.Task;
        }

        private void Run(Func<IInTaskManager<TProgressValue>, TResult> function)
        {
            cancelSource = new CancellationTokenSource();
            token = cancelSource.Token;

            Task<TResult> task = _Run(function);

            task.ContinueWith((t) =>
            {
                isStarted = false;
                OnCanceled(this, new TaskCanceledEventArgs());
            }, TaskContinuationOptions.OnlyOnCanceled);

            task.ContinueWith((t) =>
            {
                isStarted = false;
                OnFaulted(this, new TaskFaultedEventArgs(t.Exception));
            }, TaskContinuationOptions.OnlyOnFaulted);

            task.ContinueWith((t) =>
            {
                isStarted = false;
                OnCompleted(this, new TaskCompletedEventArgs<TResult>(t.Result));
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        protected virtual void OnStarted(object sender, TaskStartedEventArgs args)
        {
            var handler = Started;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        protected virtual void OnCanceled(object sender, TaskCanceledEventArgs args)
        {
            var handler = Canceled;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        protected virtual void OnFaulted(object sender, TaskFaultedEventArgs args)
        {
            var handler = Faulted;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        protected virtual void OnCompleted(object sender, TaskCompletedEventArgs<TResult> args)
        {
            var handler = Completed;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        protected virtual void OnProgressed(object sender, TaskProgressedEventArgs<TProgressValue> args)
        {
            var handler = Progressed;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        #endregion

        #region ITaskManager<TResult,TProgressValue> Members

        public bool IsStarted { get { lock (sync) { return isStarted; } } }

        public void Start()
        {
            lock (sync)
            {
                if (function != null && !isStarted)
                {
                    isStarted = true;
                    Run(function);
                }
            }
        }

        public void Stop()
        {
            lock (sync)
            {
                if (isStarted)
                    cancelSource.Cancel();
            }
        }

        public event EventHandler<TaskStartedEventArgs> Started;

        public event EventHandler<TaskCanceledEventArgs> Canceled;

        public event EventHandler<TaskFaultedEventArgs> Faulted;

        public event EventHandler<TaskCompletedEventArgs<TResult>> Completed;

        public event EventHandler<TaskProgressedEventArgs<TProgressValue>> Progressed;

        #endregion

        #region IInTaskManager<TProgressValue> Members

        public void OnProgress(TProgressValue value)
        {
            OnProgressed(this, new TaskProgressedEventArgs<TProgressValue>(value));
        }

        #endregion

        #region IInTaskManager Members

        public void IfCancellation()
        {
            token.ThrowIfCancellationRequested();
        }

        public SynchronizationContext SynchronizationContext
        {
            get { return syncContext; }
        }

        #endregion
    }

    public class TaskManager<TResult> : ITaskManager<TResult>
    {
        #region fields

        private bool isStarted;
        private CancellationTokenSource cancelSource;
        private CancellationToken token;
        private SynchronizationContext syncContext;
        private Func<IInTaskManager, TResult> function;
        private object sync = new object();

        #endregion

        #region ctor

        public TaskManager(Func<IInTaskManager, TResult> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");

            this.function = function;

            isStarted = false;
            syncContext = SynchronizationContext.Current;
        }

        #endregion

        #region methods

        protected virtual Task<TResult> _Run(Func<IInTaskManager, TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();          
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnStarted(this, new TaskStartedEventArgs());
                    tcs.SetResult(function(this));
                }
                catch (OperationCanceledException)
                {
                    tcs.SetCanceled();
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }

            }, token);


            return tcs.Task;
        }

        private void Run(Func<IInTaskManager, TResult> function)
        {
            cancelSource = new CancellationTokenSource();
            token = cancelSource.Token;

            Task<TResult> task = _Run(function);

            task.ContinueWith((t) =>
            {
                isStarted = false;
                OnCanceled(this, new TaskCanceledEventArgs());
            }, TaskContinuationOptions.OnlyOnCanceled);

            task.ContinueWith((t) =>
            {
                isStarted = false;
                OnFaulted(this, new TaskFaultedEventArgs(t.Exception));
            }, TaskContinuationOptions.OnlyOnFaulted);

            task.ContinueWith((t) =>
            {
                isStarted = false;
                OnCompleted(this, new TaskCompletedEventArgs<TResult>(t.Result));
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        protected virtual void OnStarted(object sender, TaskStartedEventArgs args)
        {
            var handler = Started;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        protected virtual void OnCanceled(object sender, TaskCanceledEventArgs args)
        {
            var handler = Canceled;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        protected virtual void OnFaulted(object sender, TaskFaultedEventArgs args)
        {
            var handler = Faulted;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        protected virtual void OnCompleted(object sender, TaskCompletedEventArgs<TResult> args)
        {
            var handler = Completed;
            if (handler != null)
                syncContext.Post(delegate { handler(sender, args); }, null);
        }

        #endregion

        #region ITaskManager<TResult> Members

        public bool IsStarted { get { lock (sync) { return isStarted; } } }

        public void Start()
        {
            lock (sync)
            {
                if (function != null && !isStarted)
                {
                    isStarted = true;
                    Run(function);
                }
            }
        }

        public void Stop()
        {
            lock (sync)
            {
                if (isStarted)
                    cancelSource.Cancel();
            }
        }

        public event EventHandler<TaskStartedEventArgs> Started;

        public event EventHandler<TaskCanceledEventArgs> Canceled;

        public event EventHandler<TaskFaultedEventArgs> Faulted;

        public event EventHandler<TaskCompletedEventArgs<TResult>> Completed;

        #endregion

        #region IInTaskManager Members

        public void IfCancellation()
        {
            token.ThrowIfCancellationRequested();
        }

        public SynchronizationContext SynchronizationContext
        {
            get { return syncContext; }
        }

        #endregion
    }
}
