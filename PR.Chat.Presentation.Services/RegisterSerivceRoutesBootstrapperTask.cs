using System.ServiceModel.Activation;
using System.Web.Routing;
using PR.Chat.Infrastructure;

namespace PR.Chat.Presentation.Services
{
    public class RegisterSerivceRoutesBootstrapperTask : IBootstrapperTask
    {
        private readonly ServiceHostFactoryBase _serviceHostFactoryBase;

        public RegisterSerivceRoutesBootstrapperTask(ServiceHostFactoryBase serviceHostFactoryBase)
        {
            _serviceHostFactoryBase = serviceHostFactoryBase;
        }

        public void Run()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(
                new ServiceRoute(
                    "membership",
                    _serviceHostFactoryBase,
                    typeof(IMembershipService)
                )
            );
        }
    }
}