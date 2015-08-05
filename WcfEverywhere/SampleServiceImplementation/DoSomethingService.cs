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
            var operationId = _operationsManager.RegistrOperation("Do something");
            var operation = new DoSomethingOperation(_operationsManager, operationId, parmaters);
            var handler = operation.RunAsync();
            handler.WaitOne();
            return DoSomethingGetResult(operationId);
        }

        //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
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
