using System.ComponentModel.DataAnnotations;
using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Services;

public class AccountValidator : BaseValidator<Account>
{
    protected override void ValidateData(Account entity)
    {
        if (entity.Balance < 0)
        {
            throw new ValidationException("Account balance cannot be less than zero");
        }
    }
}
