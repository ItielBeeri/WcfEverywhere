using System;
using Ziv.ServiceModel.Operations;

namespace SevriceContract
{
    public interface IDoSomethingService
    {
        OperationResult<SomeResult> DoSomething(SomeParameters parmaters);

        OperationStartInformation DoSomethingAsync(SomeParameters parmaters);

        OperationResult<SomeResult> DoSomethingGetResult(Guid guid);
    }
}