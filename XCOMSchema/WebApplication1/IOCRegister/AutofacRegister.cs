using Autofac;
using System.Reflection;

namespace WebApplication1.IOCRegister
{
    public class AutofacRegister : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            var service = Assembly.Load("");
            var iservice = Assembly.Load("");

            builder.RegisterAssemblyTypes(iservice, service)
                .AsImplementedInterfaces();

            //base.Load(builder);
        }

    }
}
