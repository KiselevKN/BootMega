using System;
using BootMega.Interaction.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Service.Settings.Enums;
using Service.Settings.Models;

namespace Module.StatusBar.ViewModels
{
    public class StatusBarViewModel : BindableBase
    {
        #region fields

        private IEventAggregator eventAggregator;
        private ILoggerFacade logger;

        private String settingsName;
        private MemoryType memoryType;
        private String processor;

        private bool hasError = true;

        #endregion

        #region ctors

        public StatusBarViewModel(IEventAggregator eventAggregator, ILoggerFacade logger)
	    {
            this.eventAggregator = eventAggregator;
            this.logger = logger;
            this.eventAggregator.GetEvent<SelectedSettingsEvent>().Subscribe(OnChangeSelectedSettings);
            this.eventAggregator.GetEvent<SettingsExceptionEvent>().Subscribe(OnSettingsException);
	    }

        #endregion

        #region methods

        private void OnChangeSelectedSettings(SelectedSettings obj)
        {
            SettingsName = obj.SettingsInfo.Name;
            MemoryType = obj.MemoryType;
            Processor = obj.SettingsInfo.Processor.Name;
            hasError = false;
        }

        private void OnSettingsException(Exception obj)
        {
            hasError = true;
        }

        #endregion

        #region properties

        public String SettingsName
        {
            get { return settingsName; }
            set { SetProperty(ref settingsName, value); }
        }

        public MemoryType MemoryType
        {
            get { return memoryType; }
            set { SetProperty(ref memoryType, value); }
        }

        public String Processor
        {
            get { return processor; }
            set { SetProperty(ref processor, value); }
        }

        public bool HasError
        {
            get { return hasError; }
            set { SetProperty(ref hasError, value); }
        }

        public bool NotHasError
        {
            get { return !HasError; }
        }

        #endregion
    }
}
