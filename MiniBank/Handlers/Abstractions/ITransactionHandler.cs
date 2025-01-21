using MiniBank.Entities.Enums;

namespace MiniBank.Handlers.Abstractions;

public interface ITransactionHandler
{
    ActionResult CreateCardToCardTransaction(string originCardNumber, string destinationCardNumber, decimal amount, string secondPassword,
                                              string? description);
    
    ActionResult CreateAccountToAccountTransaction(string originAccountNumber, string destinationAccountNumber, decimal amount,
                                                                string? description = null);
}
