using System;
using System.Linq;
using System.Collections.Generic;
using System.IO.Ports;
using Service.Settings;
using Service.Settings.Enums;
using Service.Settings.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.Prism.Commands;
using Module.Settings.Properties;

namespace Module.Settings.ViewModels.Base
{
    public abstract class SettingsBaseViewModel : BaseViewModel
    {
        #region fields

        protected int indexOfTheSelectedProcessor;
        protected int indexOfTheSelectedBaudRate;
        protected int indexOfTheSelectedParity;
        protected int indexOfTheSelectedStopBits;
        protected int indexOfTheSelectedHeaderTx;
        protected int indexOfTheSelectedHeaderRx;
        protected bool headersAreEqual;

        #endregion

        #region ctors

        public SettingsBaseViewModel(ISettingsRepository settingsRepository)
            : base(settingsRepository)
        {
            ListOfProcessors = settingsRepository.Processors;
            ListOfBaudRates = Enum.GetValues(typeof(BaudRate)).Cast<BaudRate>();
            ListOfParities = Enum.GetValues(typeof(Parity)).Cast<Parity>();
            ListOfStopBits = Enum.GetValues(typeof(StopBits)).Cast<StopBits>();
            ListOfAllowableHeaders = SerialPortSettings.AllowableHeadersList;

            IndexOfTheSelectedHeaderTx = 1;
            IndexOfTheSelectedHeaderRx = 0;
        }

        #endregion

        #region properties

        #region public IEnumerable<string> ListOfProcessors

        public IEnumerable<Processor> ListOfProcessors { get; private set; }

        #endregion

        #region public int IndexOfTheSelectedProcessor

        public int IndexOfTheSelectedProcessor
        {
            get { return indexOfTheSelectedProcessor; }
            set { SetProperty(ref indexOfTheSelectedProcessor, value); }
        }

        #endregion

        #region public IEnumerable<BaudRate> ListOfBaudRates

        public IEnumerable<BaudRate> ListOfBaudRates { get; private set; }

        #endregion

        #region public int IndexOfTheSelectedBaudRate

        public int IndexOfTheSelectedBaudRate
        {
            get { return indexOfTheSelectedBaudRate; }
            set { SetProperty(ref indexOfTheSelectedBaudRate, value); }
        }

        #endregion

        #region public IEnumerable<Parity> ListOfParities

        public IEnumerable<Parity> ListOfParities { get; private set; }

        #endregion

        #region public int IndexOfTheSelectedParity

        public int IndexOfTheSelectedParity
        {
            get { return indexOfTheSelectedParity; }
            set { SetProperty(ref indexOfTheSelectedParity, value); }
        }

        #endregion

        #region public IEnumerable<StopBits> ListOfStopBits

        public IEnumerable<StopBits> ListOfStopBits { get; private set; }

        #endregion

        #region public int IndexOfTheSelectedStopBits

        public int IndexOfTheSelectedStopBits
        {
            get { return indexOfTheSelectedStopBits; }
            set { SetProperty(ref indexOfTheSelectedStopBits, value); }
        }

        #endregion

        #region public IEnumerable<string> ListOfAllowableHeaders

        public IEnumerable<byte> ListOfAllowableHeaders { get; private set; }

        #endregion

        #region public int IndexOfTheSelectedHeaderTx

        [CustomValidation(typeof(SettingsBaseViewModel), "CheckIndexOfTheSelectedHeaderTx")]
        public int IndexOfTheSelectedHeaderTx
        {
            get { return indexOfTheSelectedHeaderTx; }
            set 
            { 
                SetProperty(ref indexOfTheSelectedHeaderTx, value);
                ValidateProperty("IndexOfTheSelectedHeaderRx");
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region public int IndexOfTheSelectedHeaderRx

        [CustomValidation(typeof(SettingsBaseViewModel), "CheckIndexOfTheSelectedHeaderRx")]
        public int IndexOfTheSelectedHeaderRx
        {
            get { return indexOfTheSelectedHeaderRx; }
            set 
            { 
                SetProperty(ref indexOfTheSelectedHeaderRx, value);
                ValidateProperty("IndexOfTheSelectedHeaderTx");
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #endregion

        #region methods

        public static ValidationResult CheckIndexOfTheSelectedHeaderTx(int value, ValidationContext context)
        {
            var c = (SettingsBaseViewModel)context.ObjectInstance;
            return (value == c.IndexOfTheSelectedHeaderRx) ? new ValidationResult(Resources.HeadersCanNotBeEqual) : ValidationResult.Success;
        }

        public static ValidationResult CheckIndexOfTheSelectedHeaderRx(int value, ValidationContext context)
        {
            var c = (SettingsBaseViewModel)context.ObjectInstance;
            return (value == c.IndexOfTheSelectedHeaderTx) ? new ValidationResult(Resources.HeadersCanNotBeEqual) : ValidationResult.Success;
        }

        public virtual SettingsInfo GetNewSettingsInfo()
        {
            var processor = ListOfProcessors.ElementAt(IndexOfTheSelectedProcessor);
            var serialPortSettings = new SerialPortSettings(ListOfBaudRates.ElementAt(IndexOfTheSelectedBaudRate), 
                ListOfParities.ElementAt(IndexOfTheSelectedParity), ListOfStopBits.ElementAt(IndexOfTheSelectedStopBits), 
                ListOfAllowableHeaders.ElementAt(IndexOfTheSelectedHeaderTx), ListOfAllowableHeaders.ElementAt(IndexOfTheSelectedHeaderRx));

            var s = new SettingsInfo(0, String.Empty, processor, serialPortSettings);

            return s;
        }

        #endregion
    }
}
