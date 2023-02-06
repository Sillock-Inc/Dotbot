using System.Reflection;
using Autofac;
using Dotbot.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Dotbot.API.Infrastructure.AutofacModules;

public class DotbotModule: Autofac.Module
{
    private readonly IConfiguration _configuration;

    public DotbotModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsImplementedInterfaces();
    }
}