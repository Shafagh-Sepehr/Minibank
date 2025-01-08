using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Interfaces;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class PrimaryKeyValidator : IPrimaryKeyValidator
{
    public void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities)
    {
        var type = typeof(T);
        var properties = typeof(T).GetProperties();
        var primaryProperties = properties
            .Where(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute)
            .ToList();
        
        switch (primaryProperties.Count)
        {
            case 0:
                throw new DatabaseException($"you must have one string type marked with primary key attribute in `{type.Name}`");
            case > 1:
                throw new DatabaseException($"you cannot have more than one string type marked with primaryKeyAttribute in `{type.Name}`");
        }
        
        var primaryProperty = primaryProperties.First();
        if (primaryProperty.PropertyType != typeof(string))
        {
            throw new DatabaseException($"the primary key property must be of type string in `{type.Name}`");
        }
        
        if (primaryProperty.GetValue(entity) == null)
        {
            throw new DatabaseException($"Invalid primary key, `{type.Name}` can't have `null` as it's `{primaryProperty.Name}`");
        }
        
        if (!entities.TryGetValue(type, out var entityList))
        {
            return;
        }
        
        if (entityList.All(e => primaryProperty.GetValue(e) != primaryProperty.GetValue(entity)))
        {
            return;
        }
        
        throw new DatabaseException(
            $"Primary key must be unique, an entity with `{primaryProperty.PropertyType.Name}` `{primaryProperty.Name}` = `{primaryProperty.GetValue(entity)}` of type `{type.Name}` is already present in the database");
    }
}
