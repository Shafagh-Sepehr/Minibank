using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class WithdrawalToDao : IEntityToDao<Withdrawal, WithdrawalDao>
{
    public WithdrawalDao EntityToDao(Withdrawal entity)
    {
        return new WithdrawalDao
        {
            Id = entity.Id,
            Status = entity.Status,
            AccountRef = entity.AccountRef,
            Amount = entity.Amount,
            Date = entity.Date
        };
    }
}