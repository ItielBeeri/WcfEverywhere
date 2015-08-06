using System;
using System.Threading;

namespace Ziv.ServiceModel.Operations
{
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