using System.Reflection;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IForeignKeyValidator
{
    void Validate<T>(T entity, PropertyInfo[] properties,IReadOnlyDictionary<string, List<object>> entities);
}
