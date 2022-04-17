using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Core.Infrastructure.IOC;

namespace XCOM.Schema.Core.Infrastructure
{
    public static class EngineContext
    {
        static EngineContext()
        {

        }
        public static void Build(ILifetimeScope lifetimeScope)
        {
            try
            {
                XMIOC.InitializeWith(lifetimeScope);
            }
            catch (Exception ex)
            {
                throw new Exception("XMIOC初始化异常", ex);
            }
        }

        public static void Build(IContainer container)
        {
            try
            {
                XMIOC.InitializeWith(container);
            }
            catch (Exception ex)
            {
                throw new Exception("XMIOC初始化异常", ex);
            }
        }


        public static void Run()
        {
            XMIOC.ResolveAll<IBootstrapperTask>()?.ToList()?.ForEach(t => t?.Execute());
        }


    }
}
