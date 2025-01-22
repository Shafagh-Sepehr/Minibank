using MiniBank.Entities.Enums;

namespace MiniBank.Handlers.Abstractions;

public interface IDepositHandler
{
    void Deposit(string accountNumber, decimal amount);
}
