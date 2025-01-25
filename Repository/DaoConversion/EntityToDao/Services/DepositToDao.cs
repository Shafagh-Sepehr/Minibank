using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class DepositToDao : IEntityToDao<Deposit, DepositDao>
{
    public DepositDao EntityToDao(Deposit entity)
    {
        return new DepositDao
        {
            Status = entity.Status,
            Id = entity.Id,
            AccountRef = entity.AccountRef,
            Amount = entity.Amount,
            Date = entity.Date
        };
    }
}