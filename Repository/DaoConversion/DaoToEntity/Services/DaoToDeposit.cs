using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToDeposit : IDaoToEntity<DepositDao, Deposit>
{
    public Deposit DaoToEntity(DepositDao dao)
    {
        var deposit = new Deposit
        {
            Status = dao.Status,
            Id = dao.Id,
            AccountRef = dao.AccountRef,
            Amount = dao.Amount,
        };
        Helper.SetValue(deposit, nameof(deposit.Date), dao.Date);
        return deposit;
    }
}