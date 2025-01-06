namespace InMemoryDataBase.DataSanitizers.Abstractions;

public interface IDefaultValueSetter
{
    void Apply<T>(T entity);
}
