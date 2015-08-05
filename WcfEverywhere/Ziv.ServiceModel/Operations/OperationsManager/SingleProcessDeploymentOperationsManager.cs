using System;
using System.Collections.Generic;
using System.Linq;

namespace Ziv.ServiceModel.Operations.OperationsManager
{


    public partial class SingleProcessDeploymentOperationsManager : IOperationsManager
    {
        readonly Dictionary<Guid, SingleProcessDeploymentOperationInformation> _operations =
            new Dictionary<Guid, SingleProcessDeploymentOperationInformation>();

        public Guid RegistrOperation(string displayName)
        {
            var info = new SingleProcessDeploymentOperationInformation
            {
                Info = new OperationStatusInformation(OperationState.Started),
                DisplayName = displayName,
            };
            var operationID = Guid.NewGuid();
            _operations.Add(operationID, info);
            return operationID;
        }

        public Guid[] GetCompletedOperations()
        {
            // todo: consider caching completed operations to reduce polling load.
            return _operations.Where(
                op => op.Value.Info.State.IsDone())
                .Select(op => op.Key).ToArray();
        }

        public OperationResult GetOperationResult(Guid operationId)
        {
            SingleProcessDeploymentOperationInformation operation;
            if (_operations.TryGetValue(operationId, out operation))
            {
                if (operation.Info.State.IsDone())
                {
                    SetOperationForCollection(operation);
                }
                return operation;
            }
            else
            {
                return new OperationResult
                {
                    Info = new OperationStatusInformation(OperationState.NoFound)
                };
            }
        }

        public OperationStatusInformation SetOperationCancelFlag(Guid operationId)
        {
            SingleProcessDeploymentOperationInformation operation;
            if (_operations.TryGetValue(operationId, out operation))
            {
                operation.Info.State = OperationState.CancelationPending;
                return operation.Info;
            }
            else
            {
                return new OperationStatusInformation(OperationState.NoFound);
            }
        }

        public void SetOperationProgress(Guid operationId, int progressPercent)
        {
            SingleProcessDeploymentOperationInformation operation;
            if (_operations.TryGetValue(operationId, out operation))
            {
                operation.Info.Progress = progressPercent;
            }
        }

        public void SetOperationResult(Guid operationId, object result)
        {
            SingleProcessDeploymentOperationInformation operation;
            if (_operations.TryGetValue(operationId, out operation))
            {
                lock (operation)
                {
                    operation.Info.State = OperationState.CompletedSucessfully;
                    operation.Result = result;

                }
            }
        }

        public void SetOperationCanceled(Guid operationId)
        {
            SingleProcessDeploymentOperationInformation operation;
            if (_operations.TryGetValue(operationId, out operation))
            {
                operation.Info.State = OperationState.Canceled;
                SetOperationForCollection(operation);
                InvokePreocessDoneEvent(operationId);
            }
        }

        private void SetOperationForCollection(SingleProcessDeploymentOperationInformation operation)
        {
            operation.IsPendingCollection = true;
        }

        public void SetOperationFailed(Guid operationId, Exception ex)
        {
            SingleProcessDeploymentOperationInformation operation;
            if (_operations.TryGetValue(operationId, out operation))
            {
                lock (operation)
                {
                    operation.Info.State = OperationState.Failed;
                    operation.Info.FailureMessage = ex.Message;
                }
                InvokePreocessDoneEvent(operationId);
            }
        }

        public bool GetIsOperationFlagedToCancel(Guid operationId)
        {
            SingleProcessDeploymentOperationInformation operation;
            return _operations.TryGetValue(operationId, out operation)
                ? operation.Info.State == OperationState.CancelationPending
                 : true;
        }

        internal class SingleProcessDeploymentOperationInformation : OperationResult
        {
            public string DisplayName { get; set; }

            public bool IsPendingCollection { get; set; }
        }

        /// <summary>
        /// This event can be used when namager is running in the same process as the requesting app to facilitate completion notificaion that does not have to rely on polling the service.
        /// </summary>
        public event EventHandler<ProcessDoneEventArgs> ProcessDone;

        private void InvokePreocessDoneEvent(Guid processId)
        {
            if (ProcessDone != null)
            {
                ProcessDone.Invoke(this, new ProcessDoneEventArgs(processId));
            }
        }
    }
}