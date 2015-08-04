using System;
using SampleServiceContract;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;
using Ziv.ServiceModel.Behaviors;

namespace SampleServiceImplementation
{
    [CustomInstanceProviderServiceBehaviorAttribute]
    public class DoSomethingService : IDoSomethingService
    {
        private readonly IOperationsManager _operationsManager;

        public DoSomethingService(IOperationsManager operationsManager)
        {
            _operationsManager = operationsManager;
        }

        public OperationResult<SomeResult> DoSomething(SomeParameters parmaters)
        {
            var operationId = _operationsManager.RegistrOperation("Do something");
            var operation = new DoSomethingOperation(_operationsManager, operationId, parmaters);
            var handler = operation.RunAsync();
            handler.WaitOne();
            return DoSomethingGetResult(operationId);
        }

        public OperationStartInformation DoSomethingAsync(SomeParameters parmaters)
        {
            var guid = _operationsManager.RegistrOperation("Do something");
            var operation = new DoSomethingOperation(_operationsManager, guid, parmaters);
            operation.RunAsync();
            return new OperationStartInformation
            {
                OperationId = guid,
                IsReportingProgress = false,
                IsSupportingCancel = false,
            };
        }

        public OperationResult<SomeResult> DoSomethingGetResult(Guid operationId)
        {
            var objectResult = _operationsManager.GetOperationResult(operationId);
            return new OperationResult<SomeResult>(
                (SomeResult)objectResult.Result,
                objectResult.Info
                );
        }

    }
}
