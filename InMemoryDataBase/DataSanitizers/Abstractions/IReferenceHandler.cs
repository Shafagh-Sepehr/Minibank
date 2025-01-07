using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.Abstractions;

public interface IReferenceHandler
{
    void HandleInsert<T>(T entity, List<Reference> references);
    public void HandleUpdate<T>(T entity, T oldEntity, List<Reference> references);
}
