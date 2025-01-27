using Abstractions.InMemoryDatabase;
using Abstractions.MiniBank;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.ExistingEntityUpdate.Services;

public class UserUpdater : IEntityUpdateFromDao<UserDao, User>
{
    public void Update(User entity, UserDao dao)
    {
        entity.Id = dao.Id;
        entity.Username = dao.Username;
        entity.PasswordHash = dao.PasswordHash;
        Helper.SetValue(entity,nameof(entity.FirstName),dao.FirstName);
        Helper.SetValue(entity,nameof(entity.LastName),dao.LastName);
        Helper.SetValue(entity,nameof(entity.NationalId),dao.NationalId);
        Helper.SetValue(entity,nameof(entity.PhoneNumber),dao.PhoneNumber);
        ((IMiniBankVersionable)entity).Version = ((IInMemoryDBVersionable)dao).Version;
    }
}