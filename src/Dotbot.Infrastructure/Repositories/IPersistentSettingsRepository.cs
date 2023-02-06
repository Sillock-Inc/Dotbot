using Dotbot.Infrastructure.Entities;

namespace Dotbot.Infrastructure.Repositories;

public interface IPersistentSettingsRepository : IRepository<PersistentSetting>
{
    Task<PersistentSetting?> GetSetting(string key);
    Task SaveOrUpdateSetting(string key, object value);
}