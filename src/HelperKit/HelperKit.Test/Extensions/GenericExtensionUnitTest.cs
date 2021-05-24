using HelperKit.Test.Models;
using NUnit.Framework;
using System;

namespace HelperKit.Test.Extensions
{
    public class GenericExtensionUnitTest
    {
        private readonly int[] _intArray = {1, 2, 3, 4};

        [Test]
        public void EnumConvert_ReturnCorrectValue()
        {
            const string enumRed = "Red";

            Assert.IsInstanceOf<Color>(enumRed.ToEnum<Color>());
            Assert.AreEqual(Color.Red, enumRed.ToEnum<Color>());
        }

        [Test]
        public void EnumConvert_ReturnError_FromNotExistingValue()
        {
            const string pinkColor = "Pink";

            var ex = Assert.Throws<ArgumentException>(() => pinkColor.ToEnum<Color>());
            Assert.AreEqual(ex.Message, $"Requested value '{pinkColor}' was not found.");
        }

        [Test]
        public void EnumToDictionary_GetCorrectItems()
        {
            var enumResult = HelperKit.Extensions.EnumNamedValues<Color>();
            Assert.AreEqual(4, enumResult.Count);
        }

        [Test]
        public void ClassToDictionary_WithPublicInstance()
        {
            var testClass = TestClass.Create();

            var result = testClass.ToDictionary();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ClassToDictionary_GetOnlyGetInstances()
        {
            var testClass = TestClassWithoutInstance.Create();

            var result = testClass.ToDictionary();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ClassToNameValueCollection_WithPublicInstance()
        {
            var testClass = TestClass.Create();

            var result = testClass.ToNameValueCollection();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ClassToNameValueCollection_GetOnlyGetInstances()
        {
            var testClass = new TestClassWithoutInstance
            {
                IntValue = 3,
                StringValue = "string value",
                BooleanValue = true,
                IntArray = _intArray,
                IntList = _intArray
            };

            var result = testClass.ToNameValueCollection();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ClassToKeyValuePair_WithPublicInstance()
        {
            var testClass = TestClass.Create();

            var result = testClass.ToKeyValuePair();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ClassToKeyValuePair_GetOnlyGetInstances()
        {
            var testClass = new TestClassWithoutInstance
            {
                IntValue = 3,
                StringValue = "string value",
                BooleanValue = true,
                IntArray = _intArray,
                IntList = _intArray
            };

            var result = testClass.ToKeyValuePair();
            Assert.AreEqual(1, result.Count);
        }
    }
}