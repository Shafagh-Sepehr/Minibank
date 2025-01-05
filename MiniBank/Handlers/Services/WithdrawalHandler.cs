using DB.Data.Abstractions;
using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class WithdrawalHandler(IDataBase dataBase, ISmsService smsService) : IWithdrawalHandler
{
    public ActionResult Withdraw(string accountNumber, decimal amount)
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
            account.DecreaseBalance(amount);
            if (account.Balance < 0)
            {
                actionResult = ActionResult.InsufficientBalance;
            }
            else
            {
                dataBase.Update(account);
                actionResult = ActionResult.Success;
            }
            
            var user = dataBase.FetchAll<User>().First(x => x.Id == account.UserRef);
            smsService.Send($"{amount} was withdrew from your account", user.PhoneNumber);
        }
        
        dataBase.Save(new Withdrawal
        {
            Amount = amount,
            AccountRef = account?.Id ?? 0,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });
        
        
        
        return actionResult;
    }
}
