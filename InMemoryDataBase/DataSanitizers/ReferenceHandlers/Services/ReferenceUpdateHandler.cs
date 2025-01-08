using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;
using InMemoryDataBase.Entities.Classes;
using InMemoryDataBase.Exceptions;

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
            
            if (ValueToValue(pair.ForeignProperty,entity,oldEntity))
            {
                var reference = references.First(r =>
                    r.MasterType == pair.MasterType &&
                    r.SlaveType == type &&
                    r.MasterId == Helper.GetStringValueFromProperty(pair.ForeignProperty, oldEntity) &&
                    r.SlaveId == Helper.GetStringValueFromProperty(primaryProperty, entity)
                );
                reference.MasterId = Helper.GetStringValueFromProperty(pair.ForeignProperty, entity);
            }
            else if (ValueToNull(pair.ForeignProperty,entity,oldEntity))
            {
                references.RemoveAll(r =>
                    r.SlaveType == type &&
                    r.MasterType == pair.MasterType &&
                    r.SlaveId == Helper.GetStringValueFromProperty(primaryProperty, oldEntity) &&
                    r.MasterId == Helper.GetStringValueFromProperty(pair.ForeignProperty, oldEntity)
                );
            }
            else if (NullToValue(pair.ForeignProperty,entity,oldEntity))
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
    
    private static bool NullToValue<T>(PropertyInfo property, T entity, T oldEntity) 
        => property.GetValue(oldEntity) is null && property.GetValue(entity) is not null;
    
    private static bool ValueToNull<T>(PropertyInfo property, T entity, T oldEntity)
        => property.GetValue(oldEntity) is not null && property.GetValue(entity) is null;
    
    private static bool ValueToValue<T>(PropertyInfo property, T entity, T oldEntity)
        => property.GetValue(oldEntity) is not null && property.GetValue(entity) is not null;
    
    private static bool RefStillTheSame<T>(T entity, T oldEntity, PropertyInfo foreignProperty)
        => foreignProperty.GetValue(entity) == foreignProperty.GetValue(oldEntity);
}
