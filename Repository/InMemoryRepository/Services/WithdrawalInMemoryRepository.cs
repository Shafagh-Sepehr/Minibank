using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class WithdrawalInMemoryRepository : IEntityRepository<Withdrawal>
{
    public List<Withdrawal> FetchAll()
    {
        throw new NotImplementedException();
    }

    public Withdrawal Read(string id)
    {
        throw new NotImplementedException();
    }

    public Withdrawal Insert(Withdrawal entity)
    {
        throw new NotImplementedException();
    }

    public Withdrawal Update(Withdrawal entity)
    {
        throw new NotImplementedException();
    }

    public Withdrawal Delete(Withdrawal entity)
    {
        throw new NotImplementedException();
    }
}