namespace HelperKit.Test.Extensions;

public class BooleanExtensionUnitTest
{
    [Fact]
    public void BooleanExtension_AcceptLiteralStringBooleanValue()
    {
        const string stringTrue = "true";
        const string stringTrueUpper = "TRUE";
        const string stringTrueCapital = "True";

        const string stringFalse = "false";
        const string stringFalseUpper = "FALSE";
        const string stringFalseCapital = "False";

        stringTrue.ToBoolean().Should<bool>();
        stringTrue.ToBoolean().Should().BeTrue();

        stringTrueUpper.ToBoolean().Should<bool>();
        stringTrueUpper.ToBoolean().Should().BeTrue();

        stringTrueCapital.ToBoolean().Should<bool>();
        stringTrueCapital.ToBoolean().Should().BeTrue();

        stringFalse.ToBoolean().Should<bool>();
        stringFalse.ToBoolean().Should().BeFalse();

        stringFalseUpper.ToBoolean().Should<bool>();
        stringFalseUpper.ToBoolean().Should().BeFalse();

        stringFalseCapital.ToBoolean().Should<bool>();
        stringFalseCapital.ToBoolean().Should().BeFalse();
    }

    [Fact]
    public void BooleanExtension_ReturnFalseWithWrongValues()
    {
        const string anyString = "asads";
        const string anyStringUpper = "ASADSADSDAS";
        const string anyStringCapital = "Aasdadsasd";

        anyString.ToBoolean().Should().BeFalse();
        anyStringUpper.ToBoolean().Should().BeFalse();
        anyStringCapital.ToBoolean().Should().BeFalse();
    }

    [Fact]
    public void BooleanExtension_ReturnFalseNullValue()
    {
        var boolFromNull = ((string)null).ToBoolean();

        boolFromNull.Should().BeFalse();
    }
}
