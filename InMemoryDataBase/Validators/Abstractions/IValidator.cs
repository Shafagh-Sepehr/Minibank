using InMemoryDataBase.Entities.Classes;
using InMemoryDataBase.Interfaces;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IValidator
{
    void ValidateInsert<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities);
    void ValidateUpdate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities);
    void ValidateDelete<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities, List<Reference> references);
}
