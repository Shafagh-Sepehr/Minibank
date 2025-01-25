using Abstractions.InMemoryDatabase;
using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class AccountInMemoryRepository(IShafaghDB shafaghDB) : IEntityRepository<Account>
{
    public List<Account> FetchAll()
    {
        return shafaghDB.FetchAll<AccountDao>().Select(AccountToDao).ToList();
    }

    public Account? FetchById(string id)
    {
        var accountDao = shafaghDB.FetchById<AccountDao>(id);
        return accountDao == null ? null : AccountToDao(accountDao);
    }

    public void Insert(Account entity)
    {
        var accountDao = DaoToAccount(entity);
        shafaghDB.Insert(accountDao);
    }

    public void Update(Account entity)
    {
        var accountDao = DaoToAccount(entity);
        ((IVersionable)accountDao).Version = ((IVersionable)accountDao).Version;
        shafaghDB.Update(accountDao);
    }

    public void Delete(string id)
    {
        shafaghDB.Delete<AccountDao>(id);
    }

    private static AccountDao DaoToAccount(Account account)
    {
        return new AccountDao
        {
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Status = account.Status,
            UserRef = account.UserRef,
            Id = account.Id,
        };
    }

    private static Account AccountToDao(AccountDao accountDao)
    {
        var account = new Account
        {
            AccountNumber = accountDao.AccountNumber,
            Status = accountDao.Status,
            UserRef = accountDao.UserRef,
            Id = accountDao.Id,
        };
        account.IncreaseBalance(accountDao.Balance);
        return account;
    }
}