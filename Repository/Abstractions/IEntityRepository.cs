using Abstractions.Repository;

namespace Repository.Abstractions;

public interface IEntityRepository<T> where T : DataBaseEntity
{
    List<T> FetchAll();
    T FetchById(string id);
    T Insert(T entity);
    T Update(T entity);
    T Delete(string id);
}