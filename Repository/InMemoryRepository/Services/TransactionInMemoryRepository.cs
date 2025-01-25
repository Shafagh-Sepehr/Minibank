using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class TransactionInMemoryRepository : IEntityRepository<Transaction>
{
    public List<Transaction> FetchAll()
    {
        throw new NotImplementedException();
    }

    public Transaction Read(string id)
    {
        throw new NotImplementedException();
    }

    public Transaction Insert(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Transaction Update(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Transaction Delete(Transaction entity)
    {
        throw new NotImplementedException();
    }
}