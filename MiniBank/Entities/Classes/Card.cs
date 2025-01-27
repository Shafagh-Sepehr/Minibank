using System.ComponentModel.DataAnnotations;
using Abstractions.MiniBank;
using MiniBank.Attributes;
using MiniBank.Exceptions;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Entities.Classes;

[Validator(typeof(IValidator<Card>), typeof(Card))]
public class Card : MiniBankDatabaseEntity
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
        init
        {
            if (value.Except("0123456789".ToCharArray()).Any())
            {
                throw new OperationFailedException("first password cant have non-numeric characters");
            }
            if(value.Length != 4)
            {
                throw new OperationFailedException("first password must have a length 4 digits");
            }
            _passwordHash = Helper.ComputeSha256Hash(value);
        }
    }

    public required string SecondPassword
    {
        init
        {
            if (value.Except("0123456789".ToCharArray()).Any())
            {
                throw new OperationFailedException("second password cant have non-numeric characters");
            }
            if (value.Length < 5)
            {
                throw new OperationFailedException("second password length greater or equal to 5");
            }
            _secondPasswordHash = Helper.ComputeSha256Hash(value);
        }
    }

    public required DateTime ExpiryDate { get; init; }
    
    public string GetPasswordHash() => _passwordHash;
    public string GetSecondPasswordHash() => _secondPasswordHash;
    public void ChangePassword(string value) => _passwordHash = Helper.ComputeSha256Hash(value);
    public string ChangeSecondPassword(string value) => _secondPasswordHash = Helper.ComputeSha256Hash(value);
}
