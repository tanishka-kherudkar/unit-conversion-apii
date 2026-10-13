
using UnitConversionApi.Services;

namespace UnitConversionApi.Tests;

public class UnitConversionServiceTests
{
    private readonly UnitConversionService _service = new();

    [Theory]
    [InlineData(100, "m", "ft", 328.0833333333333)]
    [InlineData(100, "c", "f", 212)]
    [InlineData(1, "kg", "lb", 2.2046226218487757)]
    public void Convert_ReturnsExpectedValue(
        double value, string from, string to, double expected)
    {
        var result = _service.Convert(value, from, to);
        Assert.Equal(expected, result.ConvertedValue, 6);
    }

    [Fact]
    public void Convert_RejectsUnsupportedUnit()
    {
        Assert.Throws<ArgumentException>(
            () => _service.Convert(10, "xyz", "m"));
    }

    [Fact]
    public void Convert_RejectsDifferentCategories()
    {
        Assert.Throws<ArgumentException>(
            () => _service.Convert(10, "m", "kg"));
    }

    [Fact]
    public void Convert_RejectsTemperatureBelowAbsoluteZero()
    {
        Assert.Throws<ArgumentException>(
            () => _service.Convert(-300, "c", "f"));
    }
}
