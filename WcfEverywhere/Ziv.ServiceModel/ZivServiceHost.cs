using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Ziv.ServiceModel.Behaviors;

namespace Ziv.ServiceModel
{
    public class ZivServiceHost : ServiceHost
    {

        public ZivServiceHost(
          Type serviceType, params Uri[] baseAddresses)
                : base(serviceType, baseAddresses)
        {
            InstanceProviderInitialization();
        }

        private void InstanceProviderInitialization()
        {
            Description.Behaviors.Add(new CustomInstanceProviderServiceBehaviorAttribute());


        }
    }
}
