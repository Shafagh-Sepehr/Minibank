using MiniBank.Entities.Classes;

namespace MiniBank.Handlers.Abstractions;

public interface ICardHandler
{
    Card CreateCard(long accountRef, string password, string secondPassword);
    void RequestDynamicPassword(decimal amount, string originCardNumber, string destinationCardNumber, string cvv2, DateTime expiryDate);
}
