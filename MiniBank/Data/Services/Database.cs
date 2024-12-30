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
        var typeName = typeof(TDatabaseEntity).Name;
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var newEntity = entityList.Single(x=> x.Id == entity.Id);
            var result = entityList.Remove(newEntity);
            
            if (result == false)
            {
                throw new OperationFailedException($"entity {entity.Id} of type {typeName} could not be removed or does not exist.");
            }
            
            entityList.Add(entity);
        }
        else
        {
            throw new InvalidOperationException($"entity {entity.Id} of type {typeName} not found.");
        }
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
