using Abstractions.InMemoryDatabase;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class DynamicPasswordToDao : IEntityToDao<DynamicPassword, DynamicPasswordDao>
{
    public DynamicPasswordDao Convert(DynamicPassword entity)
    {
        var dao = new DynamicPasswordDao
        {
            Id = entity.Id,
            Amount = entity.Amount,
            DestinationCardNumber = entity.DestinationCardNumber,
            DynamicPasswordHash = entity.DynamicPasswordHash,
            ExpiryDate = entity.ExpiryDate,
            OriginCardNumber = entity.OriginCardNumber
        };
        ((IVersionable)dao).Version = ((IVersionable)entity).Version;
        return dao;
    }
}