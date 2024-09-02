namespace HelperKit.Tests.Extensions;

public class GenericComparableUnitTest
{
    private readonly List<Color> _colorList =
    [
        Color.Blue,
        Color.Yellow,
        Color.Yellow
    ];

    private readonly List<Color> _colorListToFind =
    [
        Color.Blue,
        Color.Red
    ];

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
    public void CreateDirectory_CreatesDirectory()
    {
        var directoryInfo = new DirectoryInfo("./Test");

        directoryInfo.CreateDirectory();

        var directoryInfoValid = new DirectoryInfo("./Test");

        directoryInfoValid.Exists.Should().BeTrue();
    }

    [Fact]
    public void CreateDirectory_CreatesNewDirectory()
    {
        var directoryName = $"./Test-{Guid.NewGuid()}";
        var directoryInfo = new DirectoryInfo(directoryName);

        directoryInfo.CreateDirectory();

        var directoryInfoValid = new DirectoryInfo(directoryName);

        directoryInfoValid.Exists.Should().BeTrue();
    }
}
