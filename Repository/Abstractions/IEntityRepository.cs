using Abstractions.Repository;

namespace Repository.Abstractions;

public interface IEntityRepository<T> where T : DataBaseEntity
{
    List<T> FetchAll();
    T FetchById(string id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(string id);
}