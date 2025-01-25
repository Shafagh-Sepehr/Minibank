using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class DepositInMemoryRepository(IShafaghDB shafaghDB, IEntityToDao<Deposit, DepositDao> entityToDao, IDaoToEntity<DepositDao, Deposit> daoToEntity) : GeneralInMemoryRepository<Deposit, DepositDao>(shafaghDB, entityToDao, daoToEntity)
{
}