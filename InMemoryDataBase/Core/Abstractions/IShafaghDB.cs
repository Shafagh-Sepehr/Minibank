using Abstractions.InMemoryDatabase;

namespace InMemoryDataBase.Core.Abstractions;

public interface IShafaghDB
{
    void Insert<T>(T entity) where T : IInMemoryDBVersionable;
    void Update<T>(T entity) where T : IInMemoryDBVersionable;
    void Delete<T>(string id) where T : IInMemoryDBVersionable;
    IEnumerable<T> FetchAll<T>() where T : IInMemoryDBVersionable;
    T? FetchById<T>(string id) where T : class, IInMemoryDBVersionable;
}
