using Abstractions.Repository;
using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class CardValidator(IRepository repository) : BaseValidator<Card>
{
    protected override void ValidateGeneralState(Card entity, List<string> errors)
    {
        if (entity.ExpiryDate < DateTime.Now)
        {
            errors.Add("Card expiry date cannot be in the past.");
        }
        
        var accounts = repository.FetchAll<Account>();
        if (accounts.All(x => x.Id != entity.AccountRef))
        {
            errors.Add("this card's AccountRef doesn't exist");
        }
    }
}
