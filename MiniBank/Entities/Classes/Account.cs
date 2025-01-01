using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class Account : DataBaseEntity
{
    private decimal       _balance;
    public  AccountStatus Status { get; set; } = AccountStatus.Active;
    
    [Range(1, long.MaxValue)]
    public long UserRef { get; init; }
    
    [StringLength(20, MinimumLength = 20)]
    public required string AccountNumber { get; init; }
    
    public required decimal Balance
    {
        get => _balance;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Balance cannot be negative");
            }
            
            _balance = value;
        }
    }
}
