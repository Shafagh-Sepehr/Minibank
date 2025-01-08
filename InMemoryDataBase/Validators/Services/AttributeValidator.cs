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
            var hasPrimaryAttribute = false;
            var hasForeignAttribute = false;
            var hasDefaultValueAttribute = false;
            var hasNullableAttribute = false;
            
            if (propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) != null)
            {
                count++;
                hasPrimaryAttribute = true;
            }
            
            if (propertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute), true) != null)
            {
                count++;
                hasForeignAttribute = true;
            }
            
            if (propertyInfo.GetCustomAttribute(typeof(DefaultValueAttribute), true) != null)
            {
                count++;
                hasDefaultValueAttribute = true;
            }
            
            if (propertyInfo.GetCustomAttribute(typeof(NullableAttribute), true) != null)
            {
                count++;
                hasNullableAttribute = true;
            }
            
            switch (count)
            {
                case <= 1:
                    continue;
                case 2 when hasForeignAttribute && hasNullableAttribute:
                    return;
                default:
                    throw new DatabaseException($"type `{type}`'s `{propertyInfo.Name}` property can't have more than one attribute");
            }
        }
    }
}
