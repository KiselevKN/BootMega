using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.Logging;
using Module.IO.Properties;
using Service.IO.TaskManager;
using Service.IO.Workers;
using Service.Settings.Models;

namespace Module.IO.ViewModels
{
    public class EraseViewModel : BaseIOViewModel
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
            get { return String.Format(Resources.TitleErase, GetMemoryType()); }
        }

        public override DrawingImage Icon
        {
            get
            {
                var resourceDictionary = new ResourceDictionary();
                resourceDictionary.Source = new Uri("/BootMega.Theme;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
                return resourceDictionary["EraseWhiteIcon"] as DrawingImage;
            }
        }

        #endregion

        #region ctors

        public EraseViewModel(SelectedSettings settings, string portName, ILoggerFacade logger) : base(settings, portName, logger)
        {
            Text = String.Format(Resources.TextErase, GetMemoryType());

            taskm = new TaskManager<bool, double>((taskManager) =>
            {
                return (new ErasePageWorkerMaster(portName, settings.SettingsInfo.SerialPortSettings, logger, 
                    settings.MemoryType, settings.SettingsInfo.Processor)).Run(taskManager);
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

        void taskm_Progressed(object sender, TaskProgressedEventArgs<double> e)
        {
            Progress = e.Value;
        }

        private void taskm_Started(object sender, TaskStartedEventArgs e)
        {
            logger.Log(String.Format(Resources.EraseStarted, GetMemoryType()), Category.Debug, Priority.None);
        }

        private void taskm_Faulted(object sender, TaskFaultedEventArgs e)
        {
            logger.Log(String.Format(Resources.EraseError, GetMemoryType(), e.Exception),  Category.Exception, Priority.None);
            Exception = e.Exception;
            Dlg.Result = false;
            Dlg.Close();
        }

        private void taskm_Completed(object sender, TaskCompletedEventArgs<bool> e)
        {
            logger.Log(String.Format(Resources.EraseCompleted, GetMemoryType()), Category.Debug, Priority.None);
            Dlg.Result = true;
            Dlg.Close();
        }

        private void taskm_Canceled(object sender, TaskCanceledEventArgs e)
        {
            logger.Log(String.Format(Resources.EraseStoped, GetMemoryType()), Category.Debug, Priority.None);
            Dlg.Result = false;
            Dlg.Close();
        }

        public override void OnStop()
        {
            taskm.Stop();
        }

        #endregion
    }
}
