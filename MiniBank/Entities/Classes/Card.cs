using System.ComponentModel.DataAnnotations;
using Abstractions.Repository;
using MiniBank.Attributes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Card>), typeof(Card))]
public class Card : DatabaseEntity
{
    private string _passwordHash = null!;
    private string _secondPasswordHash = null!;

    [StringLength(36, MinimumLength = 36)]
    public required string AccountRef { get; init; }
    
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
