using MiniBank.Entities.Classes;

namespace MiniBank.Handlers.Abstractions;

public interface IAccountHandler
{
    string CreateAccount(long userRef, string password, string secondPassword);
    decimal? GetAccountBalance(User user, string AccountNumber);
    IEnumerable<Account> GetAllUserAccounts(User user);
}
