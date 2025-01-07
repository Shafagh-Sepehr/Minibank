namespace InMemoryDataBase.Interfaces;

public interface IVersionable
{
    int Version { get; set; }
    void IncrementVersion() => Version++;
}
