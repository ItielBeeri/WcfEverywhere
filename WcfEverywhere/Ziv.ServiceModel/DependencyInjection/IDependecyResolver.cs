using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ziv.ServiceModel.DependencyInjection
{
    public interface IDependecyResolver
    {
        T Resolve<T>();

        object Resolve(Type type);
    }
}
