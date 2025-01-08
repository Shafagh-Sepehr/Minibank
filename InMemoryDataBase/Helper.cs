using System.Reflection;
using InMemoryDataBase.Attributes;

namespace InMemoryDataBase;

public static class Helper
{
    public static PropertyInfo GetPrimaryPropertyInfo(Type type)
    {
        var properties = type.GetProperties();
        return properties.First(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
    }
}
