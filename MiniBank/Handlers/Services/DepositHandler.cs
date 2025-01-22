using DB.Data.Abstractions;
using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class DepositHandler(IDataBase dataBase, ISmsService smsService) : IDepositHandler
{
    public void Deposit(string accountNumber, decimal amount)
    {
        var account = dataBase.FetchAll<Account>().FirstOrDefault(x => x.AccountNumber == accountNumber);
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

            dataBase.Update(account);
            
            var user = dataBase.FetchAll<User>().First(x => x.Id == account.UserRef);
            smsService.Send($"{amount} was deposited to your account", user.PhoneNumber);
        }
        
        dataBase.Save(new Deposit
        {
            Amount = amount,
            AccountRef = account?.Id ?? 0,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });
        
        Helper.ThrowExceptionIfActionFailed(actionResult);
    }

    public IEnumerable<Deposit> GetAllDeposits(string accountNumber)
    {
        var account = dataBase.FetchAll<Account>().First(acc => acc.AccountNumber == accountNumber);
        return dataBase.FetchAll<Deposit>().Where(dep => dep.AccountRef == account.Id);
    }
}
