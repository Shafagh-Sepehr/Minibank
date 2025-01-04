using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class Withdrawal : DataBaseEntity
{
    public required decimal           Amount { get; init; }
    public required TransactionStatus Status { get; init; }
    public          DateTime          Date   { get; } = DateTime.Now;
    
    [Range(1, long.MaxValue)]
    public required long AccountRef { get; init; }
}
