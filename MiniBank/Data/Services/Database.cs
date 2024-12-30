﻿using MiniBank.Data.Abstractions;
using MiniBank.Exceptions;

namespace MiniBank.Data.Services;

public class Database : IDataBase
{
    private readonly Dictionary<string, List<IDatabaseEntity>> _entities = new();
    
    
    public void Save<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        var typeName = typeof(TDatabaseEntity).Name;
        var entityCopy = DeepCopier.Copier.Copy(entity);
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            FixId(entityCopy, entityList);
            AssertIdIsUnique(entityCopy, entityList);
            
            if (!entityList.Contains(entityCopy))
            {
                entityList.Add(entityCopy);
            }
            else
            {
                throw new OperationFailedException("Entity already exists.");
            }
        }
        else
        {
            _entities[typeName] = [entityCopy, ];
        }
    }
    
    public void Update<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        var typeName = typeof(TDatabaseEntity).Name;
        var entityCopy = DeepCopier.Copier.Copy(entity);
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var entityId = entityList.FindIndex(x => x.Id == entityCopy.Id);
            if (entityId != -1)
            {
                entityList[entityId] = entityCopy;
                return;
            }
        }
        
        throw new InvalidOperationException($"entity {entityCopy.Id} of type {typeName} not found.");
    }
    
    
    public void Delete<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        var typeName = typeof(TDatabaseEntity).Name;
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var newEntity = entityList.Single(x=> x.Id == entity.Id);
            var result = entityList.Remove(newEntity);
            
            if (result == false)
            {
                throw new OperationFailedException($"entity {entity.Id} of type {typeName} could not be removed or does not exist.");
            }
        }
        else
        {
            throw new InvalidOperationException($"entity {entity.Id} of type {typeName} not found.");
        }
    }
    
    
    public IEnumerable<TDatabaseEntity> FetchAll<TDatabaseEntity>() where TDatabaseEntity : IDatabaseEntity
    {
        var typeName = typeof(TDatabaseEntity).Name;
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            return entityList.Where(x => x is TDatabaseEntity).Cast<TDatabaseEntity>();
        }
        else
        {
            return [];
        }
    }
    
    private static bool FixId<TDatabaseEntity>(TDatabaseEntity entityCopy, List<TDatabaseEntity> entityList) where TDatabaseEntity : IDatabaseEntity
    {
        if (entityCopy.Id != 0) return false;
        var biggestId = entityList.Select(x => x.Id).Last();
        entityCopy.Id = biggestId + 1;
        return true;
    }
    
    private static void AssertIdIsUnique<TDatabaseEntity>(TDatabaseEntity entityCopy, List<TDatabaseEntity> entityList) where TDatabaseEntity : IDatabaseEntity
    {
        if (entityList.Any(x => x.Id == entityCopy.Id))
        {
            throw new OperationFailedException("an entity with the same id already exists.");
        }
    }
}
