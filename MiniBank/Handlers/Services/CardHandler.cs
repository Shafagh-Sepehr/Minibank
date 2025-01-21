using DB.Data.Abstractions;
using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Exceptions;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class CardHandler(IDataBase dataBase, ISmsService smsService) : ICardHandler
{
    public Card CreateCard(long accountRef, string password, string secondPassword)
    {
        var card = new Card
        {
            AccountRef = accountRef,
            CardNumber = GenerateCardNumber(),
            Cvv2 = Helper.GenerateRandomNumberAsString(3),
            ExpiryDate = DateTime.Now.AddYears(5),
            Password = password,
            SecondPassword = secondPassword,
        };
        dataBase.Save(card);
        return card;
    }
    
    public void RequestDynamicPassword(decimal amount, string originCardNumber, string destinationCardNumber, string cvv2, DateTime expiryDate)
    {
        var cards = dataBase.FetchAll<Card>().ToList();
        var accounts = dataBase.FetchAll<Account>().ToList();
        var users = dataBase.FetchAll<User>().ToList();
        var originCard = cards.FirstOrDefault(c => c.CardNumber == originCardNumber);
        
        var user = (from u in users
                    join account in accounts on u.Id equals account.UserRef
                    join card in cards on account.Id equals card.AccountRef
                    select u).SingleOrDefault();
        
        if (user == null || originCard == null || originCard.Cvv2 != cvv2 || 
            originCard.ExpiryDate.Month != expiryDate.Month || originCard.ExpiryDate.Year != expiryDate.Year)
        {
            throw new OperationFailedException("a card with this information couldn't be found");
        }
        
        var dynamicPasswordString = Helper.GenerateRandomNumberAsString(8);
        
        var dynamicPassword = new DynamicPassword
        {
            Amount = amount,
            OriginCardNumber = originCardNumber,
            DestinationCardNumber = destinationCardNumber,
            DynamicPasswordHash = Helper.ComputeSha256Hash(dynamicPasswordString),
        };
        
        dataBase.Save(dynamicPassword);
        smsService.Send($"dynamic password: {destinationCardNumber}", user.PhoneNumber);
    }

    public Card GetCard(Account account)
    {
        var card = dataBase.FetchAll<Card>().FirstOrDefault(card => card.AccountRef == account.Id);
        return card ?? throw new OperationFailedException("couldn't find the requested card");
    }

    private string GenerateCardNumber()
    {
        var cards = dataBase.FetchAll<Card>().ToList();
        string cardNumber;
        do
        {
            cardNumber = Helper.GenerateRandomNumberAsString(16);
        } while (cards.Any(x => x.CardNumber == cardNumber));
        
        return cardNumber;
    }
}
