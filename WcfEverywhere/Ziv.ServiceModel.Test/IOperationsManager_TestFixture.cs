using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel.Test
{
    [TestClass]
    public abstract class IOperationsManager_TestFixture
    {
        private const int PROGRESS_PERCENT = 37;
        private const string OPERATION_DISPLAY_NAME = "DisplayName";
        private const string FAILURE_REASON = "FialureResult";

        protected abstract IOperationsManager GetOperationsManager();

        [TestMethod]
        public void SingleOperationStarted_Test()
        {
            var operationsManager = GetOperationsManager();
            var operationId = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
            var operationStatus = operationsManager.GetOperationResult(operationId);
            Assert.AreEqual(OperationState.Started, operationStatus.Info.State);
        }

        [TestMethod]
        public void SingleOperationCompleted_Test()
        {
            object result = new object();
            var operationsManager = GetOperationsManager();
            var operationId = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
            operationsManager.SetOperationResult(operationId, result);
            var operationStatus = operationsManager.GetOperationResult(operationId);
            Assert.AreEqual(OperationState.CompletedSucessfully, operationStatus.Info.State);
            Assert.AreEqual(result, operationStatus.Result);
        }

        [TestMethod]
        public void SingleOperationFailed_Test()
        {

            var operationsManager = GetOperationsManager();
            var operationId = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
            operationsManager.SetOperationFailed(operationId, new Exception(FAILURE_REASON));
            var operationStatus = operationsManager.GetOperationResult(operationId);
            Assert.AreEqual(OperationState.Failed, operationStatus.Info.State);
            Assert.AreEqual(FAILURE_REASON, operationStatus.Info.FailureMessage);
        }

        [TestMethod]
        public void SingleOperationCancelationPending_Test()
        {
            var operationsManager = GetOperationsManager();
            var operationId = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
            operationsManager.SetOperationCancelFlag(operationId);
            var operationStatus = operationsManager.GetOperationResult(operationId);
            Assert.AreEqual(OperationState.CancelationPending, operationStatus.Info.State);
        }

        [TestMethod]
        public void SingleOperationCanceled_Test()
        {
            var operationsManager = GetOperationsManager();
            var operationId = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
            operationsManager.SetOperationCanceled(operationId);
            var operationStatus = operationsManager.GetOperationResult(operationId);
            Assert.AreEqual(OperationState.Canceled, operationStatus.Info.State);
        }

        [TestMethod]
        public void SingleOperationNotFound_Test()
        {
            var operationsManager = GetOperationsManager();
            var operationStatus = operationsManager.GetOperationResult(Guid.NewGuid());
            Assert.AreEqual(OperationState.NoFound, operationStatus.Info.State);
        }

        [TestMethod]
        public void SingleOperationProgress_Test()
        {
            var operationsManager = GetOperationsManager();
            var operationId = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
            operationsManager.SetOperationProgress(operationId, PROGRESS_PERCENT);
            var operationStatus = operationsManager.GetOperationResult(operationId);
            Assert.AreEqual(OperationState.Started, operationStatus.Info.State);
            Assert.AreEqual(PROGRESS_PERCENT, operationStatus.Info.Progress);
        }

        [TestMethod]
        public void MultipleOperations_CompletedOperations_Test()
        {
            IOperationsManager operationsManager = GetOperationsManager();

            int numberOfOperations = 500;
            Guid[] completed = new Guid[numberOfOperations];
            Guid[] running = new Guid[numberOfOperations];
            Guid[] failed = new Guid[numberOfOperations];
            Guid[] canceled = new Guid[numberOfOperations];
            Guid[] pendingCancelation = new Guid[numberOfOperations];
            for (int i = 0; i < numberOfOperations; i++)
            {
                completed[i] = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
                running[i] = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
                failed[i] = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
                canceled[i] = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
                pendingCancelation[i] = operationsManager.RegistrOperation(OPERATION_DISPLAY_NAME);
            }
            foreach (var guid in pendingCancelation)
            {
                operationsManager.SetOperationCancelFlag(guid);
            }
            foreach (var guid in completed)
            {
                operationsManager.SetOperationResult(guid, null);
            }
            foreach (var guid in failed)
            {
                operationsManager.SetOperationFailed(guid, new Exception(FAILURE_REASON));
            }
            foreach (var guid in canceled)
            {
                operationsManager.SetOperationCanceled(guid);
            }
            Guid[] completedOperations = operationsManager.GetCompletedOperations();
            CollectionAssert.AreEquivalent(completed.Concat(failed).Concat(canceled).ToArray(), completedOperations);
        }
    }
}
