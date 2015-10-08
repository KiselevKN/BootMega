using System;
using System.Windows.Input;
using System.Windows.Media;
using BootMega.Theme;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Service.Settings.Enums;
using Service.Settings.Models;

namespace Module.IO.ViewModels
{
    public abstract class BaseIOViewModel : BindableBase
    {
        #region fields

        protected ILoggerFacade logger;
        protected SelectedSettings settings;
        protected string portName;
        private double progress;
        private string text;

        #endregion

        #region ctors

        public BaseIOViewModel(SelectedSettings settings, string portName, ILoggerFacade logger)
        {
            this.logger = logger;
            this.settings = settings;
            this.portName = portName;
            StopCommand = new DelegateCommand(OnStop);
        }

        #endregion

        #region properties

        public ICommand StopCommand { get; private set; }

        public DialogWindow Dlg { get; set; }

        public Exception Exception { get; set; }

        public double Progress
        {
            get { return progress; }
            set { SetProperty(ref progress, value); }
        }

        public abstract bool IsIndeterminate { get; }
        public abstract string Title { get; }
        public abstract DrawingImage Icon { get; }

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        #endregion

        #region methods

        public abstract void OnStop();

        protected string GetMemoryType()
        {
            return settings.MemoryType == MemoryType.FLASH ? "FLASH" : "EEPROM";
        }
        #endregion
    }
}
