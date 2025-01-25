using Abstractions.Repository;
using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class DepositHandler(IRepository repository, ISmsService smsService) : IDepositHandler
{
    public void Deposit(string accountNumber, decimal amount)
    {
        var account = repository.FetchAll<Account>().FirstOrDefault(x => x.AccountNumber == accountNumber);
        ActionResult actionResult;

        if (account == null)
        {
            actionResult = ActionResult.AccountNotFound;
        }
        else
        {
            account.IncreaseBalance(amount);
            repository.Update(account);
            actionResult = ActionResult.Success;

            var user = repository.FetchAll<User>().First(x => x.Id == account.UserRef);
            smsService.Send($"{amount} was deposited to your account", accountNumber, user.PhoneNumber);
        }

        repository.Insert(new Deposit
        {
            Amount = amount,
            AccountRef = account?.Id,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });

        Helper.ThrowExceptionIfActionFailed(actionResult);
    }

    public IEnumerable<Deposit> GetAllDeposits(string accountNumber)
    {
        var account = repository.FetchAll<Account>().First(acc => acc.AccountNumber == accountNumber);
        return repository.FetchAll<Deposit>().Where(dep => dep.AccountRef == account.Id);
    }
}
