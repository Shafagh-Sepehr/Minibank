using MiniBank.Data.Abstractions;
using MiniBank.Entities;

namespace MiniBank.Handlers;

public class AccountHandler(IDataBase dataBase)
{
    public string CreateAccount(long userRef, string password, string secondPassword)
    {
        var newAccount = new Account
        {
            AccountNumber = GenerateAccountNumber(),
            UserRef = userRef,
            Balance = 0,
            Status = AccountStatus.Active,
        };
        
        dataBase.Save(newAccount);
        return newAccount.AccountNumber;
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
