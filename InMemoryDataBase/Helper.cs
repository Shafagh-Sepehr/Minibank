using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Exceptions;

namespace InMemoryDataBase;

public static class Helper
{
    public static PropertyInfo GetPrimaryPropertyInfo(Type type)
    {
        var properties = type.GetProperties();
        return properties.First(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
    }
    
    public static string GetNonNullStringValueFromPropertyOrThrow<T>(PropertyInfo propertyInfo, T entity)
    {
        if (propertyInfo.GetValue(entity) is string value)
        {
            return value;
        }
        else
        {
            throw new DatabaseException($"property of entity {typeof(T).Name} value isn't of type string or is null");
        }
    }
}
