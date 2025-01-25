using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToUser : IDaoToEntity<UserDao, User>
{
    public User Convert(UserDao dao)
    {
        throw new NotImplementedException();
    }
}