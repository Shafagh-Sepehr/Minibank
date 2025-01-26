using MiniBank.Entities.Enums;
using Abstractions.Repository;

namespace MiniBank.Validators.Abstractions;

public interface IValidator<in TEntity> where TEntity : DataBaseEntity
{
    public void Validate(TEntity entity, DatabaseAction databaseAction);
}
