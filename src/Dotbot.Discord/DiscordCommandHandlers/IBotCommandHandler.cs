using FluentResults;

namespace Dotbot.Discord.DiscordCommandHandlers;

public interface IBotCommandHandler
{
    CommandType CommandType { get; }
    Task<Result> HandleAsync(string content, DiscordChannelMessageContext context);
}