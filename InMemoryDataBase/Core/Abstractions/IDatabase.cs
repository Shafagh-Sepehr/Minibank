namespace InMemoryDataBase.Core.Abstractions;

public interface IDatabase
{
    void Insert<T>(T entity) where T : IDatabaseEntity;
    void Update<T>(T entity) where T : IDatabaseEntity;
    void Delete<T>(string id) where T : IDatabaseEntity;
    IEnumerable<T> FetchAll<T>() where T : IDatabaseEntity;
    T? FetchById<T>(string id) where T : IDatabaseEntity;
}
