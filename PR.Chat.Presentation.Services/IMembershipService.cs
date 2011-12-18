using System.ServiceModel;
using System.ServiceModel.Web;
using PR.Chat.Application;

namespace PR.Chat.Presentation.Services
{
    [ServiceContract]
    public interface IMembershipService
    {
        [OperationContract]
        EnterResult EnterAsUnregistered(string nickName);
    }
}