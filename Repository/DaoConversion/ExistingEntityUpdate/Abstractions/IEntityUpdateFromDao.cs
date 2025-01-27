using Abstractions.Repository;

namespace Repository.DaoConversion.ExistingEntityUpdate.Abstractions;

public interface IEntityUpdateFromDao<in TDao, in TEntity> where TDao : DatabaseEntity where TEntity : DatabaseEntity
{
    void Update(TEntity entity, TDao dao);
}
