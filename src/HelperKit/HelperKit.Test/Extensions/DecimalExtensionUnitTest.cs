using NUnit.Framework;

namespace HelperKit.Test.Extensions
{
    public class DecimalExtensionUnitTest
    {
        [Test]
        public void DecimalExtension_ReturnTrue()
        {
            var decimalValue = new decimal(5);
            const string stringValue = "5";
            const int intValue = 5;
            const double doubleValue = 5F;
            const float floatValue = 5;

            Assert.IsInstanceOf<decimal>(intValue.ToDecimal());

            Assert.AreEqual(decimalValue, decimalValue.ToDecimal());
            Assert.AreEqual(decimalValue, stringValue.ToDecimal());
            Assert.AreEqual(decimalValue, intValue.ToDecimal());
            Assert.AreEqual(decimalValue, doubleValue.ToDecimal());
            Assert.AreEqual(decimalValue, floatValue.ToDecimal());
        }

        [Test]
        public void DecimalExtension_ReturnZeroFromNullValue()
        {
            Assert.AreEqual(0, ((string) null).ToDecimal());
        }

        [Test]
        public void DecimalExtension_ReturnZeroFromNonNumericStringValue()
        {
            const string stringFalse = "5F";
            Assert.AreEqual(0, stringFalse.ToDecimal());
        }

        [Test]
        public void LongExtension_GivesTheCorrectValue()
        {
            const long floatValue = 5;
            var decimalValue = new decimal(5);
            Assert.IsInstanceOf<long>(decimalValue.ToLong());
            Assert.AreEqual(floatValue, decimalValue.ToLong());
        }
    }
}