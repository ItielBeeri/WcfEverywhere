using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    class MyInstanceProvider : IInstanceProvider
    {
        Type serviceType;
        public MyInstanceProvider(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return Activator.CreateInstance(this.serviceType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return Activator.CreateInstance(this.serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
        }
    }
}
