namespace Abstractions.Repository;

public interface IRepository
{
    List<T> FetchAll<T>() where T : DataBaseEntity;
    T FetchById<T>(string id) where T : DataBaseEntity;
    T Insert<T>(T entity) where T : DataBaseEntity;
    T Update<T>(T entity) where T : DataBaseEntity;
    T Delete<T>(string id) where T : DataBaseEntity;
}
