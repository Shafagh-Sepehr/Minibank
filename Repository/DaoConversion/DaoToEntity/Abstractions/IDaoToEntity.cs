using Abstractions.Repository;

namespace Repository.DaoConversion.DaoToEntity.Abstractions;

public interface IDaoToEntity<in TDao, out TEntity> where TDao : DataBaseEntity where TEntity : DataBaseEntity
{
    TEntity Convert(TDao dao)
    {
        var newEntity = DaoToEntity(dao);
        Helper.CopyVersion(dao, newEntity);
        return newEntity;
    }

    TEntity DaoToEntity(TDao dao);
}
