﻿using System.Data;
using DB.Data.Abstractions;
using DB.Entities.Enums;
using DB.Exceptions;
using DB.Validators.Abstractions;
using DeepCopier;

namespace DB.Data.Services;

public sealed class DataBase : IDataBase
{
    private readonly Dictionary<string, List<IDatabaseEntity>> _entities  = new();
    private readonly Dictionary<string, long>                  _entityIds = new();
    
    public event EventHandler<EntityEventArgs> EntitySaved   = delegate { };
    public event EventHandler<EntityEventArgs> EntityUpdated = delegate { };
    public event EventHandler<EntityEventArgs> EntityDeleted = delegate { };
    
    
    public void Save<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        Validate(entity, DataBaseAction.Save);
        
        var typeName = typeof(TDatabaseEntity).Name;
        var entityCopy = Copier.Copy(entity);
        
        SetId(entityCopy, typeName);
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            entityList.Add(entityCopy);
        }
        else
        {
            _entities[typeName] = [entityCopy,];
        }
        
        OnEntitySaved(new() { EntityType = typeof(TDatabaseEntity), Entity = entity, });
    }
    
    public void Update<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        Validate(entity, DataBaseAction.Update);
        
        var typeName = typeof(TDatabaseEntity).Name;
        var entityCopy = Copier.Copy(entity);
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var entityId = entityList.FindIndex(x => x.Id == entityCopy.Id); // entityId will always have a valid value
            entityList[entityId] = entityCopy;
            OnEntityUpdated(new() { EntityType = typeof(TDatabaseEntity), Entity = entity, });
        }
        else
        {
            throw new InvalidOperationException($"entity {entityCopy.Id} of type {typeName} not found.");
        }
    }
    
    
    public void Delete<TDatabaseEntity>(TDatabaseEntity entity) where TDatabaseEntity : IDatabaseEntity
    {
        Validate(entity, DataBaseAction.Delete);
        
        var typeName = typeof(TDatabaseEntity).Name;
        
        if (_entities.TryGetValue(typeName, out var entityList))
        {
            var newEntity = entityList.Single(x => x.Id == entity.Id); // entityId will always have a valid value
            entityList.Remove(newEntity);
            
            OnEntityDeleted(new() { EntityType = typeof(TDatabaseEntity), Entity = entity, });
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
    
    private void SetId<TDatabaseEntity>(TDatabaseEntity entityCopy, string typeName) where TDatabaseEntity : IDatabaseEntity
    {
        if (_entityIds.TryGetValue(typeName, out var lastId))
        {
            entityCopy.Id = lastId;
            _entityIds[typeName]++;
        }
        else
        {
            entityCopy.Id = 1;
            _entityIds[typeName] = 2;
        }
    }
    
    private static void Validate<TDataBaseEntity>(TDataBaseEntity entity, DataBaseAction dataBaseAction) where TDataBaseEntity : IDatabaseEntity
    {
        if (Attribute.GetCustomAttribute(typeof(TDataBaseEntity), typeof(ValidatorAttributeBase)) is ValidatorAttributeBase validatorBase)
        {
            if (validatorBase.Validator is IValidator<TDataBaseEntity> validator)
            {
                validator.Validate(entity, dataBaseAction);
            }
            else
            {
                throw new DataBaseException(
                    $"validator type and entity type don't match. " +
                    $"{validatorBase.Validator.GetType().Name} was used on {entity.GetType().Name} entity.");
            }
        }
        else
        {
            throw new DataException($"wrong attribute was used on {entity.GetType().Name} entity.");
        }
    }
    
    private void OnEntitySaved(EntityEventArgs e)
    {
        EntitySaved(this, e);
    }
    
    private void OnEntityUpdated(EntityEventArgs e)
    {
        EntityUpdated(this, e);
    }
    
    private void OnEntityDeleted(EntityEventArgs e)
    {
        EntityDeleted(this, e);
    }
}
