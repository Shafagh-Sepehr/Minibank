using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class TransactionInMemoryRepository(
    IShafaghDB shafaghDB,
    IEntityToDao<Transaction, TransactionDao> entityToDao,
    IDaoToEntity<TransactionDao, Transaction> daoToEntity,
    IEntityUpdateFromDao<TransactionDao, Transaction> entityUpdater
    ) : GeneralInMemoryRepository<Transaction, TransactionDao>(shafaghDB, entityToDao, daoToEntity, entityUpdater)
{
}