using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.ExistingEntityUpdate.Services;

public class CardUpdater : IEntityUpdateFromDao<CardDao, Card>
{
    public void Update(Card entity, CardDao dao)
    {
        entity.Id = dao.Id;
        Helper.SetValue(entity, "_passwordHash", dao.Password);
        Helper.SetValue(entity, "_secondPasswordHash", dao.SecondPassword);
        Helper.SetValue(entity, nameof(dao.CardNumber), dao.CardNumber);
        Helper.SetValue(entity, nameof(dao.AccountRef), dao.AccountRef);
        Helper.SetValue(entity, nameof(dao.Cvv2), dao.Cvv2);
        Helper.SetValue(entity, nameof(dao.ExpiryDate), dao.ExpiryDate);
        ((IMiniBankVersionable)entity).Version = ((IInMemoryDBVersionable)dao).Version;
    }
}