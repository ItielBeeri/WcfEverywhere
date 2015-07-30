using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    class MyDispatchMessageFormatter : IDispatchMessageFormatter
    {
        IDispatchMessageFormatter inner;
        public MyDispatchMessageFormatter(IDispatchMessageFormatter inner)
        {
            this.inner = inner;
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            this.inner.DeserializeRequest(message, parameters);
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return this.inner.SerializeReply(messageVersion, parameters, result);
        }
    }
}
