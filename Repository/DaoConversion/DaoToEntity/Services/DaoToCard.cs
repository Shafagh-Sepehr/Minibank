using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToCard : IDaoToEntity<CardDao, Card>
{
    public Card Convert(CardDao dao)
    {
        throw new NotImplementedException();
    }
}