using MiniBank.Entities.Classes;

namespace MiniBank.Handlers.Abstractions;

public interface IAccountHandler
{
    Account CreateAccount(string userRef);
    decimal? GetAccountBalance(string AccountNumber);
    IEnumerable<Account> GetAllUserAccounts(User user);
    bool AccountExistsAndBelongsToUser(string accountNumber, string userRef);
}
