using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.ExistingEntityUpdate.Services;

public class WithdrawalUpdater : IEntityUpdateFromDao<WithdrawalDao, Withdrawal>
{

    public void Update(Withdrawal entity, WithdrawalDao dao)
    {
        entity.Id = dao.Id;
        Helper.SetValue(entity, nameof(entity.Date), dao.Date);
        Helper.SetValue(entity, nameof(entity.Status), dao.Status);
        Helper.SetValue(entity, nameof(entity.AccountRef), dao.AccountRef);
        Helper.SetValue(entity, nameof(entity.Amount), dao.Amount);
        ((IMiniBankVersionable)entity).Version = ((IInMemoryDBVersionable)dao).Version;
    }
}