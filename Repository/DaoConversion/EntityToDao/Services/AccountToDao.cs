using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class AccountToDao : IEntityToDao<Account, AccountDao>
{
    public AccountDao Convert(Account dao)
    {
        throw new NotImplementedException();
    }
}