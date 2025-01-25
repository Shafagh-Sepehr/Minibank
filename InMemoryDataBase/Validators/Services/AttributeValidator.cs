using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class AttributeValidator : IAttributeValidator
{
    public void Validate<T>()
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        
        foreach (var propertyInfo in properties)
        {
            if (PropertyHasMoreThanOneAttribute(propertyInfo) && PropertyHasAttributesOtherThanForeignKeyAttributeAndNullableAttribute(propertyInfo))
            {
                throw new DatabaseException($"type `{type}`'s `{propertyInfo.Name}` property can't have more than one attribute");
            }
        }
    }
    
    private static bool PropertyHasMoreThanOneAttribute(PropertyInfo propertyInfo) => propertyInfo.CustomAttributes
        .Count(p => p.AttributeType.Namespace == typeof(ForeignKeyAttribute).Namespace) > 1;
    
    private static bool PropertyHasAttributesOtherThanForeignKeyAttributeAndNullableAttribute(PropertyInfo propertyInfo)
        => propertyInfo.CustomAttributes
            .Any(a => a.AttributeType == typeof(PrimaryKeyAttribute) || a.AttributeType == typeof(DefaultMemberAttribute));
}
