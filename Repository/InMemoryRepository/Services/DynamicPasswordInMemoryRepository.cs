using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class DynamicPasswordInMemoryRepository : IEntityRepository<DynamicPassword>
{
    public List<DynamicPassword> FetchAll()
    {
        throw new NotImplementedException();
    }

    public DynamicPassword? FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public void Insert(DynamicPassword entity)
    {
        throw new NotImplementedException();
    }

    public void Update(DynamicPassword entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}