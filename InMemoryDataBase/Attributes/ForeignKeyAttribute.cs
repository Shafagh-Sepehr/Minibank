namespace InMemoryDataBase.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ForeignKeyAttribute(Type relationKey) : Attribute
{
    public Type RelationKey { get; } = relationKey;
}
