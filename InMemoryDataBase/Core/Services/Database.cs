﻿using System.Reflection;
using System.Text;
using DeepCopier;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Core.Abstractions;
using InMemoryDataBase.DataSanitizers.Abstractions;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Core.Services;

public class Database(
    IPrimaryKeyValidator primaryKeyValidator,
    IForeignKeyValidator foreignKeyValidator,
    INullablePropertyValidator nullablePropertyValidator,
    IDefaultValueSetter defaultValueSetter) : IDatabase
{
    private readonly Dictionary<string, List<object>> _entities  = new();
    private readonly Dictionary<string, string>       _entityIds = new();
    
    
    public void Insert<T>(T entity)
    {
        var typeName = typeof(T).Name;
        var entityCopy = DeepCopy(entity)!;
        
        
        defaultValueSetter.Apply(entity);
        primaryKeyValidator.Validate(entity, _entities);
        foreignKeyValidator.Validate(entity, _entities);
        nullablePropertyValidator.Validate(entity);
        
        
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
        
        foreignKeyValidator.Validate(entity, _entities);
        nullablePropertyValidator.Validate(entity);
        
        var properties = typeof(T).GetProperties();
        var primaryProperty = properties
            .First(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
        
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
    
    private static T DeepCopy<T>(T entity) => Copier.Copy(entity) ?? throw new ArgumentException("entity cannot be copied by Copier.");
}
