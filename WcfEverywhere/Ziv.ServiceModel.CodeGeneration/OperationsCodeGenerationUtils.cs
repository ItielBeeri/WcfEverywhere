using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel.CodeGeneration
{
    public static class OperationsCodeGenerationUtils
    {
        public static IEnumerable<Type> GetAllOperationTypes()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsOperationType())
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}
