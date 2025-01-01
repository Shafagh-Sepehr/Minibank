using System.ComponentModel.DataAnnotations;
using DataBase.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class UserValidator(IDataBase dataBase) : BaseValidator<User> {
    
    protected override void ValidateData(User entity)
    {
        var users = dataBase.FetchAll<User>();
        if (users.Any(x => x.Username == entity.Username || x.NationalId == entity.NationalId))
        {
            throw new ValidationException("Username is already taken");
        }
    }
}
