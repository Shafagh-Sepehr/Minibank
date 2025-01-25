
using Abstractions.InMemoryDatabase;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IForeignKeyValidator
{
    void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities);
}
