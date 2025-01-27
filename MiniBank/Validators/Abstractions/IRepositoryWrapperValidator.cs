using Abstractions.MiniBank;

namespace MiniBank.Validators.Abstractions;

public interface IRepositoryWrapperValidator
{
    List<T> FetchAll<T>() where T : MiniBankDatabaseEntity;
    T? FetchById<T>(string id) where T : MiniBankDatabaseEntity;
    void Insert<T>(T entity) where T : MiniBankDatabaseEntity;
    void Update<T>(T entity) where T : MiniBankDatabaseEntity;
    void Delete<T>(string id) where T : MiniBankDatabaseEntity;
}