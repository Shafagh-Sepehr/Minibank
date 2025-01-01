using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class UserValidator(IDataBase dataBase) : BaseValidator<User>
{
    protected override void ValidateGeneralState(User entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    protected override void ValidateSaveState(User entity, List<string> errors)
    {
        var users = dataBase.FetchAll<User>();
        if (users.Any(x => x.Username == entity.Username || x.NationalId == entity.NationalId))
        {
            throw new ValidationException("Username is already taken");
        }
    }
    protected override void ValidateUpdateState(User entity, List<string> errors)
    {
        throw new NotImplementedException();
    }
    protected override void ValidateDeleteState(User entity)
    {
        throw new NotImplementedException();
    }
}
