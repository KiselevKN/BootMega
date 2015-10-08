using System;

namespace Service.IO.TaskManager
{
    public class TaskCompletedEventArgs<TResult> : EventArgs
    {
        private TResult result;

        public TaskCompletedEventArgs(TResult result)
        {
            this.result = result;
        }

        public TResult Result
        {
            get
            {
                return result;
            }
        }
    }
}
