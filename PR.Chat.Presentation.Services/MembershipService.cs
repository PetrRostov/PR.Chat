using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Threading;
using PR.Chat.Application;

namespace PR.Chat.Presentation.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MembershipService : IMembershipService, IDisposable
    {
        private readonly Application.IMembershipService _membershipService;
        private Timer _timer;

        public MembershipService(Application.IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [WebGet(UriTemplate = "/enterAsUnregistered/{nickName}")]
        public EnterResult EnterAsUnregistered(string nickName)
        {
            return _membershipService.EnterAsUnregistered(nickName);
        }

        [WebGet(UriTemplate = "/async")]
        public IAsyncResult BeginAsyncOperation(AsyncCallback callback, object asyncState)
        {
            var result = new AsyncOperationResult {
                Value       = "opa",
                AsyncState  = asyncState
            };
            _timer = new Timer(
                state => {
                    var asyncResult = (AsyncOperationResult) state;
                    asyncResult.IsCompleted = true;
                    callback(asyncResult);
                }, 
                result, 
                5000, 
                int.MaxValue
            );

            return result;
        }

        public string EndAsyncOperation(IAsyncResult asyncResult)
        {
            var result = (AsyncOperationResult) asyncResult;
            
            return result.Value;
        }

        public void Dispose()
        {
            if (_timer != null)
                _timer.Dispose();
        }
    }
}
