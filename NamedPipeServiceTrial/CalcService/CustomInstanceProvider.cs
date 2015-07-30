using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CalcService;

namespace CalcService
{
    public class CustomInstanceProvider : IInstanceProvider
    {
        Dictionary<string, CalculatorService> _instancesByUsernames = new Dictionary<string, CalculatorService>();

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            object usernameAsObject;
            CalculatorService serviceInstance;
            if (message.Properties.TryGetValue("UserName", out usernameAsObject))
             {
                 string username = (string)usernameAsObject;
                 if (!_instancesByUsernames.TryGetValue(username, out serviceInstance))
                 {
                     serviceInstance = new CalculatorService(username);
                     _instancesByUsernames.Add(username, serviceInstance);
                 }
             }
             else
             {
                 serviceInstance = new CalculatorService(null);
             }
            return serviceInstance;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return new CalculatorService(null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}
