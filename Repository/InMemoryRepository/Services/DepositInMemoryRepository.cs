using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class DepositInMemoryRepository(
    IShafaghDB shafaghDB,
    IEntityToDao<Deposit, DepositDao> entityToDao,
    IDaoToEntity<DepositDao, Deposit> daoToEntity,
    IEntityUpdateFromDao<DepositDao, Deposit> entityUpdater
    ) : GeneralInMemoryRepository<Deposit, DepositDao>(shafaghDB, entityToDao, daoToEntity, entityUpdater)
{
}