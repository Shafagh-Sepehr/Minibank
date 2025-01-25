using Abstractions.Repository;
using InMemoryDataBase.Attributes;
using MiniBank.Entities.Enums;

namespace Repository.Data;

public class AccountDao : DataBaseEntity
{
    [PrimaryKey]
    public new string Id { get; set; } = string.Empty;

    [DefaultValue(0)]
    public required decimal Balance { get; init; }

    public required AccountStatus Status { get; init; }

    [ForeignKey(typeof(UserDao))]
    public required string UserRef { get; init; }

    public required string AccountNumber { get; init; }
}
