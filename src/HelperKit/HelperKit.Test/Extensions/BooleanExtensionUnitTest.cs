using NUnit.Framework;

namespace HelperKit.Test.Extensions
{
    public class BooleanExtensionUnitTest
    {
        [Test]
        public void BooleanExtension_AcceptLiteralStringBooleanValue()
        {
            const string stringTrue = "true";
            const string stringTrueUpper = "TRUE";
            const string stringTrueCapital = "True";

            const string stringFalse = "false";
            const string stringFalseUpper = "FALSE";
            const string stringFalseCapital = "False";

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
            const string anyString = "asads";
            const string anyStringUpper = "ASADSADSDAS";
            const string anyStringCapital = "Aasdadsasd";

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