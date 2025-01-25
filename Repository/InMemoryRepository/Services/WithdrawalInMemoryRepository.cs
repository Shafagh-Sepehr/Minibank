using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class WithdrawalInMemoryRepository(
    IShafaghDB shafaghDB,
    IEntityToDao<Withdrawal, WithdrawalDao> entityToDao,
    IDaoToEntity<WithdrawalDao, Withdrawal> daoToEntity,
    IEntityUpdateFromDao<WithdrawalDao, Withdrawal> entityUpdater
    ) : GeneralInMemoryRepository<Withdrawal, WithdrawalDao>(shafaghDB, entityToDao, daoToEntity, entityUpdater)
{
}