using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Core.Infrastructure.IOC
{
    public static class XMIOC
    {
        private static IDependencyResolver _resolver;

        public static void InitializeWith(ILifetimeScope lifetimeScope)
        {
            if (lifetimeScope == null)
            {
                throw new Exception("方法 InitializeWith(ILifetimeScope lifetimeScope) 的 lifetimeScope 为空");
            }
            _resolver = new AutofacLifetimeResolver(lifetimeScope);
        }


        public static T Resolve<T>()
        {
            if (_resolver == null)
                throw new Exception("容器resolver为空");
            return _resolver.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("方法 Resolve<T>(string name) 的 name 为空");
            }
            if (_resolver == null)
                throw new Exception("容器resolver为空");

            return _resolver.Resolve<T>(name);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            if (_resolver == null)
                throw new Exception("容器resolver为空");
            return _resolver.ResolveAll<T>();
        }

        public static void Reset()
        {
            if (_resolver != null)
            {
                _resolver.Dispose();
            }
        }


    }
}
