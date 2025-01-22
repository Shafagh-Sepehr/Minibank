using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class TransactionValidator(IDataBase dataBase) : BaseValidator<Transaction> // not complete
{
    protected override void ValidateSaveState(Transaction entity, List<string> errors)
    {
        if(entity.Status == Entities.Enums.TransactionStatus.Failed)
        {
            return;
        }

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

        if (entity.OriginAccountRef == entity.DestinationAccountRef)
        {
            errors.Add("Origin and destination accounts can't be the same");
        }

        if (entity.OriginAccountNumber == entity.DestinationAccountNumber)
        {
            errors.Add("Origin and destination account numbers can't be the same");
        }

        if (OriginAccountNumberNotFound(accounts, entity.OriginAccountNumber))
        {
            errors.Add("no account found for this transaction's OriginAccountNumber");
        }

        if (DestinationAccountNumberNotFound(accounts, entity.DestinationAccountNumber))
        {
            errors.Add("no account found for this transaction's DestinationAccountNumber");
        }

        if (OriginAccountNumberAndRefMismatch(accounts, entity.OriginAccountRef, entity.OriginAccountNumber))
        {
            errors.Add("Origin account number and reference mismatch");
        }

        if (DestianaitonAccountNumberAndRefMismatch(accounts, entity.DestinationAccountRef, entity.DestinationAccountNumber))
        {
            errors.Add("Destination account number and reference mismatch");
        }

        if(entity.DestinationAccountNumber.Length != 20)
        {
            errors.Add("The field DestinationAccountNumber must be a string with a minimum length of 20 and a maximum length of 20.");
        }
    }
    
    private static bool OriginAccountNotFound(IEnumerable<Account> accounts, long originAccountRef)
        => accounts.All(x => x.Id != originAccountRef);
    
    private static bool DestinationAccountNotFound(IEnumerable<Account> accounts, long destinationAccountRef)
        => accounts.All(x => x.Id != destinationAccountRef);

    private static bool OriginAccountNumberNotFound(IEnumerable<Account> accounts, string originAccountNumber)
        => accounts.All(x => x.AccountNumber != originAccountNumber);

    private static bool DestinationAccountNumberNotFound(IEnumerable<Account> accounts, string destinationAccountNumber)
        => accounts.All(x => x.AccountNumber != destinationAccountNumber);

    private static bool OriginAccountNumberAndRefMismatch(IEnumerable<Account> accounts, long originAccountRef, string originAccountNumber)
        => accounts.All(x => x.Id != originAccountRef && x.AccountNumber != originAccountNumber);

    private static bool DestianaitonAccountNumberAndRefMismatch(IEnumerable<Account> accounts, long destinationAccountRef, string destinationAccountNumber)
        => accounts.All(x => x.Id != destinationAccountRef && x.AccountNumber != destinationAccountNumber);


    protected override void ValidateUpdateState(Transaction entity, List<string> errors)
    {
        throw new ValidationException("Transaction entities can't get updated");
    }
    
    protected override void ValidateDeleteState(Transaction entity)
    {
        throw new ValidationException("Transaction entities can't be deleted");
    }
}
