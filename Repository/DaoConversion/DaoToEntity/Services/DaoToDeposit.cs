using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToDeposit : IDaoToEntity<DepositDao, Deposit>
{
    public Deposit Convert(DepositDao dao)
    {
        throw new NotImplementedException();
    }
}