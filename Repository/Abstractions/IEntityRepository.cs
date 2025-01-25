using Abstractions.Repository;

namespace Repository.Abstractions;

public interface IEntityRepository<T> where T : DataBaseEntity
{
    List<T> FetchAll();
    T Read(string id);
    T Insert(T entity);
    T Update(T entity);
    T Delete(T entity);
}