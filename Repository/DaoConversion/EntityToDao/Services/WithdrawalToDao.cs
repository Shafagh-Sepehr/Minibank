using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class WithdrawalToDao : IEntityToDao<Withdrawal, WithdrawalDao>
{
    public WithdrawalDao Convert(Withdrawal entity)
    {
        var dao = new WithdrawalDao
        {
            Id = entity.Id,
            Status = entity.Status,
            AccountRef = entity.AccountRef,
            Amount = entity.Amount,
            Date = entity.Date
        };
        ((IInMemoryDBVersionable)dao).Version = ((IMiniBankVersionable)entity).Version;
        return dao;
    }
}