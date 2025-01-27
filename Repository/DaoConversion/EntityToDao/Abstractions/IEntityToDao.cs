using Abstractions.MiniBank;
using Repository.Abstractions;

namespace Repository.DaoConversion.EntityToDao.Abstractions;

public interface IEntityToDao<in TEntity, out TDao> where TDao : RepositoryEntity where TEntity : IMiniBankVersionable
{
    TDao Convert(TEntity entity);
}
