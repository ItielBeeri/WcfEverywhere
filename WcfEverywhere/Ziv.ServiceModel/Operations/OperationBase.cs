using System;
using System.Threading;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel.Operations
{
    public abstract class OperationBase<TResult>
    {
        public IOperationsManager OperationsManager { get; private set; }
        public Guid OperationGuid { get; private set; }

        public OperationBase(IOperationsManager operationsManager, Guid operationGuid)
        {
            OperationsManager = operationsManager;
            OperationGuid = operationGuid;
        }

        public AutoResetEvent RunAsync()
        {
            var operationContext = new AsyncRunOperationContext
                                       {
                                           ResetHandler = new AutoResetEvent(false),
                                           OperationsManager = OperationsManager
                                       };

            ThreadPool.QueueUserWorkItem(ThreadedWork, operationContext);
            return operationContext.ResetHandler;
        }

        private void ThreadedWork(object state)
        {
            var context = ((AsyncRunOperationContext)state);
            TResult result;
            try
            {
                result= Run();
            }
            catch (Exception ex)
            {
                context.OperationsManager.SetOperationFailed(OperationGuid, ex);
                return;
            }
            context.OperationsManager.SetOperationResult(OperationGuid, result);
            context.ResetHandler.Set();
        }

        protected abstract TResult Run();
    }
}
