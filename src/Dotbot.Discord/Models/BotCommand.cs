using Dotbot.Discord.Enums;

namespace Dotbot.Discord.Models;

public class BotCommand
{
    public string ServiceId { get; set; }
    public string Key { get; set; }

    public string? Content { get; set; }

    public string? FileName { get; set; }
    
    public CommandType Type { get; set; }
}