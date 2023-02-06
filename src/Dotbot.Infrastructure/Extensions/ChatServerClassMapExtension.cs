using Dotbot.Infrastructure.Entities;
using MongoDB.Bson.Serialization;

namespace Dotbot.Infrastructure.Extensions;

public class ChatServerClassMapExtension : IClassMapExtension
{
    public BsonClassMap Register()
    {
        return BsonClassMap.RegisterClassMap<ChatServer>(cm =>
        {
            cm.SetIgnoreExtraElements(true);
            cm.MapMember(m => m.ServiceId)
                .SetElementName("guildId");
            cm.MapMember(m => m.UserWordCounts)
                .SetElementName("userWordCounts");
            cm.MapMember(m => m.ModeratorIds)
                .SetElementName("privilegedUsers");
            cm.MapMember(m => m.MemeChannelIds)
                .SetElementName("memeChannels");
            cm.MapMember(m => m.DeafenedChannelIds)
                .SetElementName("deafenedChannels");
            cm.MapMember(m => m.XkcdChannelId)
                .SetElementName("xkcdChannelId");
            cm.MapMember(m => m.Volume)
                .SetElementName("volume");
        });
    }
}