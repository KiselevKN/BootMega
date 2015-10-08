using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Module.Settings.Properties;
using Module.Settings.ViewModels.Base;
using Service.Settings;
using Service.Settings.Models;

namespace Module.Settings.ViewModels
{
    public class RemoveSettingsViewModel : BaseViewModel
    {
        #region fields

        private int indexOfSelectedSettings = -1;

        #endregion

        #region ctors

        public RemoveSettingsViewModel(ISettingsRepository settingsRepository) : base(settingsRepository)
        {
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

                return resourceDictionary["RemoveSettingsWhiteIcon"] as DrawingImage;
            }
        }

        #endregion

        #region public override string Title

        public override string Title 
        { 
            get 
            { 
                return Resources.TitleRemoveSettings; 
            } 
        }

        #endregion

        #region public IEnumerable<SettingsInfo> ListOfSettings

        public IEnumerable<SettingsInfo> ListOfSettings
        {
            get { return settingsRepository.Settings; }
        }

        #endregion

        #region public int IndexOfSelectedSettings

        [CustomValidation(typeof(RemoveSettingsViewModel), "CheckIndexOfSelectedSettings")]
        public int IndexOfSelectedSettings
        {
            get { return indexOfSelectedSettings; }
            set
            {
                SetProperty(ref indexOfSelectedSettings, value);
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #endregion

        #region methods

        public static ValidationResult CheckIndexOfSelectedSettings(int index, ValidationContext context)
        {
            var s = (RemoveSettingsViewModel)context.ObjectInstance;
            if (s.ListOfSettings.ToList()[index].Id == s.settingsRepository.SelectedSettings.SettingsInfo.Id)
                return new ValidationResult(Resources.CanNotDeleteTheCurrentSettings);
            else
                return ValidationResult.Success;
        }

        #endregion
    }
}
