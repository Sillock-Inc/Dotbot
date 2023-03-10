using Dotbot.Database.Entities;
using FluentResults;

namespace Dotbot.Database.Repositories;

public interface IBotCommandRepository : IRepository<BotCommand>
{
    Task<Result<BotCommand>> GetCommand(string serverId, string key);
    Task<Result> SaveCommand(string serverId, string creatorId, string key, string content, bool overwrite = false);
    Task<Result> SaveCommand(string serverId, string creatorId, string key, string fileName, Stream fileStream, bool overwrite = false);
    Task<Result<List<BotCommand>>> GetCommands(string serverId, int page, int pageSize);
    Task<Result<long>> GetCommandCount(string serverId);
    Task<Result<List<string>>> GetAllNames(string serverId);
}