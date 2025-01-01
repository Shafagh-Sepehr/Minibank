using DB.Data.Abstractions;

namespace DB.Validators.Abstractions;

public interface IValidator<TEntity> where TEntity : IDatabaseEntity
{
    public void Validate(TEntity entity);
}
