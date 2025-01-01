using DB.Data.Abstractions;
using DB.Entities.Enums;

namespace DB.Validators.Abstractions;

public interface IValidator<in TEntity> where TEntity : IDatabaseEntity
{
    public void Validate(TEntity entity, DataBaseAction dataBaseAction);
}
