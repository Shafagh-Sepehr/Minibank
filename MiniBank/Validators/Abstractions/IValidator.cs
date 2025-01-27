using Abstractions.MiniBank;
using MiniBank.Entities.Enums;

namespace MiniBank.Validators.Abstractions;

public interface IValidator<in TEntity> where TEntity : MiniBankDatabaseEntity
{
    public void Validate(TEntity entity, DatabaseAction databaseAction);
    public void ValidateDelete(string id);
}
