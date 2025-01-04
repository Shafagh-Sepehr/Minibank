using DB.Data.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class DepositHandler(IDataBase dataBase) : IDepositHandler
{
    public ActionResult Deposit(string accountNumber, decimal amount)
    {
        var accounts = dataBase.FetchAll<Account>();
        var account = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        ActionResult actionResult;
        
        if (account == null)
        {
            actionResult = ActionResult.AccountNotFound;
        }
        else
        {
            account.IncreaseBalance(amount);
            dataBase.Update(account);
            actionResult = ActionResult.Success;
        }
        
        dataBase.Save(new Deposit
        {
            Amount = amount,
            AccountRef = account?.Id ?? 0,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });
        
        return actionResult;
    }
}
