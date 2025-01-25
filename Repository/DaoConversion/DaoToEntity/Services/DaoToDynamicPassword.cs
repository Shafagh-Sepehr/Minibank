using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToDynamicPassword : IDaoToEntity<DynamicPasswordDao, DynamicPassword>
{
    public DynamicPassword DaoToEntity(DynamicPasswordDao dao)
    {
        var newEntity = new DynamicPassword
        {
            Amount = dao.Amount,
            DestinationCardNumber = dao.DestinationCardNumber,
            DynamicPasswordHash = dao.DynamicPasswordHash,
            OriginCardNumber = dao.OriginCardNumber,
            Id = dao.Id,
        };
        Helper.SetValue(newEntity, nameof(newEntity.ExpiryDate), dao.ExpiryDate);
        return newEntity;
    }
}