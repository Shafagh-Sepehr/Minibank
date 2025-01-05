namespace InMemoryDataBase.Core.Abstractions;

public interface IDatabase
{
    void Insert<T>(T entity);
    void Update<T>(T entity);
    void Delete<T>(string id);
    IEnumerable<T> FetchAll<T>();
    T? FetchById<T>(string id);
}
