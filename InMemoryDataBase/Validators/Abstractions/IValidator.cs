using InMemoryDataBase.Entities.Enums;

namespace InMemoryDataBase.Validators.Services;

public interface IValidator
{
    void Validate<T>(T entity, IReadOnlyDictionary<string, List<object>> entities, DataBaseAction dataBaseAction);
}
