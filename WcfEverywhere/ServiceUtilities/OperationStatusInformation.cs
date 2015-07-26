using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceUtilities
{
    public class OperationStatusInformation
    {
        public OperationStatusInformation(OperationState state,
            int progress = 0,
            string failureMessage = null)
        {
            State = state;
            Progress = progress;
            FailureMessage = failureMessage;
        }

        public OperationState State { get; internal set; }
        public int Progress { get; internal set; }
        public string FailureMessage { get; internal set; }
    }
}
