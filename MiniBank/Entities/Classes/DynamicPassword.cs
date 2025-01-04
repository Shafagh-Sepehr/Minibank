using System.ComponentModel.DataAnnotations;
using DB.Validators.Abstractions;
using MiniBank.Attributes;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<DynamicPassword>), typeof(DynamicPassword))]
public class DynamicPassword : DataBaseEntity
{
    public required decimal Amount { get; init; }
    
    [StringLength(16, MinimumLength = 16)]
    public required string OriginCardNumber { get; init; }
    
    [StringLength(16, MinimumLength = 16)]
    public required string DestinationCardNumber { get; init; }
    
    [StringLength(64, MinimumLength = 64, ErrorMessage = "sha256 hash must be 64 characters.")]
    public required string DynamicPasswordHash { get; init; }
    
    public DateTime ExpiryDate { get; } = DateTime.Now.AddMinutes(2);
}
