using DB.Data.Abstractions;

namespace DB.Data.Services;

public class EntityEventArgs : EventArgs
{
    public required Type EntityType { get; set; }
    public required IDatabaseEntity Entity { get; set; }
}
