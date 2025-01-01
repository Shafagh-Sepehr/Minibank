using DB.Data.Abstractions;
using MiniBank.AppSettings.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;
using MiniBank.Handlers.Abstractions;

namespace MiniBank.Handlers.Services;

public class TransactionHandler(IDataBase dataBase, IAppSettings appSettings) : ITransactionHandler
{
    public ActionResult CreateTransaction_CardToCard(string originCardNumber, string destinationCardNumber, decimal amount, string secondPassword,
                                                     string? description)
    {
        var accounts = dataBase.FetchAll<Account>().ToList();
        var cards = dataBase.FetchAll<AccountCard>().ToList();
        
        var originCard = cards.FirstOrDefault(c => c.CardNumber == originCardNumber);
        var destinationCard = cards.FirstOrDefault(c => c.CardNumber == destinationCardNumber);
        
        var originAccount = accounts.FirstOrDefault(a => a.Id == originCard?.AccountRef);
        var destinationAccount = accounts.FirstOrDefault(a => a.Id == destinationCard?.AccountRef);
        
        ActionResult actionResult;
        var transactionType = TransactionType.FailedCardToCard;
        
        if (originCard == null || destinationCard == null)
        {
            actionResult = ActionResult.AccountNotFound;
        }
        else
        {
            if (dataBase.FetchAll<DynamicPassword>().Any(d =>
                    d.OriginCardNumber == originCardNumber && d.DestinationCardNumber == destinationCardNumber && d.Amount == amount &&
                    d.DynamicPasswordHash == Helper.ComputeSha256Hash(secondPassword) && d.ExpiryDate <= DateTime.Now))
            {
                transactionType = TransactionType.DynamicCardToCard;
                originAccount!.Balance -= amount;
                destinationAccount!.Balance += amount;
                
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
            }
            else if(Helper.ComputeSha256Hash(secondPassword) == originCard.SecondPasswordHash)
            {
                transactionType = TransactionType.StaticCardToCard;
                var staticPasswordPurchaseAmount = dataBase.FetchAll<Transaction>().Where(x=>x.Date == DateTime.Today && x.Type == TransactionType.StaticCardToCard).Sum(x=>x.Amount);
                if (staticPasswordPurchaseAmount > appSettings.MaximumStaticPasswordPurchaseLimit)
                {
                    actionResult = ActionResult.MaximumStaticPasswordPurchaseLimitExceeded;
                }
                else
                {
                    originAccount!.Balance -= amount;
                    destinationAccount!.Balance += amount;
                    
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
                }
            }
            else
            {
                actionResult = ActionResult.IncorrectPassword;
            }
                
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
            originAccount.Balance -= amount;
            destinationAccount.Balance += amount;
            
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
}
