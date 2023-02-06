namespace Dotbot.Infrastructure.Entities;

public class BotCommand : Entity
{
    public enum CommandType
    {
        STRING,
        FILE
    }
    
    public string ServiceId { get; set; }
    public string Key { get; set; }

    public string? Content { get; set; }

    public string? FileName { get; set; }
    
    public CommandType Type { get; set; }
    
}