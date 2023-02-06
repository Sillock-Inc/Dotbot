using Dotbot.Discord.Models;
using FluentResults;
using static FluentResults.Result;

namespace Dotbot.Discord.DiscordCommandHandlers;

public class AvatarCommandHandler : IBotCommandHandler
{
    public CommandType CommandType => CommandType.Avatar;

    public async Task<Result> HandleAsync(string content, DiscordChannelMessageContext context)
    {
        var mentionedIds = await context.GetUserMentionsAsync();

        foreach (var user in mentionedIds)
        {
            await SendAvatarEmbed(user, context);
        }

        return Ok();
    }

    private static async Task SendAvatarEmbed(User user, DiscordChannelMessageContext context)
    {
        await context.SendEmbedAsync(new FormattedMessage
        {
            Title = user.Username, 
            ImageUrl = user.EffectiveAvatarUrl
        });
    }
}