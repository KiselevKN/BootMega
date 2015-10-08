using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Logging;
using Module.IO.Properties;

namespace Module.IO
{
    public class IOModule : IModule
    {
        private IUnityContainer container;
        private ILoggerFacade logger;

        public IOModule(IUnityContainer container, ILoggerFacade logger)
        {
            this.container = container;
            this.logger = logger;
        }

        public void Initialize()
        {
            logger.Log(Resources.InitializingIOModule, Category.Debug, Priority.None);
            container.RegisterType<IOManager>(new PerThreadLifetimeManager());
            container.Resolve<IOManager>();
        }
    }
}
