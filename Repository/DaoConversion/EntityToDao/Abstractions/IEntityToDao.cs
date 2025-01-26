using Abstractions.Repository;

namespace Repository.DaoConversion.EntityToDao.Abstractions;

public interface IEntityToDao<in TEntity, out TDao> where TDao : DatabaseEntity where TEntity : DatabaseEntity
{
    TDao Convert(TEntity entity)
    {
        var newDao = EntityToDao(entity);
        Helper.CopyVersion(entity, newDao);
        return newDao;
    }
    TDao EntityToDao(TEntity entity);
}
