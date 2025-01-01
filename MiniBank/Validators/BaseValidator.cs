using System.ComponentModel.DataAnnotations;
using DB.Data.Abstractions;
using DB.Validators.Abstractions;

namespace MiniBank.Validators;

public abstract class BaseValidator<TEntity> : IValidator<TEntity>  where TEntity : IDatabaseEntity{
    public void Validate(TEntity entity)
    {
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
            
            if (errors.Count > 0)
            {
                throw new ValidationException(string.Join(", ", errors));
            }
        }
        
        ValidateData(entity);
    }
    
    protected abstract void ValidateData(TEntity entity);
}
