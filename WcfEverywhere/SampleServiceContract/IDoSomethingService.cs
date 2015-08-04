using System;
using System.ServiceModel;
using Ziv.ServiceModel.Operations;

namespace SampleServiceContract
{
    [ServiceContract]
    public interface IDoSomethingService
    {
        [OperationContract]
        OperationResult<SomeResult> DoSomething(SomeParameters parmaters);

        [OperationContract]
        OperationStartInformation DoSomethingAsync(SomeParameters parmaters);

        [OperationContract]
        OperationResult<SomeResult> DoSomethingGetResult(Guid guid);

    }
}