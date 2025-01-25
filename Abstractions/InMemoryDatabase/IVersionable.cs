namespace Abstractions.InMemoryDatabase;

public interface IVersionable
{
    int Version { get; set; }
    void IncrementVersion() => Version++;
}
