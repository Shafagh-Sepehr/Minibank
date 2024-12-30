using System.ComponentModel.DataAnnotations;
using MiniBank.Data.Abstractions;

namespace MiniBank.Entities;

public abstract class Account : IDatabaseEntity
{
    public enum AccountStatus
    {
        Active,
        DeActive,
        Blocked,
    }
    
    private decimal _balance;
    public  long    UserRef { get; set; }
    
    [StringLength(20, MinimumLength = 20)]
    public required string AccountNumber { get; init; }
    
    public          AccountStatus Status { get; set; } = AccountStatus.Active;
    public required AccountCard   Card   { get; init; }
    
    
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
    
    public long Id { get; set; }
}
