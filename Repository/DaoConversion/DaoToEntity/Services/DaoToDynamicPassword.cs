using Abstractions.InMemoryDatabase;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToDynamicPassword : IDaoToEntity<DynamicPasswordDao, DynamicPassword>
{
    public DynamicPassword Convert(DynamicPasswordDao dao)
    {
        var entity = new DynamicPassword
        {
            Amount = dao.Amount,
            DestinationCardNumber = dao.DestinationCardNumber,
            DynamicPasswordHash = dao.DynamicPasswordHash,
            OriginCardNumber = dao.OriginCardNumber,
            Id = dao.Id,
        };
        Helper.SetValue(entity, nameof(entity.ExpiryDate), dao.ExpiryDate);
        ((IVersionable)entity).Version = ((IVersionable)dao).Version;
        return entity;
    }
}