using System.Data;
using Abstractions.Repository;
using MiniBank.Attributes;
using MiniBank.Entities.Enums;
using MiniBank.Exceptions;
using MiniBank.Validators.Abstractions;

namespace MiniBank.Validators.Services;

public class RepositoryWrapperValidator(IRepository repository) : IRepositoryWrapperValidator
{
    public List<T> FetchAll<T>() where T : DatabaseEntity => repository.FetchAll<T>();

    public T? FetchById<T>(string id) where T : DatabaseEntity => repository.FetchById<T>(id);

    public void Insert<T>(T entity) where T : DatabaseEntity
    {
        Validate(entity, DatabaseAction.Insert);
        repository.Insert(entity);
    }

    public void Update<T>(T entity) where T : DatabaseEntity
    {
        Validate(entity, DatabaseAction.Update);
        repository.Update(entity);
    }

    public void Delete<T>(string id) where T : DatabaseEntity
    {
        ValidateDelete<T>(id);
        repository.Delete<T>(id);
    }

    private static void Validate<T>(T entity, DatabaseAction databaseAction) where T : DatabaseEntity
    {
        if (Attribute.GetCustomAttribute(typeof(T), typeof(ValidatorAttribute)) is ValidatorAttribute validatorBase)
        {
            if (validatorBase.Validator is IValidator<T> validator)
            {
                validator.Validate(entity, databaseAction);
            }
            else
            {
                throw new OperationFailedException(
                    $"validator type and entity type don't match. " +
                    $"{validatorBase.Validator.GetType().Name} was used on {entity.GetType().Name} entity.");
            }
        }
        else
        {
            throw new DataException($"wrong attribute was used on {entity.GetType().Name} entity.");
        }
    }

    private static void ValidateDelete<T>(string id) where T : DatabaseEntity
    {
        if (Attribute.GetCustomAttribute(typeof(T), typeof(ValidatorAttribute)) is ValidatorAttribute validatorBase)
        {
            if (validatorBase.Validator is IValidator<T> validator)
            {
                validator.ValidateDelete(id);
            }
            else
            {
                throw new OperationFailedException(
                    $"validator type and entity type don't match. " +
                    $"{validatorBase.Validator.GetType().Name} was used on {typeof(T).Name} entity.");
            }
        }
        else
        {
            throw new DataException($"wrong attribute was used on {typeof(T).Name} entity.");
        }
    }
}