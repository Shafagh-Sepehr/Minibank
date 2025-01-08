using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;

public interface IReferenceInsertHandler
{
    void Handle<T>(T entity, List<Reference> references);
}
