using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;

public interface IReferenceDeleteHandler
{
    void Handle<T>(T entity, List<Reference> references);
}
