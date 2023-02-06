using Dotbot.Infrastructure.Entities;
using MongoDB.Driver;

namespace Dotbot.Infrastructure;

public class DbContext
{
    public DbContext(IMongoCollection<BotCommand> botCommands, IMongoCollection<ChatServer> chatServers, IMongoCollection<PersistentSetting> persistentSettings)
    {
        BotCommands = botCommands;
        ChatServers = chatServers;
        PersistentSettings = persistentSettings;
    }

    public IMongoCollection<BotCommand> BotCommands { get; }
    public IMongoCollection<ChatServer> ChatServers { get; }
    public IMongoCollection<PersistentSetting> PersistentSettings { get; }
}