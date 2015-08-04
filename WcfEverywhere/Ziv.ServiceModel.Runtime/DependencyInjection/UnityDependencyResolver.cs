using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziv.ServiceModel.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Ziv.ServiceModel.Runtime.DependencyInjection
{
    public class UnityDependencyResolver : IDependecyResolver
    {
        IUnityContainer _container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
