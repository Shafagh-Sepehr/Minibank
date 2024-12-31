using MiniBank.Data.Abstractions;
using MiniBank.Entities;

namespace MiniBank.Handlers;

public class DepositHandler(IDataBase dataBase)
{
    public bool Deposit(string accountNumber, decimal amount)
    {
        var accounts = dataBase.FetchAll<Account>();
        
        foreach (var account in accounts)
        {
            if (account.AccountNumber == accountNumber)
            {
                account.Balance += amount;
                dataBase.Update(account);
                return true;
            }
        }
        
        return false;
    }
}
