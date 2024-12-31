using MiniBank.Data.Abstractions;
using MiniBank.Entities;

namespace MiniBank.Handlers;

public class WithdrawalHandler(IDataBase dataBase)
{
    public ActionResult Withdraw(string accountNumber, decimal amount)
    {
        var accounts = dataBase.FetchAll<Account>();
        var account = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        
        if (account == null)
        {
            return ActionResult.AccountNotFound;
        }
        
        account.Balance -= amount;
        if (account.Balance < 0)
        {
            return ActionResult.InsufficientBalance;
        }
        
        dataBase.Update(account);
        return ActionResult.Success;
    }
}
