using DB.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class CardValidator(IDataBase dataBase) : BaseValidator<Card>
{
    protected override void ValidateGeneralState(Card entity, List<string> errors)
    {
        if (entity.ExpiryDate < DateTime.Now)
        {
            errors.Add("Card expiry date cannot be in the past.");
        }
        
        var accounts = dataBase.FetchAll<Account>();
        if (accounts.All(x => x.Id != entity.AccountRef))
        {
            errors.Add("this account's UserRef doesn't exist");
        }
    }
}
