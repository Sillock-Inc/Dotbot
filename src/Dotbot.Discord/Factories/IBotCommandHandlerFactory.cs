using Dotbot.Discord.DiscordCommandHandlers;

namespace Dotbot.Discord.Factories;

public interface IBotCommandHandlerFactory
{
    IBotCommandHandler GetCommand(string str);
}