using Service.Settings.Models;
using System.Collections.Generic;

namespace Service.Settings
{
    public interface ISettingsRepository
    {
        IEnumerable<SettingsInfo> Settings { get; }
        void AddNewSettings(SettingsInfo settingsInfo);
        void RemoveSettings(SettingsInfo settingsInfo);
        void UpdateSettings(SettingsInfo settingsInfo);
        SelectedSettings SelectedSettings { get; set; }
        IEnumerable<Processor> Processors { get; }
    }
}
