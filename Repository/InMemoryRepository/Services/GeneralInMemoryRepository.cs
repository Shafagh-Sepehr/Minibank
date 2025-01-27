using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using InMemoryDataBase.Core.Abstractions;
using Repository.Abstractions;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;

namespace Repository.InMemoryRepository.Services;

public abstract class GeneralInMemoryRepository<TEntity, TDao>
    (IShafaghDB shafaghDB,
     IEntityToDao<TEntity, TDao> entityToDao,
     IDaoToEntity<TDao, TEntity> daoToEntity,
     IEntityUpdateFromDao<TDao,TEntity> entityUpdater
     ) : IEntityRepository<TEntity> where TEntity : MiniBankDatabaseEntity where TDao : RepositoryEntity
{
    public List<TEntity> FetchAll()
    {
        return shafaghDB.FetchAll<TDao>().Select(daoToEntity.Convert).ToList();
    }

    public TEntity? FetchById(string id)
    {
        var accountDao = shafaghDB.FetchById<TDao>(id);
        return accountDao == null ? null : daoToEntity.Convert(accountDao);
    }

    public void Insert(TEntity entity)
    {
        var accountDao = entityToDao.Convert(entity);
        shafaghDB.Insert(accountDao);
        entityUpdater.Update(entity, accountDao);
    }

    public void Update(TEntity entity)
    {
        var accountDao = entityToDao.Convert(entity);
        ((IInMemoryDBVersionable)accountDao).Version = ((IInMemoryDBVersionable)accountDao).Version;
        shafaghDB.Update(accountDao);
        entityUpdater.Update(entity, accountDao);
    }

    public void Delete(string id)
    {
        shafaghDB.Delete<TDao>(id);
    }
}
