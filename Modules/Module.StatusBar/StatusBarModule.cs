using Module.StatusBar.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Logging;
using Module.StatusBar.Properties;

namespace Module.StatusBar
{
    public class StatusBarModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly ILoggerFacade logger;

        public StatusBarModule(IRegionManager regionManager, ILoggerFacade logger)
        {
            this.regionManager = regionManager;
            this.logger = logger;
        }

        public void Initialize()
        {
            logger.Log(Resources.InitializingStatusBarModule, Category.Debug, Priority.None);
            regionManager.RegisterViewWithRegion("StatusBarRegion", typeof(StatusBarView));
        }
    }
}
