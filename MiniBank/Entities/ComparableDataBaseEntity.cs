using MiniBank.Data.Abstractions;

namespace MiniBank.Entities;

public abstract class ComparableDataBaseEntity : IDatabaseEntity
{
    public long Id { get; set; }
    
    public int CompareTo(IDatabaseEntity? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Id.CompareTo(other.Id);
    }
}
