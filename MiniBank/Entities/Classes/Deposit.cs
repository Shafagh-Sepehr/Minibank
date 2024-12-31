using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class Deposit : DataBaseEntity
{
    private readonly decimal _amount;
    
    public required TransactionStatus Status { get; init; } = TransactionStatus.Success;
    
    [Range(0, long.MaxValue)]
    public required long AccountRef { get; init; }
    
    public required decimal Amount
    {
        get => _amount;
        init
        {
            if (value < 0)
            {
                throw new ArgumentException("Amount cannot be negative");
            }
            
            _amount = value;
        }
    }
    
    public required DateTime Date { get; init; } = DateTime.Now;
}
