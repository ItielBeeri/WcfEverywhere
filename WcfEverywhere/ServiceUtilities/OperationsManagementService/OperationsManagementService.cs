using ServiceUtilities.OperationsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceUtilities.OperationsManagementService
{
    public class OperationsManagementService : IOperationsManagementService
    {
        private readonly IOperationsManager _operationsManager;

        public OperationsManagementService(IOperationsManager operationsManager)
        {
            _operationsManager = operationsManager;
        }

        public Guid[] GetOperationsCompleted()
        {
            return _operationsManager.GetCompletedOperations();
        }

        public void SetOperationCancelFlag(Guid operationId)
        {
            _operationsManager.SetOperationCancelFlag(operationId);
        }
    }
}
