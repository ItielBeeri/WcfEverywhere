using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceUtilities
{
    /// <summary>
    /// Represents the sate of an operation running at a remote server
    /// </summary>
    public enum OperationState
    {
        Started,
        CompletedSucessfully,
        Failed,
        /// <summary>
        /// Operation has been signaled to cancel but has not yest halted.
        /// </summary>
        CancelationPending,
        Canceled,
        NoFound
    }
}
