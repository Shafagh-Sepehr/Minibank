using InMemoryDataBase.Core.Abstractions;
using MiniBank.Entities.Classes;
using Repository.DaoConversion.DaoToEntity.Abstractions;
using Repository.DaoConversion.EntityToDao.Abstractions;
using Repository.DaoConversion.ExistingEntityUpdate.Abstractions;
using Repository.Data;

namespace Repository.InMemoryRepository.Services;

public class CardInMemoryRepository(
    IShafaghDB shafaghDB,
    IEntityToDao<Card, CardDao> entityToDao,
    IDaoToEntity<CardDao, Card> daoToEntity,
    IEntityUpdateFromDao<CardDao, Card> entityUpdater
    ) : GeneralInMemoryRepository<Card, CardDao>(shafaghDB, entityToDao, daoToEntity, entityUpdater)
{
}