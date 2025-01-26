using System.ComponentModel.DataAnnotations;
using Abstractions.Repository;
using Microsoft.Extensions.DependencyInjection;
using MiniBank.Entities.Enums;

namespace MiniBank.Validators.Abstractions;

public abstract class BaseValidator<TEntity> : IValidator<TEntity> where TEntity : DatabaseEntity
{
    public void Validate(TEntity entity, DatabaseAction databaseAction)
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
        }


        if (databaseAction == DatabaseAction.Insert)
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

    public void ValidateDelete(string id)
    {
        ValidateThatEntityExists_BeforeDelete(id);
        ValidateDeleteState(id);
    }

    private static void ValidateIdIsNotSet_BeforeSave(TEntity entity, List<string> errors)
    {
        if (entity.Id != string.Empty)
        {
            errors.Add("Id must not be set when creating a new entity");
        }
    }

    private static void ValidateThatEntityExists_BeforeUpdate(TEntity entity)
    {
        var repoWrapper = ServiceCollection.ServiceProvider.GetRequiredService<IRepositoryWrapperValidator>();
        if (repoWrapper.FetchById<TEntity>(entity.Id) == null)
        {
            throw new ValidationException($"cannot update non-existing {entity.GetType().Name}");
        }
    }

    private static void ValidateThatEntityExists_BeforeDelete(string id)
    {
        var repoWrapper = ServiceCollection.ServiceProvider.GetRequiredService<IRepositoryWrapperValidator>();
        if (repoWrapper.FetchById<TEntity>(id) == null)
        {
            throw new ValidationException($"{typeof(TEntity).Name} with id {id} doesn't exists, can't delete");
        }
    }


    protected virtual void ValidateGeneralState(TEntity entity, List<string> errors) { }
    protected virtual void ValidateSaveState(TEntity entity, List<string> errors) { }
    protected virtual void ValidateUpdateState(TEntity entity, List<string> errors) { }
    protected virtual void ValidateDeleteState(string id) { }
}