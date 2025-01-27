using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class CardToDao : IEntityToDao<Card, CardDao>
{
    public CardDao Convert(Card entity)
    {
        var dao = new CardDao
        {
            CardNumber = entity.CardNumber,
            Id = entity.Id,
            AccountRef = entity.AccountRef,
            Cvv2 = entity.Cvv2,
            Password = entity.GetPasswordHash(),
            SecondPassword = entity.GetSecondPasswordHash(),
            ExpiryDate = entity.ExpiryDate
        };
        ((IInMemoryDBVersionable)dao).Version = ((IMiniBankVersionable)entity).Version;
        return dao;
    }
}