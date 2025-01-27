using Abstractions.InMemoryDatabase;
using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IValidator
{
    void ValidateInsert<T>(T entity, IReadOnlyDictionary<Type, List<IInMemoryDBVersionable>> entities);
    void ValidateUpdate<T>(T entity, IReadOnlyDictionary<Type, List<IInMemoryDBVersionable>> entities);
    void ValidateDelete<T>(T entity, IReadOnlyDictionary<Type, List<IInMemoryDBVersionable>> entities, List<Reference> references);
}
