using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class UserInMemoryRepository(
    IShafaghDB shafaghDB,
    IEntityToDao<User, UserDao> entityToDao,
    IDaoToEntity<UserDao, User> daoToEntity,
    IEntityUpdateFromDao<UserDao, User> entityUpdater
    ) : GeneralInMemoryRepository<User, UserDao>(shafaghDB, entityToDao, daoToEntity, entityUpdater)
{
}