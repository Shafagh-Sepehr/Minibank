namespace Repository.DaoConversion.EntityToDao.Abstractions;

public interface IEntityToDao<in TEntity, out TDao>
{
    TDao Convert(TEntity dao);
}
