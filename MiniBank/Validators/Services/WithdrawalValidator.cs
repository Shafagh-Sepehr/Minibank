using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class WithdrawalValidator(IRepositoryWrapperValidator repoWrapper) : BaseValidator<Withdrawal>
{
    protected override void ValidateSaveState(Withdrawal entity, List<string> errors)
    {
        if (entity.Amount <= 0)
        {
            errors.Add("Withdrawal amount must be greater than 0");
        }
        
        var account = repoWrapper.FetchById<Account>(entity.Id);
        if (account == null)
        {
            errors.Add("no account found for this withdrawal's AccountRef");
        }
    }
    
    protected override void ValidateUpdateState(Withdrawal entity, List<string> errors)
    {
        throw new ValidationException("Withdrawal entities can't get updated");
    }
    
    protected override void ValidateDeleteState(Withdrawal entity)
    {
        throw new ValidationException("Withdrawal entities can't be deleted");
    }
}
