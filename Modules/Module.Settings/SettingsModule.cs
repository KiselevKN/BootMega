using Service.Settings;
using Service.Settings.XmlFile;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Logging;
using Module.Settings.Properties;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Module.Settings
{
    public class SettingsModule : IModule
    {
        private IUnityContainer container;
        private ILoggerFacade logger;

        public SettingsModule(IUnityContainer container, ILoggerFacade logger)
        {
            this.container = container;
            this.logger = logger;
        }

        public void Initialize()
        {
            logger.Log(Resources.InitializingSettingsModule, Category.Debug, Priority.None);
            container.RegisterType<IXmlFileValidator, XmlFileSchemaValidator>(new InjectionConstructor("SettingsSchema.xsd"));
            container.RegisterType<IXmlFileManager, XmlFileManager>(new InjectionConstructor(container.Resolve<IXmlFileValidator>(), "Settings.xml"));
            container.RegisterType<ISettingsRepository, SettingsRepository>();
            container.AddNewExtension<Interception>();
            container.RegisterType<ISettingsManager, SettingsManager>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<ExceptionInterceptor>());
            container.RegisterType<WorkerWithSettings>(new PerThreadLifetimeManager());
            container.Resolve<WorkerWithSettings>();
        }
    }
}
