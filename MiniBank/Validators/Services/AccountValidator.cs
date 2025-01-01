using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class AccountValidator : BaseValidator<Account>
{
    protected override void ValidateGeneralState(Account entity, List<string> errors)
    {
        if (entity.Balance < 0)
        {
            throw new ValidationException("Account balance cannot be less than zero");
        }
    }
    
    protected override void ValidateSaveState(Account entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    
    protected override void ValidateUpdateState(Account entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    
    protected override void ValidateDeleteState(Account entity)
    {
        throw new NotImplementedException();
    }
}
