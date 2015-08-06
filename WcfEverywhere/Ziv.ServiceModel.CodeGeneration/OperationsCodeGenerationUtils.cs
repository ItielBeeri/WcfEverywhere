using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel.CodeGeneration
{
    public class OperationViewModel
    {
        private readonly Type _operationType;

        public OperationViewModel(Type operationType)
        {
            AssertType(operationType);
            _operationType = operationType;
        }

        public string ShortName
        {
            get { return _operationType.OperationShortName(); }
        }

        private void AssertType(Type operationType)
        {
            // Opeartion must implement IOperation
            if (!operationType.IsOperationType())
            {
                throw new NotSupportedException(string.Format("Type '{0}' does not implement IOperation interface.", operationType));
            }
        }
    }

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

    public static class OperationExtensions
    {
        public static bool IsOperationType(this Type operationType)
        {
            return operationType.GetInterfaces().Any(
                intfc => intfc.IsGenericType && intfc.GetGenericTypeDefinition() == typeof(IOperation<>))
                && !operationType.IsGenericTypeDefinition
                && !operationType.IsGenericType
                && !operationType.IsInterface;
        }

        public static string OperationShortName(this Type operationType)
        {
            return operationType.Name.EndsWith("Operation")
                ? operationType.Name.Substring(0, operationType.Name.Length - "Operation".Length)
                : operationType.Name;
        }

        public static Type OperationResultType(this Type operationType)
        {
            return operationType.GetInterfaces().Single(
                intfc => intfc.IsGenericType && intfc.GetGenericTypeDefinition() == typeof(IOperation<>)).
                GetGenericArguments().Single();
        }

        public static string ToInvocation(this IEnumerable<ParameterInfo> parameters)
        {
            return parameters.Select(prm => prm.Name).ToCommaSeperatedString();
        }

        public static string ToDecleration(this IEnumerable<ParameterInfo> parameters)
        {
            return parameters.Select(prm => prm.ParameterType.Name + " " + prm.Name).ToCommaSeperatedString();
        }

        public static string ToCommaSeperatedString(this IEnumerable<string> strings)
        {

            if (!strings.Any())
            {
                return string.Empty;
            }
            if (strings.Count() == 1)
            {
                return strings.Single();
            }

            return strings.Aggregate((seed, str) => seed + ", " + str);
        }

        public static IEnumerable<ParameterInfo> OperationParameters(this Type operationType)
        {
            ConstructorInfo constructorInfo = operationType.GetConstructors().Single();
            return constructorInfo.GetParameters().TakeWhile(prm => prm.ParameterType != typeof(IOperationsManager));
        }

        public static IEnumerable<ParameterInfo> OperationServices(this Type operationType)
        {
            ConstructorInfo constructorInfo = operationType.GetConstructors().Single();
            var operationParameters = operationType.OperationParameters();
            return constructorInfo.GetParameters().Where(prm => !operationParameters.Contains(prm));
        }

        public static string OperationConstructorInvocation(this Type operationType)
        {
            ConstructorInfo constructorInfo = operationType.GetConstructors().Single();
            var operationParameters = operationType.OperationParameters().Select(prm => prm.Name);
            var operationServices = operationType.OperationServices().Select(srv => srv.ToPrivateValirableName());
            return operationParameters.Union(operationServices).ToCommaSeperatedString();
        }

        public static string ToPrivateValirableName(this ParameterInfo parameterInfo)
        {
            return string.Format("_{0}{1}", parameterInfo.Name.Substring(0, 1).ToLower(),
                                 parameterInfo.Name.Substring(1));
        }

        public static IEnumerable<string> OperationServiceNamespaces(this Type operationType)
        {
            List<string> nameSpaces = new List<string>();
            nameSpaces.AddIfNotIncluded("System", operationType);
            nameSpaces.AddIfNotIncluded("Ziv.ServiceModel", operationType);
            nameSpaces.AddIfNotIncluded("Ziv.ServiceModel.Operations", operationType);
            nameSpaces.AddIfNotIncluded(operationType.OperationResultType().Namespace, operationType);
            foreach (var constructorParam in operationType.GetConstructors().Single().GetParameters())
            {
                nameSpaces.AddIfNotIncluded(constructorParam.ParameterType.Namespace, operationType);

            }
            return nameSpaces;
        }

        public static IEnumerable<string> OperationContractNamespaces(this Type operationType)
        {
            List<string> nameSpaces = new List<string>();
            nameSpaces.AddIfNotIncluded("System", operationType);
            nameSpaces.AddIfNotIncluded("Ziv.ServiceModel.Operations", operationType);
            nameSpaces.AddIfNotIncluded("Ziv.ServiceModel.Operations.OperationsManager", operationType);
            nameSpaces.AddIfNotIncluded(operationType.OperationResultType().Namespace, operationType);
            foreach (var constructorParam in operationType.GetConstructors().Single().GetParameters())
            {
                nameSpaces.AddIfNotIncluded(constructorParam.ParameterType.Namespace, operationType);

            }
            return nameSpaces;
        }

        public static void AddIfNotIncluded(this List<string> list, string element, Type operationType)
        {
            if (operationType.Namespace == element)
            {
                return;
            }
            if (!list.Contains(element))
            {
                list.Add(element);
            }
        }
    }
}
