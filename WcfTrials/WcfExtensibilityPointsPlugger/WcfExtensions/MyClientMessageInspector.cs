using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WcfExtensibilityPointsPlugger
{
    class MyClientMessageInspector : IClientMessageInspector
    {
        public MyClientMessageInspector()
        {
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return null;
        }
    }
}
