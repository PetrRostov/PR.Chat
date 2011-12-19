using System.ServiceModel;
using PR.Chat.Application;

namespace PR.Chat.Presentation.Services
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IMembershipService
    {
        [OperationContract]
        EnterResult EnterAsUnregistered(string nickName);
    }
}