using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToWithdrawal : IDaoToEntity<WithdrawalDao, Withdrawal>
{
    public Withdrawal DaoToEntity(WithdrawalDao dao)
    {
        var newEntity = new Withdrawal
        {
            Id = dao.Id,
            Status = dao.Status,
            AccountRef = dao.AccountRef,
            Amount = dao.Amount
        };
        Helper.SetValue(newEntity, nameof(newEntity.Date), dao.Date);
        return newEntity;
    }
}