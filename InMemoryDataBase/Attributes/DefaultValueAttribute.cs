namespace InMemoryDataBase.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DefaultValueAttribute(object defaultValue) : Attribute
{
    public object DefaultValue { get; } = defaultValue;
}
