using System.Reflection;

namespace InMemoryDataBase.DataSanitizers.Abstractions;

public interface IDefaultValueSetter
{
    void Apply<T>(T entity, PropertyInfo[] properties);
}
