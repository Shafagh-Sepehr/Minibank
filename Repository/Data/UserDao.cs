using Abstractions.Repository;
using InMemoryDataBase.Attributes;

namespace Repository.Data;

public class UserDao : DataBaseEntity
{
    [PrimaryKey]
    public new string Id { get; set; } = string.Empty;

    public required string Username { get; init; }

    public required string PasswordHash { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string PhoneNumber { get; init; }

    public required string NationalId { get; init; }
}
