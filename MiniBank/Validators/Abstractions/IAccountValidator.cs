using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Abstractions;

public interface IAccountValidator
{
    void Validate(Account entity);
}
