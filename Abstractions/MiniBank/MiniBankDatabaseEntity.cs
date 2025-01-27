namespace Abstractions.MiniBank;

public abstract class MiniBankDatabaseEntity : IMiniBankVersionable
{
    public virtual string Id { get; set; } = string.Empty;
    int IMiniBankVersionable.Version { get; set; }
}
