using InMemoryDataBase.Entities.Enums;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IValidator
{
    void Validate<T>(T entity, IReadOnlyDictionary<Type, List<object>> entities, DataBaseAction dataBaseAction);
}
