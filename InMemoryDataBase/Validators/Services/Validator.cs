using InMemoryDataBase.Entities.Classes;
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
    public void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities, List<Reference> references, DataBaseAction dataBaseAction)
    {
        switch (dataBaseAction)
        {
            case DataBaseAction.Save:
                primaryKeyValidator.Validate(entity, entities);
                nullablePropertyValidator.Validate(entity);
                foreignKeyValidator.Validate(entity, entities);
                break;
            case DataBaseAction.Update:
                nullablePropertyValidator.Validate(entity);
                foreignKeyValidator.Validate(entity, entities);
                break;
            case DataBaseAction.Delete:
                deletionIntegrityValidator.Validate(entity, entities, references);
                break;
            default:
                throw new DatabaseException("Wrong invocation of Validator in Database");
        }
        
        
    }
}
