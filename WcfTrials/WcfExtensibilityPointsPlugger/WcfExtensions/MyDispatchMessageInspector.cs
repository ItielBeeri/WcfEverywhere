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
    class MyDispatchMessageInspector : IDispatchMessageInspector
    {
        public MyDispatchMessageInspector()
        {
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
        }
    }
}
