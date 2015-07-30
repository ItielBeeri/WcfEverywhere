using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    class MyBehavior : IOperationBehavior, IContractBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.Formatter = new MyClientMessageFormatter(clientOperation.Formatter);
            clientOperation.ParameterInspectors.Add(new MyParameterInspector(false));
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.CallContextInitializers.Add(new MyCallContextInitializer());
            dispatchOperation.Formatter = new MyDispatchMessageFormatter(dispatchOperation.Formatter);
            dispatchOperation.Invoker = new MyOperationInvoker(dispatchOperation.Invoker);
            dispatchOperation.ParameterInspectors.Add(new MyParameterInspector(true));
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ChannelInitializers.Add(new MyChannelInitializer(false));
            clientRuntime.InteractiveChannelInitializers.Add(new MyInteractiveChannelInitializer());
            clientRuntime.MessageInspectors.Add(new MyClientMessageInspector());
            clientRuntime.OperationSelector = new MyClientOperationSelector();
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.ChannelDispatcher.ChannelInitializers.Add(new MyChannelInitializer(true));
            dispatchRuntime.ChannelDispatcher.ErrorHandlers.Add(new MyErrorHandler());
            dispatchRuntime.InstanceContextInitializers.Add(new MyInstanceContextInitializer());
            dispatchRuntime.InstanceContextProvider = new MyInstanceContextProvider(dispatchRuntime.InstanceContextProvider);
            dispatchRuntime.InstanceProvider = new MyInstanceProvider(dispatchRuntime.ChannelDispatcher.Host.Description.ServiceType);
            dispatchRuntime.MessageInspectors.Add(new MyDispatchMessageInspector());
            dispatchRuntime.OperationSelector = new MyDispatchOperationSelector();
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}
