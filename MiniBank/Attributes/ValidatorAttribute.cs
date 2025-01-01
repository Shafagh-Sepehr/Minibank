using DB.Validators.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MiniBank.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ValidatorAttribute : Attribute
{
    public ValidatorAttribute(Type validatorType, Type modelType)
    {
        var validatorObj = ServiceCollection.ServiceProvider.GetRequiredService(validatorType);
        
        if (validatorObj.GetType().IsAssignableTo(typeof(IValidator<>).MakeGenericType(modelType)))
        {
            Validator = validatorObj;
        }
        else
        {
            throw new ArgumentException($"validator type and provided entity type to attribute don't match. {validatorObj.GetType().Name} was used on {modelType.Name} entity.");
        }
    }
    
    public object Validator { get; set; }
}
