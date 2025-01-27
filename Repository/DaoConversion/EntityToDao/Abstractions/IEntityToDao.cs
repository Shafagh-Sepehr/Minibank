using Abstractions.Repository;

namespace Repository.DaoConversion.EntityToDao.Abstractions;

public interface IEntityToDao<in TEntity, out TDao> where TDao : DatabaseEntity where TEntity : DatabaseEntity
{
    TDao Convert(TEntity entity);
}
