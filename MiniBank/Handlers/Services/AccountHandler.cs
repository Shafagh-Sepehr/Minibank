using DB.Data.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class AccountHandler(IDataBase dataBase) : IAccountHandler
{
    public string CreateAccount(long userRef, string password, string secondPassword)
    {
        var newAccount = new Account
        {
            AccountNumber = GenerateAccountNumber(),
            UserRef = userRef,
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
