using System.ComponentModel.DataAnnotations;
using Abstractions.MiniBank;
using MiniBank.Attributes;
using MiniBank.Entities.Enums;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Account>), typeof(Account))]
public class Account : MiniBankDatabaseEntity
{
    private decimal _balance;
    
    public void IncreaseBalance(decimal amount) => _balance += amount;
    public void DecreaseBalance(decimal amount) => _balance -= amount;
    public decimal Balance => _balance;

    public AccountStatus Status { get; set; } = AccountStatus.Active;
    
    [StringLength(36, MinimumLength = 36)]
    public required string UserRef { get; init; }
    
    [StringLength(20, MinimumLength = 20)]
    public required string AccountNumber { get; init; }
}
