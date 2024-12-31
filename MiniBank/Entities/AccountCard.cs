using System.ComponentModel.DataAnnotations;

namespace MiniBank.Entities;

public class AccountCard
{
    public required long AccountRef { get; init; }
    
    [StringLength(21, MinimumLength = 16)]
    public required string CardNumber { get; set; }
    
    [StringLength(4, MinimumLength = 3)]
    public required string Cvv2 { get; set; }
    
    public required string   PasswordHash       { get; set; }
    public required string   SecondPasswordHash { get; set; }
    public required DateTime ExpiryDate         { get; set; }
}
