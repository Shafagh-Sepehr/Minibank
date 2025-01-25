using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class CardToDao : IEntityToDao<Card, CardDao>
{
    public CardDao EntityToData(Card entity)
    {
        return new CardDao
        {
            CardNumber = entity.CardNumber,
            Id = entity.Id,
            AccountRef = entity.AccountRef,
            Cvv2 = entity.Cvv2,
            Password = entity.GetPasswordHash(),
            SecondPassword = entity.GetSecondPasswordHash(),
            ExpiryDate = entity.ExpiryDate
        };
    }
}