namespace HelperKit;

/// <summary>
/// Extension functions
/// </summary>
public static partial class Extensions
{
    /// <summary>
    /// Allows a Task to safely run on a different thread while the calling thread does not wait for its completion
    /// </summary>
    /// <param name="task"></param>
    /// <param name="onException"></param>
    /// <param name="configureAwait"></param>
    /// <typeparam name="TException"></typeparam>
    public static async void SafeFireAndForget<TException>(this Task task, Action<TException> onException = null, bool configureAwait = true) where TException : Exception
    {
        try
        {
            await task.ConfigureAwait(configureAwait);
        }
        catch (TException ex) when (onException is not null)
        {
            onException(ex);
        }
    }

    /// <summary>
    /// Allows a Task to safely run on a different thread while the calling thread does not wait for its completion
    /// </summary>
    /// <param name="task"></param>
    /// <param name="onException"></param>
    /// <param name="configureAwait"></param>
    public static void SafeFireAndForget(this Task task, Action<Exception> onException = null, bool configureAwait = true)
    {
        task.SafeFireAndForget<Exception>(onException, configureAwait);
    }
}
