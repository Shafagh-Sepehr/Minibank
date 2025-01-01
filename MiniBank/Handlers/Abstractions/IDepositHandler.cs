using MiniBank.Entities.Enums;

namespace MiniBank.Handlers.Abstractions;

public interface IDepositHandler
{
    ActionResult Deposit(string accountNumber, decimal amount);
}
