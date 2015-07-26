using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceUtilities
{
    public static class OperationStateExtensions
    {
        public static bool IsDone(this OperationState operationState)
        {
            return operationState == OperationState.CompletedSucessfully
                        || operationState == OperationState.Failed
                        || operationState == OperationState.Canceled;
        }
    }
}
