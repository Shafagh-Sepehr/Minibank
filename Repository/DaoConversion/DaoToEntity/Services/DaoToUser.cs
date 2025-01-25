using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToUser : IDaoToEntity<UserDao, User>
{
    public User DaoToEntity(UserDao dao)
    {
        return new User
        {
            Id = dao.Id,
            FirstName = dao.FirstName,
            LastName = dao.LastName,
            NationalId = dao.NationalId,
            PasswordHash = dao.PasswordHash,
            PhoneNumber = dao.PhoneNumber,
            Username = dao.Username
        };
    }
}