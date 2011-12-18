using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PR.Chat.Presentation.Web.Services
{
    [ServiceContract]
    public interface ITest
    {
        [OperationContract]
        string DoWork();
    }
}
