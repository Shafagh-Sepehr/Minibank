using System.Reflection;
using InMemoryDataBase.Attributes;
using InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;
using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.ReferenceHandlers.Services;

public class ReferenceHandler(IReferenceInsertHandler insertHandler,
                              IReferenceUpdateHandler updateHandler, IReferenceDeleteHandler deleteHandler)
    : IReferenceHandler
{
    public void HandleInsert<T>(T entity, List<Reference> references)
        => insertHandler.Handle(entity, references);
    
    public void HandleUpdate<T>(T entity, T oldEntity, List<Reference> references)
        => updateHandler.Handle(entity, oldEntity, references);
    
    public void HandleDelete<T>(T entity, List<Reference> references)
        => deleteHandler.Handle(entity, references);
}
