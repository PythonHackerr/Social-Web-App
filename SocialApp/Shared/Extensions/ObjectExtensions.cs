namespace Shared.Extensions;

public static class ObjectExtensions
{
    private static bool DeepCompare(this object? obj, object? other)
    {     
        if (obj != null && !obj.GetType().IsClass) return obj.Equals(other);
        if (ReferenceEquals(obj, other)) return true;
        if (obj == null || other == null) return false;
        if (obj.GetType() != other.GetType()) return false;

        var result = true;
        foreach (var property in obj.GetType().GetProperties())
        {
            var objValue = property.GetValue(obj);
            var anotherValue = property.GetValue(other);
            if (!objValue.Equals(anotherValue)) result = false;
        }

        return result;
    }

    public static bool EqualsByValue(this object? obj, object? other)
    {
        if (ReferenceEquals(obj, other)) return true;
        if (obj == null || other == null) return false;
        if (obj.GetType() != other.GetType()) return false;

        if (!obj.GetType().IsClass) return obj.Equals(other);

        var result = true;
        foreach (var property in obj.GetType().GetProperties())
        {
            var objValue = property.GetValue(obj);
            var anotherValue = property.GetValue(other);
            if (!objValue.DeepCompare(anotherValue))   result = false;
        }
        return result;
    }
}