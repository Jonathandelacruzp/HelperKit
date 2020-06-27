using HelperKit.Test.Builders;
using HelperKit.Test.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace HelperKit.Test.Extensions
{
    public class GenericExtensionUnitTest
    {
        [Test]
        public void EnumConvert_ReturnCorrectValue()
        {
            string enumRed = "Red";

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
        public void EnumToDIctionary_GetCoorectItems()
        {
            var enumResult = HelperKit.Extensions.EnumNamedValues<Color>();
            Assert.AreEqual(4, enumResult.Count);
        }

        [Test]
        public void ClassToDictionary_WithPublicInstance()
        {
            var intArray = new int[4] { 1, 2, 3, 4 };
            var testClass = TestClassBuilder.GenerateTestClass();

            var result = testClass.ToDictionary();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ClassToDictionary_GetOnlyGetInstances()
        {
            var intArray = new int[4] { 1, 2, 3, 4 };
            var testClass = new TestClassWithoutInstance()
            {
                IntValue = 3,
                StringValue = "string value",
                BooleanValue = true,
                IntArray = intArray,
                IntList = intArray.ToList()
            };

            var result = testClass.ToDictionary();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ClassToNameValueCollection_WithPublicInstance()
        {
            var testClass = TestClassBuilder.GenerateTestClass();

            var result = testClass.ToNameValueCollection();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ClassToNameValueCollection_GetOnlyGetInstances()
        {
            var intArray = new int[4] { 1, 2, 3, 4 };
            var testClass = new TestClassWithoutInstance()
            {
                IntValue = 3,
                StringValue = "string value",
                BooleanValue = true,
                IntArray = intArray,
                IntList = intArray.ToList()
            };

            var result = testClass.ToNameValueCollection();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ClassToKeyValuePair_WithPublicInstance()
        {
            var testClass = TestClassBuilder.GenerateTestClass();

            var result = testClass.ToKeyValuePair();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ClassToKeyValuePair_GetOnlyGetInstances()
        {
            var intArray = new int[4] { 1, 2, 3, 4 };
            var testClass = new TestClassWithoutInstance()
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