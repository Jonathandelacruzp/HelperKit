using NUnit.Framework;

namespace HelperKit.Test.Extensions
{
    public class IntExtensionUnitTest
    {
        [Test]
        public void IntegerExtension_IsValid()
        {
            string stringNumber = "20";
            string stringFloatLiteral = "10F";
            string stringNumberNull = null;

            Assert.IsInstanceOf<int>(stringNumber.ToInteger());

            Assert.AreEqual(20, stringNumber.ToInteger());
            Assert.AreNotEqual(10, stringFloatLiteral.ToInteger());
            Assert.AreEqual(0, stringNumberNull.ToInteger());
        }

        [Test]
        public void IntegerExtension_ReturnDefaultValue_WithNullValue()
        {
            var defaultValue = 5;
            string stringNumber = null;
            Assert.AreEqual(5, stringNumber.ToInteger(defaultValue));
        }
    }
}