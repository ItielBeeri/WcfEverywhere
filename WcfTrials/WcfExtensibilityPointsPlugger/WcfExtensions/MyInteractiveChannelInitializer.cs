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
    class MyInteractiveChannelInitializer : IInteractiveChannelInitializer
    {
        public IAsyncResult BeginDisplayInitializationUI(IClientChannel channel, AsyncCallback callback, object state)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            Action act = new Action(this.DoNothing);
            return act.BeginInvoke(callback, state);
        }

        public void EndDisplayInitializationUI(IAsyncResult result)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
        }

        private void DoNothing() { }
    }
}
