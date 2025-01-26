using MiniBank.Entities.Classes;

namespace MiniBank.Handlers.Abstractions;

public interface ICardHandler
{
    Card CreateCard(string accountRef, string password, string secondPassword);
    void RequestDynamicPassword(decimal amount, string originCardNumber, string destinationCardNumber, string cvv2, DateTime expiryDate);
    void RequestAccountToAccountDynamicPassword(decimal amount, string originAccountNumber, string destinationAccountNumber);

    Card GetCard(Account account);
    Card GetCard(string accountNumber);
}
