using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Module.Settings.Properties;
using Module.Settings.ViewModels.Base;
using Service.Settings;
using Service.Settings.Enums;
using Service.Settings.Models;
using Module.Settings.Extensions;

namespace Module.Settings.ViewModels
{
    public class SelectSettingsViewModel : BaseViewModel
    {
        #region fields

        private int indexOfSelectedSettings = -1;
        private MemoryType memoryType;

        #endregion

        #region ctors

        public SelectSettingsViewModel(ISettingsRepository settingsRepository) : base(settingsRepository)
        {
            MemoryType = MemoryType.FLASH;
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

                return resourceDictionary["SelectSettingsWhiteIcon"] as DrawingImage;
            }
        }

        #endregion

        #region public override string Title
        
        public override string Title 
        { 
            get 
            { 
                return Resources.TitleSelectSettings; 
            } 
        }

        #endregion

        #region public MemoryType MemoryType

        public MemoryType MemoryType
        {
            get { return memoryType; }
            set 
            {
                SetProperty(ref memoryType, value);
                ValidateProperty("IndexOfSelectedSettings");
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
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

        [CustomValidation(typeof(SelectSettingsViewModel), "CheckIndexOfSelectedSettings")]
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

        public static ValidationResult CheckIndexOfSelectedSettings(int value, ValidationContext context)
        {
            var c = (SelectSettingsViewModel)context.ObjectInstance;

            var condition = ((c.settingsRepository.SelectedSettings.MemoryType == c.MemoryType) &&
                (value == c.ListOfSettings.IndexWhere(n=>n.Equals(c.settingsRepository.SelectedSettings.SettingsInfo))));
            
            return (condition) ? new ValidationResult(Resources.CanNotSelectTheCurrentSettings) : ValidationResult.Success;
        }

        #endregion
    }
}
