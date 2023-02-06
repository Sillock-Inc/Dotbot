using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Dotbot.API.Infrastructure.AutofacModules;
using Dotbot.Discord.AutofacModules;
using Dotbot.Discord.DiscordCommandHandlers;
using Dotbot.Discord.EventListeners;
using Dotbot.Discord.Events;
using Dotbot.Discord.InteractionHandler;
using Dotbot.Discord.Settings;
using Dotbot.Infrastructure.Autofac;
using Dotbot.Infrastructure.Entities;
using Dotbot.Infrastructure.Extensions;
using Dotbot.Service.AutofacModules;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace Dotbot.API;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());
        // Add services to the container.

        //builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddMongoDbCollection<ChatServer, ChatServerClassMapExtension>("springGuild", new ChatServerClassMapExtension());
        builder.Services.AddMongoDbCollection<BotCommand, DiscordCommandClassMapExtension>("DiscordCommands", new DiscordCommandClassMapExtension());
        builder.Services.AddMongoDbCollection<PersistentSetting, PersistentSettingClassMapExtension>("PersistentSettings", new PersistentSettingClassMapExtension());
        
        builder.Services.AddHttpClient<SaveBotCommandHandler>();
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>((context, autofacBuilder) =>
        {
            var mediatrConfiguration = MediatRConfigurationBuilder
                .Create(new []{typeof(DiscordMessageReceivedNotification).Assembly})
                .WithAllOpenGenericHandlerTypesRegistered()
                .Build();
            autofacBuilder.RegisterMediatR(mediatrConfiguration);
            autofacBuilder.RegisterModule(new DotbotModule(builder.Configuration));
            autofacBuilder.RegisterModule(new DiscordModule());
            autofacBuilder.RegisterModule(new ServiceModule());
            autofacBuilder.RegisterModule(new InfrastructureModule(builder.Configuration));
        });
        
        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        //app.MapControllers();
       

        var app = builder.Build();
        
        
        
        var client = app.Services.GetRequiredService<DiscordSocketClient>();
        var messageReceivedEventListener = app.Services.GetRequiredService<MessageReceivedEventListener>();

        client.MessageReceived += messageReceivedEventListener.OnMessageReceivedAsync;

        await app.Services.GetRequiredService<InteractionHandler>()
            .InitializeAsync();
        var discordBotSettings = builder.Configuration.GetRequiredSection("DiscordBotSettings").Get<DiscordBotSettings>();
        await client.LoginAsync(TokenType.Bot, discordBotSettings.Token);
        await client.StartAsync();
        await client.SetGameAsync(discordBotSettings.GameName);
        await app.RunAsync();
    }
}