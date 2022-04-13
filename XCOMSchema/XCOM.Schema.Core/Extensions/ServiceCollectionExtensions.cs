using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Core.Infrastructure.IOC;
using Autofac.Extensions.DependencyInjection;
using XCOM.Schema.Core.Infrastructure;

namespace XCOM.Schema.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceProvider services, Action? extensionService = null)
        {
            if (services == null)
            {
                throw new Exception("ServiceCollectionExtensions 扩展对象services为空");
            }

            DefaultStart(services);

            extensionService?.Invoke();
        }


        private static void DefaultStart(IServiceProvider services)
        {
            EngineContext.Build(services.GetAutofacRoot());
        }

    }
}
