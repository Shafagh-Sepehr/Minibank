using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class DefaultValueValidator : IDefaultValueValidator
{
    public void Validate<T>(T entity, PropertyInfo[] properties)
    {
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.GetCustomAttribute(typeof(DefaultValueAttribute), true) is not DefaultValueAttribute defaultValueAttribute)
            {
                continue;
            }
            
            if(defaultValueAttribute.DefaultValue.GetType() != propertyInfo.PropertyType)
            {
                throw new DatabaseException($"the default value of property `{propertyInfo.Name}` must be of type `{propertyInfo.PropertyType.Name}`, but was `{defaultValueAttribute.DefaultValue.GetType()}` was given");
            }
            if (propertyInfo.GetValue(entity) == null)
            {
                propertyInfo.SetValue(entity,defaultValueAttribute.DefaultValue);
            }
        }
    }
}
