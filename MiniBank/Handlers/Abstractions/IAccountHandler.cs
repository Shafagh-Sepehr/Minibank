namespace MiniBank.Handlers.Abstractions;

public interface IAccountHandler
{
    string CreateAccount(long userRef, string password, string secondPassword);
}
