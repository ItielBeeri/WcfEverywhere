using System;
using System.ServiceModel;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleServiceContract;
using SampleServiceImplementation;
using Microsoft.Practices.Unity;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using Ziv.ServiceModel.Operations.OperationsManager;
using Ziv.ServiceModel.Operations;

namespace Ziv.ServiceModel.Test
{
    [TestClass]
    public class BasicHttp_TestFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            //TODO: Configure DI container

            using (IUnityContainer container = new UnityContainer())
            {
                RegisterTypes(container);
                var baseAddresses = new Uri("http://localhost:8180/");
                using (var serviceHost = new UnityServiceHost(container, typeof(DoSomethingService)))
                {
                    // TODO: Configure service host with dependency injecction

                    //serviceHost.AddServiceEndpoint(typeof(IDoSomethingService), new BasicHttpBinding(), "DoSomethingService");

                    //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    //smb.HttpGetEnabled = true;
                    //serviceHost.Description.Behaviors.Add(smb);

                    serviceHost.Open();

                    using (ChannelFactory<IDoSomethingService> channelFactory = new ChannelFactory<IDoSomethingService>("ClientEP"))
                    {
                        IDoSomethingService channel = channelFactory.CreateChannel();
                        channel.DoSomething(new SomeParameters() { Parameter = 37 });
                    }

                    //using (var client = new DoSomethingClient(new WSHttpBinding(), new EndpointAddress(baseAddresses)))
                    //{
                    //    IDoSomethingService channel = client.ChannelFactory.CreateChannel();
                    //    channel.DoSomething(new SomeParameters() { Parameter = 37 });
                    //}
                }
            }
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IOperationsManager, SingleProcessDeploymentOperationsManager>();
        }
    }

    class DoSomethingClient : ClientBase<IDoSomethingService>, IDoSomethingService
    {
        public DoSomethingClient()
        {
        }

        public DoSomethingClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public DoSomethingClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public DoSomethingClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public DoSomethingClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public OperationResult<SomeResult> DoSomething(SomeParameters parmaters)
        {
            return base.Channel.DoSomething(parmaters);
        }

        public OperationStartInformation DoSomethingAsync(SomeParameters parmaters)
        {
            return base.Channel.DoSomethingAsync(parmaters);
        }

        public OperationResult<SomeResult> DoSomethingGetResult(Guid guid)
        {
            return base.Channel.DoSomethingGetResult(guid);
        }
    }

    public class UnityServiceHost : ServiceHost
    {
       
        public UnityServiceHost(IUnityContainer container,
          Type serviceType, params Uri[] baseAddresses)
          : base(serviceType, baseAddresses)
        {
            UnityInitialization(container);
        }

        private void UnityInitialization(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            foreach (var implementedContract in this.ImplementedContracts.Values)
            {
                implementedContract.Behaviors.Add(new UnityInstanceProvider(container));
            }
        }
    }
    public class UnityInstanceProvider
  : IInstanceProvider, IContractBehavior
    {
        private readonly IUnityContainer container;

        public UnityInstanceProvider(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext,
          Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return this.container.Resolve(
              instanceContext.Host.Description.ServiceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext,
          object instance)
        {
        }

        #endregion

        #region IContractBehavior Members

        public void AddBindingParameters(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint,
          BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint,
          DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
