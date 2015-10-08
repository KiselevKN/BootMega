using System;
using System.Collections.Generic;
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
    public class ReadViewModel : BaseIOViewModel
    {
        #region fields

        private ITaskManager<List<int>, double> taskmIsEmptyPage;
        private ITaskManager<IMemory, double> taskm;

        #endregion

        #region properties

        public IMemory Memory { get; set; }

        public override bool IsIndeterminate
        {
            get { return false; }
        }

        public override string Title
        {
            get { return string.Format(Resources.TitleRead, GetMemoryType()); }
        }

        public override DrawingImage Icon
        {
            get
            {
                var resourceDictionary = new ResourceDictionary();
                resourceDictionary.Source = new Uri("/BootMega.Theme;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
                return resourceDictionary["UploadWhiteIcon"] as DrawingImage;
            }
        }

        #endregion

        #region ctors

        public ReadViewModel(SelectedSettings settings, string portName, ILoggerFacade logger) : base(settings, portName, logger)
        {
            Text = string.Format(Resources.TextAnalysis, GetMemoryType());
            taskmIsEmptyPage = new TaskManager<List<int>, double>((taskManager) =>
            {
                return (new IsEmptyPageWorkerMaster(portName, settings.SettingsInfo.SerialPortSettings, logger,
                    settings.MemoryType, settings.SettingsInfo.Processor)).Run(taskManager);
            });

            taskmIsEmptyPage.Canceled += taskmIsEmptyPage_Canceled;
            taskmIsEmptyPage.Completed += taskmIsEmptyPage_Completed;
            taskmIsEmptyPage.Faulted += taskmIsEmptyPage_Faulted;
            taskmIsEmptyPage.Started += taskmIsEmptyPage_Started;
            taskmIsEmptyPage.Progressed += taskmIsEmptyPage_Progressed;

            taskmIsEmptyPage.Start();
        }

        #endregion

        #region methods

        void taskmIsEmptyPage_Progressed(object sender, TaskProgressedEventArgs<double> e)
        {
            Progress = e.Value;
        }

        private void taskmIsEmptyPage_Started(object sender, TaskStartedEventArgs e)
        {
            logger.Log(string.Format(Resources.IsEmptyStarted, GetMemoryType()), Category.Debug, Priority.None);
        }

        private void taskmIsEmptyPage_Faulted(object sender, TaskFaultedEventArgs e)
        {
            logger.Log(string.Format(Resources.IsEmptyError, GetMemoryType(), e.Exception), Category.Exception, Priority.None);
            Exception = e.Exception;
            Dlg.Result = false;
            Dlg.Close();
        }

        private void taskmIsEmptyPage_Completed(object sender, TaskCompletedEventArgs<List<int>> e)
        {
            Text = string.Format(Resources.TextRead, GetMemoryType());
            logger.Log(string.Format(Resources.IsEmptyCompleted, GetMemoryType()), Category.Debug, Priority.None);
            taskm = new TaskManager<IMemory, double>((taskManager) =>
            {
                return (new ReadPageWorkerMaster(portName, settings.SettingsInfo.SerialPortSettings, logger,
                    settings.MemoryType, settings.SettingsInfo.Processor, e.Result)).Run(taskManager);
            });

            taskm.Canceled += taskm_Canceled;
            taskm.Completed += taskm_Completed;
            taskm.Faulted += taskm_Faulted;
            taskm.Started += taskm_Started;
            taskm.Progressed += taskm_Progressed;

            taskm.Start();
        }

        private void taskmIsEmptyPage_Canceled(object sender, TaskCanceledEventArgs e)
        {
            logger.Log(string.Format(Resources.IsEmptyStoped, GetMemoryType()), Category.Debug, Priority.None);
            Dlg.Result = false;
            Dlg.Close();
        }

        private void taskm_Progressed(object sender, TaskProgressedEventArgs<double> e)
        {
            Progress = e.Value;
        }

        private void taskm_Started(object sender, TaskStartedEventArgs e)
        {
            logger.Log(string.Format(Resources.ReadStarted, GetMemoryType()), Category.Debug, Priority.None);
        }

        private void taskm_Faulted(object sender, TaskFaultedEventArgs e)
        {
            logger.Log(string.Format(Resources.ReadError, GetMemoryType(), e.Exception), Category.Exception, Priority.None);
            Exception = e.Exception;
            Dlg.Result = false;
            Dlg.Close();
        }

        private void taskm_Completed(object sender, TaskCompletedEventArgs<IMemory> e)
        {
            logger.Log(string.Format(Resources.ReadCompleted, GetMemoryType()), Category.Debug, Priority.None);        
            Memory = e.Result;
            Dlg.Result = true;
            Dlg.Close();
        }

        private void taskm_Canceled(object sender, TaskCanceledEventArgs e)
        {
            logger.Log(string.Format(Resources.ReadStoped, GetMemoryType()), Category.Debug, Priority.None);
            Dlg.Result = false;
            Dlg.Close();
        }

        public override void OnStop()
        {
            if (taskmIsEmptyPage != null)
                taskmIsEmptyPage.Stop();
            if (taskm != null)
                taskm.Stop();
        }

        #endregion
    }
}
