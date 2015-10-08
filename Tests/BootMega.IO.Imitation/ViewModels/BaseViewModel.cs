using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Service.Settings.Models;

namespace BootMega.IO.Imitation.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        protected string portName;
        protected ILoggerFacade logger;
        protected SerialPortSettings serialPortSettings;

        public BaseViewModel(string portName, SerialPortSettings serialPortSettings, ILoggerFacade logger)
        {
            StartCommand = new DelegateCommand(OnStart, CanExecuteStart);
            StopCommand = new DelegateCommand(OnStop, CanExecuteStop);
            this.portName = portName;
            this.serialPortSettings = serialPortSettings;
            this.logger = logger;
        }

        public abstract bool CanExecuteStart();
        public abstract void OnStart();

        public abstract bool CanExecuteStop();
        public abstract void OnStop();

        protected void RaiseCanExecuteChanged()
        {
            ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)StopCommand).RaiseCanExecuteChanged();
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }
    }
}
