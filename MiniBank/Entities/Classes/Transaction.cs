using System.ComponentModel.DataAnnotations;
using DB.Validators.Abstractions;
using MiniBank.Attributes;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Account>), typeof(Account))]
public class Transaction : DataBaseEntity
{
    public required decimal           Amount      { get; init; }
    public required TransactionStatus Status      { get; init; }
    public required TransactionType   Type        { get; init; }
    public          DateTime          Date        { get; } = DateTime.Now;
    public          string?           Description { get; init; }
    
    [Range(1, long.MaxValue)]
    public required long OriginAccountRef { get; init; }
    
    [Range(1, long.MaxValue)]
    public required long DestinationAccountRef { get; init; }
}
