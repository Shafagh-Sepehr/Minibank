using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class DepositToDao : IEntityToDao<Deposit, DepositDao>
{
    public DepositDao Convert(Deposit entity)
    {
        var dao = new DepositDao
        {
            Status = entity.Status,
            Id = entity.Id,
            AccountRef = entity.AccountRef,
            Amount = entity.Amount,
            Date = entity.Date
        };
        ((IInMemoryDBVersionable)dao).Version = ((IMiniBankVersionable)entity).Version;
        return dao;
    }
}
