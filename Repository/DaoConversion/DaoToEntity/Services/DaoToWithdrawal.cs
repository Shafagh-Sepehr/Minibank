using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToWithdrawal : IDaoToEntity<WithdrawalDao, Withdrawal>
{
    public Withdrawal Convert(WithdrawalDao dao)
    {
        throw new NotImplementedException();
    }
}