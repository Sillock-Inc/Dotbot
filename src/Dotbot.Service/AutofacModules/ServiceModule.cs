using Autofac;

namespace Dotbot.Service.AutofacModules;

public class ServiceModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        
        //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

        
    }
}