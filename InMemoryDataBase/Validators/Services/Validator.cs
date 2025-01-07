using InMemoryDataBase.Entities.Enums;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Interfaces;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class Validator(
    IPrimaryKeyValidator primaryKeyValidator,
    IForeignKeyValidator foreignKeyValidator,
    INullablePropertyValidator nullablePropertyValidator,
    IDeletionIntegrityValidator deletionIntegrityValidator) : IValidator
{
    public void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities, Dictionary<Type, HashSet<Type>> entityRelatives, DataBaseAction dataBaseAction)
    {
        switch (dataBaseAction)
        {
            case DataBaseAction.Save:
                primaryKeyValidator.Validate(entity, entities);
                foreignKeyValidator.Validate(entity, entities, entityRelatives);
                nullablePropertyValidator.Validate(entity);
                break;
            case DataBaseAction.Update:
                foreignKeyValidator.Validate(entity, entities, entityRelatives);
                nullablePropertyValidator.Validate(entity);
                break;
            case DataBaseAction.Delete:
                deletionIntegrityValidator.Validate(entity, entities, entityRelatives);
                break;
            default:
                throw new DatabaseException("Wrong invocation of Validator in Database");
        }
        
        
    }
}
