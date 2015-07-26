using System;
using ServiceUtilities;

namespace SevriceContract
{
    public interface IDoSomethingService
    {
        OperationResult<SomeResult> DoSomething(SomeParameters parmaters);

        OperationStartInformation DoSomethingAsync(SomeParameters parmaters);

        OperationResult<SomeResult> DoSomethingGetResult(Guid guid);
    }
}