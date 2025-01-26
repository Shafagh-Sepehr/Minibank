using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class UserValidator(IRepositoryWrapperValidator repoWrapper) : BaseValidator<User>
{
    protected override void ValidateSaveState(User entity, List<string> errors)
    {
        var users = repoWrapper.FetchAll<User>();
        if (users.Any(x => x.Username == entity.Username || x.NationalId == entity.NationalId))
        {
            errors.Add("Username is already taken");
        }
    }
    
    protected override void ValidateUpdateState(User entity, List<string> errors)
    {
        var user = repoWrapper.FetchById<User>(entity.Id);
        if (entity.FirstName != user!.FirstName || entity.LastName != user.LastName || entity.NationalId != user.NationalId)
        {
            errors.Add("First name, last name and national id cannot be change");
        }
    }
    
    protected override void ValidateDeleteState(User entity)
    {
        var account = repoWrapper.FetchById<Account>(entity.Id);
        if (account != null)
        {
            throw new ValidationException("Can't delete user, first deleted owned accounts");
        }
    }
}
