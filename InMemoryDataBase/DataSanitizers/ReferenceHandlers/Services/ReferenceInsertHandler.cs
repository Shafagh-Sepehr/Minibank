using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;
using InMemoryDataBase.Entities.Classes;
using InMemoryDataBase.Exceptions;

namespace InMemoryDataBase.DataSanitizers.ReferenceHandlers.Services;

public class ReferenceInsertHandler : IReferenceInsertHandler
{
    public void Handle<T>(T entity, List<Reference> references)
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        var primaryProperty = Helper.GetPrimaryPropertyInfo(type);
        
        var foreignPropertyAndMasterTypePairs = properties
            .Select(p => new
            {
                MasterType = (p.GetCustomAttribute(typeof(ForeignKeyAttribute), true) as ForeignKeyAttribute)?.ReferenceType,
                ForeignProperty = p,
            }).Where(pair => pair.MasterType != null);
        
        foreach (var pair in foreignPropertyAndMasterTypePairs)
        {
            references.Add(new()
            {
                MasterType = pair.MasterType ?? throw new DatabaseException("some how Master type is null in referenceInsertHandler"),
                SlaveType = type,
                MasterId = Helper.GetStringValueFromProperty(pair.ForeignProperty, entity),
                SlaveId = Helper.GetStringValueFromProperty(primaryProperty, entity),
            });
        }
    }
}
