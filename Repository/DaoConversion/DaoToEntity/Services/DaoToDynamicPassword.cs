using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.DaoToEntity.Services;

public class DaoToDynamicPassword : IDaoToEntity<DynamicPasswordDao, DynamicPassword>
{
    public DynamicPassword Convert(DynamicPasswordDao dao)
    {
        throw new NotImplementedException();
    }
}