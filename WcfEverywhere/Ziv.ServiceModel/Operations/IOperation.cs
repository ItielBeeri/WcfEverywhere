namespace Ziv.ServiceModel.Operations
{
    public interface IOperation<TResult>
    {
        OperationStartInfo RunAsync();
        OperationResult<TResult> RunSync();
    }
}