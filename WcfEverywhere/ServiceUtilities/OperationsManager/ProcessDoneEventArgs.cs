using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceUtilities.OperationsManager
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
