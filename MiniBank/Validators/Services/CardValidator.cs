using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class CardValidator(IRepositoryWrapperValidator repoWrapper) : BaseValidator<Card>
{
    protected override void ValidateGeneralState(Card entity, List<string> errors)
    {
        if (entity.ExpiryDate < DateTime.Now)
        {
            errors.Add("Card expiry date cannot be in the past.");
        }
        
        var account = repoWrapper.FetchById<Account>(entity.AccountRef);
        if (account == null)
        {
            errors.Add("this card's AccountRef doesn't exist");
        }
    }
}
