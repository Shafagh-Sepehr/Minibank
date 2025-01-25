using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.ExistingEntityUpdate.Services;

public class DepositUpdater : IEntityUpdateFromDao<DepositDao, Deposit>
{
    public void EntityUpdate(Deposit entity, DepositDao dao)
    {
        entity.Id = dao.Id;
        Helper.SetValue(entity, nameof(entity.Date), dao.Date);
        Helper.SetValue(entity, nameof(entity.Amount), dao.Amount);
        Helper.SetValue(entity, nameof(entity.AccountRef), dao.AccountRef);
        Helper.SetValue(entity, nameof(entity.Status), dao.Status);
    }
}