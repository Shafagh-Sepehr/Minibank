using InMemoryDataBase.Interfaces;

namespace InMemoryDataBase.Core.Abstractions;

public interface IShafaghDB
{
    void Insert<T>(T entity) where T : IVersionable;
    void Update<T>(T entity) where T : IVersionable;
    void Delete<T>(string id) where T : IVersionable;
    IEnumerable<T> FetchAll<T>() where T : IVersionable;
    T? FetchById<T>(string id) where T : class, IVersionable;
}
