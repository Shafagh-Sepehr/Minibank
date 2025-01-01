using MiniBank.Entities.Classes;

namespace MiniBank.Validators.Abstractions;

public interface ICardValidator
{
    void Validate(Card entity);
}
