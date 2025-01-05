using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class PrimaryKeyValidator : IPrimaryKeyValidator
{
    public void Validate<T>(T entity, PropertyInfo[] properties,IReadOnlyDictionary<string, List<object>> entities)
    {
        var typeName = typeof(T).Name;
        
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is not PrimaryKeyAttribute)
            {
                continue;
            }
            if (!entities.TryGetValue(typeName, out var entityList))
            {
                continue;
            }
            
            if (entityList.Any(x => propertyInfo.GetValue(x) == propertyInfo.GetValue(entity)))
            {
                throw new DatabaseException(
                    $"Primary key must be unique, value `{propertyInfo.GetValue(entity)}` of property `{propertyInfo.Name}` of type `{typeName}` is already present in the database");
            }
        }
    }
}
