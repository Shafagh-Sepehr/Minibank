using Abstractions.Repository;
using InMemoryDataBase.Attributes;

namespace Repository.Data;

public class CardDao : DataBaseEntity
{
    [PrimaryKey]
    public new string Id { get; set; } = string.Empty;

    [ForeignKey(typeof(AccountDao))]
    public required string AccountRef { get; init; }

    public required string CardNumber { get; init; }

    public required string Cvv2 { get; init; }

    public required string Password { get; init; }

    public required string SecondPassword { get; init; }

    public required DateTime ExpiryDate { get; init; }
}
