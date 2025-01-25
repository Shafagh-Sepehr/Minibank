namespace Repository.DaoConversion.DaoToEntity.Abstractions;

public interface IDaoToEntity<in TDao, out TEntity>
{
    TEntity Convert(TDao dao);
}
