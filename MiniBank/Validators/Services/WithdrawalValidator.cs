using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Exceptions;
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

        if (entity.Status == TransactionStatus.Failed)
        {
            return;
        }

        if(entity.AccountRef == null)
        {
            throw new OperationFailedException("Withdrawal's AccountRef can't be null when the withdrawal is successful");
        }
        
        var account = repoWrapper.FetchById<Account>(entity.AccountRef);
        if (account == null)
        {
            errors.Add("No account found for this withdrawal's AccountRef");
        }
    }
    
    protected override void ValidateUpdateState(Withdrawal entity, List<string> errors)
    {
        throw new ValidationException("Withdrawal entities can't get updated");
    }
    
    protected override void ValidateDeleteState(string id)
    {
        throw new ValidationException("Withdrawal entities can't be deleted");
    }
}
