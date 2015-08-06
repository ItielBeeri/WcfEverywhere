using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Ziv.ServiceModel.Operations;

namespace SampleServiceContract
{
    [ServiceContract]
   public interface IDoSomethingAuthenticated
    {
        [OperationContract]
        OperationResult<SomeResult> DoSomethingAuthenticated(SomeParameters parmaters);

        [OperationContract]
        OperationStartInformation DoSomethingAuthenticatedAsync(SomeParameters parmaters);

        [OperationContract]
        OperationResult<SomeResult> DoSomethingAuthenticatedGetResult(Guid guid);

        [OperationContract]
        void Login();
    }
}
