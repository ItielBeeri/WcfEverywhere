namespace Ziv.ServiceModel.Operations
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
