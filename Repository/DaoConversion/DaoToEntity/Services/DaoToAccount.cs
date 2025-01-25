using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class AccountToDao : IDaoToEntity< AccountDao,  Account>
{
    public Account Convert(AccountDao dao)
    {
        throw new NotImplementedException();
    }
}