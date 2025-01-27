using Abstractions.InMemoryDatabase;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class AccountToDao : IEntityToDao<Account, AccountDao>
{
    public AccountDao Convert(Account entity)
    {
        var dao = new AccountDao
        {
            AccountNumber = entity.AccountNumber,
            Balance = entity.Balance,
            Status = entity.Status,
            UserRef = entity.UserRef,
            Id = entity.Id,
        };
        ((IVersionable)dao).Version = ((IVersionable)entity).Version;
        return dao;
    }
}