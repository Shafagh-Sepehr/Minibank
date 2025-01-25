using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class DepositToDao : IEntityToDao<Deposit, DepositDao>
{
    public DepositDao Convert(Deposit dao)
    {
        throw new NotImplementedException();
    }
}