using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class AccountInMemoryRepository : IEntityRepository<Account>
{
    public List<Account> FetchAll()
    {
        throw new NotImplementedException();
    }

    public Account? FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public void Insert(Account entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Account entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}