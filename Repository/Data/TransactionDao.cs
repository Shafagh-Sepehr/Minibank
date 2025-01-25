using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class TransactionDao : DataBaseEntity
{
    public required decimal Amount { get; init; }

    public required TransactionStatus Status { get; init; }

    public required TransactionType Type { get; init; }

    public required DateTime Date { get; init; }

    public required string? Description { get; init; }

    public required long OriginAccountRef { get; init; }

    public required string OriginAccountNumber { get; init; }

    public required long DestinationAccountRef { get; init; }

    public required string DestinationAccountNumber { get; init; }
}
