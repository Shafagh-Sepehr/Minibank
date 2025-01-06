namespace InMemoryDataBase.Validators.Abstractions;

public interface IForeignKeyValidator
{
    void Validate<T>(T entity, IReadOnlyDictionary<string, List<object>> entities);
}
