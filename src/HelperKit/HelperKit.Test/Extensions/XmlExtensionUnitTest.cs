using HelperKit.Test.Builders;
using HelperKit.Test.Models;
using NUnit.Framework;
using System.IO;
using System.Xml.Serialization;

namespace HelperKit.Test.Extensions
{
    public class XmlExtensionUnitTest
    {
        [Test]
        public void SaveAsXmlExtension_SaveAValidFile()
        {
            string filename = "testClassFile.xml";
            var testClass = TestClassBuilder.GenerateTestClass();

            System.IO.File.Delete(filename);

            testClass.SaveAsXML(filename);
            var stream = new StreamReader(filename);

            Assert.IsNotNull(stream, "File exist");

            var xmlSerializer = new XmlSerializer(typeof(TestClass));
            var testClassfromXml = xmlSerializer.Deserialize(stream) as TestClass;

            CollectionAssert.AreEqual(testClassfromXml.IntArray, testClass.IntArray);
            CollectionAssert.AreEqual(testClassfromXml.IntList, testClass.IntList);
            Assert.AreEqual(testClassfromXml.BooleanValue, testClass.BooleanValue);
            Assert.AreEqual(testClassfromXml.IntValue, testClass.IntValue);
            Assert.AreEqual(testClassfromXml.StringValue, testClass.StringValue);
        }
    }
}