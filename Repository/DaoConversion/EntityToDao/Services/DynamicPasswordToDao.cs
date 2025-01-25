using MiniBank.Entities.Classes;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.DaoConversion.EntityToDao.Services;

public class DynamicPasswordToDao : IEntityToDao<DynamicPassword, DynamicPasswordDao>
{
    public DynamicPasswordDao Convert(DynamicPassword dao)
    {
        throw new NotImplementedException();
    }
}