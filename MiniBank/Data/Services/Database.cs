using MiniBank.Data.Abstractions;
using MiniBank.Exceptions;

namespace MiniBank.Data.Services;

public class Database : IDataBase
{
    private readonly Dictionary<string, List<IDatabaseEntity>> _entities = new();
    
    public void Save<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        var typeName = typeof(TDatabaseEntity).Name;
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            entityList.Add(entity);
        }
        else
        {
            _entities[typeName] = [entity, ];
        }
    }
    
    public void Update<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        
    }
    
    
    public void Delete<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        throw new NotImplementedException();
    }
    
    
    public IEnumerable<TDatabaseEntity> FetchAll<TDatabaseEntity>() where TDatabaseEntity : IDatabaseEntity
    {
        throw new NotImplementedException();
    }
}
