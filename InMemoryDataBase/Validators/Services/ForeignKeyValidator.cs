using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Interfaces;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class ForeignKeyValidator : IForeignKeyValidator
{
    public void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities,
                            Dictionary<Type, HashSet<Type>> entityRelatives)
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
            var referenceIdNotExists = referenceEntityList?.All(e => referencePropertyInfo.GetValue(e) != propertyInfo.GetValue(entity));
            
            if (!referenceListExists || referenceIdNotExists.GetValueOrDefault())
            {
                throw new DatabaseException(
                    $"Invalid foreign key, No `{referenceType.Name}` having value `{propertyInfo.GetValue(entity)}` for its `{propertyInfo.Name}` property found in the database");
            }
            
            if (entityRelatives.TryGetValue(referenceType, out var relativesSet))
            {
                relativesSet.Add(type);
            }
            else
            {
                entityRelatives[referenceType] = [type,];
            }
        }
    }
}
