using System.ComponentModel.DataAnnotations;
using DB.Validators.Abstractions;
using MiniBank.Attributes;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Account>), typeof(Account))]
public class Account : DataBaseEntity
{
    private decimal _balance;
    
    public void IncreaseBalance(decimal amount) => _balance += amount;
    public void DecreaseBalance(decimal amount) => _balance -= amount;
    public decimal Balance => _balance;
    
    
    public          AccountStatus Status  { get; set; } = AccountStatus.Active;
    
    [Range(1, long.MaxValue)]
    public required long UserRef { get; init; }
    
    [StringLength(20, MinimumLength = 20)]
    public required string AccountNumber { get; init; }
}
