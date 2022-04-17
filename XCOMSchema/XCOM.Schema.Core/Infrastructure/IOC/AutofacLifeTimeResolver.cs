using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.System;

namespace XCOM.Schema.Core.Infrastructure.IOC
{
    public class AutofacLifetimeResolver : XMDisposableResource, IDependencyResolver
    {
        private readonly ILifetimeScope _container;
        public AutofacLifetimeResolver(ILifetimeScope container)
        {
            if (container == null)
            {
                throw new Exception("方法 AutofacDependencyResolver(ILifetimeScope container) 的 container 为空");
            }
            _container = container;
        }


        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("方法 Resolve<T>(string name) 的 name 为空");
            }
            return _container.ResolveNamed<T>(name);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.Resolve<IEnumerable<T>>();
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _container.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
