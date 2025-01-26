using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Exceptions;
using MiniBank.Handlers.Abstractions;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Handlers.Services;

public class CardHandler(IRepositoryWrapperValidator repoWrapper, ISmsService smsService) : ICardHandler
{
    public Card CreateCard(string accountRef, string password, string secondPassword)
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
        repoWrapper.Insert(card);
        return card;
    }

    public void RequestDynamicPassword(decimal amount, string originCardNumber, string destinationCardNumber, string cvv2, DateTime expiryDate)
    {
        var cards = repoWrapper.FetchAll<Card>();
        var accounts = repoWrapper.FetchAll<Account>();
        var users = repoWrapper.FetchAll<User>();
        var originCard = cards.FirstOrDefault(c => c.CardNumber == originCardNumber);

        var user = (from u in users
                    join account in accounts on u.Id equals account.UserRef
                    join card in cards on account.Id equals card.AccountRef
                    select u).Distinct().SingleOrDefault();

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

        repoWrapper.Insert(dynamicPassword);
        smsService.Send($"dynamic password: {dynamicPasswordString}", accounts.First(acc => acc.Id == originCard.AccountRef).AccountNumber, user.PhoneNumber);
    }

    public Card GetCard(Account account)
    {
        var card = repoWrapper.FetchAll<Card>().FirstOrDefault(card => card.AccountRef == account.Id);
        return card ?? throw new OperationFailedException("couldn't find the requested card");
    }

    public Card GetCard(string accountNumber)
    {
        var account = repoWrapper.FetchAll<Account>().Where(acc => acc.AccountNumber == accountNumber).First();
        var card = repoWrapper.FetchAll<Card>().FirstOrDefault(card => card.AccountRef == account.Id);
        return card ?? throw new OperationFailedException("couldn't find the requested card");
    }

    private string GenerateCardNumber()
    {
        var cards = repoWrapper.FetchAll<Card>();
        string cardNumber;
        do
        {
            cardNumber = Helper.GenerateRandomNumberAsString(16);
        } while (cards.Any(x => x.CardNumber == cardNumber));

        return cardNumber;
    }
}
