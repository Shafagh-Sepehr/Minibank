namespace Abstractions.Repository;

public interface IRepository
{
    List<T> FetchAll<T>() where T : DataBaseEntity;
    T Read<T>(string Id) where T : DataBaseEntity;
    T Insert<T>(T entity) where T : DataBaseEntity;
    T Update<T>(T entity) where T : DataBaseEntity;
    T Delete<T>(T entity) where T : DataBaseEntity;
}
