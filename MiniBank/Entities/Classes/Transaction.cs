using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class Transaction : DataBaseEntity
{
    private readonly decimal _amount;
    
    public required TransactionStatus Status      { get; init; }
    public required TransactionType Type      { get; init; }
    public          DateTime          Date        { get; init; } = DateTime.Now;
    public          string?           Description { get; init; }
    
    [Range(0, long.MaxValue)]
    public required long OriginAccountRef { get; init; }
    
    [Range(0, long.MaxValue)]
    public required long DestinationAccountRef { get; init; }
    
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
}
