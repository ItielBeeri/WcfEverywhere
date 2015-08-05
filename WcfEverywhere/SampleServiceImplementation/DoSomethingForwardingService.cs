using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleServiceContract;
using Ziv.ServiceModel.Operations;

namespace SampleServiceImplementation
{
    public class DoSomethingForwardingService : IDoSomethingService
    {
        private readonly IDoSomethingService _target;

        public DoSomethingForwardingService(IDoSomethingService target)
        {
            _target = target;
        }

        public OperationResult<SomeResult> DoSomething(SomeParameters parmaters)
        {
            return _target.DoSomething(parmaters);
        }

        public OperationStartInformation DoSomethingAsync(SomeParameters parmaters)
        {
            return _target.DoSomethingAsync(parmaters);
        }

        public OperationResult<SomeResult> DoSomethingGetResult(Guid guid)
        {
            return _target.DoSomethingGetResult(guid);
        }

        public int TestOperation(int x)
        {
            return x;
        }
    }
}
