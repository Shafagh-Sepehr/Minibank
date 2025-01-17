﻿using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Interfaces;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class ForeignKeyValidator : IForeignKeyValidator
{
    public void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities)
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute), true) is not ForeignKeyAttribute foreignKeyAttribute)
            {
                continue;
            }
            
            if (propertyInfo.PropertyType != typeof(string))
            {
                throw new DatabaseException($"foreign key property `{propertyInfo.Name}` must be of type string in `{type.Name}`");
            }
            
            if (propertyInfo.GetValue(entity) == null)
            {
                continue;
            }
            
            var referenceType = foreignKeyAttribute.ReferenceType;
            var referencePropertyInfo = Helper.GetPrimaryPropertyInfo(referenceType);
            
            var referenceListExists = entities.TryGetValue(referenceType, out var referenceEntityList);
            var referenceIdNotExists = referenceEntityList?.All(e => referencePropertyInfo.GetValue(e) != propertyInfo.GetValue(entity));
            
            if (!referenceListExists || referenceIdNotExists.GetValueOrDefault())
            {
                throw new DatabaseException(
                    $"Invalid foreign key, No `{referenceType.Name}` having value `{propertyInfo.GetValue(entity)}` for its `{propertyInfo.Name}` property found in the database");
            }
        }
    }
}
