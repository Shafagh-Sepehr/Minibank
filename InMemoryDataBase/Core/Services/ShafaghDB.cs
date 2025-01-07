using System.Reflection;
using DeepCopier;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Core.Abstractions;
using InMemoryDataBase.DataSanitizers.Abstractions;
using InMemoryDataBase.Entities.Classes;
using InMemoryDataBase.Entities.Enums;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Interfaces;
using InMemoryDataBase.Validators.Abstractions;
using InMemoryDataBase.Validators.Services;

namespace InMemoryDataBase.Core.Services;

public class ShafaghDB(IDefaultValueSetter defaultValueSetter, IValidator validator, IReferenceHandler referenceHandler, IAttributeValidator attributeValidator) : IShafaghDB
{
    private readonly Dictionary<Type, List<IVersionable>> _entities        = new();
    private readonly Dictionary<string, string>           _entityIds       = new();
    private readonly List<Reference>                      _references      = new();
    
    public void Insert<T>(T entity) where T : IVersionable
    {
        var type = typeof(T);
        var entityCopy = DeepCopy(entity);
        
        defaultValueSetter.Apply(entity);
        validator.Validate(entity, _entities, _references, DataBaseAction.Save);
        referenceHandler.HandleInsert(entityCopy, _references);
        
        if (_entities.TryGetValue(type, out var entityList))
        {
            entityList.Add(entityCopy);
        }
        else
        {
            attributeValidator.Validate<T>();
            _entities[type] = [entityCopy,];
        }
    }
    
    public void Update<T>(T entity) where T : IVersionable
    {
        var type = typeof(T);
        var entityCopy = DeepCopy(entity);
        
        defaultValueSetter.Apply(entity);
        validator.Validate(entity, _entities, _references, DataBaseAction.Update);
        
        
        var primaryProperty = GetPrimaryPropertyInfo<T>();
        
        if (_entities.TryGetValue(type, out var entityList))
        {
            var entityIndex = entityList.FindIndex(e => primaryProperty.GetValue(e) == primaryProperty.GetValue(entity));
            
            if (entityIndex != -1)
            {
                if (entityList[entityIndex].Version != entity.Version)
                {
                    throw new DatabaseException(
                        $"entity with type `{type.Name}` with primary key `{primaryProperty.GetValue(entity)}` was modified by another user, fetch the new entity and redo the process");
                }
                
                referenceHandler.HandleUpdate(entityCopy, (T)entityList[entityIndex], _references);
                entityCopy.IncrementVersion();
                entityList[entityIndex] = entityCopy;
                return;
            }
        }
        
        throw new DatabaseException(
            $"Update failed, no entity found with this primary key: `{primaryProperty.PropertyType.Name}` `{primaryProperty.Name}` = `{primaryProperty.GetValue(entity)}` of type `{type.Name}`");
    }
    
    public void Delete<T>(string id) where T : IVersionable
    {
        var type = typeof(T);
        
        var primaryProperty = GetPrimaryPropertyInfo<T>();
        
        if (_entities.TryGetValue(type, out var entityList))
        {
            var entityIndex = GetEntityIndex(entityList, primaryProperty, id);
            
            if (entityIndex != -1)
            {
                validator.Validate((T)entityList[entityIndex], _entities, _references, DataBaseAction.Delete);
                referenceHandler.HandleDelete((T)entityList[entityIndex], _references);
                entityList.RemoveAt(entityIndex);
                return;
            }
        }
        
        throw new InvalidOperationException($"{type.Name} having {primaryProperty.Name} with value {id} was not found");
    }
    
    public IEnumerable<T> FetchAll<T>() where T : IVersionable
    {
        var type = typeof(T);
        
        if (_entities.TryGetValue(type, out var entityList))
        {
            return entityList.Cast<T>().Select(DeepCopy);
        }
        else
        {
            return [];
        }
    }
    
    public T? FetchById<T>(string id) where T : class, IVersionable
    {
        var type = typeof(T);
        
        var primaryProperty = GetPrimaryPropertyInfo<T>();
        
        if (_entities.TryGetValue(type, out var entityList))
        {
            var entityIndex = GetEntityIndex(entityList, primaryProperty, id);
            
            if (entityIndex != -1)
            {
                return DeepCopy((T)entityList[entityIndex]);
            }
        }
        
        return null;
    }
    
    private static int GetEntityIndex(List<IVersionable> entityList, PropertyInfo primaryProperty, string id)
        => entityList.FindIndex(e => (string)primaryProperty.GetValue(e)! == id);
    
    private static T DeepCopy<T>(T entity) => Copier.Copy(entity) ?? throw new ArgumentException("entity cannot be copied by Copier");
    
    private static PropertyInfo GetPrimaryPropertyInfo<T>()
    {
        var properties = typeof(T).GetProperties();
        return properties.First(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
    }
}
