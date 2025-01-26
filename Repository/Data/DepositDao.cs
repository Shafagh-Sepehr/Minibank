using Abstractions.Repository;
using InMemoryDataBase.Attributes;
using MiniBank.Entities.Enums;

namespace Repository.Data;

public class DepositDao : DatabaseEntity
{
    [PrimaryKey]
    public override string Id { get; set; } = string.Empty;

    public required decimal Amount { get; init; }

    public required TransactionStatus Status { get; init; }

    public required DateTime Date { get; init; }

    [ForeignKey(typeof(AccountDao))]
    [Nullable]
    public required string? AccountRef { get; init; }
}
