using DataBase.Data.Abstractions;
using DataBase.Exceptions;

namespace DataBase.Data.Services;

public sealed class Database : IDataBase
{
    private readonly Dictionary<string, List<IDatabaseEntity>> _entities = new();
    
    public event EventHandler<EntityEventArgs> EntitySaved   = delegate { };
    public event EventHandler<EntityEventArgs> EntityUpdated = delegate { };
    public event EventHandler<EntityEventArgs> EntityDeleted = delegate { };
    
    
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
                throw new DataBaseException("Entity already exists.");
            }
        }
        else
        {
            _entities[typeName] = [entityCopy, ];
        }
        
        OnEntitySaved(new(){EntityType = typeof(TDatabaseEntity), Entity = DeepCopier.Copier.Copy(entityCopy),});
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
                OnEntityUpdated(new(){EntityType = typeof(TDatabaseEntity), Entity = DeepCopier.Copier.Copy(entityCopy),});
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
                throw new DataBaseException($"entity {entity.Id} of type {typeName} could not be removed or does not exist.");
            }
            
            OnEntityDeleted(new(){EntityType = typeof(TDatabaseEntity), Entity = DeepCopier.Copier.Copy(entity),});
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
            throw new DataBaseException("an entity with the same id already exists.");
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
