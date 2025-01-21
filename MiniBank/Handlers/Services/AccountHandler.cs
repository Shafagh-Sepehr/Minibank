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
    
    public decimal? GetAccountBalance(string AccountNumber)
        => dataBase.FetchAll<Account>()
        .FirstOrDefault(account => account.AccountNumber == AccountNumber)
        ?.Balance;

    public IEnumerable<Account> GetAllUserAccounts(User user)
        => dataBase.FetchAll<Account>().Where(acc => acc.UserRef == user.Id);

    public bool AccountExistsAndBelongsToUser(string accountNumber, long userRef)
    {
        return dataBase.FetchAll<Account>().FirstOrDefault(acc => acc.AccountNumber == accountNumber && acc.UserRef == userRef) != null;
    }
    
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
