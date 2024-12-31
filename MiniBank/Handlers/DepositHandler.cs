﻿using MiniBank.Data.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Entities.Enums;

namespace MiniBank.Handlers;

public class DepositHandler(IDataBase dataBase)
{
    public ActionResult Deposit(string accountNumber, decimal amount)
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
            account.Balance += amount;
            dataBase.Update(account);
            actionResult = ActionResult.Success;
        }
        
        dataBase.Save(new Deposit
        {
            Amount = amount,
            AccountRef = account?.Id ?? 0,
            Status = actionResult == ActionResult.Success ? TransactionStatus.Success : TransactionStatus.Failed,
        });
        
        return actionResult;
    }
}
