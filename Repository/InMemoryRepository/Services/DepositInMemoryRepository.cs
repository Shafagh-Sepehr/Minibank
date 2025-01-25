using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class DepositInMemoryRepository : IEntityRepository<Deposit>
{
    public List<Deposit> FetchAll()
    {
        throw new NotImplementedException();
    }

    public Deposit? FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public void Insert(Deposit entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Deposit entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}