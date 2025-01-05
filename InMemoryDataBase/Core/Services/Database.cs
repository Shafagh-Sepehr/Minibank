using System.Reflection;
using DeepCopier;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Core.Abstractions;
using InMemoryDataBase.Exceptions;

namespace InMemoryDataBase.Core.Services;

public class Database : IDatabase
{
    private readonly Dictionary<string, List<object>> _entities  = new();
    private readonly Dictionary<string, string>       _entityIds = new();
    
    
    public void Insert<T>(T entity)
    {
        var typeName = typeof(T).Name;
        var entityCopy = Copier.Copy(entity);
        if (entityCopy == null)
        {
            throw new ArgumentException("entity cannot be copied by Copier.");
        }
        
        var properties = typeof(T).GetProperties();
        ValidatePrimaryKeyValue(entity,properties);
        
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            entityList.Add(entityCopy);
        }
        else
        {
            _entities[typeName] = [entityCopy,];
        }
    }
    
    public void Update<T>(T entity)
    {
        // var typeName = typeof(T).Name;
        // var entityCopy = Copier.Copy(entity);
        //
        // if (_entities.TryGetValue(typeName, out var entityList))
        // {
        //     var entityId = entityList.FindIndex(x => x.Id == entityCopy.Id);
        //     entityList[entityId] = entityCopy;
        // }
        // else
        // {
        //     throw new InvalidOperationException($"entity {entityCopy.Id} of type {typeName} not found.");
        // }
    }
    
    public void Delete<T>(string id)
    {
        // var typeName = typeof(T).Name;
        //
        // if (_entities.TryGetValue(typeName, out var entityList))
        // {
        //     entityList.RemoveAll(x => x.Id == id);
        // }
        // else
        // {
        //     throw new InvalidOperationException($"entity {id} of type {typeName} not found.");
        // }
    }
    
    public IEnumerable<T> FetchAll<T>()
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
    
    public T? FetchById<T>(string id)
    {
        // var typeName = typeof(T).Name;
        //
        // if (_entities.TryGetValue(typeName, out var entityList))
        // {
        //     var entity = entityList.Find(x => x.Id == id);
        //     if (entity is T e)
        //     {
        //         return e;
        //     }
        // } 
        //
        // throw new InvalidOperationException($"entity {id} of type {typeName} not found.");
        return default;
    }
    
    
    private void ValidatePrimaryKeyValue<T>(T entity, PropertyInfo[] properties)
    {
        var typeName = typeof(T).Name;
        
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is not PrimaryKeyAttribute) continue;
            if (!_entities.TryGetValue(typeName, out var entityList)) continue;
            if (entityList.Any(x => propertyInfo.GetValue(x) == propertyInfo.GetValue(entity)))
            {
                throw new DatabaseException($"Primary key must be unique, value `{propertyInfo.GetValue(entity)}` of property `{propertyInfo.Name}` of type `{typeName}` is already present in the database");
            }
        }
    }
}
