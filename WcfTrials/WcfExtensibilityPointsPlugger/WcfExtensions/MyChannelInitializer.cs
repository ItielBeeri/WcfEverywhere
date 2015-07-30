using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    class MyChannelInitializer : IChannelInitializer
    {
        ConsoleColor consoleColor;
        public MyChannelInitializer(bool isServer)
        {
            this.consoleColor = isServer ? ConsoleColor.Cyan : ConsoleColor.Yellow;
        }
        public void Initialize(IClientChannel channel)
        {
            ColorConsole.WriteLine(this.consoleColor, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
        }
    }
}
