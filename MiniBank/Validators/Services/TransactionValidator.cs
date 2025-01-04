using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class TransactionValidator(IDataBase dataBase) : BaseValidator<Transaction> // not complete
{
    protected override void ValidateSaveState(Transaction entity, List<string> errors)
    {
        if (entity.Amount <= 0)
        {
            errors.Add("Withdrawal amount must be greater than 0");
        }
        
        var accounts = dataBase.FetchAll<Account>().ToArray();
        if (OriginAccountNotFound(accounts, entity.OriginAccountRef))
        {
            errors.Add("no account found for this transaction's OriginAccountRef");
        }
        
        if (DestinationAccountNotFound(accounts, entity.DestinationAccountRef))
        {
            errors.Add("no account found for this transaction's DestinationAccountRef");
        }
    }
    
    private static bool OriginAccountNotFound(IEnumerable<Account> accounts, long originAccountRef)
        => accounts.All(x => x.Id != originAccountRef);
    
    private static bool DestinationAccountNotFound(IEnumerable<Account> accounts, long destinationAccountRef)
        => accounts.All(x => x.Id != destinationAccountRef);
    
    
    protected override void ValidateUpdateState(Transaction entity, List<string> errors)
    {
        throw new ValidationException("Transaction entities can't get updated");
    }
    
    protected override void ValidateDeleteState(Transaction entity)
    {
        throw new ValidationException("Transaction entities can't be deleted");
    }
}
