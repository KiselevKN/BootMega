namespace Module.Settings
{
    public interface ISettingsManager
    {
        void LastSessionSettings();
        void UpdateSettings();
        void RemoveSettings();
        void AddNewSettings();
        void SelectSettings();
    }
}
