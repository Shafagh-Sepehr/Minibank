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
            if(propertyInfo.CustomAttributes.Count() > 1 && propertyInfo.CustomAttributes.All(a=>a.AttributeType == typeof(ForeignKeyAttribute) || a.AttributeType == typeof(NullableAttribute)))
            {
                throw new DatabaseException($"type `{type}`'s `{propertyInfo.Name}` property can't have more than one attribute");
            }
        }
    }
}
