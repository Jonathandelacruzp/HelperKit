namespace HelperKit.Functions;

/// <summary>
/// Memoization is a technique for improving performance by caching the return values of expensive function calls
/// </summary>
public static class Memoizer
{
    /// <summary>
    /// Cache a function and its value
    /// </summary>
    /// <param name="func"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static Func<TResult> Memoize<TResult>(Func<TResult> func)
    {
        object memo = null;
        return () =>
        {
            memo ??= func();
            return (TResult)memo;
        };
    }

    /// <summary>
    /// Cache a function by function parameter returning the value
    /// </summary>
    /// <param name="func"></param>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static Func<TInput, TResult> Memoize<TInput, TResult>(Func<TInput, TResult> func)
    {
        var memo = new Dictionary<TInput, TResult>();
        return a =>
        {
            if (memo.TryGetValue(a, out var value))
                return value;
            value = func(a);
            memo.Add(a, value);
            return value;
        };
    }

    /// <summary>
    /// Cache concurrently a function by function parameter returning the value
    /// </summary>
    /// <param name="func"></param>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static Func<TInput, TResult> ConcurrentMemoize<TInput, TResult>(Func<TInput, TResult> func)
    {
        var memo = new ConcurrentDictionary<TInput, TResult>();
        return argument => memo.GetOrAdd(argument, func);
    }
}
