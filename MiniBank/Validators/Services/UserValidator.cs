using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class UserValidator(IDataBase dataBase) : BaseValidator<User>, IUserValidator
{
    
    protected override void ValidateData(User entity)
    {
        var users = dataBase.FetchAll<User>();
        if (users.Any(x => x.Username == entity.Username || x.NationalId == entity.NationalId))
        {
            throw new ValidationException("Username is already taken");
        }
    }
}
