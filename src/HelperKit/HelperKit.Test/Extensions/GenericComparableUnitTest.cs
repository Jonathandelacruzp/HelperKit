namespace HelperKit.Test.Extensions;

public class GenericComparableUnitTest
{
    private readonly List<Color> _colorList = new()
    {
        Color.Blue,
        Color.Yellow,
        Color.Yellow
    };

    private readonly List<Color> _colorListToFind = new()
    {
        Color.Blue,
        Color.Red
    };

    [Fact]
    public void HasAnyExtension_ShouldReturn_Valid_Result()
    {
        Assert.True(_colorList.HasAny(Color.Blue));
        Assert.True(_colorList.HasAny(Color.Yellow));
    }

    [Fact]
    public void HasAnyExtension_ShouldReturn_Valid_False_Result()
    {
        _colorList.HasAny(Color.Red).Should().BeFalse();
    }

    [Fact]
    public void IsContainedOnExtension_ShouldReturn_Valid_Result()
    {
        const Color colorBlue = Color.Blue;

        var paramTest = colorBlue.IsContainedIn(Color.Blue, Color.Yellow, Color.Red);
        paramTest.Should().BeTrue();

        var enumerableTest = colorBlue.IsContainedIn(_colorListToFind);
        enumerableTest.Should().BeTrue();
    }

    [Fact]
    public void IsContainedOnExtension_ShouldTrowAnArgumentException()
    {
        var action = () => _colorListToFind.IsContainedIn(_colorList);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsContainedOnExtension_ShouldTrowAnArgumentNUllException()
    {
        var nullAction = () => ((string)null).IsContainedIn("rojo", "verde");

        nullAction.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CreateDirectory_CreatesDirectory()
    {
        var directoryInfo = new DirectoryInfo("./Test");

        directoryInfo.CreateDirectory();

        var directoryInfoValid = new DirectoryInfo("./Test");

        directoryInfoValid.Exists.Should().BeTrue();
    }
}
