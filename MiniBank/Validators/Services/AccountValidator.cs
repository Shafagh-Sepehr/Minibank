using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class AccountValidator(IDataBase dataBase) : BaseValidator<Account>
{
    protected override void ValidateGeneralState(Account entity, List<string> errors)
    {
        if (entity.Balance < 0)
        {
            errors.Add("Account balance cannot be less than zero");
        }
        
        var users = dataBase.FetchAll<User>();
        if (users.All(x => x.Id != entity.UserRef))
        {
            errors.Add("this account's UserRef doesn't exist");
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
        
    }
    
    protected override void ValidateDeleteState(Account entity)
    {
        if (entity.Balance > 1)
        {
            throw new ValidationException("Account balance cannot be more than 1 when deleting, withdraw money");
        }
    }
}
