using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class AccountInMemoryRepository(IShafaghDB shafaghDB, IEntityToDao<Account, AccountDao> entityToDao, IDaoToEntity<AccountDao, Account> daoToEntity) : GeneralInMemoryRepository<Account, AccountDao>(shafaghDB, entityToDao, daoToEntity)
{
}