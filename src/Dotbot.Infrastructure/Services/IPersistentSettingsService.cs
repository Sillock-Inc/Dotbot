using FluentResults;

namespace Dotbot.Infrastructure.Services;

public interface IPersistentSettingsService
{
    Task<Result<T>> GetSetting<T>(string key);
    Task<Result> SetSetting(string key, object value);
}