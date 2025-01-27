using System.ComponentModel.DataAnnotations;
using Abstractions.MiniBank;
using MiniBank.Attributes;
using MiniBank.Entities.Enums;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Withdrawal>), typeof(Withdrawal))]
public class Withdrawal : MiniBankDatabaseEntity
{
    public required decimal Amount { get; init; }
    public required TransactionStatus Status { get; init; }
    public DateTime Date { get; } = DateTime.Now;

    [StringLength(36, MinimumLength = 36)]
    public required string? AccountRef { get; init; }
}
