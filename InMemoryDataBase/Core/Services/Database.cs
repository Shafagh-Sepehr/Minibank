using System.Reflection;
using DeepCopier;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Core.Abstractions;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Core.Services;

public class Database(IPrimaryKeyValidator primaryKeyValidator, IForeignKeyValidator foreignKeyValidator, INullablePropertyValidator nullablePropertyValidator, IDefaultValueValidator defaultValueValidator) : IDatabase
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
        primaryKeyValidator.Validate(entity, properties, _entities);
        foreignKeyValidator.Validate(entity, properties, _entities);
        defaultValueValidator.Validate(entity, properties);
        nullablePropertyValidator.Validate(entity, properties);
        
        
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
    
    public T? FetchById<T>(string id) =>
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
        default;
    
    
}
