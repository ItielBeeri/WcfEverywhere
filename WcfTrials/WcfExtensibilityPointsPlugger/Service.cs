using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service : ITest
    {
        public int Add(int x, int y)
        {
            ColorConsole.WriteLine(ConsoleColor.Green, "In service operation '{0}'", MethodBase.GetCurrentMethod().Name);

            if (x == 0 && y == 0)
            {
                throw new ArgumentException("This will cause IErrorHandler to be called");
            }
            else
            {
                return x + y;
            }
        }

        public void Process(string text)
        {
            ColorConsole.WriteLine(ConsoleColor.Green, "In service operation '{0}'", MethodBase.GetCurrentMethod().Name);
        }
    }
}
