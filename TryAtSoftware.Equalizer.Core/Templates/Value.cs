namespace TryAtSoftware.Equalizer.Core.Templates;

public static class Value
{
    public static EmptyValueTemplate Empty => new();

    public static GreaterThanValueTemplate GreaterThan<T>(T lowerBound) => new(lowerBound);
    public static LowerThanValueTemplate LowerThan<T>(T upperBound) => new(upperBound);
    public static GreaterThanOrEqualValueTemplate GreaterThanOrEqual<T>(T lowerBound) => new(lowerBound);
    public static LowerThanOrEqualValueTemplate LowerThanOrEqual<T>(T upperBound) => new(upperBound);
}