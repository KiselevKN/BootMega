using System.Windows;
using BootMega.Interaction.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Module.HexFile;
using Module.IO;
using Module.Menu;
using Module.Settings;
using Module.StatusBar;
using Service.Logging;

namespace BootMega
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new LoggerAdapter();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            GlobalCommands.LastSessionSettingsCommand.Execute(null);
            GlobalCommands.UpdateComPortsListCommand.Execute(null);
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(MenuModule));
            moduleCatalog.AddModule(typeof(StatusBarModule));
            moduleCatalog.AddModule(typeof(SettingsModule));
            moduleCatalog.AddModule(typeof(HexFileManagerModule));
            moduleCatalog.AddModule(typeof(IOModule));
        }
    }
}
