using NUnit.Framework;

namespace HelperKit.Test.TestExtensions
{
    public class IntExtensionUnitTest
    {
        [Test]
        public void IntegerExtension_IsValid()
        {
            const string stringNumber = "20";
            const string stringFloatLiteral = "10F";

            Assert.IsInstanceOf<int>(stringNumber.ToInteger());

            Assert.AreEqual(20, stringNumber.ToInteger());
            Assert.AreNotEqual(10, stringFloatLiteral.ToInteger());
            Assert.AreEqual(0, ((string) null).ToInteger());
        }

        [Test]
        public void IntegerExtension_ReturnDefaultValue_WithNullValue()
        {
            const int defaultValue = 5;
            Assert.AreEqual(5, ((string) null).ToInteger(defaultValue));
        }
    }
}