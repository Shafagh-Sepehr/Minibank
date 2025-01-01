using DataBase.Data.Abstractions;

namespace DataBase.Data.Services;

public class EntityEventArgs : EventArgs
{
    public required Type EntityType { get; set; }
    public required IDatabaseEntity Entity { get; set; }
}
