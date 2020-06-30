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
            const string filename = "testClassFile.xml";
            var testClass = TestClass.Create();

            File.Delete(filename);

            testClass.SaveAsXml(filename);
            var stream = new StreamReader(filename);

            Assert.IsNotNull(stream, "File exist");

            var xmlSerializer = new XmlSerializer(typeof(TestClass));
            var testClassFromXml = xmlSerializer.Deserialize(stream) as TestClass;

            CollectionAssert.AreEqual(testClassFromXml.IntArray, testClass.IntArray);
            CollectionAssert.AreEqual(testClassFromXml.IntList, testClass.IntList);
            Assert.AreEqual(testClassFromXml.BooleanValue, testClass.BooleanValue);
            Assert.AreEqual(testClassFromXml.IntValue, testClass.IntValue);
            Assert.AreEqual(testClassFromXml.StringValue, testClass.StringValue);
        }
    }
}