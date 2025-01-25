using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class TransactionInMemoryRepository : IEntityRepository<Transaction>
{
    public List<Transaction> FetchAll()
    {
        throw new NotImplementedException();
    }

    public Transaction? FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public void Insert(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}