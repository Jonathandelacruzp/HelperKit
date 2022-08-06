namespace HelperKit.Tests.Results;

public class ResultTests
{
    [Fact]
    public void ResultImplicitOperator_ReturnsSuccess()
    {
        var testClass = TestClass.Create();

        Result<TestClass> result = testClass;

        result.Should().NotBeNull();
        result.Should().As<IResult<TestClass>>();
        result.Value.Should().Be(testClass);
    }

    [Fact]
    public void IResultTryGetValue_WithCorrectValue_ReturnsSuccess()
    {
        var testClass = TestClass.Create();

        Result<TestClass> result = testClass;
        var success = result.TryGet(out var testClassResult);

        success.Should().BeTrue();
        testClassResult.Should().NotBeNull();
        testClassResult.Should().Be(testClass);
    }

    [Fact]
    public void IResultTryGetValue_WithNullValue_ReturnsFailure()
    {
        var testVlass = TestClass.Create();

        Result<TestClass> result = new Result<TestClass>();

        var success = result.TryGet(out var testClassResult);

        success.Should().BeFalse();
        testClassResult.Should().BeNull();
    }

    [Fact]
    public void IResultTryGetValue_WithException_ReturnsFailure()
    {
        var exception = new Exception("Test exception");

        Result<TestClass> result = exception;

        var success = result.TryGet(out var testClassResult);

        success.Should().BeFalse();
        testClassResult.Should().BeNull();
        result.Message.Should().NotBeNullOrEmpty();
        result.Message.Should().Be(exception.Message);
    }
}
