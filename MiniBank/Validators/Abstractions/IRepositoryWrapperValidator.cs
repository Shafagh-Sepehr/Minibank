using Abstractions.Repository;

namespace MiniBank.Validators.Abstractions;

public interface IRepositoryWrapperValidator
{
    List<T> FetchAll<T>() where T : DatabaseEntity;
    T? FetchById<T>(string id) where T : DatabaseEntity;
    void Insert<T>(T entity) where T : DatabaseEntity;
    void Update<T>(T entity) where T : DatabaseEntity;
    void Delete<T>(string id) where T : DatabaseEntity;
}