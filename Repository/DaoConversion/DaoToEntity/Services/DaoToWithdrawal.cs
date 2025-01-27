using Abstractions.InMemoryDatabase;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToWithdrawal : IDaoToEntity<WithdrawalDao, Withdrawal>
{
    public Withdrawal Convert(WithdrawalDao dao)
    {
        var entity = new Withdrawal
        {
            Id = dao.Id,
            Status = dao.Status,
            AccountRef = dao.AccountRef,
            Amount = dao.Amount
        };
        Helper.SetValue(entity, nameof(entity.Date), dao.Date);
        ((IVersionable)entity).Version = ((IVersionable)dao).Version;
        return entity;
    }
}