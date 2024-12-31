using MiniBank.Data.Abstractions;
using MiniBank.Entities;

namespace MiniBank.Handlers;

public class CardHandler(IDataBase dataBase)
{
    public AccountCard CreateCard(long accountRef, string password, string secondPassword)
    {
        var card = new AccountCard
        {
            AccountRef = accountRef,
            CardNumber = GenerateCardNumber(),
            Cvv2 = Helper.GenerateRandomNumberAsString(3),
            ExpiryDate = DateTime.Now.AddYears(5),
            PasswordHash = Helper.ComputeSha256Hash(password),
            SecondPasswordHash = Helper.ComputeSha256Hash(secondPassword),
        };
        dataBase.Save(card);
        return card;
    }
    
    private string GenerateCardNumber()
    {
        var cards = dataBase.FetchAll<AccountCard>().ToList();
        string cardNumber;
        do
        {
            cardNumber = Helper.GenerateRandomNumberAsString(16);
            
        } while (cards.Any(x => x.CardNumber == cardNumber));
        
        return cardNumber;
    }
}
