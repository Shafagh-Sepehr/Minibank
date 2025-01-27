using Abstractions.MiniBank;
using Repository.Abstractions;

namespace Repository.DaoConversion.DaoToEntity.Abstractions;

public interface IDaoToEntity<in TDao, out TEntity> where TDao : RepositoryEntity where TEntity : IMiniBankVersionable
{
    TEntity Convert(TDao dao);
}
