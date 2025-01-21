using MiniBank.Entities.Classes;

namespace MiniBank.Handlers.Abstractions;

public interface IAccountHandler
{
    Account CreateAccount(long userRef);
    decimal? GetAccountBalance(User user, string AccountNumber);
    IEnumerable<Account> GetAllUserAccounts(User user);
}
