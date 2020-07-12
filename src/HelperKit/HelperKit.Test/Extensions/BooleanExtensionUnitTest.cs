using NUnit.Framework;

namespace HelperKit.Test.Extensions
{
    public class BooleanExtensionUnitTest
    {
        [Test]
        public void BooleanExtension_AcceptLiteralStringBoleanValue()
        {
            var stringTrue = "true";
            var stringTrueUpper = "TRUE";
            var stringTrueCapital = "True";

            var stringFalse = "false";
            var stringFalseUpper = "FALSE";
            var stringFalseCapital = "False";

            Assert.IsInstanceOf<bool>(stringTrue.ToBoolean());

            Assert.AreEqual(true, stringTrue.ToBoolean());
            Assert.AreEqual(true, stringTrueUpper.ToBoolean());
            Assert.AreEqual(true, stringTrueCapital.ToBoolean());

            Assert.AreEqual(false, stringFalse.ToBoolean());
            Assert.AreEqual(false, stringFalseUpper.ToBoolean());
            Assert.AreEqual(false, stringFalseCapital.ToBoolean());
        }

        [Test]
        public void BooleanExtension_ReturnFalseWithWrongValues()
        {
            var anyString = "asads";
            var anyStringUpper = "ASADSADSDAS";
            var anyStringCapital = "Aasdadsasd";

            Assert.AreEqual(false, anyString.ToBoolean());
            Assert.AreEqual(false, anyStringUpper.ToBoolean());
            Assert.AreEqual(false, anyStringCapital.ToBoolean());
        }

        [Test]
        public void BooleanExtension_ReturnFalseNullValue()
        {
            Assert.AreEqual(false, ((string) null).ToBoolean());
        }
    }
}