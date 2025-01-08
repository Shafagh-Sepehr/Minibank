using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.Entities.Classes;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Interfaces;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class DeletionIntegrityValidator : IDeletionIntegrityValidator
{
    public void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities, List<Reference> references)
    {
        var type = typeof(T);
        var primaryProperty = Helper.GetPrimaryPropertyInfo(type);
        var reference = references.FirstOrDefault(r => r.MasterType == type && r.MasterId == (string)primaryProperty.GetValue(entity)!);
        
        if (reference == null)
        {
            return;
        }
        
        var slaveProperty = reference.SlaveType.GetProperties()
            .First(p =>
                p.GetCustomAttribute(typeof(ForeignKeyAttribute), true) is ForeignKeyAttribute fkAttribute &&
                fkAttribute.ReferenceType == type
            );
        
        throw new DatabaseException(
            $"Cannot delete an entity which is referenced by other entities, `{type.Name}` with primary key `{primaryProperty.Name}` with value `{primaryProperty.GetValue(entity)}` is referenced by a `{reference.SlaveType.Name}`'s `{slaveProperty.Name}` property");
    }
}
