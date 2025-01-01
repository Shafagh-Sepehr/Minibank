namespace DB.Data.Abstractions;

public abstract class ValidatorAttributeBase : Attribute
{
    public abstract object Validator { get; set; }
}