using Abstractions.Repository;
using Microsoft.Extensions.DependencyInjection;
using Repository.Abstractions;

namespace Repository.InMemoryRepository;

public class InMemoryRepository : IRepository
{
    public List<T> FetchAll<T>() where T : DataBaseEntity => GetEntityRepository<T>().FetchAll();

    public T FetchById<T>(string id) where T : DataBaseEntity => GetEntityRepository<T>().FetchById(id);

    public T Insert<T>(T entity) where T : DataBaseEntity => GetEntityRepository<T>().Insert(entity);

    public T Update<T>(T entity) where T : DataBaseEntity => GetEntityRepository<T>().Update(entity);

    public T Delete<T>(string id) where T : DataBaseEntity => GetEntityRepository<T>().Delete(id);

    private static IEntityRepository<T> GetEntityRepository<T>() where T : DataBaseEntity
    => ServiceCollection.ServiceProvider.GetRequiredService<IEntityRepository<T>>();
}