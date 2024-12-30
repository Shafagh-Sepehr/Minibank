using System.ComponentModel.DataAnnotations;

namespace MiniBank.Entities;

public class User : ComparableDataBaseEntity
{
    [StringLength(50, MinimumLength = 5)]
    public required string Username { get; set; }
    
    public required string PasswordHash { get; set; }
    
    [StringLength(70, MinimumLength = 2)]
    public required string FirstName { get; init; }
    
    [StringLength(70, MinimumLength = 2)]
    public required string LastName { get; init; }
    
    [StringLength(11, MinimumLength = 11)]
    public required string PhoneNumber { get; set; }
    
    [StringLength(10, MinimumLength = 10)]
    public required string NationalId { get; init; }
}
