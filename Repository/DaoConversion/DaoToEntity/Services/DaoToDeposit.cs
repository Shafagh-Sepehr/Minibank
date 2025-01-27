using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToDeposit : IDaoToEntity<DepositDao, Deposit>
{
    public Deposit Convert(DepositDao dao)
    {
        var entity = new Deposit
        {
            Status = dao.Status,
            Id = dao.Id,
            AccountRef = dao.AccountRef,
            Amount = dao.Amount,
        };
        Helper.SetValue(entity, nameof(entity.Date), dao.Date);
        ((IMiniBankVersionable)entity).Version = ((IInMemoryDBVersionable)dao).Version;
        return entity;
    }
}