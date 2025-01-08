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
            var count = 0;
            
            if (propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) != null)
            {
                count++;
            }
            
            if (propertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute), true) != null)
            {
                count++;
            }
            
            if (propertyInfo.GetCustomAttribute(typeof(DefaultValueAttribute), true) != null)
            {
                count++;
            }
            
            if (propertyInfo.GetCustomAttribute(typeof(NullableAttribute), true) != null)
            {
                count++;
            }
            
            if (count > 1)
            {
                throw new DatabaseException($"type `{type}`'s `{propertyInfo.Name}` property can't have more than one attribute");
            }
        }
    }
}
