using System.ComponentModel.DataAnnotations;
using Abstractions.Repository;
using MiniBank.Entities.Classes;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class UserValidator(IRepository repository) : BaseValidator<User>
{
    protected override void ValidateSaveState(User entity, List<string> errors)
    {
        var users = repository.FetchAll<User>();
        if (users.Any(x => x.Username == entity.Username || x.NationalId == entity.NationalId))
        {
            errors.Add("Username is already taken");
        }
    }
    
    protected override void ValidateUpdateState(User entity, List<string> errors)
    {
        var user = repository.FetchAll<User>().FirstOrDefault(x => x.Id == entity.Id);
        if (entity.FirstName != user!.FirstName || entity.LastName != user.LastName || entity.NationalId != user.NationalId)
        {
            errors.Add("First name, last name and national id cannot be change");
        }
    }
    
    protected override void ValidateDeleteState(User entity)
    {
        var accounts = repository.FetchAll<Account>();
        if (accounts.Any(x => x.UserRef == entity.Id))
        {
            throw new ValidationException("Can't delete user, first deleted owned accounts");
        }
    }
}
