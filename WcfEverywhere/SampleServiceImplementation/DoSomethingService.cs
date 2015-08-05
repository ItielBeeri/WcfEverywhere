using System;
using System.Security.Permissions;
using SampleServiceContract;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace SampleServiceImplementation
{
    public class DoSomethingService : IDoSomethingService, IDoSomethingLoggedIn
    {
        private readonly IOperationsManager _operationsManager;


        public DoSomethingService(IOperationsManager operationsManager)
        {
            _operationsManager = operationsManager;
        }

       // [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public OperationResult<SomeResult> DoSomething(SomeParameters parmaters)
        {
            var operation = new DoSomethingOperation(_operationsManager, parmaters);
            return operation.RunSync();
        }

        //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public OperationStartInformation DoSomethingAsync(SomeParameters parmaters)
        {
            var operation = new DoSomethingOperation(_operationsManager, parmaters);
            var operationStart = operation.RunAsync();
            return new OperationStartInformation
            {
                OperationId = operationStart.OperationId,
                IsReportingProgress = false,
                IsSupportingCancel = false,
            };
        }

       // [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
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
