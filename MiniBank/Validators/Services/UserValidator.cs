using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class UserValidator(IDataBase dataBase) : BaseValidator<User>
{
    protected override void ValidateGeneralState(User entity, List<string> errors)
    {
        
    }
    protected override void ValidateSaveState(User entity, List<string> errors)
    {
        var users = dataBase.FetchAll<User>();
        if (users.Any(x => x.Username == entity.Username || x.NationalId == entity.NationalId))
        {
            errors.Add("Username is already taken");
        }
    }
    protected override void ValidateUpdateState(User entity, List<string> errors)
    {
        var user = dataBase.FetchAll<User>().FirstOrDefault(x => x.Id == entity.Id);
        if (entity.FirstName != user!.FirstName || entity.LastName != user.LastName || entity.NationalId != user.NationalId)
        {
            errors.Add("First name, last name and national id cannot be change");
        }
    }
    protected override void ValidateDeleteState(User entity)
    {
        
    }
}
