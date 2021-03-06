﻿///////////////////////////////////////////////////////////////////////////////////////////////////////
//
//     (c) Ziv Systems 2015
//
//      !!!   DO NOT CHANGE - This code was generated by a tool. Changes would be reverted by regeneration   
//
//      This code contains service classes for Operations implementing IOperation<T>.
//      Operations calsses must have a single constructor. All constructor parameters before IOperationsManager
//      are considered parameters of the operation to be provided upon call. All parmeters after are
//      considerd services to be injected to service class.
//
///////////////////////////////////////////////////////////////////////////////////////////////////////
    

namespace Sample.Operations 
{
    using System;
    using Ziv.ServiceModel;
    using Ziv.ServiceModel.Operations;
    using Sample.DTO;
    using Ziv.ServiceModel.Operations.OperationsManager;

    public class DoSomethingService : ServiceBase<SomeResult>, IDoSomethingService
    {
        private readonly IOperationsManager _operationsManager;
        private readonly ISomeRequiredService _someRequiredService;
        private readonly ISomeRequiredService _someRequiredService2;

        public DoSomethingService(IOperationsManager operationsManager, ISomeRequiredService someRequiredService, ISomeRequiredService someRequiredService2)
            : base(operationsManager)
        {
            _operationsManager = operationsManager;
            _someRequiredService = someRequiredService;
            _someRequiredService2 = someRequiredService2;
        }

        public OperationResult<SomeResult> DoSomething(SomeParameters parameters)
        {
            return DoOperation(GetOperation(parameters));
        }

        public OperationStartInformation DoSomethingAsync(SomeParameters parameters)
        {
            return DoOperationAsync(GetOperation(parameters));
        }

        public OperationResult<SomeResult> DoSomethingGetResult(Guid operationId)
        {
            return GetOperationResult(operationId);
        }

        protected DoSomethingOperation GetOperation(SomeParameters parameters)
        {
            return new DoSomethingOperation(parameters, _operationsManager, _someRequiredService, _someRequiredService2);
        }
    }
}



    

namespace Sample.Operations 
{
    using System;
    using Ziv.ServiceModel;
    using Ziv.ServiceModel.Operations;
    using Sample.DTO;
    using Ziv.ServiceModel.Operations.OperationsManager;

    public class DoSomethingLoggedInRequiredService : ServiceBase<SomeResult>, IDoSomethingLoggedInRequiredService
    {
        private readonly IOperationsManager _operationsManager;
        private readonly ISomeRequiredService _requiredService;

        public DoSomethingLoggedInRequiredService(IOperationsManager operationsManager, ISomeRequiredService requiredService)
            : base(operationsManager)
        {
            _operationsManager = operationsManager;
            _requiredService = requiredService;
        }

        public OperationResult<SomeResult> DoSomethingLoggedInRequired(SomeParameters parameters)
        {
            return DoOperation(GetOperation(parameters));
        }

        public OperationStartInformation DoSomethingLoggedInRequiredAsync(SomeParameters parameters)
        {
            return DoOperationAsync(GetOperation(parameters));
        }

        public OperationResult<SomeResult> DoSomethingLoggedInRequiredGetResult(Guid operationId)
        {
            return GetOperationResult(operationId);
        }

        protected DoSomethingLoggedInRequiredOperation GetOperation(SomeParameters parameters)
        {
            return new DoSomethingLoggedInRequiredOperation(parameters, _operationsManager, _requiredService);
        }
    }
}




