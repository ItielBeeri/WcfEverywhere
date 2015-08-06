using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.ServiceModel;

namespace SampleServiceContract
{
    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        void Login(string username, string password);
    }
}
