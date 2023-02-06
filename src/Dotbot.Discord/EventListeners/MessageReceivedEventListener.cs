using Discord;
using Discord.WebSocket;
using Dotbot.Discord.DiscordCommandHandlers;
using Dotbot.Discord.Events;
using Dotbot.Discord.Factories;
using Dotbot.Discord.Settings;
using Microsoft.Extensions.Logging;

namespace Dotbot.Discord.EventListeners;

public class MessageReceivedEventListener
{
    private readonly ILogger _logger;
    private readonly IBotCommandHandlerFactory _commandHandlerFactory;
    private readonly DiscordBotSettings _discordBotSettings;
    public MessageReceivedEventListener(ILogger logger, IBotCommandHandlerFactory commandHandlerFactory, DiscordBotSettings discordBotSettings)
    {
        _logger = logger;
        _commandHandlerFactory = commandHandlerFactory;
        _discordBotSettings = discordBotSettings;
    }

    public async Task OnMessageReceivedAsync(SocketMessage arg)
    {
        if(arg.Author.IsBot) return;
        
        if(arg.Content.ToLower() == "lol")
            await arg.AddReactionAsync(new Emoji("🤣"));
        
        _logger.LogInformation("<{AuthorUsername}>: {Message}", arg.Author.Username, arg.Content);

        var messageSplit = arg.Content.Split(' ');

        if (messageSplit[0].StartsWith(_discordBotSettings.CommandPrefix))
        {
            
            
            await _commandHandlerFactory.GetCommand(messageSplit[0][1..]).HandleAsync(arg.Content[1..], new DiscordChannelMessageContext(arg));
        }
        
        //await _mediator.Publish(new DiscordMessageReceivedNotification(arg));
    }
}