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
        var originCard = repoWrapper.FetchAll<Card>().FirstOrDefault(c => c.CardNumber == originCardNumber);
        if (originCard == null)
        {
            throw new OperationFailedException($"origin card not found, no card exists with card number {originCardNumber}");
        }

        var originAccount = repoWrapper.FetchById<Account>(originCard.AccountRef);
        if (originAccount == null)
        {
            throw new OperationFailedException($"origin account not found, no account exists for card number {originCardNumber}");
        }

        var user = repoWrapper.FetchById<User>(originAccount.UserRef);

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
        smsService.Send($"dynamic password: {dynamicPasswordString}", originAccount.AccountNumber, user.PhoneNumber);
    }

    public void RequestAccountToAccountDynamicPassword(decimal amount, string originAccountNumber, string destinationAccountNumber)
    {
        var cards = repoWrapper.FetchAll<Card>();
        var accounts = repoWrapper.FetchAll<Account>();
        var users = repoWrapper.FetchAll<User>();

        var originAccount = accounts.FirstOrDefault(a => a.AccountNumber == originAccountNumber);
        var destinationAccount = accounts.FirstOrDefault(a => a.AccountNumber == destinationAccountNumber);

        
        if (originAccount == null || destinationAccount == null)
        {
            throw new OperationFailedException("a card with this information couldn't be found");
        }

        var originCard = cards.FirstOrDefault(c => c.AccountRef == originAccount.Id);
        var destinationCard = cards.FirstOrDefault(c => c.AccountRef == destinationAccount.Id);

        var user = repoWrapper.FetchById<User>(originAccount.UserRef);

        if(originCard == null || destinationCard == null || user == null)
        {
            throw new OperationFailedException("a card with this information couldn't be found");
        }

        var dynamicPasswordString = Helper.GenerateRandomNumberAsString(8);

        var dynamicPassword = new DynamicPassword
        {
            Amount = amount,
            OriginCardNumber = originCard.CardNumber,
            DestinationCardNumber = destinationCard.CardNumber,
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
        var account = repoWrapper.FetchAll<Account>().First(acc => acc.AccountNumber == accountNumber);
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
