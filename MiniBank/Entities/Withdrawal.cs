using System.ComponentModel.DataAnnotations;

namespace MiniBank.Entities;

public class Withdrawal : ComparableDataBaseEntity
{
    public required string AccountNumber { get; init; }
    
    [Range(typeof(decimal), "0.000001", "79228162514264337593543950335")]
    public required decimal Amount { get; init; }
    
    public required DateTime Date { get; init; } = DateTime.Now;
}
