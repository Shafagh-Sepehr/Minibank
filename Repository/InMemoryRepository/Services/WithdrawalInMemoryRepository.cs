using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class WithdrawalInMemoryRepository : IEntityRepository<Withdrawal>
{
    public List<Withdrawal> FetchAll()
    {
        throw new NotImplementedException();
    }

    public Withdrawal FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public void Insert(Withdrawal entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Withdrawal entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}