using System;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel
{
    public abstract class ServiceBase<TResult>
    {
        private readonly IOperationsManager _baseOperationsManager;
        private readonly bool _isReportingProgress;
        private readonly bool _isSupportingCancel;
        private readonly int _expectedCompletionTimeMilliseconds;
        private readonly int _suggestedPollingIntervalMilliseconds;

        public ServiceBase(IOperationsManager operationsManager,
            bool isReportingProgress = false,
            bool isSupportingCancel = false,
            int expectedCompletionTimeMilliseconds = 0,
            int suggestedPollingIntervalMilliseconds = 0
            )
        {
            _baseOperationsManager = operationsManager;
            _isReportingProgress = isReportingProgress;
            _isSupportingCancel = isSupportingCancel;
            _expectedCompletionTimeMilliseconds = expectedCompletionTimeMilliseconds;
            _suggestedPollingIntervalMilliseconds = suggestedPollingIntervalMilliseconds;
        }

        protected OperationResult<TResult> DoOperation(IOperation<TResult> operation)
        {
            return operation.RunSync();
        }

        protected OperationStartInformation DoOperationAsync(IOperation<TResult> operation)
        {
            var operationStart = operation.RunAsync();
            return new OperationStartInformation
            {
                OperationId = operationStart.OperationId,
                IsReportingProgress = _isReportingProgress,
                IsSupportingCancel = _isSupportingCancel,
                ExpectedCompletionTimeMilliseconds = _expectedCompletionTimeMilliseconds,
                SuggestedPollingIntervalMilliseconds = _suggestedPollingIntervalMilliseconds
            };
        }

        protected OperationResult<TResult> GetOperationResult(Guid operationId)
        {
            var objectResult = _baseOperationsManager.GetOperationResult(operationId);
            return new OperationResult<TResult>(
                (TResult)objectResult.Result,
                objectResult.Info
                );
        }

    }
}