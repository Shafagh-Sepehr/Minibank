using MiniBank.Entities.Enums;

namespace MiniBank.Handlers.Abstractions;

public interface ITransactionHandler
{
    ActionResult CreateTransaction_CardToCard(string originCardNumber, string destinationCardNumber, decimal amount, string secondPassword,
                                              string? description);
    
    ActionResult CreateTransaction_AccountNumberToAccountNumber(string originAccountNumber, string destinationAccountNumber, decimal amount,
                                                                string? description = null);
}
