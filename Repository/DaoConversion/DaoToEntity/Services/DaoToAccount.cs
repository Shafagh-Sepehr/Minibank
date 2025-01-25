using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class AccountToDao : IDaoToEntity< AccountDao,  Account>
{
    public Account DaoToEntity(AccountDao dao)
    {
        var account = new Account
        {
            AccountNumber = dao.AccountNumber,
            Status = dao.Status,
            UserRef = dao.UserRef,
            Id = dao.Id,
        };
        account.IncreaseBalance(dao.Balance);
        return account;
    }
}