using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToTransaction : IDaoToEntity<TransactionDao, Transaction>
{
    public Transaction Convert(TransactionDao dao)
    {
        throw new NotImplementedException();
    }
}