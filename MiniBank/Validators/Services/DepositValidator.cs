using System.ComponentModel.DataAnnotations;
using Abstractions.Repository;
using MiniBank.Entities.Classes;
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
        
        var accounts = repoWrapper.FetchAll<Account>();
        if (accounts.All(x => x.Id != entity.AccountRef))
        {
            errors.Add("no account found for this deposit's AccountRef");
        }
    }
    
    protected override void ValidateUpdateState(Deposit entity, List<string> errors)
    {
        throw new ValidationException("Deposit entities can't get updated");
    }
    
    protected override void ValidateDeleteState(Deposit entity)
    {
        throw new ValidationException("Deposit entities can't be deleted");
    }
}
