using Abstractions.InMemoryDatabase;
using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IDeletionIntegrityValidator
{
    void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IInMemoryDBVersionable>> entities, List<Reference> references);
}
