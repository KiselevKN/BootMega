using System;
using System.Linq;
using Service.Settings;
using Service.Settings.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using Module.Settings.Properties;
using System.Windows.Media;
using Module.Settings.ViewModels.Base;

namespace Module.Settings.ViewModels
{
    public class AddNewSettingsViewModel : SettingsBaseViewModel
    {
        #region fields

        private string name;

        #endregion

        #region ctors

        public AddNewSettingsViewModel(ISettingsRepository settingsRepository) : base(settingsRepository)
        {
            Name = string.Empty;
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

                return resourceDictionary["AddNewSettingsWhiteIcon"] as DrawingImage;
            } 
        }

        #endregion

        #region public override string Title

        public override string Title 
        { 
            get 
            { 
                return Resources.TitleAddNewSettings; 
            } 
        }

        #endregion

        #region public String Name

        [CustomValidation(typeof(AddNewSettingsViewModel), "CheckName")]
        public string Name
        {
            get { return name; }
            set 
            {
                SetProperty(ref name, value);
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #endregion

        #region methods

        public static ValidationResult CheckName(string name, ValidationContext context)
        {
            var s = (AddNewSettingsViewModel)context.ObjectInstance;
            var settings = s.settingsRepository.Settings.ToList();

            if (name == null || name == string.Empty || name.Trim() == string.Empty)
                return new ValidationResult(Resources.NameСanNotBeEmpty);
            else if (settings.Exists(n => n.Name == name))
                return new ValidationResult(Resources.SettingsWithTheSameNameAlreadyExists);
            else
                return ValidationResult.Success;
        }

        public override SettingsInfo GetNewSettingsInfo()
        {
            var s = base.GetNewSettingsInfo();
            s.Name = Name.Trim();

            return s;
        }

        #endregion
    }
}
