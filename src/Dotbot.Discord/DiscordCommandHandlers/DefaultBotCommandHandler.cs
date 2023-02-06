using Dotbot.Discord.Enums;
using Dotbot.Discord.Models;
using FluentResults;
using static FluentResults.Result;

namespace Dotbot.Discord.DiscordCommandHandlers;

public class DefaultBotCommandHandler : IBotCommandHandler
{
    private readonly IBotCommandRepository _botCommandRepository;
    private readonly IFileService _fileService;

    public DefaultBotCommandHandler(IBotCommandRepository botCommandRepository, IFileService fileService)
    {
        _botCommandRepository = botCommandRepository;
        _fileService = fileService;
    }

    public CommandType CommandType => CommandType.Default;

    public async Task<Result> HandleAsync(string content, DiscordChannelMessageContext context)
    {
        var messageSplit = content.Split(' ');

        var key = messageSplit[0];
        var command = await _botCommandRepository.GetCommand(await context.GetServerId(), key);

        if (command.IsSuccess)
        {
            return command.Value.Type switch
            {
                Enums.CommandType.STRING => await HandleString(command.Value, context),
                Enums.CommandType.FILE => await HandleFile(command.Value, context),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        await context.SendMessageAsync($"No command {key} found");

        return Fail($"No command {key} found");
    }

    private async Task<Result> HandleFile(BotCommand command, DiscordChannelMessageContext context)
    {
        if (command.Type != Enums.CommandType.FILE) return Fail("Command is not a file");
        var fileStream = await _fileService.GetFile($"{command.ServiceId}:{command.FileName}:{command.Key}");
        if (fileStream.IsFailed)
        {
            await context.SendMessageAsync($"Cannot find file content for {command.Key}");
            return Fail($"Cannot find file content for {command.Key}");
        }

        await context.SendFileAsync(command.FileName, fileStream.Value);
        return Ok();
    }

    private static async Task<Result> HandleString(BotCommand command, DiscordChannelMessageContext context)
    {
        await context.SendMessageAsync(command.Content);
        return Ok();
    }
    
}