namespace HelperKit.Tests.Extensions;

public class GenericExtensionUnitTest
{
    private readonly int[] _intArray = { 1, 2, 3, 4 };

    [Fact]
    public void EnumConvert_ReturnCorrectValue()
    {
        const string enumRed = "Red";

        enumRed.ToEnum<Color>().Should();
        enumRed.ToEnum<Color>().Should().Be(Color.Red);
    }

    [Fact]
    public void EnumConvert_ReturnError_FromNotExistingValue()
    {
        const string pinkColor = "Pink";

        var pinkAction = () => pinkColor.ToEnum<Color>();

        pinkAction.Should().Throw<ArgumentException>()
            .WithMessage($"Requested value '{pinkColor}' was not found.");
    }

    [Fact]
    public void EnumToDictionary_GetCorrectItems()
    {
        var enumResult = HelperKit.Extensions.EnumNamedValues<Color>();

        enumResult.Should().HaveCount(4);
    }

    [Fact]
    public void ClassToDictionary_WithPublicInstance()
    {
        var testClass = TestClass.Create();

        var result = testClass.ToDictionary();

        result.Should().HaveCount(6);
    }

    [Fact]
    public void ClassToDictionary_GetOnlyGetInstances()
    {
        var testClass = TestClassWithoutInstance.Create();

        var result = testClass.ToDictionary();

        result.Should().HaveCount(1);
    }

    [Fact]
    public void ClassToNameValueCollection_WithPublicInstance()
    {
        var testClass = TestClass.Create();

        var result = testClass.ToNameValueCollection();

        Assert.Equal(5, result.Count);
    }

    [Fact]
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

        Assert.Single(result);
    }

    [Fact]
    public void ClassToKeyValuePair_WithPublicInstance()
    {
        var testClass = TestClass.Create();

        var result = testClass.ToKeyValuePair();

        result.Should().HaveCount(5);
    }

    [Fact]
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

        result.Should().HaveCount(1);
    }

    [Fact]
    public void ToDataTable_ReturnsAValidDataTable()
    {
        var elements = TestClass.CreateElements(5);
        var lst = elements.ToList();
        lst.Add(null);
        elements = lst.ToArray();
        var dataTable = elements.ToDataTable();

        dataTable.Should().NotBeNull();
    }

    [Fact]
    public void ToDataTable_WithEmptyClass_ThrowsException()
    {
        var elements = EmptyTestClass.CreateElements(2);

        var action = () => elements.ToDataTable();

        action.Should().Throw<MissingFieldException>();
    }

    [Fact]
    public void ToDataTable_WithNullInput_ReturnsEmptyTable()
    {
        var dataTable = ((IEnumerable<TestClass>)null).ToDataTable();

        dataTable.Should().NotBeNull();
    }

    [Fact]
    public void ToDataValue_WithEmptyClass_ThrowsException()
    {
        var elements = TestClass.Create();

        var dictionary = elements.ToDictionary();

        var nullAction = () => dictionary.ToValue<TestClass>(null);

        nullAction.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToDataValue_WithEmptyClass_ReturnsValues()
    {
        var elements = TestClass.Create();
        var dictionary = elements.ToDictionary();

        var result = dictionary.ToValue<int[]>("IntArray");

        result.Should().BeEquivalentTo(elements.IntArray);
    }

    [Fact]
    public void SerializeObjectToXml_WithClass_ReturnsSuccess()
    {
        const string regexStr = @"<StringValue>[\s\S]*?<\/StringValue>";

        var elements = TestClass.Create();
        var xmlstring = elements.SerializeObjectToXml();

        xmlstring.Should().MatchRegex(regexStr);
    }

    [Fact]
    [Obsolete("ConvertObjectToXmlString obsolete")]
    public void ConvertObjectToXmlString_WithClass_ReturnsSuccess()
    {
        const string regexStr = @"<StringValue>[\s\S]*?<\/StringValue>";

        var elements = TestClass.Create();
        var xmlstring = elements.ConvertObjectToXmlString();

        xmlstring.Should().MatchRegex(regexStr);
    }

    [Fact]
    public void DeserializeXmlToObject_WithClass_ReturnsSuccess()
    {
        var elements = TestClass.Create();
        var xmlstring = elements.SerializeObjectToXml();

        var restClass = xmlstring.DeserializeXmlToObject<TestClass>();

        restClass.IntValue.Should().Be(elements.IntValue);
        restClass.IntArray.Should().BeEquivalentTo(elements.IntArray);
    }

    [Fact]
    public void CloneObject_WithClass_ReturnsSuccess()
    {
        var original = TestClass.Create();
        var shallowCopy = original;

        var clone = original.CloneObject();

        original.Should().Be(shallowCopy);
        original.Should().NotBe(clone);

        clone.StringValue.Should().BeEquivalentTo(original.StringValue);
        clone.IntValue.Should().Be(original.IntValue);
        clone.BooleanValue.Should().Be(original.BooleanValue);
        clone.IntArray.Should().BeEquivalentTo(original.IntArray);
        clone.IntList.Should().BeEquivalentTo(original.IntList);
    }

    [Fact]
    public void CloneObject_WithSerializableClass_ReturnsSuccess()
    {
        var original = TestClassSerializable.Create();
        var shallowCopy = original;

        var clone = original.CloneObject();

        original.Should().Be(shallowCopy);
        original.Should().NotBe(clone);

        clone.StringValue.Should().BeEquivalentTo(original.StringValue);
        clone.IntValue.Should().Be(original.IntValue);
        clone.BooleanValue.Should().Be(original.BooleanValue);
        clone.IntArray.Should().BeEquivalentTo(original.IntArray);
        clone.IntList.Should().BeEquivalentTo(original.IntList);
    }
}
