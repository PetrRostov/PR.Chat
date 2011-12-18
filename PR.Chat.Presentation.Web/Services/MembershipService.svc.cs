using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using PR.Chat.Application;
using IMembershipService = PR.Chat.Presentation.Services.IMembershipService;

namespace PR.Chat.Presentation.Web.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class MembershipService : IMembershipService
    {
        private readonly Application.IMembershipService _membershipService;

        public MembershipService(Application.IMembershipService membershipService )
        {
            _membershipService = membershipService;
        }

        [WebGet(UriTemplate = "/enterAsUnregistered/{nickName}")]
        public EnterResult EnterAsUnregistered(string nickName)
        {
            return _membershipService.EnterAsUnregistered(nickName);
        }
    }
}
