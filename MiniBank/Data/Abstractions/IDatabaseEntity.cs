namespace MiniBank.Data.Abstractions;

public interface IDatabaseEntity : IComparable<IDatabaseEntity>
{
    long Id { get; set; }
}
