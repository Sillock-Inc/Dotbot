using MongoDB.Bson.Serialization;

namespace Dotbot.Infrastructure.Extensions;

public interface IClassMapExtension
{
    public BsonClassMap Register();
}