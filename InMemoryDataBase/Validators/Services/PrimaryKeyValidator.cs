using System.Reflection;
using System.Text;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class PrimaryKeyValidator : IPrimaryKeyValidator
{
    public void Validate<T>(T entity, PropertyInfo[] properties,IReadOnlyDictionary<string, List<object>> entities)
    {
        var typeName = typeof(T).Name;
        var primaryProperties = properties
            .Where(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute)
            .ToList();
        
        if (primaryProperties.Count == 0)
        {
            throw new DatabaseException($"At least one property must be primary key in {typeName}");
        }
        if (!entities.TryGetValue(typeName, out var entityList))
        {
            return;
        }
        if (false == entityList.Any(e => primaryProperties.All(p => p.GetValue(e) == p.GetValue(entity))))
        {
            return;
        }
        
        var builder = new StringBuilder();
        builder.Append("Primary key(s) must be unique, an entity with ");
        
        foreach (var propertyInfo in primaryProperties)
        {
            builder.Append($"[`{propertyInfo.PropertyType.Name}` `{propertyInfo.Name}` = `{propertyInfo.GetValue(entity)}` of type `{typeName}`], ");
        }
        
        builder.Append(" is already present in the database");
        
        throw new DatabaseException(builder.ToString());
    }
}
