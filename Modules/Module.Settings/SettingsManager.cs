using System.Linq;
using System.Windows;
using BootMega.Interaction.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Module.Settings.Properties;
using Module.Settings.ViewModels;
using Module.Settings.ViewModels.Base;
using Module.Settings.Views;
using Service.Settings;
using Service.Settings.Models;

namespace Module.Settings
{
    public class SettingsManager : ISettingsManager
    {
        #region fields

        private ISettingsRepository settingsRepository;
        private readonly IEventAggregator eventAggregator;
        private readonly ILoggerFacade logger;

        #endregion

        #region ctors

        public SettingsManager(ISettingsRepository settingsRepository, IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            this.settingsRepository = settingsRepository;
            this.eventAggregator = eventAggregator;
            this.logger = logger;
        }

        #endregion

        #region ISettingsManager Members

        public void LastSessionSettings()
        {
            eventAggregator.GetEvent<SelectedSettingsEvent>().Publish(settingsRepository.SelectedSettings);
            logger.Log(string.Format(Resources.LogSelectedSettings, settingsRepository.SelectedSettings.ToString("LOG", null)), Category.Debug, Priority.None);
        }

        public void UpdateSettings()
        {
            var vm = new UpdateSettingsViewModel(settingsRepository);
            if (ShowDialog(vm))
            {
                var sOld = vm.GetOldSettingsInfo();
                var sNew = vm.GetNewSettingsInfo();

                settingsRepository.UpdateSettings(sNew);

                logger.Log(string.Format(Resources.LogUpdatedSettings, sOld.ToString("LOG", null), sNew.ToString("LOG", null)), Category.Debug, Priority.None);
            }
        }

        public void RemoveSettings()
        {
            var vm = new RemoveSettingsViewModel(settingsRepository);
            if (ShowDialog(vm))
            {
                var s = vm.ListOfSettings.ElementAt(vm.IndexOfSelectedSettings);
                settingsRepository.RemoveSettings(s);
                logger.Log(string.Format(Resources.LogRemovedSettings, s.ToString("LOG", null)), Category.Debug, Priority.None);
            }
        }

        public void AddNewSettings()
        {
            var vm = new AddNewSettingsViewModel(settingsRepository);
            if (ShowDialog(vm))
            {
                var s = vm.GetNewSettingsInfo();
                settingsRepository.AddNewSettings(s);
                logger.Log(string.Format(Resources.LogAddedNewSettings, s.ToString("LOG", null)), Category.Debug, Priority.None);
            }
        }

        public void SelectSettings()
        {
            var vm = new SelectSettingsViewModel(settingsRepository);
            if (ShowDialog(vm))
            {
                settingsRepository.SelectedSettings = new SelectedSettings(vm.MemoryType, vm.ListOfSettings.ToList()[vm.IndexOfSelectedSettings]);
                eventAggregator.GetEvent<SelectedSettingsEvent>().Publish(settingsRepository.SelectedSettings);
                logger.Log(string.Format(Resources.LogSelectedSettings, settingsRepository.SelectedSettings.ToString("LOG", null)), Category.Debug, Priority.None);
            }
        }

        private bool ShowDialog(BaseViewModel vm)
        {
            SettingsView window = new SettingsView(vm);
            window.Owner = Application.Current.MainWindow;
            return (bool)window.ShowDialog();
        }

        #endregion
    }
}
