using System.ComponentModel.DataAnnotations;

namespace MiniBank.Entities.Classes;

public class CardDao : DataBaseEntity
{
    public required long AccountRef { get; init; }

    public required string CardNumber { get; init; }

    public required string Cvv2 { get; init; }

    public required string Password { get; init; }

    public required string SecondPassword { get; init; }

    public required DateTime ExpiryDate { get; init; }
}
