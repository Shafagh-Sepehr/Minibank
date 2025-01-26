using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Handlers.Services;

public class AccountHandler(IRepositoryWrapperValidator repoWrapper) : IAccountHandler
{
    public Account CreateAccount(string userRef)
    {
        var newAccount = new Account
        {
            AccountNumber = GenerateAccountNumber(),
            UserRef = userRef,
            Status = AccountStatus.Active,
        };

        repoWrapper.Insert(newAccount);
        return newAccount;
    }

    public decimal? GetAccountBalance(string AccountNumber)
        => repoWrapper.FetchAll<Account>()
        .FirstOrDefault(account => account.AccountNumber == AccountNumber)
        ?.Balance;

    public IEnumerable<Account> GetAllUserAccounts(User user)
        => repoWrapper.FetchAll<Account>().Where(acc => acc.UserRef == user.Id);

    public bool AccountExistsAndBelongsToUser(string accountNumber, string userRef)
    {
        return repoWrapper.FetchAll<Account>().FirstOrDefault(acc => acc.AccountNumber == accountNumber && acc.UserRef == userRef) != null;
    }

    private string GenerateAccountNumber()
    {
        var accounts = repoWrapper.FetchAll<Account>();
        string accountNumber;

        do
        {
            accountNumber = Helper.GenerateRandomNumberAsString(20);
        } while (accounts.Any(x => x.AccountNumber == accountNumber));

        return accountNumber;
    }
}
