using System;
using Microsoft.Practices.Prism.Logging;
using Service.IO.TaskManager;
using Service.IO.Workers;
using Service.Settings.Models;

namespace BootMega.IO.Imitation.ViewModels
{
    public class ConnectionViewModel : BaseViewModel
    {
        private ITaskManager<bool> taskm;

        public ConnectionViewModel(string portName, SerialPortSettings serialPortSettings, ILoggerFacade logger, int version, DateTime date)
            : base(portName, serialPortSettings, logger)
        {
            Text = "Connection";
            taskm = new TaskManager<bool>((taskManager) =>
            {
                return (new ConnectionWorkerSlave(portName, serialPortSettings, logger, version, date).Run(taskManager));
            });

            taskm.Canceled += taskm_Canceled;
            taskm.Completed += taskm_Completed;
            taskm.Faulted += taskm_Faulted;
            taskm.Started += taskm_Started;
        }

        private void taskm_Started(object sender, TaskStartedEventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        private void taskm_Faulted(object sender, TaskFaultedEventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        private void taskm_Completed(object sender, TaskCompletedEventArgs<bool> e)
        {
            RaiseCanExecuteChanged();
        }

        private void taskm_Canceled(object sender, TaskCanceledEventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        public override bool CanExecuteStart()
        {
            return !taskm.IsStarted;
        }

        public override void OnStart()
        {
            taskm.Start();
            RaiseCanExecuteChanged();
        }

        public override bool CanExecuteStop()
        {
            return taskm.IsStarted;
        }

        public override void OnStop()
        {
            taskm.Stop();
            RaiseCanExecuteChanged();
        }
    }
}
