using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class UserToDao : IEntityToDao<User, UserDao>
{
    public UserDao EntityToDao(User entity)
    {
        return new UserDao
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            NationalId = entity.NationalId,
            PasswordHash = entity.PasswordHash,
            PhoneNumber = entity.PhoneNumber,
            Username = entity.Username
        };
    }
}