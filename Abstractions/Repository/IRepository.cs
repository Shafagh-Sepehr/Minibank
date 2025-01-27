using Abstractions.MiniBank;

namespace Abstractions.Repository;

public interface IRepository
{
    List<T> FetchAll<T>() where T : IMiniBankVersionable;
    T? FetchById<T>(string id) where T : IMiniBankVersionable;
    void Insert<T>(T entity) where T : IMiniBankVersionable;
    void Update<T>(T entity) where T : IMiniBankVersionable;
    void Delete<T>(string id) where T : IMiniBankVersionable;
}
