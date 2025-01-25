using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class CardInMemoryRepository : IEntityRepository<Card>
{
    public List<Card> FetchAll()
    {
        throw new NotImplementedException();
    }

    public Card? FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public void Insert(Card entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Card entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}