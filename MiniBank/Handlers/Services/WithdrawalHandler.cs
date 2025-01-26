using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Handlers.Services;

public class WithdrawalHandler(IRepositoryWrapperValidator repoWrapper, ISmsService smsService) : IWithdrawalHandler
{
    public void Withdraw(string accountNumber, decimal amount)
    {
        var accounts = repoWrapper.FetchAll<Account>();
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
                repoWrapper.Update(account);
                actionResult = ActionResult.Success;
                var user = repoWrapper.FetchById<User>(account.UserRef);
                smsService.Send($"{amount} was withdrawn from your account", accountNumber, user?.PhoneNumber ?? "unknown phone number");
            }
            
        }
        
        repoWrapper.Insert(new Withdrawal
        {
            Amount = amount,
            AccountRef = account?.Id,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });

        Helper.ThrowExceptionIfActionFailed(actionResult);
    }

    public IEnumerable<Withdrawal> GetAllWithdrawals(string accountNumber)
    {
        var account = repoWrapper.FetchAll<Account>().First(acc => acc.AccountNumber == accountNumber);
        return repoWrapper.FetchAll<Withdrawal>().Where(dep => dep.AccountRef == account.Id);
    }
}
