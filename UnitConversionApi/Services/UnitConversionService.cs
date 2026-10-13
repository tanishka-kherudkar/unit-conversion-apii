
using UnitConversionApi.Models;

namespace UnitConversionApi.Services;

public interface IUnitConversionService
{
    ConversionResponse Convert(double value, string from, string to);
    IEnumerable<object> GetSupportedUnits();
}

public sealed class UnitConversionService : IUnitConversionService
{
    private sealed record UnitDefinition(
        string Symbol,
        string Category,
        double Factor,
        double Offset = 0);

    // Base units: metre, Celsius and kilogram.
    // Base value = value * Factor + Offset.
    private static readonly Dictionary<string, UnitDefinition> Units =
        new(StringComparer.OrdinalIgnoreCase)
        {
            // Length
            ["m"]  = new("m", "length", 1),
            ["km"] = new("km", "length", 1000),
            ["cm"] = new("cm", "length", 0.01),
            ["mm"] = new("mm", "length", 0.001),
            ["mi"] = new("mi", "length", 1609.344),
            ["ft"] = new("ft", "length", 0.3048),
            ["in"] = new("in", "length", 0.0254),

            // Temperature: Celsius is the base unit.
            ["c"] = new("c", "temperature", 1, 0),
            ["f"] = new("f", "temperature", 5.0 / 9.0, -32 * 5.0 / 9.0),
            ["k"] = new("k", "temperature", 1, -273.15),

            // Weight / mass: kilogram is the base unit.
            ["kg"] = new("kg", "weight", 1),
            ["g"]  = new("g", "weight", 0.001),
            ["mg"] = new("mg", "weight", 0.000001),
            ["lb"] = new("lb", "weight", 0.45359237),
            ["oz"] = new("oz", "weight", 0.028349523125)
        };

    public ConversionResponse Convert(double value, string from, string to)
    {
        if (!double.IsFinite(value))
            throw new ArgumentException("Value must be a finite number.");

        if (!Units.TryGetValue(from, out var source))
            throw new ArgumentException($"Unsupported source unit: '{from}'.");

        if (!Units.TryGetValue(to, out var target))
            throw new ArgumentException($"Unsupported target unit: '{to}'.");

        if (source.Category != target.Category)
        {
            throw new ArgumentException(
                "Source and target units must belong to the same category.");
        }

        double baseValue = value * source.Factor + source.Offset;

        if (source.Category == "temperature" && baseValue < -273.15)
        {
            throw new ArgumentException(
                "Temperature cannot be below absolute zero (-273.15 °C).");
        }

        double result = (baseValue - target.Offset) / target.Factor;

        if (!double.IsFinite(result))
            throw new ArgumentException("Conversion result is outside the supported range.");

        if (Math.Abs(result) < 1e-12)
            result = 0;

        return new ConversionResponse
        {
            OriginalValue = value,
            FromUnit = source.Symbol,
            ToUnit = target.Symbol,
            ConvertedValue = result,
            Category = source.Category
        };
    }

    public IEnumerable<object> GetSupportedUnits()
    {
        return Units.Values
            .OrderBy(unit => unit.Category)
            .ThenBy(unit => unit.Symbol)
            .Select(unit => (object)new
            {
                unit.Symbol,
                unit.Category
            });
    }
}
