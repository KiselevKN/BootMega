using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using System.Text;
using System.Windows.Input;
using BootMega.Interaction.Events;
using BootMega.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Service.Settings.Models;
using BootMega.Interaction.Commands;
using Module.Menu.Properties;

namespace Module.Menu.ViewModels
{
    public class ToolBarViewModel : ValidatableBindableBase
    {
        #region fields

        private IEventAggregator eventAggregator;
        private ILoggerFacade logger;

        private List<String> ports;
        private ICommand updateComPortsListCommand;
        private int? selectedPortIndex = null;
        private bool memoryType;

        #endregion

        #region ctors

        public ToolBarViewModel(IEventAggregator eventAggregator, ILoggerFacade logger)
	    {
            this.eventAggregator = eventAggregator;
            this.logger = logger;

            if (this.eventAggregator != null)
                this.eventAggregator.GetEvent<SelectedSettingsEvent>().Subscribe(OnChangeSelectedSettings);

            Ports = new List<string>();

            updateComPortsListCommand = new DelegateCommand(OnUpdateComPortsList);
            GlobalCommands.UpdateComPortsListCommand.RegisterCommand(updateComPortsListCommand);
	    }

        #endregion

        #region methods

        private void OnChangeSelectedSettings(SelectedSettings obj)
        {
            MemoryType = obj.MemoryType == Service.Settings.Enums.MemoryType.FLASH;
        }

        private void OnUpdateComPortsList()
        {
            var p = SerialPort.GetPortNames();

            if (p != null && p.Count() > 0)
                Ports = p.ToList<string>();

            if (Ports.Count == 0)
            {
                eventAggregator.GetEvent<ComPortsNotFoundEvent>().Publish(null);
                logger.Log(Resources.PortsNotFound, Category.Debug, Priority.None);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Ports.Count(); i++)
                {
                    sb.Append(Ports[i]);
                    if (i != (Ports.Count() - 1)) 
                        sb.Append(", ");
                }
                logger.Log(String.Format(Resources.FoundTheFollowingPorts, sb.ToString()), Category.Debug, Priority.None);
                SelectedPortIndex = 0;
            }
        }

        public static ValidationResult CheckPorts(List<String> ports, ValidationContext context)
        {
            var vm = (ToolBarViewModel)context.ObjectInstance;
            if ((ports == null) || ((ports != null) && (ports.Count == 0)))
                return new ValidationResult(Resources.PortsNotFound);
            else
                return ValidationResult.Success;
        }

        #endregion

        #region properties

        [Required]
        [CustomValidation(typeof(ToolBarViewModel), "CheckPorts")]
        public List<String> Ports
        {
            get { return ports; }
            set { SetProperty(ref ports, value); }
        }

        public bool MemoryType
        {
            get { return memoryType; }
            set { SetProperty(ref memoryType, value); }
        }

        public int? SelectedPortIndex
        {
            get { return selectedPortIndex; }
            set 
            { 
                if(SetProperty(ref selectedPortIndex, value))
                {
                    if (value != null)
                    {
                        eventAggregator.GetEvent<SelectedComPortEvent>().Publish(Ports[(int)value]);
                        logger.Log(String.Format(Resources.SelectTheFollowingPort, Ports[(int)value]), Category.Debug, Priority.None);
                    }
                }
            }
        }

        #endregion
    }
}
