using System.Threading;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel.Operations
{
    public class AsyncRunOperationContext
    {
        public AutoResetEvent ResetHandler { get; set; }

        public IOperationsManager OperationsManager { get; set; }
    }
}