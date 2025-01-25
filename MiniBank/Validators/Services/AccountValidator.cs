using System.ComponentModel.DataAnnotations;
using Abstractions.Repository;
using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class AccountValidator(IRepository repository) : BaseValidator<Account>
{
    protected override void ValidateGeneralState(Account entity, List<string> errors)
    {
        if (entity.Balance < 0)
        {
            errors.Add("Account balance cannot be less than zero");
        }
        
        var users = repository.FetchAll<User>();
        if (users.All(x => x.Id != entity.UserRef))
        {
            errors.Add("no user found for this account's UserRef");
        }
    }
    
    protected override void ValidateSaveState(Account entity, List<string> errors)
    {
        if (entity.Balance != 0)
        {
            errors.Add("Account balance must be zero when creating it, deposit money after account creation");
        }
    }
    
    protected override void ValidateUpdateState(Account entity, List<string> errors)
    {
        var accounts = repository.FetchAll<Account>();
        var oldAccount = accounts.First(x => x.Id == entity.Id);
        if (oldAccount.UserRef != entity.UserRef || oldAccount.AccountNumber != entity.AccountNumber)
        {
            errors.Add("Can't change account's owner or AccountNumber");
        }
    }
    
    protected override void ValidateDeleteState(Account entity)
    {
        if (entity.Balance > 1)
        {
            throw new ValidationException("Account balance cannot be more than 1 when deleting, withdraw money");
        }
        
        var cards = repository.FetchAll<Card>();
        if (cards.Any(x => x.AccountRef == entity.Id))
        {
            throw new ValidationException("Account can't have cards when deleting, first delete its cards");
        }
    }
}
