using Abstractions.MiniBank;

namespace Repository.Abstractions;

public interface IEntityRepository<T> where T : IMiniBankVersionable
{
    List<T> FetchAll();
    T? FetchById(string id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(string id);
}