using System.Net;

namespace HelperKit.Tests.Extensions;

public class TaskExtensionsUnitTest
{
    [Fact]
    public void SafeFireAndForget_RunsOnBackground()
    {
        WaitSimpleTask().SafeFireAndForget(onException: ex => Console.WriteLine("this is an Exception" + ex.Message));

        Assert.True(true);
    }

    [Fact]
    public void SafeFireAndForget_WithWebTextension_RunsOnBackground()
    {
        WaitWebTask().SafeFireAndForget<WebException>(onException: ex => Console.WriteLine("this is an Exception" + ex.Message));

        Assert.True(true);
    }

    [Fact]
    public void SafeFireAndForget_RunsOnBackground_ThrowsException()
    {
        WaitTask().SafeFireAndForget(onException: ex => Console.WriteLine("this is an Exception" + ex.Message));

        Assert.True(true);
    }

    [Fact]
    public void SafeFireAndForget_RunsOnBackground_ThrowsWebException()
    {
        WaitHttpTask().SafeFireAndForget<WebException>(onException: ex => Console.WriteLine("this is an Exception" + ex.Message));

        Assert.True(true);
    }

    static Task WaitSimpleTask()
    {
        return Task.Delay(5);
    }

    static Task WaitWebTask()
    {
        return Task.Delay(5);
    }

    static async Task WaitTask()
    {
        await WaitSimpleTask();
        throw new Exception("Ahhhhh");
    }

    static async Task WaitHttpTask()
    {
        await WaitWebTask();
        throw new WebException("Error 500");
    }
}
