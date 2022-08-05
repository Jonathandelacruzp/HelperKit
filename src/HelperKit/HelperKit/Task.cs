namespace HelperKit;

/// <summary>
/// Extension functions
/// </summary>
public static partial class Extensions
{
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

    public static async void SafeFireAndForget(this Task task, Action<Exception> onException = null, bool configureAwait = true)
    {
        try
        {
            await task.ConfigureAwait(configureAwait);
        }
        catch (Exception ex) when (onException is not null)
        {
            onException(ex);
        }
    }
}