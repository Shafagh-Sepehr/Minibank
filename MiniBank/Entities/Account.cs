using System.ComponentModel.DataAnnotations;

namespace MiniBank.Entities;

public class Account : ComparableDataBaseEntity
{
    private decimal _balance;
    public  long    UserRef { get; set; }
    public          AccountStatus Status { get; set; } = AccountStatus.Active;
    
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
