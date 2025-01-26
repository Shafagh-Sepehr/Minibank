using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Classes;
using MiniBank.Exceptions;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class AccountValidator(IRepositoryWrapperValidator repoWrapper) : BaseValidator<Account>
{
    protected override void ValidateGeneralState(Account entity, List<string> errors)
    {
        if (entity.Balance < 0)
        {
            errors.Add("Account balance cannot be less than zero");
        }
        
        var user = repoWrapper.FetchById<User>(entity.UserRef);
        if (user == null)
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
        var oldAccount = repoWrapper.FetchById<Account>(entity.Id);

        if(oldAccount ==  null)
        {
            throw new OperationFailedException($"the account's user doesn't exists (UserRef:{entity.UserRef})");
        }

        if (oldAccount.UserRef != entity.UserRef || oldAccount.AccountNumber != entity.AccountNumber)
        {
            errors.Add("Can't change account's owner or AccountNumber");
        }
    }
    
    protected override void ValidateDeleteState(string id)
    {
        var entity = repoWrapper.FetchById<Account>(id);

        if (entity == null)
        {
            throw new ValidationException($"can't delete non-existent Account with id {id}");
        }

        if (entity.Balance > 1)
        {
            throw new ValidationException("Account balance cannot be more than 1 when deleting, withdraw money");
        }
        
        var cards = repoWrapper.FetchAll<Card>();
        if (cards.Any(x => x.AccountRef == entity.Id))
        {
            throw new ValidationException("Account can't have cards when deleting, first delete its cards");
        }
    }
}
