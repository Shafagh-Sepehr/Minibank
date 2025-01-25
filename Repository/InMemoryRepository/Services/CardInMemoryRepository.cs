using System.Reflection;
using Abstractions.InMemoryDatabase;
using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class CardInMemoryRepository(IShafaghDB shafaghDB) : IEntityRepository<Card>
{
    public List<Card> FetchAll()
    {
        return shafaghDB.FetchAll<CardDao>().Select(CardToDao).ToList();
    }

    public Card? FetchById(string id)
    {
        var cardDao = shafaghDB.FetchById<CardDao>(id);
        return cardDao == null ? null : CardToDao(cardDao);
    }

    public void Insert(Card entity)
    {
        var cardDao = DaoToCard(entity);
        shafaghDB.Insert(cardDao);
    }

    public void Update(Card entity)
    {
        var cardDao = DaoToCard(entity);
        ((IVersionable)cardDao).Version = ((IVersionable)cardDao).Version;
        shafaghDB.Update(cardDao);
    }

    public void Delete(string id)
    {
        shafaghDB.Delete<CardDao>(id);
    }

    private static CardDao DaoToCard(Card card)
    {
        return new CardDao
        {
            CardNumber = card.CardNumber,
            Id = card.Id,
            AccountRef = card.AccountRef,
            Cvv2 = card.Cvv2,
            Password = card.GetPasswordHash(),
            SecondPassword = card.GetSecondPasswordHash(),
            ExpiryDate = card.ExpiryDate
        };
    }

    private static Card CardToDao(CardDao cardDao)
    {
        var card = new Card
        {
            Id = cardDao.Id,
            CardNumber = cardDao.CardNumber,
            AccountRef = cardDao.AccountRef,
            Cvv2 = cardDao.Cvv2,
            Password = "holder",
            SecondPassword = "holder",
            ExpiryDate = cardDao.ExpiryDate
        };

        Helper.SetValue(card, "_passwordHash", cardDao.Password);
        Helper.SetValue(card, "_secondPasswordHash", cardDao.SecondPassword);

        return card;
    }
}