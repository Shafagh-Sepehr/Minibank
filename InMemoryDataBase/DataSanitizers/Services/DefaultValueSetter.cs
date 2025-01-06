using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.DataSanitizers.Abstractions;
using InMemoryDataBase.Exceptions;

namespace InMemoryDataBase.DataSanitizers.Services;

public class DefaultValueSetter : IDefaultValueSetter
{
    public void Apply<T>(T entity)
    {
        var properties = typeof(T).GetProperties();
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.GetCustomAttribute(typeof(DefaultValueAttribute), true) is not DefaultValueAttribute defaultValueAttribute)
            {
                continue;
            }
            
            if (defaultValueAttribute.DefaultValue.GetType() != propertyInfo.PropertyType)
            {
                throw new DatabaseException(
                    $"the default value of property `{propertyInfo.Name}` must be of type `{propertyInfo.PropertyType.Name}`, but was `{defaultValueAttribute.DefaultValue.GetType()}` was given");
            }
            
            if (propertyInfo.GetValue(entity) == null)
            {
                propertyInfo.SetValue(entity, defaultValueAttribute.DefaultValue);
            }
        }
    }
}
