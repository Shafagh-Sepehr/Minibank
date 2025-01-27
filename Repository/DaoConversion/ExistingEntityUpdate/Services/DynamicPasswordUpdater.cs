using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.ExistingEntityUpdate.Services;

public class DynamicPasswordUpdater : IEntityUpdateFromDao<DynamicPasswordDao, DynamicPassword>
{
    public void Update(DynamicPassword entity, DynamicPasswordDao dao)
    {
        entity.Id = dao.Id;
        Helper.SetValue(entity, nameof(entity.ExpiryDate), dao.ExpiryDate);
        Helper.SetValue(entity, nameof(entity.Amount), dao.Amount);
        Helper.SetValue(entity, nameof(entity.DestinationCardNumber), dao.DestinationCardNumber);
        Helper.SetValue(entity, nameof(entity.DynamicPasswordHash), dao.DynamicPasswordHash);
        Helper.SetValue(entity, nameof(entity.OriginCardNumber), dao.OriginCardNumber);
        ((IMiniBankVersionable)entity).Version = ((IInMemoryDBVersionable)dao).Version;
    }
}