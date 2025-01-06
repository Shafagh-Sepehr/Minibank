using System.Reflection;
using DeepCopier;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Core.Abstractions;
using InMemoryDataBase.DataSanitizers.Abstractions;
using InMemoryDataBase.Entities.Enums;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Services;

namespace InMemoryDataBase.Core.Services;

public class Database(IDefaultValueSetter defaultValueSetter, IValidator validator) : IDatabase
{
    private readonly Dictionary<string, List<object>> _entities  = new();
    private readonly Dictionary<string, string>       _entityIds = new();
    
    
    public void Insert<T>(T entity)
    {
        var typeName = typeof(T).Name;
        var entityCopy = DeepCopy(entity)!;
        
        
        defaultValueSetter.Apply(entity);
        validator.Validate(entity,_entities,DataBaseAction.Save);
        
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
        var typeName = typeof(T).Name;
        var entityCopy = DeepCopy(entity)!;
        
        defaultValueSetter.Apply(entity);
        validator.Validate(entity,_entities,DataBaseAction.Update);
        
        
        var primaryProperty = GetPrimaryPropertyInfo<T>();
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var entityIndex = entityList.FindIndex(e => primaryProperty.GetValue(e) == primaryProperty.GetValue(entity));
            if (entityIndex != -1)
            {
                entityList[entityIndex] = entityCopy;
                return;
            }
        }
        
        throw new DatabaseException(
            $"Update failed, no entity found with this primary key: `{primaryProperty.PropertyType.Name}` `{primaryProperty.Name}` = `{primaryProperty.GetValue(entity)}` of type `{typeName}`");
    }
    
    public void Delete<T>(string id)
    {
        var typeName = typeof(T).Name;
        
        var primaryProperty = GetPrimaryPropertyInfo<T>();
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var entityIndex = GetEntityIndex(entityList, primaryProperty, id);
            entityList.RemoveAt(entityIndex);
        }
        else
        {
            throw new InvalidOperationException($"{typeName} having {primaryProperty.Name} with value {id} was not found");
        }
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
        var typeName = typeof(T).Name;
        
        var primaryProperty = GetPrimaryPropertyInfo<T>();
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var entityIndex = GetEntityIndex(entityList, primaryProperty, id);
            return (T)entityList[entityIndex];
        }
        else
        {
            return default;
        }
    }
    
    private static int GetEntityIndex(List<object> entityList, PropertyInfo primaryProperty, string id) 
        => entityList.FindIndex(e => (string)primaryProperty.GetValue(e)! == id);
    
    private static T DeepCopy<T>(T entity) => Copier.Copy(entity) ?? throw new ArgumentException("entity cannot be copied by Copier.");
    
    private static PropertyInfo GetPrimaryPropertyInfo<T>()
    {
        var properties = typeof(T).GetProperties();
        return properties.First(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
    }
}
