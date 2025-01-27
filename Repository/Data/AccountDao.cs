using InMemoryDataBase.Attributes;
using MiniBank.Entities.Enums;
using Repository.Abstractions;

namespace Repository.Data;

public class AccountDao : RepositoryEntity
{
    [PrimaryKey]
    public override string Id { get; set; } = string.Empty;

    [DefaultValue(0)]
    public required decimal Balance { get; init; }

    public required AccountStatus Status { get; init; }

    [ForeignKey(typeof(UserDao))]
    public required string UserRef { get; init; }

    public required string AccountNumber { get; init; }
}
