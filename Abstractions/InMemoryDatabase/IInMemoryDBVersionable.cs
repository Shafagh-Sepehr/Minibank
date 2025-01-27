namespace Abstractions.InMemoryDatabase;

public interface IInMemoryDBVersionable
{
    int Version { get; set; }
    void IncrementVersion() => Version++;
}
