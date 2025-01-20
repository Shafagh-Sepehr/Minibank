using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.DataSanitizers.Abstractions;
using InMemoryDataBase.Exceptions;

namespace InMemoryDataBase.DataSanitizers.Services;

public class DefaultValueSetter : IDefaultValueSetter
{
    public void Apply<T>(T entity)
    {
        var propertyAttributePairs = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<DefaultValueAttribute>() != null)
            .Select(p => new
            {
                Property = p,
                Attribute = p.GetCustomAttribute<DefaultValueAttribute>() ?? throw new ArgumentNullException(),
            });
        
        foreach (var pair in propertyAttributePairs)
        {
            if (pair.Attribute.DefaultValue.GetType() != pair.Property.PropertyType)
            {
                throw new DatabaseException(
                    $"the default value of property `{pair.Property.Name}` must be of type `{pair.Property.PropertyType.Name}`, but was `{pair.Attribute.DefaultValue.GetType()}` was given");
            }
            
            if (pair.Property.GetValue(entity) == null)
            {
                pair.Property.SetValue(entity, pair.Attribute.DefaultValue);
            }
        }
    }
}
