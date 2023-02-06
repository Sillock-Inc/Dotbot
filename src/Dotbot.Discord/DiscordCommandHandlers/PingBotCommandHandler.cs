using FluentResults;
using static FluentResults.Result;

namespace Dotbot.Discord.DiscordCommandHandlers;

public class PingBotCommandHandler: IBotCommandHandler
{
    public CommandType CommandType => CommandType.Ping;
    public async Task<Result> HandleAsync(string content, DiscordChannelMessageContext context)
    {
        await context.ReplyAsync("pong");
        return Ok();
    }
}