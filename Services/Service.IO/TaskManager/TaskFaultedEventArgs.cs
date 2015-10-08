using System;

namespace Service.IO.TaskManager
{
    public class TaskFaultedEventArgs : EventArgs
    {
        private Exception ex;

        public TaskFaultedEventArgs(Exception ex)
        {
            this.ex = ex;
        }

        public Exception Exception
        {
            get
            {
                return ex;
            }
        }
    }
}
