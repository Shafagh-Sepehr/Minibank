using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class UserInMemoryRepository : IEntityRepository<User>
{
    public List<User> FetchAll()
    {
        throw new NotImplementedException();
    }

    public User FetchById(string id)
    {
        throw new NotImplementedException();
    }

    public void Insert(User entity)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}