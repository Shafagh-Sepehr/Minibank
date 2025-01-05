using DB.Data.Abstractions;
using MiniBank.AppSettings.Abstractions;
using MiniBank.Communication.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class TransactionHandler(IDataBase dataBase, IAppSettings appSettings, ISmsService smsService) : ITransactionHandler
{
    public ActionResult CreateTransaction_CardToCard(string originCardNumber, string destinationCardNumber, decimal amount, string secondPassword,
                                                     string? description)
    {
        var accounts = dataBase.FetchAll<Account>().ToList();
        var cards = dataBase.FetchAll<Card>().ToList();
        
        var originCard = cards.FirstOrDefault(c => c.CardNumber == originCardNumber);
        var destinationCard = cards.FirstOrDefault(c => c.CardNumber == destinationCardNumber);
        
        var originAccount = accounts.FirstOrDefault(a => a.Id == originCard?.AccountRef)!;
        var destinationAccount = accounts.FirstOrDefault(a => a.Id == destinationCard?.AccountRef)!;
        
        ActionResult actionResult;
        var transactionType = TransactionType.FailedCardToCard;
        
        if (originCard == null || destinationCard == null)
        {
            actionResult = ActionResult.AccountNotFound;
        }
        else
        {
            actionResult = ExecuteCardToCardTransaction(originCardNumber, destinationCardNumber, amount,
                secondPassword, originAccount, destinationAccount, originCard, ref transactionType);
        }
        
        dataBase.Save(new Transaction
        {
            Amount = amount,
            OriginAccountRef = originAccount?.Id ?? 0,
            DestinationAccountRef = destinationAccount?.Id ?? 0,
            Description = description,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
            Type = transactionType,
        });
        
        return actionResult;
    }
    
    
    public ActionResult CreateTransaction_AccountNumberToAccountNumber(string originAccountNumber, string destinationAccountNumber, decimal amount,
                                                                       string? description = null)
    {
        var accounts = dataBase.FetchAll<Account>().ToList();
        var originAccount = accounts.FirstOrDefault(x => x.AccountNumber == originAccountNumber);
        var destinationAccount = accounts.FirstOrDefault(x => x.AccountNumber == destinationAccountNumber);
        
        ActionResult actionResult;
        
        if (originAccount == null || destinationAccount == null)
        {
            actionResult = ActionResult.AccountNotFound;
        }
        else
        {
            actionResult = TransactAndValidateAndUpdate(amount, originAccount, destinationAccount);
            if(actionResult == ActionResult.Success)
            {
                Sms(originAccount, destinationAccount, amount);
            }
        }
        
        dataBase.Save(new Transaction
        {
            Amount = amount,
            OriginAccountRef = originAccount?.Id ?? 0,
            DestinationAccountRef = destinationAccount?.Id ?? 0,
            Description = description,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
            Type = TransactionType.AccountToAccount,
        });
        
        return actionResult;
    }
    
    private ActionResult ExecuteCardToCardTransaction(string originCardNumber, string destinationCardNumber, decimal amount, string secondPassword,
                                                      Account originAccount, Account destinationAccount, Card originCard,
                                                      ref TransactionType transactionType)
    {
        ActionResult actionResult;
        if (IsDynamicPassword(originCardNumber, destinationCardNumber, amount, secondPassword))
        {
            transactionType = TransactionType.DynamicCardToCard;
            
            actionResult = TransactAndValidateAndUpdate(amount, originAccount, destinationAccount);
            if(actionResult == ActionResult.Success)
            {
                Sms(originAccount, destinationAccount, amount);
            }
        }
        else if (IsStaticPassword(secondPassword, originCard))
        {
            transactionType = TransactionType.StaticCardToCard;
            
            if (CanUseStaticPassword(originAccount))
            {
                actionResult = ActionResult.MaximumStaticPasswordPurchaseLimitExceeded;
            }
            else
            {
                actionResult = TransactAndValidateAndUpdate(amount, originAccount, destinationAccount);
                if(actionResult == ActionResult.Success)
                {
                    Sms(originAccount, destinationAccount, amount);
                }
            }
        }
        else
        {
            actionResult = ActionResult.IncorrectPassword;
        }
        
        return actionResult;
    }
    
    private static bool IsStaticPassword(string secondPassword, Card originCard) =>
        Helper.ComputeSha256Hash(secondPassword) == originCard.SecondPasswordHash;
    
    private ActionResult TransactAndValidateAndUpdate(decimal amount, Account originAccount, Account destinationAccount)
    {
        Transact(originAccount, destinationAccount, amount);
        var actionResult = ValidateBalanceAndUpdateDataBase(originAccount, destinationAccount);
        return actionResult;
    }
    
    private bool CanUseStaticPassword(Account account)
    {
        var transactions = dataBase.FetchAll<Transaction>();
        var staticPasswordPurchaseAmount = transactions.Where(x => account.Id == x.OriginAccountRef && x.Date == DateTime.Today &&
                                                                   x.Type == TransactionType.StaticCardToCard)
            .Sum(x => x.Amount);
        
        return staticPasswordPurchaseAmount > appSettings.MaximumStaticPasswordPurchaseLimit;
    }
    
    private bool IsDynamicPassword(string originCardNumber, string destinationCardNumber, decimal amount, string secondPassword) =>
        dataBase.FetchAll<DynamicPassword>().Any(d =>
            d.OriginCardNumber == originCardNumber && d.DestinationCardNumber == destinationCardNumber && d.Amount == amount &&
            d.DynamicPasswordHash == Helper.ComputeSha256Hash(secondPassword) && d.ExpiryDate <= DateTime.Now);
    
    
    private ActionResult ValidateBalanceAndUpdateDataBase(Account originAccount, Account destinationAccount)
    {
        ActionResult actionResult;
        if (originAccount.Balance < 0)
        {
            actionResult = ActionResult.InsufficientBalance;
        }
        else
        {
            dataBase.Update(originAccount);
            dataBase.Update(destinationAccount);
            actionResult = ActionResult.Success;
        }
        
        return actionResult;
    }
    
    private static void Transact(Account originAccount, Account destinationAccount, decimal amount)
    {
        originAccount.DecreaseBalance(amount);
        destinationAccount.IncreaseBalance(amount);
    }
    
    private void Sms(Account originAccount, Account destinationAccount, decimal amount)
    {
        var originUser = dataBase.FetchAll<User>().First(x => x.Id == originAccount.UserRef);
        smsService.Send($"{amount} was taken from your account", originUser.PhoneNumber);
        
        var destinationUser = dataBase.FetchAll<User>().First(x => x.Id == destinationAccount.UserRef);
        smsService.Send($"{amount} was sent to your account", destinationUser.PhoneNumber);
    }
}
