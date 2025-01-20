using MiniBank.Entities.Enums;

namespace MiniBank.Handlers.Abstractions;

public interface ITransactionHandler
{
    ActionResult CreateCardToCardTransaction(string originCardNumber, string destinationCardNumber, decimal amount, string secondPassword,
                                              string? description);
    
    ActionResult CreateAccountNumberToAccountNumberTransaction(string originAccountNumber, string destinationAccountNumber, decimal amount,
                                                                string? description = null);
}
