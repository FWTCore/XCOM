using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, Action extensionService = null)
        {
            if (services == null)
            {
                throw new Exception("ServiceCollectionExtensions 扩展对象services为空");
            }

            DefaultStart();

            extensionService?.Invoke();
        }


        private static void DefaultStart()
        {

        }

    }
}
