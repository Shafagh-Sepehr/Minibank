using InMemoryDataBase.Entities.Classes;
using InMemoryDataBase.Interfaces;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IDeletionIntegrityValidator
{
    void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities, List<Reference> references);
}
