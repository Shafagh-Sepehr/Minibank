using Abstractions.InMemoryDatabase;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.ExistingEntityUpdate.Services;

public class AccountUpdater : IEntityUpdateFromDao< AccountDao,  Account>
{

    public void Update(Account entity, AccountDao dao)
    {
        entity.Status = dao.Status;
        entity.Id = dao.Id;
        Helper.SetValue(entity, nameof(dao.AccountNumber),dao.AccountNumber);
        Helper.SetValue(entity, nameof(dao.UserRef), dao.UserRef);
        Helper.SetValue(entity, "_balance", dao.Balance);
        ((IVersionable)entity).Version = ((IVersionable)dao).Version;
    }
}