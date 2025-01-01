using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class CardValidator : BaseValidator<Card>, ICardValidator
{
    protected override void ValidateData(Card entity)
    {
        if (entity.ExpiryDate < DateTime.Now)
        {
            throw new InvalidDataException("Card expiry date cannot be in the past.");
        }
    }
}
