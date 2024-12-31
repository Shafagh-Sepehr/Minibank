using System.ComponentModel.DataAnnotations;
using MiniBank.Data.Abstractions;

namespace MiniBank.Entities;

public abstract class DataBaseEntity : IDatabaseEntity
{
    [Range(0, long.MaxValue)]
    public long Id { get; set; }
}
