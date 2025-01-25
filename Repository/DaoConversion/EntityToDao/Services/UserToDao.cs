using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class UserToDao : IEntityToDao<User, UserDao>
{
    public UserDao Convert(User dao)
    {
        throw new NotImplementedException();
    }
}