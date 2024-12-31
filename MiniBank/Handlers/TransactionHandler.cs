using MiniBank.Data.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;

namespace MiniBank.Handlers;

public class TransactionHandler(IDataBase dataBase)
{
    public ActionResult CreateTransaction(string originAccountNumber, string destinationAccountNumber, decimal amount, string? description = null)
    {
        var accounts = dataBase.FetchAll<Account>().ToList();
        var originAccount = accounts.FirstOrDefault(x => x.AccountNumber == originAccountNumber);
        var destinationAccount = accounts.FirstOrDefault(x => x.AccountNumber == destinationAccountNumber);
        
        ActionResult actionResult;
        
        if (originAccount == null || destinationAccount == null)
        {
            actionResult = ActionResult.AccountNotFound;
        }
        else
        {
            originAccount.Balance -= amount;
            destinationAccount.Balance += amount;
            
            if (originAccount.Balance < 0)
            {
                actionResult = ActionResult.InsufficientBalance;
            }
            else
            {
                dataBase.Update(originAccount);
                dataBase.Update(destinationAccount);
                actionResult = ActionResult.Success;
            }
        }
        
        dataBase.Save(new Transaction
        {
            Amount = amount,
            OriginAccountRef = originAccount?.Id ?? 0,
            DestinationAccountRef = destinationAccount?.Id ?? 0,
            Description = description,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });
        
        return actionResult;
    }
}
