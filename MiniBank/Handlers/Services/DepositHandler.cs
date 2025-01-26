using Abstractions.Repository;
using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Handlers.Services;

public class DepositHandler(IRepositoryWrapperValidator repoWrapper, ISmsService smsService) : IDepositHandler
{
    public void Deposit(string accountNumber, decimal amount)
    {
        var account = repoWrapper.FetchAll<Account>().FirstOrDefault(x => x.AccountNumber == accountNumber);
        ActionResult actionResult;

        if (account == null)
        {
            actionResult = ActionResult.AccountNotFound;
        }
        else
        {
            account.IncreaseBalance(amount);
            repoWrapper.Update(account);
            actionResult = ActionResult.Success;

            var user = repoWrapper.FetchAll<User>().First(x => x.Id == account.UserRef);
            smsService.Send($"{amount} was deposited to your account", accountNumber, user.PhoneNumber);
        }

        repoWrapper.Insert(new Deposit
        {
            Amount = amount,
            AccountRef = account?.Id,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });

        Helper.ThrowExceptionIfActionFailed(actionResult);
    }

    public IEnumerable<Deposit> GetAllDeposits(string accountNumber)
    {
        var account = repoWrapper.FetchAll<Account>().First(acc => acc.AccountNumber == accountNumber);
        return repoWrapper.FetchAll<Deposit>().Where(dep => dep.AccountRef == account.Id);
    }
}
