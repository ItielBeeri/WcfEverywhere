using System.Threading;

namespace ServiceUtilities
{
    public class AsyncRunOperationContext
    {
        public AutoResetEvent ResetHandler { get; set; }

        public IOperationsManager OperationsManager { get; set; }
    }
}