using MiniBank.Entities.Classes;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.ExistingEntityUpdate.Services;

public class AccountUpdater : IEntityUpdateFromDao< AccountDao,  Account>
{

    public void EntityUpdate(Account entity, AccountDao dao)
    {
        entity.Status = dao.Status;
        entity.Id = dao.Id;
        Helper.SetValue(entity, "AccountNumber",dao.AccountNumber);
        Helper.SetValue(entity, "UserRef", dao.UserRef);
        Helper.SetValue(entity, "Balance", dao.Balance);
    }
}