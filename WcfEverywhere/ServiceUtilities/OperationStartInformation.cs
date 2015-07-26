using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceUtilities
{
    public class OperationStartInformation
    {
        public Guid OperationGuid { get; set; }

        public bool IsReportingProgress { get; set; }

        public bool IsSupportingCancel { get; set; }

        public int ExpectedCompletionTimeMilliseconds { get; set; }

        public int SuggestedPollingIntervalMilliseconds { get; set; }
    }
}
