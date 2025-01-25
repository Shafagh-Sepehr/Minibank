using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToTransaction : IDaoToEntity<TransactionDao, Transaction>
{
    public Transaction DaoToEntity(TransactionDao dao)
    {
        var newEntity = new Transaction
        {
            Amount = dao.Amount,
            Id = dao.Id,
            DestinationAccountNumber = dao.DestinationAccountNumber,
            DestinationAccountRef = dao.DestinationAccountRef,
            OriginAccountNumber = dao.OriginAccountNumber,
            OriginAccountRef = dao.OriginAccountRef,
            Status = dao.Status,
            Type = dao.Type,
            Description = dao.Description
        };
        Helper.SetValue(newEntity, nameof(newEntity.Date), dao.Date);
        return newEntity;
    }
}