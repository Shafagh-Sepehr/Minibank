using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToCard : IDaoToEntity<CardDao, Card>
{
    public Card DaoToEntity(CardDao dao)
    {
        var card = new Card
        {
            Id = dao.Id,
            CardNumber = dao.CardNumber,
            AccountRef = dao.AccountRef,
            Cvv2 = dao.Cvv2,
            Password = "holder",
            SecondPassword = "holder",
            ExpiryDate = dao.ExpiryDate
        };

        Helper.SetValue(card, "_passwordHash", dao.Password);
        Helper.SetValue(card, "_secondPasswordHash", dao.SecondPassword);
        return card;
    }
}