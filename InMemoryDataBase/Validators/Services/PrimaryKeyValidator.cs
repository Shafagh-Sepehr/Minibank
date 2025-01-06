using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class PrimaryKeyValidator : IPrimaryKeyValidator
{
    public void Validate<T>(T entity, IReadOnlyDictionary<string, List<object>> entities)
    {
        var typeName = typeof(T).Name;
        var properties = typeof(T).GetProperties();
        var primaryProperties = properties
            .Where(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute)
            .ToList();
        
        switch (primaryProperties.Count)
        {
            case 0:
                throw new DatabaseException($"you must have one string type marked with primary key attribute in {typeName}");
            case > 1:
                throw new DatabaseException($"you cannot have more than one string type marked with primaryKeyAttribute in {typeName}");
        }
        
        var primaryProperty = primaryProperties.First();
        if (primaryProperty.PropertyType != typeof(string))
        {
            throw new DatabaseException($"the primary key property must be of type string in {typeName}");
        }
        
        if (!entities.TryGetValue(typeName, out var entityList))
        {
            return;
        }
        
        if (entityList.All(e => primaryProperty.GetValue(e) != primaryProperty.GetValue(entity)))
        {
            return;
        }
        
        throw new DatabaseException(
            $"Primary key must be unique, an entity with `{primaryProperty.PropertyType.Name}` `{primaryProperty.Name}` = `{primaryProperty.GetValue(entity)}` of type `{typeName}` is already present in the database");
    }
}
