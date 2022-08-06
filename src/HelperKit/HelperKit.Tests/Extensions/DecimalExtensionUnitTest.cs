namespace HelperKit.Tests.Extensions;

public class DecimalExtensionUnitTest
{
    [Fact]
    public void DecimalExtension_ReturnTrue()
    {
        var decimalValue = new decimal(5);
        const string stringValue = "5";
        const int intValue = 5;
        const double doubleValue = 5F;
        const float floatValue = 5;

        Assert.IsType<decimal>(intValue.ToDecimal());

        stringValue.ToDecimal().Should().Be(decimalValue);
        intValue.ToDecimal().Should().Be(decimalValue);
        doubleValue.ToDecimal().Should().Be(decimalValue);
        floatValue.ToDecimal().Should().Be(decimalValue);
    }

    [Fact]
    public void DecimalExtension_ReturnZeroFromNullValue()
    {
        var nullDecimal = ((string)null).ToDecimal();

        nullDecimal.Should().Be(0);
    }

    [Fact]
    public void DecimalExtension_ReturnZeroFromNonNumericStringValue()
    {
        var stringFalse = "5F".ToDecimal();

        stringFalse.Should().Be(0);
    }

    [Fact]
    public void LongExtension_GivesTheCorrectValue()
    {
        const long floatValue = 5;
        var decimalValue = new decimal(5);

        decimalValue.ToLong().Should<long>();
        decimalValue.ToLong().Should().Be(floatValue);
    }

    [Fact]
    public void DoubleExtension_ReturnTrue()
    {
        const double decimalValue = 5;
        const string stringValue = "5";
        const int intValue = 5;
        const double doubleValue = 5F;
        const float floatValue = 5;

        intValue.ToDecimal().Should<decimal>();

        decimalValue.ToDouble().Should().Be(decimalValue);
        stringValue.ToDouble().Should().Be(decimalValue);
        intValue.ToDouble().Should().Be(decimalValue);
        doubleValue.ToDouble().Should().Be(decimalValue);
        floatValue.ToDouble().Should().Be(decimalValue);
    }
}
