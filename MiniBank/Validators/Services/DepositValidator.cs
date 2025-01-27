using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Exceptions;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class DepositValidator(IRepositoryWrapperValidator repoWrapper) : BaseValidator<Deposit>
{
    protected override void ValidateSaveState(Deposit entity, List<string> errors)
    {
        if (entity.Amount <= 0)
        {
            errors.Add("Deposit amount must be greater than 0");
        }

        if (entity.Status == TransactionStatus.Failed)
        {
            return;
        }

        if (entity.AccountRef == null)
        {
            throw new OperationFailedException("Withdrawal's AccountRef can't be null when the withdrawal is successful");
        }

        if (repoWrapper.FetchById<Account>(entity.AccountRef) == null)
        {
            errors.Add("no account found for this deposit's AccountRef");
        }
    }
    
    protected override void ValidateUpdateState(Deposit entity, List<string> errors)
    {
        throw new ValidationException("Deposit entities can't get updated");
    }
    
    protected override void ValidateDeleteState(string id)
    {
        throw new ValidationException("Deposit entities can't be deleted");
    }
}
