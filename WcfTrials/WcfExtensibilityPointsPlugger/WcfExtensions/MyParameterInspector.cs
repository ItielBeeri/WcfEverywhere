using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    class MyParameterInspector : IParameterInspector
    {
        ConsoleColor consoleColor;
        bool isServer;
        public MyParameterInspector(bool isServer)
        {
            this.isServer = isServer;
            this.consoleColor = isServer ? ConsoleColor.Cyan : ConsoleColor.Yellow;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            ColorConsole.WriteLine(this.consoleColor, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            ColorConsole.WriteLine(this.consoleColor, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return null;
        }
    }
}
