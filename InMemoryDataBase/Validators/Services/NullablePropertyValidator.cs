using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class NullablePropertyValidator : INullablePropertyValidator
{
    public void Validate<T>(T entity)
    {
        var properties = typeof(T).GetProperties();
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.GetCustomAttribute(typeof(NullableAttribute), true) is NullableAttribute defaultValueAttribute) continue;
            if (propertyInfo.GetValue(entity) == null)
            {
                throw new DatabaseException($"Property `{propertyInfo.Name}` is null while its not nullable");
            }
        }
    }
}
