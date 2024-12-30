using System.ComponentModel.DataAnnotations;

namespace MiniBank.Entities;

public class Transaction : ComparableDataBaseEntity
{
    public required string OriginAccountNumber      { get; init; }
    public required string DestinationAccountNumber { get; init; }
    
    [Range(typeof(decimal), "0.000001", "79228162514264337593543950335")]
    public required decimal Amount { get; init; }
    
    public required DateTime Date { get; init; } = DateTime.Now;
}
