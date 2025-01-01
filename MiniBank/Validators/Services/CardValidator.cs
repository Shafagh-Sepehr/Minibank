using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class CardValidator : BaseValidator<Card>
{
    protected override void ValidateGeneralState(Card entity, List<string> errors)
    {
        if (entity.ExpiryDate < DateTime.Now)
        {
            throw new InvalidDataException("Card expiry date cannot be in the past.");
        }
    }
    
    protected override void ValidateSaveState(Card entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    
    protected override void ValidateUpdateState(Card entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    
    protected override void ValidateDeleteState(Card entity)
    {
        throw new NotImplementedException();
    }
}
