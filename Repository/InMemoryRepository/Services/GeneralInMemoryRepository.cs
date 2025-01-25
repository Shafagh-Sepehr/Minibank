using Abstractions.InMemoryDatabase;
using Abstractions.Repository;
using InMemoryDataBase.Core.Abstractions;
using Repository.Abstractions;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;

namespace Repository.InMemoryRepository.Services;

public abstract class GeneralInMemoryRepository<TEntity, TDao>
    (IShafaghDB shafaghDB,
     IEntityToDao<TEntity, TDao> entityToDao,
     IDaoToEntity<TDao, TEntity> daoToEntity
     ) : IEntityRepository<TEntity> where TEntity : DataBaseEntity where TDao : DataBaseEntity
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
    }

    public void Update(TEntity entity)
    {
        var accountDao = entityToDao.Convert(entity);
        ((IVersionable)accountDao).Version = ((IVersionable)accountDao).Version;
        shafaghDB.Update(accountDao);
    }

    public void Delete(string id)
    {
        shafaghDB.Delete<TDao>(id);
    }
}
