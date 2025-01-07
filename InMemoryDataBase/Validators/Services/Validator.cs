using InMemoryDataBase.Entities.Enums;
using InMemoryDataBase.Exceptions;
using InMemoryDataBase.Interfaces;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class Validator(
    IPrimaryKeyValidator primaryKeyValidator,
    IForeignKeyValidator foreignKeyValidator,
    INullablePropertyValidator nullablePropertyValidator) : IValidator
{
    public void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities, DataBaseAction dataBaseAction)
    {
        switch (dataBaseAction)
        {
            case DataBaseAction.Save:
                primaryKeyValidator.Validate(entity, entities);
                break;
            case DataBaseAction.Update:
                break;
            default:
                throw new DatabaseException("Wrong invocation of Validator in Database");
        }
        
        foreignKeyValidator.Validate(entity, entities);
        nullablePropertyValidator.Validate(entity);
    }
}
