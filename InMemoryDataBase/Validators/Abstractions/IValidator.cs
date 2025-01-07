using InMemoryDataBase.Entities.Enums;
using InMemoryDataBase.Interfaces;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IValidator
{
    void Validate<T>(T entity, IReadOnlyDictionary<Type, List<IVersionable>> entities, Dictionary<Type, HashSet<Type>> entityRelatives,
                     DataBaseAction dataBaseAction);
}
