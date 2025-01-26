using Abstractions.InMemoryDatabase;

namespace Abstractions.Repository;

public abstract class DatabaseEntity : IVersionable
{
    public virtual string Id { get; set; } = string.Empty;
    int IVersionable.Version { get; set; }
}
