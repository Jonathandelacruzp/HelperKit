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
        public void EnumConvert_ReturnsDefaultValue()
        {
            const string pinkColor = "Blue";
            Assert.AreEqual(Color.Blue, pinkColor.ToEnum<Color>());
        }

        [Test]
        public void EnumConvert_ReturnsCorrectValue_IgnoringTheDefault()
        {
            const string pinkColor = "Red";
            Assert.AreEqual(Color.Red, pinkColor.ToEnum(Color.Blue));
        }

        [Test]
        public void EnumConvert_ReturnsDefaultValue_FromNotExistingValue()
        {
            const string pinkColor = "Pink";
            Assert.AreEqual(Color.Default, pinkColor.ToEnum<Color>());
        }

        [Test]
        public void EnumToDictionary_GetCorrectItems()
        {
            var enumResult = HelperKit.Extensions.EnumNamedValues<Color>();
            Assert.AreEqual(5, enumResult.Count);
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
        public void ClassToDictionaryWithType_WithPublicInstance()
        {
            var testClass = TestClass.Create();

            var result = testClass.ToDictionaryWithType();
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(typeof(int[]), result["IntArray"].Item1);
        }

        [Test]
        public void ClassToDictionaryWithType_ReturnsTheCorrectType()
        {
            var testClass = TestClass.Create();

            var result = testClass.ToDictionaryWithType();

            Assert.AreEqual(typeof(int), result[nameof(testClass.IntValue)].Item1);
            Assert.AreEqual(typeof(string), result[nameof(testClass.StringValue)].Item1);
        }

        [Test]
        public void DictionaryToValue_ReturnsExpectedObject()
        {
            var testClass = TestClass.Create();

            var resultDict = testClass.ToDictionary();
            var finalValue = resultDict.ToValue<int[]>(nameof(testClass.IntArray));
            var finalIntValue = resultDict.ToValue<int>(nameof(testClass.IntValue));

            Assert.AreEqual(testClass.IntArray, finalValue);
            Assert.AreEqual(testClass.IntValue, finalIntValue);
        }

        [Test]
        public void DictionaryToValue_ThrowsArgumentNullException()
        {
            var testClass = TestClass.Create();
            var resultDict = testClass.ToDictionary();

            Assert.Throws<ArgumentNullException>(() => resultDict.ToValue<int>(null));
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
                IntEnum = intArray
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
                IntEnum = intArray
            };

            var result = testClass.ToKeyValuePair();
            Assert.AreEqual(1, result.Count);
        }
    }
}