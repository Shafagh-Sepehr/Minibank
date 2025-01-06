namespace InMemoryDataBase.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ForeignKeyAttribute(Type referenceType) : Attribute
{
    public Type ReferenceType { get; } = referenceType;
}
