using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToAccount : IDaoToEntity< AccountDao,  Account>
{
    public Account Convert(AccountDao dao)
    {
        var entity = new Account
        {
            AccountNumber = dao.AccountNumber,
            Status = dao.Status,
            UserRef = dao.UserRef,
            Id = dao.Id,
        };
        entity.IncreaseBalance(dao.Balance);
        ((IMiniBankVersionable)entity).Version = ((IInMemoryDBVersionable)dao).Version;
        return entity;
    }
}