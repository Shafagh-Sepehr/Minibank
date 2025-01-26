using Abstractions.Repository;

namespace Repository.DaoConversion.ExistingEntityUpdate.Abstractions;

public interface IEntityUpdateFromDao<in TDao, in TEntity> where TDao : DatabaseEntity where TEntity : DatabaseEntity
{
    void Update(TEntity entity, TDao dao)
    {
        EntityUpdate(entity, dao);
        Helper.CopyVersion(dao, entity);
    }

    void EntityUpdate(TEntity entity, TDao dao);
}
