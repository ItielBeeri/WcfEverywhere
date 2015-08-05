using System;
using System.Threading;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel.Operations
{
    public abstract class OperationBase<TResult>
    {
        private IOperationsManager _operationsManager;
        private readonly string _displayName;
        private Guid _operationGuid;

        public OperationBase(IOperationsManager operationsManager, string displayName)
        {
            _operationsManager = operationsManager;
            _displayName = displayName;
        }

        public OperationStartInfo RunAsync()
        {
            _operationGuid = _operationsManager.RegistrOperation(_displayName);
            var operationContext = new AsyncRunOperationContext
                                       {
                                           ResetHandler = new AutoResetEvent(false),
                                           OperationsManager = _operationsManager
                                       };
            ThreadPool.QueueUserWorkItem(ThreadedWork, operationContext);
            return new OperationStartInfo(_operationGuid, operationContext.ResetHandler);
        }

        public OperationResult<TResult> RunSync()
        {
            _operationsManager = new SingleProcessDeploymentOperationsManager();
            var operationStart = RunAsync();
            operationStart.Handler.WaitOne();
            var result = _operationsManager.GetOperationResult(_operationGuid);
            return new OperationResult<TResult>((TResult)result.Result, result.Info);
        }

        protected void ReportProgress(int progressPercent)
        {
            _operationsManager.SetOperationProgress(_operationGuid, progressPercent);
        }

        protected bool IsCancelationPending()
        {
            return _operationsManager.GetIsOperationFlagedToCancel(_operationGuid);
        }

        protected void ReportCancelationCompleted()
        {
            _operationsManager.SetOperationCanceled(_operationGuid);
        }

        private void ThreadedWork(object state)
        {
            var context = ((AsyncRunOperationContext)state);
            try
            {
                TResult result;
                try
                {
                    result = Run();
                }
                catch (Exception ex)
                {
                    context.OperationsManager.SetOperationFailed(_operationGuid, ex);
                    return;
                }
                context.OperationsManager.SetOperationResult(_operationGuid, result);

            }
            finally
            {
                context.ResetHandler.Set();
            }
        }
        protected abstract TResult Run();
    }

    public class OperationStartInfo
    {
        public OperationStartInfo(Guid guid, AutoResetEvent handler)
        {
            OperationId = guid;
            Handler = handler;
        }

        public Guid OperationId { get; private set; }
        public AutoResetEvent Handler { get; private set; }
    }
}
