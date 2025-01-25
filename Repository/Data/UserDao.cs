using System.ComponentModel.DataAnnotations;

namespace MiniBank.Entities.Classes;

public class UserDao : DataBaseEntity
{
    public required string Username { get; init; }

    public required string PasswordHash { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string PhoneNumber { get; init; }

    public required string NationalId { get; init; }
}
