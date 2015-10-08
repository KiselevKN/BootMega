using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.Logging;
using Module.IO.Properties;
using Service.HexFile.MemoryMapping;
using Service.IO.TaskManager;
using Service.IO.Workers;
using Service.Settings.Models;

namespace Module.IO.ViewModels
{
    public class WriteViewModel : BaseIOViewModel
    {
        #region fields

        private ITaskManager<bool, double> taskm;

        #endregion

        #region properties

        public override bool IsIndeterminate
        {
            get { return false; }
        }

        public override string Title
        {
            get { return string.Format(Resources.TitleWrite, GetMemoryType()); }
        }

        public override DrawingImage Icon
        {
            get
            {
                var resourceDictionary = new ResourceDictionary();
                resourceDictionary.Source =
                    new Uri("/BootMega.Theme;component/Themes/Generic.xaml",
                            UriKind.RelativeOrAbsolute);

                return resourceDictionary["DownloadWhiteIcon"] as DrawingImage;
            }
        }

        #endregion

        #region ctors

        public WriteViewModel(SelectedSettings settings, string portName, ILoggerFacade logger, IMemory memory) : base(settings, portName, logger)
        {
            Text = string.Format(Resources.TextWrite, GetMemoryType());
            taskm = new TaskManager<bool, double>((taskManager) =>
            {
                return (new WritePageWorkerMaster(portName, settings.SettingsInfo.SerialPortSettings, logger,
                    settings.MemoryType, settings.SettingsInfo.Processor, memory)).Run(taskManager);
            });

            taskm.Canceled += taskm_Canceled;
            taskm.Completed += taskm_Completed;
            taskm.Faulted += taskm_Faulted;
            taskm.Started += taskm_Started;
            taskm.Progressed += taskm_Progressed;

            taskm.Start();
        }

        #endregion

        #region methods

        private void taskm_Progressed(object sender, TaskProgressedEventArgs<double> e)
        {
            Progress = e.Value;
        }

        private void taskm_Started(object sender, TaskStartedEventArgs e)
        {
            logger.Log(string.Format(Resources.WriteStarted, GetMemoryType()), Category.Debug, Priority.None);
        }

        private void taskm_Faulted(object sender, TaskFaultedEventArgs e)
        {
            logger.Log(string.Format(Resources.WriteError, GetMemoryType(), e.Exception), Category.Exception, Priority.None);
            Exception = e.Exception;
            Dlg.Result = false;
            Dlg.Close();
        }

        private void taskm_Completed(object sender, TaskCompletedEventArgs<bool> e)
        {
            logger.Log(string.Format(Resources.WriteCompleted, GetMemoryType()), Category.Debug, Priority.None);        
            Dlg.Result = true;
            Dlg.Close();
        }

        private void taskm_Canceled(object sender, TaskCanceledEventArgs e)
        {
            logger.Log(string.Format(Resources.WriteStoped, GetMemoryType()), Category.Debug, Priority.None);
            Dlg.Result = false;
            Dlg.Close();
        }

        public override void OnStop()
        {
            if (taskm != null)
                taskm.Stop();
        }

        #endregion
    }
}
