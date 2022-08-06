using System.Xml.Serialization;

namespace HelperKit.Tests.Extensions;

public class XmlExtensionUnitTest
{
    [Fact]
    public void SaveAsXmlExtension_SaveAValidFile()
    {
        const string filename = "testClassFile.xml";
        var testClass = TestClass.Create();

        File.Delete(filename);

        testClass.SaveAsXml(filename);
        var stream = new StreamReader(filename);

        stream.Should().NotBeNull();

        var xmlSerializer = new XmlSerializer(typeof(TestClass));
        var testClassFromXml = xmlSerializer.Deserialize(stream) as TestClass;

        testClassFromXml.Should().NotBeNull();
        testClassFromXml.IntArray.Should().BeEquivalentTo(testClass.IntArray);
        testClassFromXml.IntList.Should().BeEquivalentTo(testClass.IntList);
        testClassFromXml.BooleanValue.Should().Be(testClass.BooleanValue);
        testClassFromXml.IntValue.Should().Be(testClass.IntValue);
        testClassFromXml.StringValue.Should().BeEquivalentTo(testClass.StringValue);
    }
}
