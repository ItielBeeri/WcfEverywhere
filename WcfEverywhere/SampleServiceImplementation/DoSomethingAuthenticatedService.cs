using SampleServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.ServiceModel;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace SampleServiceImplementation
{
    public class DoSomethingAuthenticatedService:IDoSomethingAuthenticated
    {
        private readonly IOperationsManager _operationsManager;


        public DoSomethingAuthenticatedService(IOperationsManager operationsManager)
        {
            _operationsManager = operationsManager;
        }

       // [OperationBehavior(Impersonation =ImpersonationOption.Required)]
       // [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public OperationResult<SomeResult> DoSomethingAuthenticated(SomeParameters parmaters)
        {
            var operation = new DoSomethingAuthenticatedOperation(_operationsManager, parmaters);
            return operation.RunSync();
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public OperationStartInformation DoSomethingAuthenticatedAsync(SomeParameters parmaters)
        {
            var operation = new DoSomethingAuthenticatedOperation(_operationsManager, parmaters);
            var operationStart = operation.RunAsync();
            return new OperationStartInformation
            {
                OperationId = operationStart.OperationId,
                IsReportingProgress = false,
                IsSupportingCancel = false,
            };
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public OperationResult<SomeResult> DoSomethingAuthenticatedGetResult(Guid operationId)
        {
            var objectResult = _operationsManager.GetOperationResult(operationId);
            return new OperationResult<SomeResult>(
                (SomeResult)objectResult.Result,
                objectResult.Info
                );
        }

        public void Login()
        {

        }
    }
}
