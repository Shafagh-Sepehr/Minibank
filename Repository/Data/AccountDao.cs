using Abstractions.Repository;
using MiniBank.Entities.Enums;

namespace Repository.Data;

public class AccountDao : DataBaseEntity
{
    public required decimal Balance { get; init; }

    public required AccountStatus Status { get; init; }

    public required long UserRef { get; init; }

    public required string AccountNumber { get; init; }
}
