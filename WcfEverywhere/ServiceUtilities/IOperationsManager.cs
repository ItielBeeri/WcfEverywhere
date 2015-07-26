using System;

namespace ServiceUtilities
{
    public interface IOperationsManager
    {
        Guid RegistrOperation(string displayName);

        int GetOperationProgress(Guid operationId);

        bool GetIsOperationComplete(Guid operationId);

        object GetOperationResult(Guid operationId);

        void SetOperationResult(Guid operationGuid, object result);

        void SetOperationProgress(Guid operationGuid, int progressPercent);
    }
}