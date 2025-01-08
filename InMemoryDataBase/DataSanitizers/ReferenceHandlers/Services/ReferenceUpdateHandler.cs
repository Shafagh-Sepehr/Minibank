using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;
using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.ReferenceHandlers.Services;

public class ReferenceUpdateHandler : IReferenceUpdateHandler
{
    public void Handle<T>(T entity, T oldEntity, List<Reference> references)
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
            if (RefStillTheSame(entity, oldEntity, pair.ForeignProperty))
            {
                continue;
            }
            
            var reference = references.First(r =>
                r.MasterType == pair.MasterType &&
                r.SlaveType == type &&
                r.MasterId == Helper.GetStringValueFromProperty(pair.ForeignProperty, oldEntity) &&
                r.SlaveId == Helper.GetStringValueFromProperty(primaryProperty, entity)
            );
            
            reference.MasterId = Helper.GetStringValueFromProperty(pair.ForeignProperty, entity);
        }
    }
    
    private static bool RefStillTheSame<T>(T entity, T oldEntity, PropertyInfo foreignProperty)
        => foreignProperty.GetValue(entity) == foreignProperty.GetValue(oldEntity);
}
