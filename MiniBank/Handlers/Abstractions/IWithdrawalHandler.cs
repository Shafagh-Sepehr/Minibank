using MiniBank.Entities.Enums;

namespace MiniBank.Handlers.Abstractions;

public interface IWithdrawalHandler
{
    ActionResult Withdraw(string accountNumber, decimal amount);
}
