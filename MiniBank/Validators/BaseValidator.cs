using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using DB.Entities.Enums;
using DB.Validators.Abstractions;

namespace MiniBank.Validators;

public abstract class BaseValidator<TEntity> : IValidator<TEntity>  where TEntity : IDatabaseEntity{
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
        
        ValidateGeneralState(entity, errors);
        
        if (dataBaseAction == DataBaseAction.Save)
        {
            ValidateSaveState(entity, errors);
        }
        else
        {
            ValidateUpdateState(entity, errors);
        }
        
        if (errors.Count > 0)
        {
            throw new ValidationException(string.Join("|", errors));
        }
    }
    
    protected abstract void ValidateGeneralState(TEntity entity, List<string> errors);
    protected abstract void ValidateSaveState(TEntity entity, List<string> errors);
    protected abstract void ValidateUpdateState(TEntity entity, List<string> errors);
    protected abstract void ValidateDeleteState(TEntity entity);
}
