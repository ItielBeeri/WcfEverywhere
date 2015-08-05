using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ziv.ServiceModel.DependencyInjection
{
    public static class DependencyResolver
    {
        private static IDependecyResolver _current;

        public static IDependecyResolver Current
        {
            get { return _current; }
        }

        public static void SetResolver(IDependecyResolver dependecyResolver)
        {
            _current = dependecyResolver;
        }

    }
}
