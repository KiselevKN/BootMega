using Module.HexFile.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Service.HexFile;
using Microsoft.Practices.Prism.Logging;
using Module.HexFile.Properties;

namespace Module.HexFile
{
    public class HexFileManagerModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private readonly ILoggerFacade logger;

        public HexFileManagerModule(IUnityContainer container, IRegionManager regionManager, ILoggerFacade logger)
        {
            this.container     = container;
            this.regionManager = regionManager;
            this.logger = logger;
        }

        public void Initialize()
        {
            logger.Log(Resources.InitializingHexFileManagerModule, Category.Debug, Priority.None);
            container.RegisterType<IHexFileManager, HexFileManager>();
            regionManager.RegisterViewWithRegion("HexFileManagerRegion", typeof(HexFileManagerView));
        }
    }
}
