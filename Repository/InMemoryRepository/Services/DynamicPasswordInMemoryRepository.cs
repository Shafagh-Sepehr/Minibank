using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class DynamicPasswordInMemoryRepository : IEntityRepository<DynamicPassword>
{
    public List<DynamicPassword> FetchAll()
    {
        throw new NotImplementedException();
    }

    public DynamicPassword FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public DynamicPassword Insert(DynamicPassword entity)
    {
        throw new NotImplementedException();
    }

    public DynamicPassword Update(DynamicPassword entity)
    {
        throw new NotImplementedException();
    }

    public DynamicPassword Delete(string id)
    {
        throw new NotImplementedException();
    }
}