using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.DataSanitizers.Abstractions;
using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.Services;

public class ReferenceHandler : IReferenceHandler
{
    public void HandleInsert<T>(T entity, List<Reference> references)
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        var primaryProperty = properties
            .First(p => p.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
        
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
                MasterType = pair.MasterType!,
                SlaveType = type,
                MasterId = (string)pair.ForeignProperty.GetValue(entity)!,
                SlaveId = (string)primaryProperty.GetValue(entity)!,
            });
        }
    }
    
    public void HandleUpdate<T>(T entity, T oldEntity, List<Reference> references)
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        var primaryProperty = properties
            .First(p => p.GetCustomAttribute(typeof(PrimaryKeyAttribute), true) is PrimaryKeyAttribute);
        
        var foreignPropertyAndMasterTypePairs = properties
            .Select(p => new
            {
                MasterType = (p.GetCustomAttribute(typeof(ForeignKeyAttribute), true) as ForeignKeyAttribute)?.ReferenceType,
                ForeignProperty = p,
            }).Where(pair => pair.MasterType != null);
        
        foreach (var pair in foreignPropertyAndMasterTypePairs)
        {
            if (RefStillTheSame(entity,oldEntity,pair.ForeignProperty))
            {
                continue;
            }
            
            var reference = references.First(r =>
                r.MasterType == pair.MasterType &&
                r.SlaveType == type &&
                r.MasterId == (string)pair.ForeignProperty.GetValue(oldEntity)! &&
                r.SlaveId == (string)primaryProperty.GetValue(entity)!
            );
            
            reference.MasterId = (string)pair.ForeignProperty.GetValue(entity)!;
        }
    }
    
    private bool RefStillTheSame<T>(T entity, T oldEntity, PropertyInfo foreignProperty)
        => foreignProperty.GetValue(entity) == foreignProperty.GetValue(oldEntity);
}
