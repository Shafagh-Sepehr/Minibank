using MiniBank.Entities.Enums;

namespace MiniBank.Handlers.Abstractions;

public interface ITransactionHandler
{
    void CreateCardToCardTransaction(string originCardNumber, string destinationCardNumber, decimal amount, string secondPassword,
                                              string? description);
    
    void CreateAccountToAccountTransaction(string originAccountNumber, string destinationAccountNumber, decimal amount,
                                                                string? description = null);
}
