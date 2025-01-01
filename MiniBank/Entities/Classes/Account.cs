using System.ComponentModel.DataAnnotations;
using DB.Validators.Abstractions;
using MiniBank.Attributes;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Account>), typeof(Account))]
public class Account : DataBaseEntity
{
    public required decimal       Balance { get; set; }
    public          AccountStatus Status  { get; set; } = AccountStatus.Active;
    
    [Range(1, long.MaxValue)]
    public required long UserRef { get; init; }
    
    [StringLength(20, MinimumLength = 20)]
    public required string AccountNumber { get; init; }
}
