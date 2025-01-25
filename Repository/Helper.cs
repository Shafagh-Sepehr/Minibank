using System.Reflection;
using Abstractions.InMemoryDatabase;
using Abstractions.Repository;

namespace Repository;

internal static class Helper
{
    public static void SetValue(object obj, string memberName, object? value)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        var type = obj.GetType();

        // Try property first
        var propertyInfo = type.GetProperty(memberName,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);

        if (propertyInfo != null)
        {
            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(obj, value);
                return;
            }

            // Try backing field for auto-properties
            var backingField = type.GetField($"<{memberName}>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (backingField != null)
            {
                backingField.SetValue(obj, value);
                return;
            }
        }

        // If property fails, try direct field
        var fieldInfo = type.GetField(memberName,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);

        if (fieldInfo != null)
        {
            fieldInfo.SetValue(obj, value);
            return;
        }

        throw new ArgumentException($"Member {memberName} not found on {type.Name}");
    }


    public static void CopyVersion(DataBaseEntity source, DataBaseEntity destination)
    {
        ((IVersionable)destination).Version = ((IVersionable)source).Version;
    }
}
