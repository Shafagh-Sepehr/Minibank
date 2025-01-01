using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class CardValidator : BaseValidator<Card>
{
    protected override void ValidateData(Card entity)
    {
        if (entity.ExpiryDate < DateTime.Now)
        {
            throw new InvalidDataException("Card expiry date cannot be in the past.");
        }
    }
}
