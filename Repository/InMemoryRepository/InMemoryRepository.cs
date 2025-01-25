using Abstractions.Repository;

namespace Repository.InMemoryRepository
{
    public class InMemoryRepository : IRepository
    {
        public List<T> FetchAll<T>() where T : DataBaseEntity
        {
            throw new NotImplementedException();
        }

        public T Read<T>(string id) where T : DataBaseEntity
        {
            throw new NotImplementedException();
        }

        public T Insert<T>(T entity) where T : DataBaseEntity
        {
            throw new NotImplementedException();
        }

        public T Update<T>(T entity) where T : DataBaseEntity
        {
            throw new NotImplementedException();
        }

        public T Delete<T>(T entity) where T : DataBaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
