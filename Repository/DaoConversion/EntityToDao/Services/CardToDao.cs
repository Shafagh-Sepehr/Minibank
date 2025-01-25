using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class CardToDao : IEntityToDao<Card, CardDao>
{
    public CardDao Convert(Card dao)
    {
        throw new NotImplementedException();
    }
}