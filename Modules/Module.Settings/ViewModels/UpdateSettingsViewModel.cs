using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using Module.Settings.Properties;
using Module.Settings.ViewModels.Base;
using Service.Settings;
using Service.Settings.Models;
using Module.Settings.Extensions;

namespace Module.Settings.ViewModels
{
    public class UpdateSettingsViewModel : SettingsBaseViewModel
    {
        #region fields

        private IEnumerable<SettingsInfo> listOfSettings;
        private int indexOfSelectedSettings = -1;

        #endregion

        #region ctors

        public UpdateSettingsViewModel(ISettingsRepository settingsRepository) : base(settingsRepository)
        {
            listOfSettings = settingsRepository.Settings;
            IndexOfSelectedSettings = 0;
        }

        #endregion

        #region properties

        #region public override DrawingImage Icon

        public override DrawingImage Icon
        {
            get
            {
                var resourceDictionary = new ResourceDictionary();
                resourceDictionary.Source =
                    new Uri("/BootMega.Theme;component/Themes/Generic.xaml",
                            UriKind.RelativeOrAbsolute);

                return resourceDictionary["UpdateSettingsWhiteIcon"] as DrawingImage;
            }
        }

        #endregion

        #region public override string Title

        public override string Title 
        { 
            get 
            { 
                return Resources.TitleUpdateSettings; 
            } 
        }

        #endregion

        #region public IEnumerable<SettingsInfo> ListOfSettings

        public IEnumerable<SettingsInfo> ListOfSettings
        {
            get { return listOfSettings; }
        }

        #endregion

        #region public int IndexOfSelectedSettings

        [CustomValidation(typeof(UpdateSettingsViewModel), "CheckIndexOfSelectedSettings")]
        public int IndexOfSelectedSettings
        {
            get { return indexOfSelectedSettings; }
            set
            {
                SetProperty(ref indexOfSelectedSettings, value);
                IndexOfTheSelectedProcessor = ListOfProcessors.IndexWhere(n=>n.Equals(listOfSettings.ElementAt(value).Processor));
                IndexOfTheSelectedBaudRate = ListOfBaudRates.IndexWhere(n => n == listOfSettings.ElementAt(value).SerialPortSettings.BaudRate);
                IndexOfTheSelectedParity = ListOfParities.IndexWhere(n => n == listOfSettings.ElementAt(value).SerialPortSettings.Parity);
                IndexOfTheSelectedStopBits = ListOfStopBits.IndexWhere(n => n == listOfSettings.ElementAt(value).SerialPortSettings.StopBits);
                IndexOfTheSelectedHeaderTx = ListOfAllowableHeaders.IndexWhere(n => n == listOfSettings.ElementAt(value).SerialPortSettings.HeaderTX);
                IndexOfTheSelectedHeaderRx = ListOfAllowableHeaders.IndexWhere(n => n == listOfSettings.ElementAt(value).SerialPortSettings.HeaderRX);
            }
        }

        #endregion

        #endregion

        #region methods

        public static ValidationResult CheckIndexOfSelectedSettings(int index, ValidationContext context)
        {
            var s = (UpdateSettingsViewModel)context.ObjectInstance;
            if (s.ListOfSettings.ElementAt(index).Id == s.settingsRepository.SelectedSettings.SettingsInfo.Id)
                return new ValidationResult(Resources.CanNotUpdateTheCurrentSettings);
            else
                return ValidationResult.Success;
        }

        public override SettingsInfo GetNewSettingsInfo()
        {
            var s = base.GetNewSettingsInfo();

            var oldS = listOfSettings.ElementAt(IndexOfSelectedSettings);
            s.Id = oldS.Id;
            s.Name = oldS.Name;

            return s;
        }

        public SettingsInfo GetOldSettingsInfo()
        {
            return listOfSettings.ElementAt(IndexOfSelectedSettings);
        }

        #endregion
    }
}
