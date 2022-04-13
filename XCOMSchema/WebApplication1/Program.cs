using Autofac;
using Autofac.Extensions.DependencyInjection;
using WebApplication1;
using WebApplication1.IOCRegister;
using WebApplication1.Pages;
using XCOM.Schema.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        // 注册集合
        //builder.RegisterModule(new AutofacRegister());
        //注册单体
        builder.RegisterType<Test>().As<ITest>().Named<ITest>("dd").InstancePerDependency();
    });



// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


app.Services.ConfigureApplicationServices(() =>
{


});
app.Run();
