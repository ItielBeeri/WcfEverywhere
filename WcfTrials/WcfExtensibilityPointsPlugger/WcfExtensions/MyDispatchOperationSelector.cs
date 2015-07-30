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
    class MyDispatchOperationSelector : IDispatchOperationSelector
    {
        public string SelectOperation(ref Message message)
        {
            ColorConsole.WriteLine(ConsoleColor.Cyan, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            string action = message.Headers.Action;
            string method = action.Substring(action.LastIndexOf('/') + 1);
            return method;
        }
    }
}
