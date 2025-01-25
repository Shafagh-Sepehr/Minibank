using Abstractions.Repository;
using InMemoryDataBase.Attributes;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;

namespace Repository.Data;

public class WithdrawalDao : DataBaseEntity
{
    [PrimaryKey]
    public new string Id { get; set; } = string.Empty;

    public required decimal Amount { get; init; }

    public required TransactionStatus Status { get; init; }

    public required DateTime Date { get; init; }

    [ForeignKey(typeof(Account))]
    [Nullable]
    public required string? AccountRef { get; init; }
}
