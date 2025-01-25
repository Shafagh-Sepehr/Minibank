namespace Abstractions.Repository;

public interface IRepository
{
    List<T> FetchAll<T>() where T : DataBaseEntity;
    T? FetchById<T>(string id) where T : DataBaseEntity;
    void Insert<T>(T entity) where T : DataBaseEntity;
    void Update<T>(T entity) where T : DataBaseEntity;
    void Delete<T>(string id) where T : DataBaseEntity;
}
