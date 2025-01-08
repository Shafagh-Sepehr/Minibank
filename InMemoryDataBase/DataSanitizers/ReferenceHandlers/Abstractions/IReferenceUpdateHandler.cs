using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;

public interface IReferenceUpdateHandler
{
    void Handle<T>(T entity, T oldEntity, List<Reference> references);
}
