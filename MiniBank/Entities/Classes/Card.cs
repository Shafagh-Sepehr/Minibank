using System.ComponentModel.DataAnnotations;
using DB.Validators.Abstractions;
using MiniBank.Attributes;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Card>), typeof(Card))]
public class Card : DataBaseEntity
{
    private string _passwordHash = null!;
    private string _secondPasswordHash = null!;
    
    [Range(1, long.MaxValue)]
    public required long AccountRef { get; init; }
    
    [StringLength(16, MinimumLength = 16)]
    public required string CardNumber { get; init; }
    
    [StringLength(4, MinimumLength = 3)]
    public required string Cvv2 { get; init; }
    
    public required string Password
    {
        init => _passwordHash = Helper.ComputeSha256Hash(value);
    }
    public required string SecondPassword
    {
        init => _secondPasswordHash = Helper.ComputeSha256Hash(value);
    }
    
    public required DateTime ExpiryDate { get; init; }
    
    public string GetPasswordHash() => _passwordHash;
    public string GetSecondPasswordHash() => _secondPasswordHash;
    public void ChangePassword(string value) => _passwordHash = Helper.ComputeSha256Hash(value);
    public string ChangeSecondPassword(string value) => _secondPasswordHash = Helper.ComputeSha256Hash(value);
}
