using Abstractions.Repository;

namespace Repository.DaoConversion.EntityToDao.Abstractions;

public interface IEntityToDao<in TEntity, out TDao> where TDao : DataBaseEntity where TEntity : DataBaseEntity
{
    TDao Convert(TEntity entity)
    {
        var newDao = EntityToDao(entity);
        Helper.CopyVersion(entity, newDao);
        return newDao;
    }
    TDao EntityToDao(TEntity entity);
}
