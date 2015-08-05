using System;
using System.Security.Permissions;
using System.ServiceModel;
using Ziv.ServiceModel.Operations;

namespace SampleServiceContract
{
    [ServiceContract]
    public interface IDoSomethingService
    {
        [OperationContract]
        OperationResult<SomeResult> DoSomething(SomeParameters parmaters);

        [OperationContract]
        OperationStartInformation DoSomethingAsync(SomeParameters parmaters);

        [OperationContract]
        OperationResult<SomeResult> DoSomethingGetResult(Guid guid);
    }

    //?
    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract]
        void Login();

        [OperationContract]
        void Logout();
    }

    [ServiceContract]
    public interface IDoSomethingLoggedIn
    {
        [OperationContract]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        OperationResult<SomeResult> DoSomething(SomeParameters parmaters);

        [OperationContract]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        OperationStartInformation DoSomethingAsync(SomeParameters parmaters);

        [OperationContract]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        OperationResult<SomeResult> DoSomethingGetResult(Guid guid);
    }

    [ServiceContract]
    public interface IDoSomethingInRole : IDoSomethingService { }

    [ServiceContract]
    public interface IDoSomethingPerSpecificUser : IDoSomethingService { }
}