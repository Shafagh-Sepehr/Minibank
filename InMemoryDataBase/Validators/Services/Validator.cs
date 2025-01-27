using Abstractions.InMemoryDatabase;
using InMemoryDataBase.Entities.Classes;
using InMemoryDataBase.Validators.Abstractions;

namespace InMemoryDataBase.Validators.Services;

public class Validator(
    IPrimaryKeyValidator primaryKeyValidator,
    IForeignKeyValidator foreignKeyValidator,
    INullablePropertyValidator nullablePropertyValidator,
    IDeletionIntegrityValidator deletionIntegrityValidator) : IValidator
{
    public void ValidateInsert<T>(T entity, IReadOnlyDictionary<Type, List<IInMemoryDBVersionable>> entities)
    {
        primaryKeyValidator.Validate(entity, entities);
        nullablePropertyValidator.Validate(entity);
        foreignKeyValidator.Validate(entity, entities);
    }
    
    public void ValidateUpdate<T>(T entity, IReadOnlyDictionary<Type, List<IInMemoryDBVersionable>> entities)
    {
        nullablePropertyValidator.Validate(entity);
        foreignKeyValidator.Validate(entity, entities);
    }
    
    public void ValidateDelete<T>(T entity, IReadOnlyDictionary<Type, List<IInMemoryDBVersionable>> entities, List<Reference> references)
    {
        deletionIntegrityValidator.Validate(entity, entities, references);
    }
}
