namespace Dotbot.Infrastructure.Entities;

public abstract class Entity
{
    string _id;
    public virtual string Id
    {
        get => _id;
        set => _id = value;
    }
}