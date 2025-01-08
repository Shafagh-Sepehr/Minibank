using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;
using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.ReferenceHandlers.Services;

public class ReferenceDeleteHandler : IReferenceDeleteHandler
{
    public void Handle<T>(T entity, List<Reference> references)
    {
        var type = typeof(T);
        var primaryProperty = GetPrimaryPropertyInfo<T>();
        
        references.RemoveAll(r => r.SlaveId == (string)primaryProperty.GetValue(entity)! && r.SlaveType == type);
    }
    
    private static PropertyInfo GetPrimaryPropertyInfo<T>()
    {
        var properties = typeof(T).GetProperties();
        return properties.First(propertyInfo => propertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
    }
}
