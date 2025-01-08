using InMemoryDataBase.DataSanitizers.ReferenceHandlers.Abstractions;
using InMemoryDataBase.Entities.Classes;

namespace InMemoryDataBase.DataSanitizers.ReferenceHandlers.Services;

public class ReferenceDeleteHandler : IReferenceDeleteHandler
{
    public void Handle<T>(T entity, List<Reference> references)
    {
        var type = typeof(T);
        var primaryProperty = Helper.GetPrimaryPropertyInfo(type);
        
        references.RemoveAll(r => r.SlaveId == Helper.GetNonNullStringValueFromPropertyOrThrow(primaryProperty, entity) && r.SlaveType == type);
    }
}
