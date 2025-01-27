using Abstractions.InMemoryDatabase;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToCard : IDaoToEntity<CardDao, Card>
{
    public Card Convert(CardDao dao)
    {
        var entity = new Card
        {
            Id = dao.Id,
            CardNumber = dao.CardNumber,
            AccountRef = dao.AccountRef,
            Cvv2 = dao.Cvv2,
            Password = "1234",
            SecondPassword = "12345",
            ExpiryDate = dao.ExpiryDate
        };

        Helper.SetValue(entity, "_passwordHash", dao.Password);
        Helper.SetValue(entity, "_secondPasswordHash", dao.SecondPassword);
        ((IVersionable)entity).Version = ((IVersionable)dao).Version;
        return entity;
    }
}