using HelperKit.Test.Models;
using NUnit.Framework;
using System;

namespace HelperKit.Test.Extensions
{
    public class GenericExtensionUnitTest
    {
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
            var pinkColor = "Pink";

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
            var intArray = new[] {1, 2, 3, 4};
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
            var intArray = new[] {1, 2, 3, 4};
            var testClass = new TestClassWithoutInstance
            {
                IntValue = 3,
                StringValue = "string value",
                BooleanValue = true,
                IntArray = intArray,
                IntList = intArray
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
            var intArray = new[] {1, 2, 3, 4};
            var testClass = new TestClassWithoutInstance
            {
                IntValue = 3,
                StringValue = "string value",
                BooleanValue = true,
                IntArray = intArray,
                IntList = intArray
            };

            var result = testClass.ToKeyValuePair();
            Assert.AreEqual(1, result.Count);
        }
    }
}