using Abstractions.MiniBank;
using Abstractions.Repository;
using Microsoft.Extensions.DependencyInjection;
using Repository.Abstractions;

namespace Repository.InMemoryRepository;

public class InMemoryRepository : IRepository
{
    public List<T> FetchAll<T>() where T : IMiniBankVersionable => GetEntityRepository<T>().FetchAll();

    public T? FetchById<T>(string id) where T : IMiniBankVersionable => GetEntityRepository<T>().FetchById(id);

    public void Insert<T>(T entity) where T : IMiniBankVersionable => GetEntityRepository<T>().Insert(entity);

    public void Update<T>(T entity) where T : IMiniBankVersionable => GetEntityRepository<T>().Update(entity);

    public void Delete<T>(string id) where T : IMiniBankVersionable => GetEntityRepository<T>().Delete(id);

    private static IEntityRepository<T> GetEntityRepository<T>() where T : IMiniBankVersionable
    => ServiceCollection.ServiceProvider.GetRequiredService<IEntityRepository<T>>();
}