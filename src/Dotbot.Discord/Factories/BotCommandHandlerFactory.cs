using Dotbot.Discord.DiscordCommandHandlers;

namespace Dotbot.Discord.Factories;

public class BotCommandHandlerFactory : IBotCommandHandlerFactory
{
    private readonly IList<IBotCommandHandler> _handlers;

    public BotCommandHandlerFactory(IEnumerable<IBotCommandHandler> handlers)
    {
        _handlers = handlers.ToList();
    }

    public IBotCommandHandler GetCommand(string str)
    {
        var commandType = CommandType.FromDisplayName(str);
        return _handlers.First(x => commandType.Equals(x.CommandType));
    }
}