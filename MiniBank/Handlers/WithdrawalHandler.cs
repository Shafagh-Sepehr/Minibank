﻿using MiniBank.Data.Abstractions;
using MiniBank.Entities;

namespace MiniBank.Handlers;

public class WithdrawalHandler(IDataBase dataBase)
{
    public ActionResult Withdraw(string accountNumber, decimal amount)
    {
        var accounts = dataBase.FetchAll<Account>();
        var account = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        ActionResult actionResult;
        
        if (account == null)
        {
            actionResult = ActionResult.AccountNotFound;
        }
        else
        {
            account.Balance -= amount;
            if (account.Balance < 0)
            {
                actionResult = ActionResult.InsufficientBalance;
            }
            else
            {
                dataBase.Update(account);
                actionResult = ActionResult.Success;
            }
        }
        
        dataBase.Save(new Withdrawal
        {
            Amount = amount,
            AccountRef = account?.Id ?? 0,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });
        
        return actionResult;
    }
}
