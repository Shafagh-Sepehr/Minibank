namespace InMemoryDataBase.Validators.Abstractions;

public interface IPrimaryKeyValidator
{
    void Validate<T>(T entity, IReadOnlyDictionary<string, List<object>> entities);
}
