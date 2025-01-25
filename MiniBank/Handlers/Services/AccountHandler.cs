using Abstractions.Repository;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class AccountHandler(IRepository repository) : IAccountHandler
{
    public Account CreateAccount(string userRef)
    {
        var newAccount = new Account
        {
            AccountNumber = GenerateAccountNumber(),
            UserRef = userRef,
            Status = AccountStatus.Active,
        };

        repository.Insert(newAccount);
        return newAccount;
    }

    public decimal? GetAccountBalance(string AccountNumber)
        => repository.FetchAll<Account>()
        .FirstOrDefault(account => account.AccountNumber == AccountNumber)
        ?.Balance;

    public IEnumerable<Account> GetAllUserAccounts(User user)
        => repository.FetchAll<Account>().Where(acc => acc.UserRef == user.Id);

    public bool AccountExistsAndBelongsToUser(string accountNumber, string userRef)
    {
        return repository.FetchAll<Account>().FirstOrDefault(acc => acc.AccountNumber == accountNumber && acc.UserRef == userRef) != null;
    }

    private string GenerateAccountNumber()
    {
        var accounts = repository.FetchAll<Account>();
        string accountNumber;

        do
        {
            accountNumber = Helper.GenerateRandomNumberAsString(20);
        } while (accounts.Any(x => x.AccountNumber == accountNumber));

        return accountNumber;
    }
}
