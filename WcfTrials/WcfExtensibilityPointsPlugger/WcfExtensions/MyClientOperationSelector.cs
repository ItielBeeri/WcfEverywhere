using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    class MyClientOperationSelector : IClientOperationSelector
    {
        public bool AreParametersRequiredForSelection
        {
            get
            {
                ColorConsole.WriteLine(ConsoleColor.Yellow, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
                return false;
            }
        }

        public string SelectOperation(MethodBase method, object[] parameters)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "{0}.{1}", this.GetType().Name, ReflectionUtil.GetMethodSignature(MethodBase.GetCurrentMethod()));
            return method.Name;
        }
    }
}
