namespace InMemoryDataBase.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ForeignKeyAttribute(Type referenceType, string propertyName) : Attribute
{
    public Type   ReferenceType  { get; } = referenceType;
    public string PropertyName { get; } = propertyName;
}
