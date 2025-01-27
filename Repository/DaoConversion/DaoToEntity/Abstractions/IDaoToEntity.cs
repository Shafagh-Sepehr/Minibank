using Abstractions.Repository;

namespace Repository.DaoConversion.DaoToEntity.Abstractions;

public interface IDaoToEntity<in TDao, out TEntity> where TDao : DatabaseEntity where TEntity : DatabaseEntity
{
    TEntity Convert(TDao dao);
}
