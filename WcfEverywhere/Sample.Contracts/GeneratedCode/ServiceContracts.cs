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
    using Ziv.ServiceModel.Operations;
    using Ziv.ServiceModel.Operations.OperationsManager;
    using Sample.DTO;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDoSomethingService 
    {
        [OperationContract]
        OperationResult<SomeResult> DoSomething(SomeParameters parameters);

        [OperationContract]
        OperationStartInformation DoSomethingAsync(SomeParameters parameters);

        [OperationContract]        
        OperationResult<SomeResult> DoSomethingGetResult(Guid operationId);
    }
}



    

namespace Sample.Operations 
{
    using System;
    using Ziv.ServiceModel.Operations;
    using Ziv.ServiceModel.Operations.OperationsManager;
    using Sample.DTO;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDoSomethingLoggedInRequiredService 
    {
        [OperationContract]
        OperationResult<SomeResult> DoSomethingLoggedInRequired(SomeParameters parameters);

        [OperationContract]
        OperationStartInformation DoSomethingLoggedInRequiredAsync(SomeParameters parameters);

        [OperationContract]        
        OperationResult<SomeResult> DoSomethingLoggedInRequiredGetResult(Guid operationId);
    }
}



