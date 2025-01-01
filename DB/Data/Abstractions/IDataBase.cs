using DB.Data.Services;

namespace DB.Data.Abstractions;

public interface IDataBase
{
    void Save<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity;
    void Update<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity;
    void Delete<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity;
    IEnumerable<TDatabaseEntity> FetchAll<TDatabaseEntity>() where TDatabaseEntity : IDatabaseEntity;
    
    event EventHandler<EntityEventArgs> EntitySaved;
    event EventHandler<EntityEventArgs> EntityUpdated;
    event EventHandler<EntityEventArgs> EntityDeleted;
}
