using Abstractions.InMemoryDatabase;

namespace Abstractions.Repository;

public abstract class DataBaseEntity : IVersionable
{
    public virtual string Id { get; set; } = string.Empty;
    int IVersionable.Version { get; set; }
}
