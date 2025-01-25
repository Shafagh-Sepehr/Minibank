using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class DynamicPasswordToDao : IEntityToDao<DynamicPassword, DynamicPasswordDao>
{
    public DynamicPasswordDao EntityToData(DynamicPassword entity)
    {
        return new DynamicPasswordDao
        {
            Id = entity.Id,
            Amount = entity.Amount,
            DestinationCardNumber = entity.DestinationCardNumber,
            DynamicPasswordHash = entity.DynamicPasswordHash,
            ExpiryDate = entity.ExpiryDate,
            OriginCardNumber = entity.OriginCardNumber
        };
    }
}