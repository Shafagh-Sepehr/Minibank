using DB.Data.Abstractions;

namespace DB.Validators.Abstractions;

public interface IValidator<TEntity> where TEntity : IDatabaseEntity
{
    void Validate(TEntity entity);
}
