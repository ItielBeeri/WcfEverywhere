using System;
using System.Threading;
using SampleServiceContract;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;
using System.ServiceModel;

namespace SampleServiceImplementation
{
    public class DoSomethingAuthenticatedOperation : OperationBase<SomeResult>
    {
        public const string DO_SOMETHING_AUTHENTICATED_RESULT_TEMPLATE = "Your name is '{0}'.";
        const int SLEEP_TIME_MILISECONDS = 1000;
        private const int PROCESS_STAGES = 10;

        private readonly SomeParameters _parmaters;

        public DoSomethingAuthenticatedOperation(IOperationsManager operationsManager, SomeParameters parmaters)
            : base(operationsManager, "Do Something")
        {
            _parmaters = parmaters;
        }

        protected override SomeResult Run()
        {

            return new SomeResult
            {
                Result = string.Format(DO_SOMETHING_AUTHENTICATED_RESULT_TEMPLATE, OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name)
            };
        }
    }
}