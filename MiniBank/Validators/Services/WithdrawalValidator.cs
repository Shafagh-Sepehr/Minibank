using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class WithdrawalValidator(IDataBase dataBase) : BaseValidator<Withdrawal>
{
    protected override void ValidateSaveState(Withdrawal entity, List<string> errors)
    {
        if (entity.Amount <= 0)
        {
            errors.Add("Withdrawal amount must be greater than 0");
        }
        
        var accounts = dataBase.FetchAll<Account>();
        if (accounts.All(x => x.Id != entity.AccountRef))
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
