using MiniBank.Data.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;

namespace MiniBank.Handlers;

public class DepositHandler(IDataBase dataBase)
{
    public ActionResult Deposit(string accountNumber, decimal amount)
    {
        var accounts = dataBase.FetchAll<Account>();
        var account = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        
        if (account == null)
        {
            return ActionResult.AccountNotFound;
        }
        
        account.Balance += amount;
        
        dataBase.Update(account);
        return ActionResult.Success;
    }
}
