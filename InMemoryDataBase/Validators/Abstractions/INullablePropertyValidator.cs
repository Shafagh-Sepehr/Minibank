using System.Reflection;

namespace InMemoryDataBase.Validators.Abstractions;

public interface INullablePropertyValidator
{
    void Validate<T>(T entity);
}
