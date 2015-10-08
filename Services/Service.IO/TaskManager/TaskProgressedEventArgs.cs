using System;

namespace Service.IO.TaskManager
{
    public class TaskProgressedEventArgs<TProgressValue> : EventArgs
    {
        private TProgressValue value;

        public TaskProgressedEventArgs(TProgressValue value)
        {
            this.value = value;
        }

        public TProgressValue Value
        {
            get
            {
                return value;
            }
        }
    }
}
