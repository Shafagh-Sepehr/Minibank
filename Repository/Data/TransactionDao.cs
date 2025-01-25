using Abstractions.Repository;
using InMemoryDataBase.Attributes;
using MiniBank.Entities.Enums;

namespace Repository.Data;

public class TransactionDao : DataBaseEntity
{
    [PrimaryKey]
    public new string Id { get; set; } = string.Empty;

    public required decimal Amount { get; init; }

    public required TransactionStatus Status { get; init; }

    public required TransactionType Type { get; init; }

    public required DateTime Date { get; init; }

    [Nullable]
    public required string? Description { get; init; }

    [ForeignKey(typeof(AccountDao))]
    [Nullable]
    public required string? OriginAccountRef { get; init; }

    [Nullable]
    public required string? OriginAccountNumber { get; init; }

    [ForeignKey(typeof(AccountDao))]
    [Nullable]
    public required string? DestinationAccountRef { get; init; }

    [Nullable]
    public required string? DestinationAccountNumber { get; init; }
}
