using Abstractions.MiniBank;
using Repository.Abstractions;

namespace Repository.DaoConversion.ExistingEntityUpdate.Abstractions;

public interface IEntityUpdateFromDao<in TDao, in TEntity> where TDao : RepositoryEntity where TEntity : IMiniBankVersionable
{
    void Update(TEntity entity, TDao dao);
}
