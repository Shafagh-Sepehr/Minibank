using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Interfaces;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class DeletionIntegrityValidator : IDeletionIntegrityValidator
{
    public void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities, IReadOnlyDictionary<Type, HashSet<Type>> entityRelatives)
    {
        var type = typeof(T);
        if (!entityRelatives.TryGetValue(type, out var referenceTypes))
        {
            return;
        }
        var properties = type.GetProperties();
        var primaryProperty = properties
            .First(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
        
        foreach (var referenceType in referenceTypes)
        {
            var referenceProperties = referenceType.GetProperties();
            foreach (var propertyInfo in referenceProperties)
            {
                if (!PropertyIsAForeignKey(propertyInfo,out var foreignKeyAttribute))
                {
                    continue;
                }
                if (ForeignKeyReferencesThisType(foreignKeyAttribute, type))
                {
                    continue;
                }
                if (!entities.TryGetValue(referenceType, out var referenceEntityList))
                {
                    continue;
                }
                if (referenceEntityList.Any(x => propertyInfo.GetValue(x) == primaryProperty.GetValue(entity)))
                {
                    throw new DatabaseException($"Cannot delete an entity which is referenced by other entities, `{type.Name}` with primary key `{primaryProperty.Name}` with value `{primaryProperty.GetValue(entity)}` is referenced by a `{referenceType.Name}`'s `{propertyInfo.Name}` property");
                }
            }
        }
    }
    
    private bool PropertyIsAForeignKey(PropertyInfo propertyInfo,[MaybeNullWhen(false)] out ForeignKeyAttribute foreignKeyAttribute)
    {
        if (propertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute), true) is ForeignKeyAttribute foreignKeyAttr)
        {
            foreignKeyAttribute = foreignKeyAttr;
            return true;
        }
        else
        {
            foreignKeyAttribute = null;
            return false;
        }
    }
    
    private bool ForeignKeyReferencesThisType(ForeignKeyAttribute foreignKeyAttribute, Type type) => foreignKeyAttribute.ReferenceType != type;
}
