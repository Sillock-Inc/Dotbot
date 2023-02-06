using System.Reflection;
using Autofac;
using Dotbot.Infrastructure.Entities;
using Dotbot.Infrastructure.Services;
using Dotbot.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Module = Autofac.Module;

namespace Dotbot.Infrastructure.Autofac;

public class InfrastructureModule: Module
{
    private readonly IConfiguration _configuration;

    public InfrastructureModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
        var mongoDbSettings = new MongoDbSettings();
        _configuration.GetSection("MongoDbSettings").Bind(mongoDbSettings);
        var settings = MongoClientSettings.FromConnectionString(mongoDbSettings.ConnectionString);
        var mongoClient = new MongoClient(settings);

        builder.Register(c => mongoClient.GetDatabase(mongoDbSettings.DatabaseName)).As<IMongoDatabase>().SingleInstance();
        builder.Register<IGridFSBucket>(c => new GridFSBucket(mongoClient.GetDatabase(mongoDbSettings.DatabaseName))).SingleInstance();
        builder.RegisterType<FileService>().As<IFileService>().InstancePerDependency();
        builder.RegisterType<XkcdService>().As<IXkcdService>().SingleInstance();
        builder.RegisterType<PersistentSettingsService>().As<IPersistentSettingsService>().SingleInstance();

        builder.Register<DbContext>(c => 
            new DbContext(
                 c.Resolve<IMongoCollection<BotCommand>>(),
                    c.Resolve<IMongoCollection<ChatServer>>(), 
                 c.Resolve<IMongoCollection<PersistentSetting>>()));
    }
}