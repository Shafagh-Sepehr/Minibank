using System.ComponentModel.DataAnnotations;
using Abstractions.Repository;
using MiniBank.Attributes;
using MiniBank.Entities.Enums;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Transaction>), typeof(Transaction))]
public class Transaction : DatabaseEntity
{
    public required decimal Amount { get; init; }
    public required TransactionStatus Status { get; init; }
    public required TransactionType Type { get; init; }
    public DateTime Date { get; } = DateTime.Now;
    public string? Description { get; init; }

    [StringLength(36, MinimumLength = 36)]
    public required string? OriginAccountRef { get; init; }
    public required string? OriginAccountNumber { get; init; }

    [StringLength(36, MinimumLength = 36)]
    public required string? DestinationAccountRef { get; init; }
    public required string? DestinationAccountNumber { get; init; }
}
