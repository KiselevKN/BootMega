using System;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Logging;
using Module.ToolBar.Views;
using Module.ToolBar.Properties;

namespace Module.ToolBar
{
    public class ToolBarModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly ILoggerFacade logger;

        public ToolBarModule(IRegionManager regionManager, ILoggerFacade logger)
        {
            this.regionManager = regionManager;
            this.logger = logger;
        }

        public void Initialize()
        {
            logger.Log(Resources.InitializingToolBarModule, Category.Debug, Priority.None);
            regionManager.RegisterViewWithRegion("ToolBarRegion", typeof(ToolBarView));
        }
    }
}
