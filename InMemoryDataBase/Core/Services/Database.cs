using DB.Entities.Enums;
using DeepCopier;
using InMemoryDataBase.Core.Abstractions;

namespace InMemoryDataBase.Core.Services;

public class Database : IDatabase
{
    private readonly Dictionary<string, List<IDatabaseEntity>> _entities  = new();
    private readonly Dictionary<string, string>                _entityIds = new();
    
    
    public void Insert<T>(T entity) where T : IDatabaseEntity
    {
        Validate(entity, DataBaseAction.Save);
        
        var typeName = typeof(T).Name;
        var entityCopy = Copier.Copy(entity);
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            entityList.Add(entityCopy);
        }
        else
        {
            _entities[typeName] = [entityCopy,];
        }
    }
    
    public void Update<T>(T entity) where T : IDatabaseEntity
    {
        Validate(entity, DataBaseAction.Update);
        
        var typeName = typeof(T).Name;
        var entityCopy = Copier.Copy(entity);
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var entityId = entityList.FindIndex(x => x.Id == entityCopy.Id);
            entityList[entityId] = entityCopy;
        }
        else
        {
            throw new InvalidOperationException($"entity {entityCopy.Id} of type {typeName} not found.");
        }
    }
    
    public void Delete<T>(string id) where T : IDatabaseEntity
    {
        var typeName = typeof(T).Name;
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            entityList.RemoveAll(x => x.Id == id);
        }
        else
        {
            throw new InvalidOperationException($"entity {id} of type {typeName} not found.");
        }
    }
    
    public IEnumerable<T> FetchAll<T>() where T : IDatabaseEntity
    {
        var typeName = typeof(T).Name;
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            return entityList.Where(x => x is T).Cast<T>();
        }
        else
        {
            return [];
        }
    }
    
    public T? FetchById<T>(string id) where T : IDatabaseEntity
    {
        var typeName = typeof(T).Name;
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var entity = entityList.Find(x => x.Id == id);
            if (entity is T e)
            {
                return e;
            }
        } 
        
        throw new InvalidOperationException($"entity {id} of type {typeName} not found.");
    }
    
    private void Validate<T>(T entity, DataBaseAction action) where T : IDatabaseEntity
    {
        throw new NotImplementedException();
    }
}
