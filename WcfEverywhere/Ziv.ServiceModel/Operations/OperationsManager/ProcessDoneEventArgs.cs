using System;

namespace Ziv.ServiceModel.Operations.OperationsManager
{
    public class ProcessDoneEventArgs : EventArgs
    {
        public ProcessDoneEventArgs(Guid processId)
        {
            ProcessId = processId;
        }

        public Guid ProcessId { get; set; }
    }
}
