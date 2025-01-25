using Abstractions.Repository;
using MiniBank.Entities.Enums;

namespace Repository.Data;

public class WithdrawalDao : DataBaseEntity
{
    public required decimal Amount { get; init; }

    public required TransactionStatus Status { get; init; }

    public required DateTime Date { get; init; }

    public required string AccountRef { get; init; }
}
