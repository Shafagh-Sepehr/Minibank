using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class DynamicPasswordInMemoryRepository(
    IShafaghDB shafaghDB,
    IEntityToDao<DynamicPassword, DynamicPasswordDao> entityToDao,
    IDaoToEntity<DynamicPasswordDao, DynamicPassword> daoToEntity,
    IEntityUpdateFromDao<DynamicPasswordDao, DynamicPassword> entityUpdater
    ) : GeneralInMemoryRepository<DynamicPassword, DynamicPasswordDao>(shafaghDB, entityToDao, daoToEntity, entityUpdater)
{
}