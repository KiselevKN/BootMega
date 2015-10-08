using Microsoft.Practices.Prism.Commands;

namespace BootMega.Interaction.Commands
{
    public static class GlobalCommands
    {
        public static CompositeCommand LastSessionSettingsCommand = new CompositeCommand();
        
        public static CompositeCommand SelectSettingsCommand = new CompositeCommand();
        public static CompositeCommand AddNewSettingsCommand = new CompositeCommand();
        public static CompositeCommand RemoveSettingsCommand = new CompositeCommand();
        public static CompositeCommand UpdateSettingsCommand = new CompositeCommand();

        public static CompositeCommand UpdateComPortsListCommand = new CompositeCommand();

        public static CompositeCommand OpenHexFileCommand = new CompositeCommand();
        public static CompositeCommand SaveHexFileCommand = new CompositeCommand();
        public static CompositeCommand SaveAllHexFilesCommand = new CompositeCommand();
        public static CompositeCommand CloseHexFileCommand = new CompositeCommand();
        public static CompositeCommand CloseAllHexFilesCommand = new CompositeCommand();
        public static CompositeCommand CompareHexFilesCommand = new CompositeCommand();

        public static CompositeCommand CloseAppCommand = new CompositeCommand();

        public static CompositeCommand ConnectionCommand = new CompositeCommand();
        public static CompositeCommand DownloadCommand = new CompositeCommand();
        public static CompositeCommand UploadCommand = new CompositeCommand();
        public static CompositeCommand EraseCommand = new CompositeCommand();
        public static CompositeCommand ReadFuseCommand = new CompositeCommand();
        public static CompositeCommand ReadLockCommand = new CompositeCommand();
        public static CompositeCommand ResetCommand = new CompositeCommand();
    }
}
