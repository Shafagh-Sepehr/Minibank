using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class WithdrawalDao : DataBaseEntity
{
    public required decimal Amount { get; init; }

    public required TransactionStatus Status { get; init; }

    public required DateTime Date { get; init; }

    public required long AccountRef { get; init; }
}
