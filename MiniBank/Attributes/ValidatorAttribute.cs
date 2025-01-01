namespace MiniBank.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ValidatorAttribute(Type validatorType) : Attribute
{
    public Type ValidatorType { get; } = validatorType;
}
