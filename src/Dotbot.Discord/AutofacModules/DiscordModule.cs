using Autofac;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Dotbot.Discord.DiscordCommandHandlers;
using Dotbot.Discord.EventListeners;
using Dotbot.Discord.Factories;
using Dotbot.Discord.Services;
using Dotbot.Discord.Settings;

namespace Dotbot.Discord.AutofacModules;

public class DiscordModule : Autofac.Module
{
    private readonly IConfiguration _configuration;

    public DiscordModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
        var discordConfig = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers |
                             GatewayIntents.MessageContent | GatewayIntents.GuildVoiceStates,
            AlwaysDownloadUsers = true,
        };
        builder.Register(c => new DiscordSocketClient(discordConfig)).SingleInstance();
        builder.RegisterType<AudioService>().As<IAudioService>().SingleInstance();
        builder.RegisterType<InteractionHandler.InteractionHandler>().SingleInstance();
        builder.RegisterType<MessageReceivedEventListener>().SingleInstance();
        builder.Register(c => new InteractionService(c.Resolve<DiscordSocketClient>()));
        //TODO: Try to optimise this with something like
        /*
         * builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("CommandHandler")).As<IBotCommandHandler>().InstancePerDependency()
         */
        
        //Command handlers
        builder.RegisterType<DefaultBotCommandHandler>().As<IBotCommandHandler>().InstancePerDependency();
        builder.RegisterType<PingBotCommandHandler>().As<IBotCommandHandler>().InstancePerDependency();
        builder.RegisterType<SaveBotCommandHandler>().As<IBotCommandHandler>().InstancePerDependency();
        builder.RegisterType<SavedCommandHandler>().As<IBotCommandHandler>().InstancePerDependency();
        builder.RegisterType<AvatarCommandHandler>().As<IBotCommandHandler>().InstancePerDependency();
        builder.RegisterType<XkcdBotCommandHandler>().As<IBotCommandHandler>().InstancePerDependency();
        builder.RegisterType<BotCommandHandlerFactory>().As<IBotCommandHandlerFactory>().InstancePerDependency();
        builder.Register(c => _configuration.GetRequiredSection("DiscordBotSettings").Get<DiscordBotSettings>())
            .SingleInstance();
    }
}