using MiniBank.Entities.Enums;

namespace MiniBank.Handlers.Abstractions;

public interface IWithdrawalHandler
{
    void Withdraw(string accountNumber, decimal amount);
}
