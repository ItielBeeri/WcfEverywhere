using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceUtilities;
using SevriceContract;

namespace ServiceImplementation
{
    public class DoSomethingService : IDoSomethingService
    {
        private readonly IOperationsManager _operationsManager;

        public DoSomethingService(IOperationsManager operationsManager)
        {
            _operationsManager = operationsManager;
        }

        public SomeResult DoSomething(SomeParameters parmaters)
        {
            var guid = _operationsManager.RegistrOperation("Do something");
            var operation = new DoSomethingOperation(_operationsManager, guid, parmaters);
            var handler=operation.RunAsync();
            handler.WaitOne();
            return (SomeResult) _operationsManager.GetOperationResult(guid);
        }

        public Guid DoSomethingAsync(SomeParameters parmaters)
        {
            var guid = _operationsManager.RegistrOperation("Do something");
            var operation = new DoSomethingOperation(_operationsManager, guid, parmaters);
            operation.RunAsync();
            return guid;
        }

        public int DoSomethingGetProgress(Guid guid)
        {
            return _operationsManager.GetOperationProgress(guid);
        }

        public bool DoSomethingGetIsComplete(Guid guid)
        {
            return _operationsManager.GetIsOperationComplete(guid);
        }

        public SomeResult DoSomethingGetResult(Guid guid)
        {
            if (_operationsManager.GetIsOperationComplete(guid))
            {
                return (SomeResult)_operationsManager.GetOperationResult(guid);
            }
            else
            {
                throw new OperationIncompleteException();
            }
        }
    }
}
