using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using DB.Entities.Enums;
using DB.Validators.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MiniBank.Validators;

public abstract class BaseValidator<TEntity> : IValidator<TEntity> where TEntity : IDatabaseEntity
{
    public void Validate(TEntity entity, DataBaseAction dataBaseAction)
    {
        if (dataBaseAction == DataBaseAction.Delete)
        {
            ValidateDeleteState(entity);
            return;
        }
        
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(entity, null, null);
        
        // Validate the user object
        var isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);
        
        // Collect error messages if the object is not valid
        var errors = new List<string>();
        if (!isValid)
        {
            foreach (var validationResult in validationResults)
            {
                if (validationResult.ErrorMessage != null)
                {
                    errors.Add(validationResult.ErrorMessage);
                }
            }
        }
        
        
        if (dataBaseAction == DataBaseAction.Save)
        {
            ValidateIdIsNotSet_BeforeSave(entity, errors);
            ValidateGeneralState(entity, errors);
            ValidateSaveState(entity, errors);
        }
        else
        {
            ValidateThatEntityExists_BeforeUpdate(entity);
            ValidateGeneralState(entity, errors);
            ValidateUpdateState(entity, errors);
        }
        
        if (errors.Count > 0)
        {
            throw new ValidationException(string.Join("|", errors));
        }
    }
    
    private static void ValidateThatEntityExists_BeforeUpdate(TEntity entity)
    {
        var dataBase = ServiceCollection.ServiceProvider.GetRequiredService<IDataBase>();
        var ent = dataBase.FetchAll<TEntity>().FirstOrDefault(x => x.Id == entity.Id);
        if (ent == null)
        {
            throw new ValidationException($"cannot update non-existing {entity.GetType().Name}");
        }
    }
    
    private static void ValidateIdIsNotSet_BeforeSave(TEntity entity, List<string> errors)
    {
        if (entity.Id != 0)
        {
            errors.Add("Id must not be set when creating a new entity");
        }
    }
    
    protected virtual void ValidateGeneralState(TEntity entity, List<string> errors) { }
    protected virtual void ValidateSaveState(TEntity entity, List<string> errors) { }
    protected virtual void ValidateUpdateState(TEntity entity, List<string> errors) { }
    protected virtual void ValidateDeleteState(TEntity entity) { }
    
}