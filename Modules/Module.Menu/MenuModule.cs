using Module.Menu.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Logging;
using Module.Menu.Properties;

namespace Module.Menu
{
    public class MenuModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly ILoggerFacade logger;

        public MenuModule(IRegionManager regionManager, ILoggerFacade logger)
        {
            this.regionManager = regionManager;
            this.logger = logger;
        }

        public void Initialize()
        {
            logger.Log(Resources.InitializingMenuModule, Category.Debug, Priority.None);
            regionManager.RegisterViewWithRegion("MenuRegion", typeof(MenuView));
            regionManager.RegisterViewWithRegion("ToolBarRegion", typeof(ToolBarView));
        }
    }
}
