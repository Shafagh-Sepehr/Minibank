using System.ComponentModel.DataAnnotations;
using Abstractions.Repository;
using MiniBank.Attributes;
using MiniBank.Entities.Enums;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Deposit>), typeof(Deposit))]
public class Deposit : DatabaseEntity
{
    public required decimal Amount { get; init; }
    public required TransactionStatus Status { get; init; }
    public DateTime Date { get; } = DateTime.Now;

    [StringLength(36, MinimumLength = 36)]
    public required string? AccountRef { get; init; }
}
