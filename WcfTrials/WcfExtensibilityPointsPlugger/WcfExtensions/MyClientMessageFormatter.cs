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
    class MyClientMessageFormatter : IClientMessageFormatter
    {
        IClientMessageFormatter inner;
        public MyClientMessageFormatter(IClientMessageFormatter inner)
        {
            this.inner = inner;
        }

        public object DeserializeReply(Message message, object[] parameters)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return this.inner.DeserializeReply(message, parameters);
        }

        public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return this.inner.SerializeRequest(messageVersion, parameters);
        }
    }
}
