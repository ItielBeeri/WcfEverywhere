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
    class MyErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return error is ArgumentException;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            MessageFault messageFault = MessageFault.CreateFault(new FaultCode("FaultCode"), new FaultReason(error.Message));
            fault = Message.CreateMessage(version, messageFault, "FaultAction");
        }
    }
}
