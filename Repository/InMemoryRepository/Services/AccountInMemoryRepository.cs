using MiniBank.Entities.Classes;
using Repository.Abstractions;

namespace Repository.InMemoryRepository.Services
{
    public class AccountInMemoryRepository : IEntityRepository<Account>
    {
        public List<Account> FetchAll()
        {
            throw new NotImplementedException();
        }

        public Account Read(string id)
        {
            throw new NotImplementedException();
        }

        public Account Insert(Account entity)
        {
            throw new NotImplementedException();
        }

        public Account Update(Account entity)
        {
            throw new NotImplementedException();
        }

        public Account Delete(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
