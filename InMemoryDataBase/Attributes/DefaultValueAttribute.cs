namespace InMemoryDataBase.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DefaultValueAttribute<T>(T defaultValue) : Attribute
{
    public T DefaultValue { get; } = defaultValue;
}
