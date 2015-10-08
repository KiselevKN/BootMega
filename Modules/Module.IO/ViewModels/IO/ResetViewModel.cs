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
    public class ResetViewModel : BaseIOViewModel
    {
        #region fields

        private ITaskManager<bool> taskm;

        #endregion

        #region ctors

        public ResetViewModel(SelectedSettings settings, string portName, ILoggerFacade logger) : base(settings, portName, logger)
        {
            Text = Resources.TextReset;
            taskm = new TaskManager<bool>((taskManager) =>
            {
                return (new ResetWorkerMaster(portName, settings.SettingsInfo.SerialPortSettings, logger)).Run(taskManager);
            });

            taskm.Canceled += taskm_Canceled;
            taskm.Completed += taskm_Completed;
            taskm.Faulted += taskm_Faulted;
            taskm.Started += taskm_Started;

            taskm.Start();
        }

        #endregion

        #region properties

        public override bool IsIndeterminate
        {
            get { return true; }
        }

        public override string Title
        {
            get { return Resources.TitleReset; }
        }

        public override DrawingImage Icon
        {
            get
            {
                var resourceDictionary = new ResourceDictionary();
                resourceDictionary.Source = new Uri("/BootMega.Theme;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
                return resourceDictionary["ResetWhiteIcon"] as DrawingImage;
            }
        }
        #endregion

        #region methods

        private void taskm_Started(object sender, TaskStartedEventArgs e)
        {
            logger.Log(Resources.ResetStarted, Category.Debug, Priority.None);
        }

        private void taskm_Faulted(object sender, TaskFaultedEventArgs e)
        {
            logger.Log(String.Format(Resources.ResetError, e.Exception), Category.Exception, Priority.None);
            Exception = e.Exception;
            Dlg.Result = false;
            Dlg.Close();
        }

        private void taskm_Completed(object sender, TaskCompletedEventArgs<bool> e)
        {
            logger.Log(Resources.ResetCompleted, Category.Debug, Priority.None);
            Dlg.Result = true;
            Dlg.Close();
        }

        private void taskm_Canceled(object sender, TaskCanceledEventArgs e)
        {
            logger.Log(Resources.ResetStoped, Category.Debug, Priority.None);
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
