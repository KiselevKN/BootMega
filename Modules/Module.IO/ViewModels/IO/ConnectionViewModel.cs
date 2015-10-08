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
    public class ConnectionViewModel : BaseIOViewModel
    {
        #region fields

        private ITaskManager<Tuple<int, DateTime>> taskm;

        #endregion

        #region ctors

        public ConnectionViewModel(SelectedSettings settings, string portName, ILoggerFacade logger) : base(settings, portName, logger)
        {
            Text = Resources.TextConnection;
            taskm = new TaskManager<Tuple<int, DateTime>>((taskManager) =>
            {
                return (new ConnectionWorkerMaster(portName, settings.SettingsInfo.SerialPortSettings, logger)).Run(taskManager);
            });

            taskm.Canceled += taskm_Canceled;
            taskm.Completed += taskm_Completed;
            taskm.Faulted += taskm_Faulted;
            taskm.Started += taskm_Started;

            taskm.Start();
        }

        #endregion

        #region methods

        private void taskm_Started(object sender, TaskStartedEventArgs e)
        {
            logger.Log(Resources.ConnectionStarted, Category.Debug, Priority.None);
        }

        private void taskm_Faulted(object sender, TaskFaultedEventArgs e)
        {
            logger.Log(string.Format(Resources.ConnectionError, e.Exception), Category.Exception, Priority.None);
            Exception = e.Exception;
            Dlg.Result = false;
            Dlg.Close();
        }

        private void taskm_Completed(object sender, TaskCompletedEventArgs<Tuple<int, DateTime>> e)
        {
            logger.Log(Resources.ConnectionCompleted, Category.Debug, Priority.None);
            Version = e.Result.Item1;
            Date = e.Result.Item2;
            Dlg.Result = true;
            Dlg.Close();
        }

        private void taskm_Canceled(object sender, TaskCanceledEventArgs e)
        {
            logger.Log(Resources.ConnectionStoped, Category.Debug, Priority.None);
            Dlg.Result = false;
            Dlg.Close();
        }

        public override void OnStop()
        {
            taskm.Stop();
        }

        #endregion

        #region properties

        public int Version { get; set; }

        public DateTime Date { get; set; }

        public override bool IsIndeterminate
        {
            get { return true; }
        }

        public override string Title
        {
            get { return Resources.TitleConnection; }
        }

        public override DrawingImage Icon
        {
            get
            {
                var resourceDictionary = new ResourceDictionary();
                resourceDictionary.Source = new Uri("/BootMega.Theme;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
                return resourceDictionary["ConnectionWhiteIcon"] as DrawingImage;
            }
        }

        #endregion
    }
}
