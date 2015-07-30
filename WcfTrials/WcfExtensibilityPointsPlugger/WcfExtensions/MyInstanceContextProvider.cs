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
    class MyInstanceContextProvider : IInstanceContextProvider
    {
        IInstanceContextProvider inner;
        public MyInstanceContextProvider(IInstanceContextProvider inner)
        {
            this.inner = inner;
        }

        public InstanceContext GetExistingInstanceContext(Message message, IContextChannel channel)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return this.inner.GetExistingInstanceContext(message, channel);
        }

        public void InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            this.inner.InitializeInstanceContext(instanceContext, message, channel);
        }

        public bool IsIdle(InstanceContext instanceContext)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return this.inner.IsIdle(instanceContext);
        }

        public void NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            this.inner.NotifyIdle(callback, instanceContext);
        }
    }
}
