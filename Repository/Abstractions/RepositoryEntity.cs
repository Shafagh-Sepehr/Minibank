using Abstractions.InMemoryDatabase;

namespace Repository.Abstractions;

public abstract class RepositoryEntity : IInMemoryDBVersionable
{
    public virtual string Id { get; set; } = string.Empty;
    int IInMemoryDBVersionable.Version { get; set; }
}
