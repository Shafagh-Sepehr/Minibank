using System.Reflection;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IDefaultValueValidator
{
    void Validate<T>(T entity, PropertyInfo[] properties);
}
