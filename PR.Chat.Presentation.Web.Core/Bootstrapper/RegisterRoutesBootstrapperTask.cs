using System.Web.Routing;
using System.Web.Mvc;
using PR.Chat.Infrastructure;

namespace PR.Chat.Presentation.Web.Core
{
    public class RegisterRoutesBootstrapperTask : IBootstrapperTask
    {
        public void Run()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }

        
    }
}