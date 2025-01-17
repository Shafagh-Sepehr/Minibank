﻿using DB.Data.Abstractions;
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
            PasswordHash = Helper.ComputeSha256Hash(password),
            SecondPasswordHash = Helper.ComputeSha256Hash(secondPassword),
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
        
        
        if (originCard == null || originCard.Cvv2 != cvv2 || originCard.ExpiryDate.Month != expiryDate.Month ||
            originCard.ExpiryDate.Year != expiryDate.Year)
        {
            throw new OperationFailedException("a card with this information couldn't be found");
        }
        
        var dynamicPasswordString = Helper.GenerateRandomNumberAsString(8);
        smsService.Send($"dynamic password: {destinationCardNumber}",user!.PhoneNumber);
        
        var dynamicPassword = new DynamicPassword
        {
            Amount = amount,
            OriginCardNumber = originCardNumber,
            DestinationCardNumber = destinationCardNumber,
            DynamicPasswordHash = Helper.ComputeSha256Hash(dynamicPasswordString),
        };
        
        dataBase.Save(dynamicPassword);
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
