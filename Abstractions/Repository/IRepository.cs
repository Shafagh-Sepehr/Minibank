namespace Abstractions.Repository;

public interface IRepository
{
    List<T> FetchAll<T>() where T : DatabaseEntity;
    T? FetchById<T>(string id) where T : DatabaseEntity;
    void Insert<T>(T entity) where T : DatabaseEntity;
    void Update<T>(T entity) where T : DatabaseEntity;
    void Delete<T>(string id) where T : DatabaseEntity;
}
