using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    static class ReflectionUtil
    {
        public static string GetMethodSignature(MethodBase method)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(method.Name);
            sb.Append("(");
            ParameterInfo[] parameters = method.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0) sb.Append(", ");
                sb.Append(parameters[i].ParameterType.Name);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}
