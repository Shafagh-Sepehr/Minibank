using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.ExistingEntityUpdate.Services;

public class TransactionUpdater : IEntityUpdateFromDao<TransactionDao, Transaction>
{
    public void EntityUpdate(Transaction entity, TransactionDao dao)
    {
        entity.Id = dao.Id;
        Helper.SetValue(entity, nameof(entity.Date), dao.Date);
        Helper.SetValue(entity, nameof(entity.Amount), dao.Amount);
        Helper.SetValue(entity, nameof(entity.DestinationAccountNumber), dao.DestinationAccountNumber);
        Helper.SetValue(entity, nameof(entity.DestinationAccountRef), dao.DestinationAccountRef);
        Helper.SetValue(entity, nameof(entity.OriginAccountNumber), dao.OriginAccountNumber);
        Helper.SetValue(entity, nameof(entity.OriginAccountRef), dao.OriginAccountRef);
        Helper.SetValue(entity, nameof(entity.Status), dao.Status);
        Helper.SetValue(entity, nameof(entity.Type), dao.Type);
        Helper.SetValue(entity, nameof(entity.Description), dao.Description);
    }
}