using System.Reflection;
using System.Text;
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
        var entityCopy = DeepCopy(entity)!;
        
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
    
    private static T DeepCopy<T>(T entity) => Copier.Copy(entity) ?? throw new ArgumentException("entity cannot be copied by Copier.");
    
    public void Update<T>(T entity)
    {
        var typeName = typeof(T).Name;
        var entityCopy = DeepCopy(entity)!;
        
        var properties = typeof(T).GetProperties();
        foreignKeyValidator.Validate(entity, properties, _entities);
        defaultValueValidator.Validate(entity, properties);
        nullablePropertyValidator.Validate(entity, properties);
        
        var primaryProperties = properties
            .Where(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute)
            .ToList();
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var entityIndex = entityList.FindIndex(e => primaryProperties.All(p => p.GetValue(e) == p.GetValue(entity)));
            if (entityIndex != -1)
            {
                entityList[entityIndex] = entityCopy;
                return;
            }
        }
        
        var builder = new StringBuilder();
        builder.Append("Update failed, no entity found with this primary keys: ");
        
        foreach (var propertyInfo in primaryProperties)
        {
            builder.Append($"[`{propertyInfo.PropertyType.Name}` `{propertyInfo.Name}` = `{propertyInfo.GetValue(entity)}` of type `{typeName}`], ");
        }
        
        throw new DatabaseException(builder.ToString());
        
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
