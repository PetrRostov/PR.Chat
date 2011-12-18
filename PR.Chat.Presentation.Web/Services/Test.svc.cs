using System;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using PR.Chat.Application;
using PR.Chat.Infrastructure.UnitOfWork;

namespace PR.Chat.Presentation.Web.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Test : ITest
    {
        private readonly IMembershipService _membershipService;

        public Test(IMembershipService membershipService)
        {
            _membershipService = membershipService;
            
        }

        [WebGet(UriTemplate = "/opa")]
        public string DoWork()
        {
            var enterResult = _membershipService.EnterAsUnregistered(Guid.NewGuid().ToString());
            return enterResult.Nick.Name;
        }
    }
}
