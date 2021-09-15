using HelperKit.Test.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HelperKit.Test.Extensions
{
    public class GenericExtensionUnitTest
    {
        private readonly int[] _intArray = { 1, 2, 3, 4 };

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
            Assert.AreEqual(ex!.Message, $"Requested value '{pinkColor}' was not found.");
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

        [Test]
        public void ToDataTable_ReturnsAValidDataTable()
        {
            var elements = TestClass.CreateElements(5);
            var dataTable = elements.ToDataTable();

            Assert.IsNotNull(dataTable);
        }

        [Test]
        public void ToDataTable_WithEmptyClass_ThrowsException()
        {
            var elements = EmptyTestClass.CreateElements(2);

            void action() => elements.ToDataTable();
            Assert.Throws<MissingFieldException>(action);
        }

        [Test]
        public void ToDataTable_TrowsNullRefferenceException()
        {
            static void NullAction() => ((IEnumerable<TestClass>)null).ToDataTable();
            Assert.Throws<NullReferenceException>(NullAction);
        }

        [Test]
        public void ToDataValue_WithEmptyClass_ThrowsException()
        {
            var elements = TestClass.Create();

            var dictionary = elements.ToDictionary();

            void nullAction() => dictionary.ToValue<TestClass>(null);

            Assert.Throws<ArgumentNullException>(nullAction);
        }

        [Test]
        public void ToDataValue_WithEmptyClass_ReturnsValues()
        {
            var elements = TestClass.Create();
            var dictionary = elements.ToDictionary();

            var result = dictionary.ToValue<int[]>("IntArray");

            CollectionAssert.AreEquivalent(elements.IntArray, result);
        }

        [Test]
        public void SerializeObjectToXml_WithClass_ReturnsSuccess()
        {
            const string regexStr = @"<StringValue>[\s\S]*?<\/StringValue>";

            var rerex = new Regex(regexStr);

            var elements = TestClass.Create();
            var xmlstring = elements.SerializeObjectToXml();

            Assert.IsTrue(rerex.IsMatch(xmlstring));
        }

        [Test]
        [Obsolete("ConvertObjectToXmlString obsolete")]
        public void ConvertObjectToXmlString_WithClass_ReturnsSuccess()
        {
            const string regexStr = @"<StringValue>[\s\S]*?<\/StringValue>";

            var rerex = new Regex(regexStr);

            var elements = TestClass.Create();
            var xmlstring = elements.ConvertObjectToXmlString();

            Assert.IsTrue(rerex.IsMatch(xmlstring));
        }

        [Test]
        public void DeserializeXmlToObject_WithClass_ReturnsSuccess()
        {
            var elements = TestClass.Create();
            var xmlstring = elements.SerializeObjectToXml();

            var restClass = xmlstring.DeserializeXmlToObject<TestClass>();

            Assert.AreEqual(elements.IntValue, restClass.IntValue);
            CollectionAssert.AreEqual(elements.IntArray, restClass.IntArray);
        }
    }
}