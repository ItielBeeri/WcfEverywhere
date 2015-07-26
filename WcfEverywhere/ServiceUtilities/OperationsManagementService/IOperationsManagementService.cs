using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceUtilities.OperationsManagementService
{
    public interface IOperationsManagementService
    {
        void SetOperationCancelFlag(Guid operationId);

        Guid[] GetOperationsCompleted();
    }
}
