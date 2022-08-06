namespace HelperKit.Tests.Extensions;

public class IntExtensionUnitTest
{
    [Fact]
    public void IntegerExtension_IsValid()
    {
        const string stringNumber = "20";
        const string stringFloatLiteral = "10F";

        Assert.IsType<int>(stringNumber.ToInteger());

        Assert.Equal(20, stringNumber.ToInteger());
        Assert.NotEqual(10, stringFloatLiteral.ToInteger());
        Assert.Equal(0, ((string)null).ToInteger());
    }

    [Fact]
    public void IntegerExtension_ReturnDefaultValue_WithNullValue()
    {
        const int defaultValue = 5;
        Assert.Equal(5, ((string)null).ToInteger(defaultValue));
    }
}
