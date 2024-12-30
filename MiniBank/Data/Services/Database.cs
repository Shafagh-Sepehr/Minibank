using MiniBank.Data.Abstractions;
using MiniBank.Exceptions;

namespace MiniBank.Data.Services;

public sealed class Database : IDataBase
{
    private readonly Dictionary<string, SortedSet<IDatabaseEntity>> _entities = new();
    
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
            
            if (!entityList.Add(entityCopy))
            {
                throw new OperationFailedException("Entity already exists.");
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
            var entityId = entityList.Contains(entityCopy);
            if (entityId == true)
            {
                entityList.Remove(entityCopy); // remove old entity 
                entityList.Add(entityCopy); // add new entity
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
            var result = entityList.Remove(entity);
            
            if (result == false)
            {
                throw new OperationFailedException($"entity {entity.Id} of type {typeName} could not be removed or does not exist.");
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
    
    private static bool FixId<TDatabaseEntity>(TDatabaseEntity entityCopy, SortedSet<TDatabaseEntity> entityList) where TDatabaseEntity : IDatabaseEntity
    {
        if (entityCopy.Id != 0)
        {
            return false;
        }
        else
        {
            var biggestId = entityList.Last().Id;
            entityCopy.Id = biggestId + 1;
            return true;
        }
    }
    
    private static void AssertIdIsUnique<TDatabaseEntity>(TDatabaseEntity entityCopy, SortedSet<TDatabaseEntity> entityList) where TDatabaseEntity : IDatabaseEntity
    {
        if (entityList.Contains(entityCopy))
        {
            throw new OperationFailedException("an entity with the same id already exists.");
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
