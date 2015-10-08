using System.Windows.Input;
using BootMega.Interaction.Commands;
using Microsoft.Practices.Prism.Commands;

namespace Module.Settings
{
    public class WorkerWithSettings
    {
        #region fields

        ISettingsManager settingsManager;

        #endregion

        #region ctors

        public WorkerWithSettings(ISettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;

            lastSessionSettingsCommand = new DelegateCommand(OnLastSessionSettings);
            GlobalCommands.LastSessionSettingsCommand.RegisterCommand(lastSessionSettingsCommand);
        }

        #endregion

        #region commands

        private ICommand selectSettingsCommand;
        private ICommand removeSettingsCommand;
        private ICommand addNewSettingsCommand;
        private ICommand lastSessionSettingsCommand;
        private ICommand updateSettingsCommand;

        #endregion

        #region methods

        private void OnLastSessionSettings()
        {
            settingsManager.LastSessionSettings();

            selectSettingsCommand = new DelegateCommand(OnSelectSettings);
            GlobalCommands.SelectSettingsCommand.RegisterCommand(selectSettingsCommand);

            removeSettingsCommand = new DelegateCommand(OnRemoveSettings);
            GlobalCommands.RemoveSettingsCommand.RegisterCommand(removeSettingsCommand);

            addNewSettingsCommand = new DelegateCommand(OnAddNewSettings);
            GlobalCommands.AddNewSettingsCommand.RegisterCommand(addNewSettingsCommand);

            updateSettingsCommand = new DelegateCommand(OnUpdateSettings);
            GlobalCommands.UpdateSettingsCommand.RegisterCommand(updateSettingsCommand);
        }

        private void OnUpdateSettings()
        {
            settingsManager.UpdateSettings();
        }

        private void OnRemoveSettings()
        {
            settingsManager.RemoveSettings();
        }

        private void OnAddNewSettings()
        {
            settingsManager.AddNewSettings();
        }

        private void OnSelectSettings()
        {
            settingsManager.SelectSettings();
        }

        #endregion
    }
}
