using System;
using System.ServiceModel;
using PR.Chat.Application;

namespace PR.Chat.Presentation.Services
{
    [ServiceContract]
    public interface IMembershipService
    {
        [OperationContract]
        EnterResult EnterAsUnregistered(string nickName);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginAsyncOperation(AsyncCallback asyncCallback, object asyncState);

        string EndAsyncOperation(IAsyncResult asyncResult);
    }
}