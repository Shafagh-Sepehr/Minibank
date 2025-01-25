using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services;

public class UserInMemoryRepository : IEntityRepository<User>
{
    public List<User> FetchAll()
    {
        throw new NotImplementedException();
    }

    public User Read(string id)
    {
        throw new NotImplementedException();
    }

    public User Insert(User entity)
    {
        throw new NotImplementedException();
    }

    public User Update(User entity)
    {
        throw new NotImplementedException();
    }

    public User Delete(User entity)
    {
        throw new NotImplementedException();
    }
}