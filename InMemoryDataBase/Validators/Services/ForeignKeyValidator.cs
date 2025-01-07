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
            
            var referenceType = foreignKeyAttribute.ReferenceType;
            var referencePropertyInfo = referenceType
                .GetProperties()
                .First(rp => rp.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
            
            var referenceListExists = entities.TryGetValue(referenceType, out var referenceEntityList);
            
            if (propertyInfo.GetValue(entity) == null)
            {
                throw new DatabaseException($"Invalid foreign key, `{type.Name}` can't have `null` as it's `{propertyInfo.Name}`");
            }
            
            var referenceIdNotExists = referenceEntityList?.All(e => referencePropertyInfo.GetValue(e) != propertyInfo.GetValue(entity));
            
            if (!referenceListExists || referenceIdNotExists.GetValueOrDefault())
            {
                throw new DatabaseException(
                    $"Invalid foreign key, No `{referenceType.Name}` having value `{propertyInfo.GetValue(entity)}` for its `{propertyInfo.Name}` property found in the database");
            }
        }
    }
}
