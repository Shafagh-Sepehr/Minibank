using System.ComponentModel.DataAnnotations;
using DB.Validators.Abstractions;
using MiniBank.Attributes;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Card>), typeof(Card))]
public class Card : DataBaseEntity
{
    [Range(1, long.MaxValue)]
    public required long AccountRef { get; init; }
    
    [StringLength(16, MinimumLength = 16)]
    public required string CardNumber { get; init; }
    
    [StringLength(4, MinimumLength = 3)]
    public required string Cvv2 { get; init; }
    
    [StringLength(64, MinimumLength = 64, ErrorMessage = "sha256 hash must be 64 characters.")]
    public required string PasswordHash { get; set; }
    
    [StringLength(64, MinimumLength = 64, ErrorMessage = "sha256 hash must be 64 characters.")]
    public required string SecondPasswordHash { get; set; }
    
    public required DateTime ExpiryDate { get; init; }
}
