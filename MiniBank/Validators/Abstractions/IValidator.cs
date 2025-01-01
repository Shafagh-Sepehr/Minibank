using DB.Data.Abstractions;

namespace MiniBank.Validators.Abstractions;

internal interface IValidator<TEntity> where TEntity : IDatabaseEntity
{
    void Validate(TEntity entity);
}
