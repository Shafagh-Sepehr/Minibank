using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class CardInMemoryRepository : IEntityRepository<Card>
{
    public List<Card> FetchAll()
    {
        throw new NotImplementedException();
    }

    public Card FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public Card Insert(Card entity)
    {
        throw new NotImplementedException();
    }

    public Card Update(Card entity)
    {
        throw new NotImplementedException();
    }

    public Card Delete(string id)
    {
        throw new NotImplementedException();
    }
}