using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Ziv.ServiceModel.Behaviors;
using Ziv.ServiceModel.DependencyInjection;
using Microsoft.Practices.Unity;
using Ziv.ServiceModel.Runtime.DependencyInjection;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel
{
    public class ZivServiceHost : ServiceHost
    {

        public ZivServiceHost(
          Type serviceType, params Uri[] baseAddresses)
                : base(serviceType, baseAddresses)
        {
            InstanceProviderInitialization();
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }

        private void InstanceProviderInitialization()
        {
            Description.Behaviors.Add(new CustomInstanceProviderServiceBehaviorAttribute());


        }
        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IOperationsManager, SingleProcessDeploymentOperationsManager>(new ContainerControlledLifetimeManager());
        }
    }
}
