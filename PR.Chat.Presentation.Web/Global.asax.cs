using System;
using System.Configuration;
using System.Web;
using PR.Chat.Infrastructure;

namespace PR.Chat.Presentation.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            InitializeIoC();
            
            Bootstrapper.Run();
        }

        protected void InitializeIoC()
        {
            var containerConfiguratorTypeName = ConfigurationManager.AppSettings.Get("ContainerConfigurator");
            var containerConfiguratorType = Type.GetType(containerConfiguratorTypeName, true);
            var containerConfigurator = (IDependencyResolverFactory)Activator.CreateInstance(containerConfiguratorType);

            IoC.InitializeWith(containerConfigurator); 
        }
    }
}