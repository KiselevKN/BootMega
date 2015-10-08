using System.Threading;

namespace Service.IO.TaskManager
{
    public interface IInTaskManager
    {
        void IfCancellation();
        SynchronizationContext SynchronizationContext { get; }
    }

    public interface IInTaskManager<TProgressValue> : IInTaskManager
    {
        void OnProgress(TProgressValue value);
    }
}
