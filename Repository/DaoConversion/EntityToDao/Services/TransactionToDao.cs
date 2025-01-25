using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class TransactionToDao : IEntityToDao<Transaction, TransactionDao>
{
    public TransactionDao EntityToDao(Transaction entity)
    {
        return new TransactionDao
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Status = entity.Status,
            Date = entity.Date,
            Description = entity.Description,
            DestinationAccountNumber = entity.DestinationAccountNumber,
            DestinationAccountRef = entity.DestinationAccountRef,
            OriginAccountNumber = entity.OriginAccountNumber,
            OriginAccountRef = entity.OriginAccountRef,
            Type = entity.Type
        };
    }
}