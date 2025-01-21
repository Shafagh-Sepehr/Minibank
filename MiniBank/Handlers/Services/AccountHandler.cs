using DB.Data.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class AccountHandler(IDataBase dataBase) : IAccountHandler
{
    public Account CreateAccount(long userRef)
    {
        var newAccount = new Account
        {
            AccountNumber = GenerateAccountNumber(),
            UserRef = userRef,
            Status = AccountStatus.Active,
        };
        
        dataBase.Save(newAccount);
        return newAccount;
    }
    
    public decimal? GetAccountBalance(User user, string AccountNumber)
        => dataBase.FetchAll<Account>()
        .FirstOrDefault(account=>account.UserRef == user.Id && account.AccountNumber == AccountNumber)
        ?.Balance;

    public IEnumerable<Account> GetAllUserAccounts(User user)
        => dataBase.FetchAll<Account>().Where(acc => acc.UserRef == user.Id);
    
    private string GenerateAccountNumber()
    {
        var accounts = dataBase.FetchAll<Account>().ToList();
        string accountNumber;
        
        do
        {
            accountNumber = Helper.GenerateRandomNumberAsString(20);
        } while (accounts.Any(x => x.AccountNumber == accountNumber));
        
        return accountNumber;
    }
}
