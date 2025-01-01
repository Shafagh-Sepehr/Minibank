using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Abstractions;

public interface IUserValidator
{
    void Validate(User entity);
}
