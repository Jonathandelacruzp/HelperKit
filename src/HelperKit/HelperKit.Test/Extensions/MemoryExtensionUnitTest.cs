namespace HelperKit.Test.Extensions;

public class MemoryExtensionUnitTest
{
    [Fact]
    public void MemoryExtension_ReturnTrue()
    {
        var solutionDir = Directory.GetCurrentDirectory();
        var stream = new StreamReader(solutionDir + "\\Files\\text2.txt");

        var result = stream.BaseStream.ToBytes();
        result.Should().AllBeOfType<byte>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void MemoryExtension_ThrowError_WithNullParameter()
    {
        var streamNull = () => ((Stream)null).ToBytes();

        streamNull.Should().Throw<ArgumentNullException>();
    }
}
