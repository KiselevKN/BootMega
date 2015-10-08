using System;

namespace Service.IO.TaskManager
{
    public interface ITaskManager<TResult> : IInTaskManager
    {
        void Start();
        void Stop();
        bool IsStarted { get; }

        event EventHandler<TaskStartedEventArgs> Started;
        event EventHandler<TaskCanceledEventArgs> Canceled;
        event EventHandler<TaskFaultedEventArgs> Faulted;
        event EventHandler<TaskCompletedEventArgs<TResult>> Completed;
    }


    public interface ITaskManager<TResult, TProgressValue>: IInTaskManager<TProgressValue>
    {
        void Start();
        void Stop();
        bool IsStarted { get; }

        event EventHandler<TaskStartedEventArgs> Started;
        event EventHandler<TaskCanceledEventArgs> Canceled;
        event EventHandler<TaskFaultedEventArgs> Faulted;
        event EventHandler<TaskCompletedEventArgs<TResult>> Completed;
        event EventHandler<TaskProgressedEventArgs<TProgressValue>> Progressed;
    }

    
}
