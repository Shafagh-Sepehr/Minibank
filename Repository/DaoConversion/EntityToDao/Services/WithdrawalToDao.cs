using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class WithdrawalToDao : IEntityToDao<Withdrawal, WithdrawalDao>
{
    public WithdrawalDao Convert(Withdrawal dao)
    {
        throw new NotImplementedException();
    }
}