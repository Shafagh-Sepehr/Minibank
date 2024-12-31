using MiniBank.Data.Abstractions;

namespace MiniBank.Entities;

public abstract class ComparableDataBaseEntity : IDatabaseEntity
{
    public long Id { get; set; }
}
