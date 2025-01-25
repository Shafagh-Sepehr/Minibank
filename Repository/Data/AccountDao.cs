using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Enums;

namespace MiniBank.Entities.Classes;

public class AccountDao : DataBaseEntity
{
    public required decimal Balance { get; init; }

    public required AccountStatus Status { get; init; }

    public required long UserRef { get; init; }

    public required string AccountNumber { get; init; }
}
