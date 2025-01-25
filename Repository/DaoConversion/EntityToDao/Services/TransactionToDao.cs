using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class TransactionToDao : IEntityToDao<Transaction, TransactionDao>
{
    public TransactionDao Convert(Transaction dao)
    {
        throw new NotImplementedException();
    }
}