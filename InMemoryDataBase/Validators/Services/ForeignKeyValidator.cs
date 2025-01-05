using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class ForeignKeyValidator : IForeignKeyValidator
{
    public void Validate<T>(T entity, PropertyInfo[] properties,IReadOnlyDictionary<string, List<object>> entities)
    {
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute), true) is not ForeignKeyAttribute foreignKeyAttribute)
            {
                continue;
            }
            
            var referenceType = foreignKeyAttribute.ReferenceType;
            var referenceTypeName = referenceType.Name;
            var propertyName = foreignKeyAttribute.PropertyName;
            var referencePropertyInfo = referenceType.GetProperties().First(x => x.Name == propertyName);
            
            if (!entities.TryGetValue(referenceTypeName, out var referenceEntityList))
            {
                continue;
            }
            
            if (referenceEntityList.All(x => referencePropertyInfo.GetValue(x) != propertyInfo.GetValue(entity)))
            {
                throw new DatabaseException(
                    $"Invalid foreign key, No `{referenceTypeName}` having value `{propertyInfo.GetValue(entity)}` for its `{propertyInfo.Name}` property found in the database");
            }
        }
    }
}
