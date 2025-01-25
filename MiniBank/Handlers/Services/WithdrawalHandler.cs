using Abstractions.Repository;
using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class WithdrawalHandler(IRepository repository, ISmsService smsService) : IWithdrawalHandler
{
    public void Withdraw(string accountNumber, decimal amount)
    {
        var accounts = repository.FetchAll<Account>();
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
                repository.Update(account);
                actionResult = ActionResult.Success;
                var user = repository.FetchAll<User>().First(x => x.Id == account.UserRef);
                smsService.Send($"{amount} was withdrawn from your account", accountNumber, user.PhoneNumber);
            }
            
        }
        
        repository.Insert(new Withdrawal
        {
            Amount = amount,
            AccountRef = account?.Id,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });

        Helper.ThrowExceptionIfActionFailed(actionResult);
    }

    public IEnumerable<Withdrawal> GetAllWithdrawals(string accountNumber)
    {
        var account = repository.FetchAll<Account>().First(acc => acc.AccountNumber == accountNumber);
        return repository.FetchAll<Withdrawal>().Where(dep => dep.AccountRef == account.Id);
    }
}
