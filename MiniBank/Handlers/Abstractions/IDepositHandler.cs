using MiniBank.Entities.Classes;

namespace MiniBank.Handlers.Abstractions;

public interface IDepositHandler
{
    void Deposit(string accountNumber, decimal amount);
    IEnumerable<Deposit> GetAllDeposits(string accountNumber);
}
