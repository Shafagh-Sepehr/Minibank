namespace InMemoryDataBase.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PrimaryKeyAttribute : Attribute
{
    public PrimaryKeyAttribute()
    {
        
    }
}
