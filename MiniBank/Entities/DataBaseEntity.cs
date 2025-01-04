using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;

namespace MiniBank.Entities;

public abstract class DataBaseEntity : IDatabaseEntity
{
    public long Id { get; set; }
}
