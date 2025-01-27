using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class TransactionValidator(IRepositoryWrapperValidator repoWrapper) : BaseValidator<Transaction>
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

        var accounts = repoWrapper.FetchAll<Account>().ToArray();
        if (entity.OriginAccountRef ==null || OriginAccountNotFound(accounts, entity.OriginAccountRef))
        {
            errors.Add("no account found for this transaction's OriginAccountRef");
        }

        if (entity.DestinationAccountRef == null || DestinationAccountNotFound(accounts, entity.DestinationAccountRef))
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

        if (entity.OriginAccountNumber == null || OriginAccountNumberNotFound(accounts, entity.OriginAccountNumber))
        {
            errors.Add("no account found for this transaction's OriginAccountNumber");
        }

        if (entity.DestinationAccountNumber == null || DestinationAccountNumberNotFound(accounts, entity.DestinationAccountNumber))
        {
            errors.Add("no account found for this transaction's DestinationAccountNumber");
        }

        if (entity.OriginAccountRef == null || entity.OriginAccountNumber == null ||  OriginAccountNumberAndRefMismatch(accounts, entity.OriginAccountRef, entity.OriginAccountNumber))
        {
            errors.Add("Origin account number and reference mismatch");
        }

        if (entity.DestinationAccountRef == null || entity.DestinationAccountNumber == null || DestinationAccountNumberAndRefMismatch(accounts, entity.DestinationAccountRef, entity.DestinationAccountNumber))
        {
            errors.Add("Destination account number and reference mismatch");
        }

        if(entity.DestinationAccountNumber != null && entity.DestinationAccountNumber.Length != 20)
        {
            errors.Add("The field DestinationAccountNumber must be a string with a minimum length of 20 and a maximum length of 20.");
        }
    }
    
    private static bool OriginAccountNotFound(IEnumerable<Account> accounts, string originAccountRef)
        => accounts.All(x => x.Id != originAccountRef);
    
    private static bool DestinationAccountNotFound(IEnumerable<Account> accounts, string destinationAccountRef)
        => accounts.All(x => x.Id != destinationAccountRef);

    private static bool OriginAccountNumberNotFound(IEnumerable<Account> accounts, string originAccountNumber)
        => accounts.All(x => x.AccountNumber != originAccountNumber);

    private static bool DestinationAccountNumberNotFound(IEnumerable<Account> accounts, string destinationAccountNumber)
        => accounts.All(x => x.AccountNumber != destinationAccountNumber);

    private static bool OriginAccountNumberAndRefMismatch(IEnumerable<Account> accounts, string originAccountRef, string originAccountNumber)
        => accounts.All(x => x.Id != originAccountRef && x.AccountNumber != originAccountNumber);

    private static bool DestinationAccountNumberAndRefMismatch(IEnumerable<Account> accounts, string destinationAccountRef, string destinationAccountNumber)
        => accounts.All(x => x.Id != destinationAccountRef && x.AccountNumber != destinationAccountNumber);


    protected override void ValidateUpdateState(Transaction entity, List<string> errors)
    {
        throw new ValidationException("Transaction entities can't get updated");
    }
    
    protected override void ValidateDeleteState(string id)
    {
        throw new ValidationException("Transaction entities can't be deleted");
    }
}
