using Dotbot.Discord.Events;
using Dotbot.Discord.Factories;
using Dotbot.Discord.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dotbot.Discord.EventHandlers;

public class MessageReceivedHandler : INotificationHandler<DiscordMessageReceivedNotification>
{
    private readonly ILogger _logger;
    private readonly IBotCommandHandlerFactory _commandHandlerFactory;
    private readonly DiscordBotSettings _discordBotSettings;
    public MessageReceivedHandler(IBotCommandHandlerFactory commandHandlerFactory, ILogger<MessageReceivedHandler> logger, DiscordBotSettings botSettings)
    {
        _commandHandlerFactory = commandHandlerFactory;
        _logger = logger;
        _discordBotSettings = botSettings;
    }

    public async Task Handle(DiscordMessageReceivedNotification notification, CancellationToken cancellationToken)
    {
       
    }
}